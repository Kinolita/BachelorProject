using BachelorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    class PixelElectrodeConversion
    {
        public static List<List<Coord>> ExpandPixelPath(Pixels[,] pixelBoard, List<Coord> pixelPath, float dropletSize) {
            var expandedPixelList = new List<List<Coord>>();
            //finding edges of contamination path for each pixel
            foreach (var that in pixelPath) {
                var a = that.X - (int)dropletSize / 2; //floor
                var b = (int)Math.Ceiling(that.X + dropletSize / 2); //ceiling
                var c = that.Y - (int)dropletSize / 2; //floor
                var d = (int)Math.Ceiling(that.Y + dropletSize / 2); //ceiling

                //accounting for large paths close to the edge
                if (a < 0) {
                    int extra = 0 - a;
                    a = 0;
                    b += extra;
                } else if (b > pixelBoard.GetLength(0)) {
                    int extra = pixelBoard.GetLength(0) - b;
                    b = pixelBoard.GetLength(0);
                    a += extra;
                }

                if (c < 0) {
                    int extra = 0 - c;
                    c = 0;
                    d += extra;
                } else if (d > pixelBoard.GetLength(1)) {
                    int extra = pixelBoard.GetLength(1) - d;
                    d = pixelBoard.GetLength(1);
                    c += extra;
                }

                var checklist = new List<Coord>();
                //if (pixelBoard[that.X, that.Y].XRange < dropletSize || pixelBoard[that.X, that.Y].YRange < dropletSize) {
                //if (pixelBoard[that.X, that.Y].Empty) {
                for (var k = a; k < b; k++) {
                    for (var j = c; j < d; j++) {
                        checklist.Add(new Coord(k, j));
                    }
                    //}
                }
                expandedPixelList.Add(checklist);
            }
            return expandedPixelList;
        }

        public static List<SortedSet<int>> PixelToElectrode(Pixels[,] pixelBoard, List<List<Coord>> expandedPixelList) {
            var expandedElectrodeList = new List<SortedSet<int>>();
            var previousChecklist = new SortedSet<int>() { -1 };

            foreach (var step in expandedPixelList) {
                var checklist = new SortedSet<int>();
                foreach (var pix in step) {
                    checklist.Add(pixelBoard[pix.X, pix.Y].WhichElectrode);
                }

                if (checklist.SetEquals(previousChecklist)) continue;
                expandedElectrodeList.Add(checklist);
                previousChecklist = checklist;
            }
            return expandedElectrodeList;
        }

        public static int SmallestElectrode(Pixels[,] pixelBoard) {
            var smallsX = 10000;
            var smallsY = 10000;
            for (var k = 0; k < pixelBoard.GetLength(0); k++) {
                for (var j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].WhichElectrode != -1 && pixelBoard[k, j].XRange < smallsX) { smallsX = pixelBoard[k, j].XRange; }
                    if (pixelBoard[k, j].WhichElectrode != -1 && pixelBoard[k, j].YRange < smallsY) { smallsY = pixelBoard[k, j].YRange; }
                }
            }

            if (smallsY < smallsX) { smallsX = smallsY; }
            return smallsX;
        }

        //takes in pixel path and returns expanded electrode list
        public static List<SortedSet<int>> FindElectrodes(Pixels[,] pixelBoard, List<Coord> listOfPixels, Droplet drop) {
            var minElectrode = SmallestElectrode(pixelBoard);
            var dropSize = Math.Max(drop.SizeX, drop.SizeY);

            if (dropSize > minElectrode) {
                //expanding the original pixel path into a list of lists
                var that = ExpandPixelPath(pixelBoard, listOfPixels, dropSize);

                //test printing expanded pixel path
                //Console.WriteLine("expanded pixel path: ");
                //foreach (var that2 in that) {
                //    foreach (var that3 in that2) {
                //        Console.Write(that3.X + "," + that3.Y + "   ");
                //    }
                //    Console.WriteLine();
                //}

                if (drop.Contamination) {
                    foreach (var step in that) {
                        //Console.WriteLine("contaminated step for " + drop.Name);
                        //foreach (var dgd in step) {
                        //    Console.Write(dgd.X + "," + dgd.Y + "  ");
                        //}
                        Scheduler.ContaminateMe(pixelBoard, step);
                    }
                }

                //converting expanded pixel path to electrodes
                var expandedElectrodeList = PixelToElectrode(pixelBoard, that);
                return expandedElectrodeList;
            }

            var finalSet = new HashSet<int>();
            //TODO - only contaminating a single wide pixel
            var expandedPath = ExpandPixelPath(pixelBoard, listOfPixels, dropSize);
            //contaminating expanded path and adding electrode# to hashset
            foreach (var step in expandedPath) {
                Scheduler.ContaminateMe(pixelBoard, step);
                foreach (var pixelCoord in step) {
                    finalSet.Add(pixelBoard[pixelCoord.X, pixelCoord.Y].WhichElectrode);
                }
            }
            return finalSet.Select(step => new SortedSet<int>() { step }).ToList();
        }
    }
}
