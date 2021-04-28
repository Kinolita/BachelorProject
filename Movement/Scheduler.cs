using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;
using BachelorProject.Printers;

namespace BachelorProject.Movement
{
    class Scheduler
    {
        public static List<Droplet> MovingDroplets(Pixels[,] pixelBoard, List<Droplet> drops, Dictionary<string, int> endElecDict) {

            List<Droplet> finalDrops = new List<Droplet>();
            List<Droplet> failedDrops = new List<Droplet>();
            List<Droplet> attemptList = new List<Droplet>();
            Dictionary<string, List<SortedSet<int>>> electrodePathCollection = new Dictionary<string, List<SortedSet<int>>>();

            //setting initial drop locations and destinations
            for (var t = 0; t < drops.Count; t++) {
                Blockages.DropletSet(pixelBoard, new Coord(drops[t].PositionX, drops[t].PositionY), drops[t]);
                var position = InputHandler.PlaceInElectrode(pixelBoard, endElecDict[drops[t].Name], drops[t]);
                Blockages.DropletSet(pixelBoard, position, drops[t]);
            }
            //BoardPrint.printBoard(pixelBoard);

            //starting with non-contaminating drops
            foreach (var drop in drops) {
                var end = endElecDict[drop.Name];
                // TODO - still need to check if these should be run sequentially or concurrently
                // also does not yet check if the clean paths are even possible or not
                if (!drop.Contamination) {
                    var that = InputHandler.CheckInputType(ref pixelBoard, new Coord(drop.PositionX, drop.PositionY), end, drop);
                    List<SortedSet<int>> finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, that, drop);
                    electrodePathCollection.Add(drop.Name, finalPath);
                    finalDrops.Add(drop);
                } else {
                    failedDrops.Add(drop);
                }
            }

            //going through permutations of contaminated drops
            foreach (var permu in Permutate(failedDrops, failedDrops.Count)) {
                Console.Write("Trying combination: ");
                foreach (Droplet one in permu) {
                    attemptList.Add(one);
                }

                foreach (var grg in attemptList) {
                    Console.Write(grg.Name + " ");
                }
                Console.WriteLine();
                bool madeItThrough = true;

                //run A* on each drop and contaminate the board after each one.
                //if all are successful add to finallist and delete from failed list
                //else go to next permutation
                for (int m = 0; m < attemptList.Count; m++) {
                    //foreach (var that in attemptList) {
                    var that = attemptList[m];
                    var end = endElecDict[that.Name];
                    Console.WriteLine("finding path for " + that.Name);

                    //BoardPrint.printBoard(pixelBoard);
                    var contaminatedPath = InputHandler.CheckInputType(ref pixelBoard, new Coord(that.PositionX, that.PositionY), end, attemptList[m]);

                    //if no path found delete from collection
                    if (!contaminatedPath.Any()) {
                        madeItThrough = false;
                        for (int n = 0; n < m; n++) {
                            electrodePathCollection.Remove(attemptList[n].Name);
                        }
                        break;
                    }

                    List<SortedSet<int>> finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, contaminatedPath, that);
                    //TODO - is first path contaminated?
                    //BoardPrint.printBoard(pixelBoard);
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
                Console.WriteLine("all succeeded:");
                foreach (var that in finalDrops) {
                    Console.WriteLine("Path for " + that.Name);
                    PathPrint.FinalPrint(electrodePathCollection[that.Name]);
                }
                return finalDrops;
            }

            Console.WriteLine("returning failed list: ");
            foreach (var that in failedDrops) {
                Console.Write(that.Name + " ");
            }
            return failedDrops;
        }

        public static void ContaminateMe(Pixels[,] pixelBoard, List<Coord> path) {
            foreach (var step in path) {
                if (pixelBoard[step.X, step.Y].Empty) {
                    pixelBoard[step.X, step.Y].Empty = false;
                    pixelBoard[step.X, step.Y].BlockageType = "Contaminated";
                }
            }
        }

        public static void UncontaminateMe(Pixels[,] pixelBoard) {
            for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].BlockageType != "Contaminated") continue;
                    pixelBoard[k, j].Empty = true;
                    pixelBoard[k, j].BlockageType = "";
                }
            }
        }


        //permutation code inspired from 
        //need to go back and tweak this
        //https://www.codeproject.com/Articles/43767/A-C-List-Permutation-Iterator
        public static void RotateRight(IList sequence, int count) {
            object tmp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }
        public static IEnumerable<IList> Permutate(IList sequence, int count) {
            if (count == 1) yield return sequence;
            else {
                for (int i = 0; i < count; i++) {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateRight(sequence, count);
                }
            }
        }
    }
}
