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
        public static void MovingDroplets(Pixels[,] pixelBoard, List<Droplet> drops, Dictionary<string, int> endElectrodeDictionary) {
            pixelBoard = Pixels.ScaleDown(pixelBoard, out var scaled);

            if (scaled) {
                foreach (var drop in drops) {
                    drop.SizeX = (int)Math.Ceiling(((double)drop.SizeX / 2));
                    drop.SizeY = (int)Math.Ceiling(((double)drop.SizeY / 2));
                    drop.PositionX /= 2;
                    drop.PositionY /= 2;
                }
            }

            Console.WriteLine("Reduced board size*: " + pixelBoard.GetLength(0) + " x " + pixelBoard.GetLength(1));
            var finalDropOrder = new List<Droplet>();
            var failedDrops = new List<Droplet>();
            var attemptList = new List<Droplet>();
            var electrodePathCollection = new Dictionary<string, List<int>>();


            //setting initial drop locations and destinations
            foreach (var tDrop in drops) {
                var position = InputHandler.PlaceInElectrode(pixelBoard, endElectrodeDictionary[tDrop.Name], tDrop);
                try {
                    Blockages.DropletSet(pixelBoard, new Coord(tDrop.PositionX, tDrop.PositionY), tDrop);
                    Blockages.DropletSet(pixelBoard, position, tDrop);
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
            //BoardPrint.PrintBoard(pixelBoard);

            //start routing with non-contaminating drops
            //if successful add to final list, else stop
            foreach (var drop in drops) {
                var end = endElectrodeDictionary[drop.Name];
                if (!drop.Contamination) {
                    Console.WriteLine("Finding the path for clean drop: " + drop.Name);
                    var pixelPath = InputHandler.CheckInputType(pixelBoard, new Coord(drop.PositionX, drop.PositionY), end, drop);
                    var finalElectrodePath = PixelElectrodeConversion.FindElectrodes(pixelBoard, pixelPath, drop);
                    if (finalElectrodePath.Any()) {
                        finalDropOrder.Add(drop);
                        var simplePath = SimplifyPath(finalElectrodePath);
                        electrodePathCollection.Add(drop.Name, simplePath);
                    } else {
                        Console.WriteLine("Scheduling Unsuccessful");
                        //return failedDrops;
                        return;
                    }
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

                foreach (var drop in attemptList) {
                    Console.Write(drop.Name + " ");
                }
                Console.WriteLine();
                var madeItThrough = true;

                //run A* on each drop and contaminate the board after each one.
                //if all are successful add to final list and delete from failed list, else go to next permutation
                for (var m = 0; m < attemptList.Count; m++) {
                    var attemptDrop = attemptList[m];
                    var end = endElectrodeDictionary[attemptDrop.Name];
                    Console.WriteLine("Finding the path for " + attemptDrop.Name);
                    var contaminatedPath = InputHandler.CheckInputType(pixelBoard, new Coord(attemptDrop.PositionX, attemptDrop.PositionY), end, attemptList[m]);

                    //if no path found delete from collection
                    if (!contaminatedPath.Any()) {
                        madeItThrough = false;
                        for (var n = 0; n < m; n++) {
                            electrodePathCollection.Remove(attemptList[n].Name);
                        }
                        break;
                    }

                    var finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, contaminatedPath, attemptDrop);
                    var simplePath = SimplifyPath(finalPath);
                    electrodePathCollection.Add(attemptDrop.Name, simplePath);
                }

                if (madeItThrough) {
                    var result = finalDropOrder.Concat(attemptList);
                    finalDropOrder = result.ToList();
                    failedDrops.Clear();
                    break;
                }
                Contamination.Decontaminate(pixelBoard);
                attemptList.Clear();
            }

            if (failedDrops.Count == 0) {
                //BoardPrint.PrintBoard(pixelBoard);
                Console.WriteLine("Successful Order:");
                foreach (var drop in finalDropOrder) {
                    Console.Write(drop.Name + " ");
                }
                Console.WriteLine();

                //TODO - temporary path printout for each droplet
                //foreach (var drop in finalDropOrder) {
                //    Console.WriteLine("Path for " + drop.Name);
                //    PathPrint.FinalPrint(electrodePathCollection[drop.Name]);
                //}

                Console.WriteLine("Path collisions are: ");
                CollisionDetector(finalDropOrder, electrodePathCollection);
                //return finalDropOrder;
                return;
            }
            Console.WriteLine("Scheduling Unsuccessful");
            //return failedDrops;
        }

        private static List<int> SimplifyPath(List<SortedSet<int>> electrodePath) {
            var simpleList = new List<int>();
            foreach (var step in electrodePath) {
                simpleList.AddRange(step.ToList());
            }
            return simpleList;
        }

        //permutation code inspired from 
        //https://www.codeproject.com/Articles/43767/A-C-List-Permutation-Iterator
        private static IEnumerable<IList> Permutate(List<Droplet> sequence, int count) {
            if (count == 1) yield return sequence;
            else {
                for (var i = 0; i < count; i++) {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    var tmp = sequence[count - 1];
                    sequence.RemoveAt(count - 1);
                    sequence.Insert(0, tmp);
                }
            }
        }

        private static void CollisionDetector(List<Droplet> finalOrder, Dictionary<string, List<int>> electrodePathCollection) {
            var dropNames = finalOrder.Select(drop => drop.Name).ToList();
            for (var i = 0; i < dropNames.Count; i++) {
                var m = i + 1;
                while (m < dropNames.Count) {
                    var collision = electrodePathCollection[dropNames[i]].Intersect(electrodePathCollection[dropNames[m]]).ToList();
                    if (collision.Any()) {
                        Console.Write(dropNames[i] + " & " + dropNames[m] + " collide on: ");
                        foreach (var electrode in collision) {
                            Console.Write(electrode + " ");
                        }
                        Console.WriteLine();
                    }
                    m++;
                }
            }
        }
    }
}
