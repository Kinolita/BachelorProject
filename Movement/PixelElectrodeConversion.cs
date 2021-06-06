using BachelorProject.Models;
using System;
using System.Collections.Generic;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    class PixelElectrodeConversion
    {
        //takes in pixel path and returns expanded electrode list
        public static List<int> FindElectrodes(Pixels[,] pixelBoard, List<Coord> listOfPixels, Droplet drop) {
            var dropSize = Math.Max(drop.SizeX, drop.SizeY);
            var finalSet = new HashSet<int>();
            var expandedPath = ExpandPixelPath(pixelBoard, listOfPixels, dropSize);
            var simpleList = new List<int>();

            //contaminating expanded path and adding electrode# to set to reduce repetitions
            foreach (var step in expandedPath) {
                if (drop.Contamination) Contamination.ContaminateBoard(pixelBoard, step);
                foreach (var pixelLocation in step) {
                    finalSet.Add(pixelBoard[pixelLocation.X, pixelLocation.Y].WhichElectrode);
                }
            }
            simpleList.AddRange(finalSet);
            return simpleList;
        }

        // takes a path list of pixels and expands to a list of lists based on given droplet size
        private static List<List<Coord>> ExpandPixelPath(Pixels[,] pixelBoard, List<Coord> pixelPath, float dropletSize) {
            var expandedPixelList = new List<List<Coord>>();
            //finding edges of contamination path for each pixel
            foreach (var location in pixelPath) {
                var left = location.X - (int)dropletSize / 2;
                var right = (int)Math.Ceiling(location.X + dropletSize / 2);
                var up = location.Y - (int)dropletSize / 2;
                var down = (int)Math.Ceiling(location.Y + dropletSize / 2);

                //accounting for large paths close to the edge
                if (left < 0) {
                    var extra = 0 - left;
                    left = 0;
                    right += extra;
                } else if (right > pixelBoard.GetLength(0)) {
                    var extra = pixelBoard.GetLength(0) - right;
                    right = pixelBoard.GetLength(0);
                    left += extra;
                }

                if (up < 0) {
                    var extra = 0 - up;
                    up = 0;
                    down += extra;
                } else if (down > pixelBoard.GetLength(1)) {
                    var extra = pixelBoard.GetLength(1) - down;
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
    }
}
