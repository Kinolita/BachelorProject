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
            foreach (var location in pixelPath) {
                var left = location.X - (int)dropletSize / 2;
                var right = (int)Math.Ceiling(location.X + dropletSize / 2);
                var up = location.Y - (int)dropletSize / 2;
                var down = (int)Math.Ceiling(location.Y + dropletSize / 2);

                //accounting for large paths close to the edge
                if (left < 0) {
                    int extra = 0 - left;
                    left = 0;
                    right += extra;
                } else if (right > pixelBoard.GetLength(0)) {
                    int extra = pixelBoard.GetLength(0) - right;
                    right = pixelBoard.GetLength(0);
                    left += extra;
                }

                if (up < 0) {
                    int extra = 0 - up;
                    up = 0;
                    down += extra;
                } else if (down > pixelBoard.GetLength(1)) {
                    int extra = pixelBoard.GetLength(1) - down;
                    down = pixelBoard.GetLength(1);
                    up += extra;
                }

                var checklist = new List<Coord>();
                for (var k = left; k < right; k++) {
                    for (var j = up; j < down; j++) {
                        checklist.Add(new Coord(k, j));
                    }
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
            var smallsX = 100000;
            var smallsY = 100000;
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
                var expandedPixelPath = ExpandPixelPath(pixelBoard, listOfPixels, dropSize);

                if (drop.Contamination) {
                    foreach (var step in expandedPixelPath) {
                        Scheduler.ContaminateBoard(pixelBoard, step);
                    }
                }

                //converting expanded pixel path to electrodes
                var expandedElectrodeList = PixelToElectrode(pixelBoard, expandedPixelPath);
                return expandedElectrodeList;
            }

            var finalSet = new HashSet<int>();
            var expandedPath = ExpandPixelPath(pixelBoard, listOfPixels, dropSize);
            //contaminating expanded path and adding electrode# to hashset
            foreach (var step in expandedPath) {
                Scheduler.ContaminateBoard(pixelBoard, step);
                foreach (var pixelLocation in step) {
                    finalSet.Add(pixelBoard[pixelLocation.X, pixelLocation.Y].WhichElectrode);
                }
            }
            return finalSet.Select(step => new SortedSet<int>() { step }).ToList();
        }
    }
}
