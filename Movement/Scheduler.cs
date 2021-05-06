using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;
using BachelorProject.Printers;

namespace BachelorProject.Movement
{
    class Scheduler
    {
        public static List<Droplet> MovingDroplets(Pixels[,] pixelBoard, List<Droplet> drops, Dictionary<string, int> endElecDict) {
            pixelBoard = Pixels.ScaleDown(pixelBoard, out var scaled);

            if (scaled) {
                foreach (var drop in drops) {
                    drop.SizeX = (int)Math.Ceiling(((double)drop.SizeX / 2));
                    drop.SizeY = (int)Math.Ceiling(((double)drop.SizeY / 2));
                    drop.PositionX /= 2;
                    drop.PositionY /= 2;
                }
            }

            Console.WriteLine("Working board size*: " + pixelBoard.GetLength(0) + " x " + pixelBoard.GetLength(1));

            var finalDrops = new List<Droplet>();
            var failedDrops = new List<Droplet>();
            var attemptList = new List<Droplet>();
            var electrodePathCollection = new Dictionary<string, List<SortedSet<int>>>();

            //setting initial drop locations and destinations
            foreach (var tDrop in drops) {
                //starting droplet location
                Blockages.DropletSet(pixelBoard, new Coord(tDrop.PositionX, tDrop.PositionY), tDrop);
                //ending droplet location
                var position = InputHandler.PlaceInElectrode(pixelBoard, endElecDict[tDrop.Name], tDrop);
                Blockages.DropletSet(pixelBoard, position, tDrop);
            }
            //BoardPrint.PrintBoard(pixelBoard);

            //starting with non-contaminating drops
            foreach (var drop in drops) {
                var end = endElecDict[drop.Name];
                // TODO - still need to check if these should be run sequentially or concurrently
                // also does not yet check if the clean paths are even possible or not
                if (!drop.Contamination) {
                    var that = InputHandler.CheckInputType(pixelBoard, new Coord(drop.PositionX, drop.PositionY), end, drop);
                    var finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, that, drop);
                    electrodePathCollection.Add(drop.Name, finalPath);
                    finalDrops.Add(drop);
                } else {
                    failedDrops.Add(drop);
                }
            }

            //going through permutations of contaminated drops
            var count = 1;
            foreach (var permutation in Permutate(failedDrops, failedDrops.Count)) {
                Console.WriteLine();
                Console.Write("Trying combination {0}: ", count);
                count++;
                attemptList.AddRange(permutation.Cast<Droplet>());

                foreach (var grg in attemptList) {
                    Console.Write(grg.Name + " ");
                }
                Console.WriteLine();
                var madeItThrough = true;

                //run A* on each drop and contaminate the board after each one.
                //if all are successful add to final list and delete from failed list else go to next permutation
                for (var m = 0; m < attemptList.Count; m++) {
                    var that = attemptList[m];
                    var end = endElecDict[that.Name];
                    Console.WriteLine("finding the path for " + that.Name);
                    var contaminatedPath = InputHandler.CheckInputType(pixelBoard, new Coord(that.PositionX, that.PositionY), end, attemptList[m]);

                    //if no path found delete from collection
                    if (!contaminatedPath.Any()) {
                        madeItThrough = false;
                        for (var n = 0; n < m; n++) {
                            electrodePathCollection.Remove(attemptList[n].Name);
                        }
                        break;
                    }

                    var finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, contaminatedPath, that);
                    electrodePathCollection.Add(that.Name, finalPath);
                }

                if (madeItThrough) {
                    var result = finalDrops.Concat(attemptList);
                    finalDrops = result.ToList();
                    failedDrops.Clear();
                    break;
                }
                UncontaminateMe(pixelBoard);
                attemptList.Clear();
            }

            if (failedDrops.Count == 0) {
                foreach (var drop in finalDrops) {
                    Console.WriteLine("Path for " + drop.Name);
                    PathPrint.FinalPrint(electrodePathCollection[drop.Name]);
                }
                Console.WriteLine("Successful Order:");
                foreach (var drop in finalDrops) {
                    Console.Write(drop.Name + " ");
                }
                return finalDrops;
            }

            Console.WriteLine("Scheduling Unsuccessful");
            return failedDrops;
        }

        public static void ContaminateMe(Pixels[,] pixelBoard, List<Coord> path) {
            foreach (var step in path) {
                if (pixelBoard[step.X, step.Y].Empty || pixelBoard[step.X, step.Y].BlockageType == "Buffer") {
                    pixelBoard[step.X, step.Y].Empty = false;
                    pixelBoard[step.X, step.Y].BlockageType = "Contaminated";
                }
            }
        }

        public static void UncontaminateMe(Pixels[,] pixelBoard) {
            for (var k = 0; k < pixelBoard.GetLength(0); k++) {
                for (var j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].BlockageType != "Contaminated") continue;
                    pixelBoard[k, j].Empty = true;
                    pixelBoard[k, j].BlockageType = "";
                }
            }
        }


        //permutation code inspired from 
        //TODO _ need to go back and tweak this
        //https://www.codeproject.com/Articles/43767/A-C-List-Permutation-Iterator
        public static void RotateRight(IList sequence, int count) {
            var tmp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }
        public static IEnumerable<IList> Permutate(IList sequence, int count) {
            if (count == 1) yield return sequence;
            else {
                for (var i = 0; i < count; i++) {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateRight(sequence, count);
                }
            }
        }
    }
}
