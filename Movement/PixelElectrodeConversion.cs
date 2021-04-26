﻿using BachelorProject.Models;
using System;
using System.Collections.Generic;

namespace BachelorProject.Movement
{
    class PixelElectrodeConversion
    {
        public static List<SortedSet<int>> ExpandElectrodePath(Pixels[,] pixelBoard, List<Coord> pixelPath, double dropletSize) {
            List<SortedSet<int>> expandedElectrodeList = new List<SortedSet<int>>();
            SortedSet<int> previousChecklist = new SortedSet<int>() { -1 };
            int minD = (int)Math.Floor(dropletSize / 2);
            int maxD = (int)Math.Ceiling(dropletSize / 2);


            foreach (Coord that in pixelPath) {
                //finding electrode edges for each coordinate in the path
                SortedSet<int> checklist = new SortedSet<int>();

                if (pixelBoard[that.X, that.Y].XRange < dropletSize || pixelBoard[that.X, that.Y].YRange < dropletSize) {
                    for (int k = that.X - minD; k < that.X + maxD; k++) {
                        for (int j = that.Y - minD; j < that.Y + maxD; j++) {
                            checklist.Add(pixelBoard[k, j].WhichElectrode);
                        }
                    }
                }

                if (!checklist.SetEquals(previousChecklist)) {
                    expandedElectrodeList.Add(checklist);
                    previousChecklist = checklist;
                }
            }
            return expandedElectrodeList;
        }

        public static int SmallestElectrode(Pixels[,] pixelBoard) {
            int smallsX = 10000;
            int smallsY = 10000;
            for (var k = 0; k < pixelBoard.GetLength(0); k++) {
                for (var j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].WhichElectrode != -1 && pixelBoard[k, j].XRange < smallsX) { smallsX = pixelBoard[k, j].XRange; }
                    if (pixelBoard[k, j].WhichElectrode != -1 && pixelBoard[k, j].YRange < smallsY) { smallsY = pixelBoard[k, j].YRange; }
                }
            }

            if (smallsY < smallsX) { smallsX = smallsY; }
            return smallsX;
        }

        public static List<SortedSet<int>> FindElectrodes(Pixels[,] pixelBoard, List<Coord> listOfPixels, int dropSize) {
            int minElec = SmallestElectrode(pixelBoard);

            if (dropSize > minElec) {
                //expanding the original list into a list of lists
                List<SortedSet<int>> expandedElecList = ExpandElectrodePath(pixelBoard, listOfPixels, dropSize);
                return expandedElecList;
            }

            List<SortedSet<int>> finalPath = new List<SortedSet<int>>();
            HashSet<int> finalSet = new HashSet<int>();
            //code for single wide corridor
            //cycling through list of pixels and adding electrode# to hashset
            for (int i = 0; i < listOfPixels.Count; i++) {
                finalSet.Add(pixelBoard[listOfPixels[i].X, listOfPixels[i].Y].WhichElectrode);
            }

            foreach (var step in finalSet) {
                finalPath.Add(new SortedSet<int>() { step });
            }

            return finalPath;
        }
    }
}
