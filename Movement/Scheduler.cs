using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BachelorProject.Exceptions;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    class Scheduler
    {
        public static Dictionary<(int, int), List<int>> MovingDroplets(Pixels[,] pixelBoard, List<Droplet> drops, Dictionary<int, int> endElectrodeDictionary) {

            // scaling down the pixel board
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
            var electrodePathCollection = new Dictionary<int, List<int>>();
            var endingPixels = new Dictionary<int, Coord>();


            //setting initial drop locations and destinations
            foreach (var tDrop in drops) {
                var position = InputHandler.PlaceInElectrode(pixelBoard, endElectrodeDictionary[tDrop.Id], tDrop);
                try {
                    Blockages.DropletSet(pixelBoard, new Coord(tDrop.PositionX, tDrop.PositionY), tDrop);
                    Blockages.DropletSet(pixelBoard, position, tDrop);
                    endingPixels.Add(tDrop.Id, position);
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new RouteException("Routing is not possible with the current droplet locations.");
                }
            }

            //start routing with non-contaminating drops
            //if successful add to final list, else stop
            foreach (var drop in drops) {
                if (!drop.Contamination) {
                    Console.WriteLine("Finding the path for clean drop: " + drop.Name);
                    var endPos = endingPixels[drop.Id];
                    var pixelPath = InputHandler.RoutingPackage(pixelBoard, new Coord(drop.PositionX, drop.PositionY), endPos, drop);
                    Console.WriteLine(drop.Name);
                    if (pixelPath.Any()) {
                        var finalElectrodePath = PixelElectrodeConversion.FindElectrodes(pixelBoard, pixelPath, drop);
                        finalDropOrder.Add(drop);
                        electrodePathCollection.Add(drop.Id, finalElectrodePath);
                    } else {
                        Console.WriteLine("Scheduling Unsuccessful");
                        throw new RouteException("Routing is not possible with the current droplet set.");
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
                    var endPos = endingPixels[attemptDrop.Id];
                    var contaminatedPath = InputHandler.RoutingPackage(pixelBoard, new Coord(attemptDrop.PositionX, attemptDrop.PositionY), endPos, attemptList[m]);
                    Console.WriteLine(attemptDrop.Name);

                    //if no path found delete from collection
                    if (!contaminatedPath.Any()) {
                        madeItThrough = false;
                        for (var n = 0; n < m; n++) {
                            electrodePathCollection.Remove(attemptList[n].Id);
                        }
                        break;
                    }
                    var finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, contaminatedPath, attemptDrop);
                    electrodePathCollection.Add(attemptDrop.Id, finalPath);
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
                Console.WriteLine();
                Console.WriteLine("Successful Order:");
                foreach (var drop in finalDropOrder) {
                    Console.Write(drop.Name + " ");
                }
                Console.WriteLine();

                var endProduct = CollisionDetector(finalDropOrder, electrodePathCollection);
                return endProduct;
            }

            Console.WriteLine();
            Console.WriteLine("Scheduling Unsuccessful");
            throw new RouteException("Routing is not possible with the current droplet set.");
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

        private static Dictionary<(int, int), List<int>> CollisionDetector(List<Droplet> finalOrder, Dictionary<int, List<int>> electrodePathCollection) {
            var dropIds = finalOrder.Select(drop => drop.Id).ToList();
            var collisionList = new Dictionary<(int, int), List<int>>();
            for (var i = 0; i < dropIds.Count; i++) {
                var m = i + 1;
                while (m < dropIds.Count) {
                    var collision = electrodePathCollection[dropIds[i]].Intersect(electrodePathCollection[dropIds[m]]).ToList();
                    if (collision.Any()) {
                        var tempList = new List<int>();
                        foreach (var electrode in collision) {
                            tempList.Add(electrode);
                        }
                        collisionList.Add((dropIds[i], dropIds[m]), tempList);
                    }
                    m++;
                }
            }
            return collisionList;
        }
    }
}
