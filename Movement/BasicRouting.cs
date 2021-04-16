using BachelorProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BachelorProject.Movement
{
    class BasicRouting
    {
        //Suppressed code here is the initial dumb routing (not used anymore)

        //public static void FindPath(Pixels[,] pixelBoard, Coord start, Coord end) {
        //    List<Coord> pixelList = new List<Coord>();
        //    BasicRouting.Move(pixelBoard, start, end, pixelList);
        //}

        //public static void Move(Pixels[,] pixelBoard, Coord start, Coord end, List<Coord> pixelList) {
        //    Coord newMove;
        //    if (start.X == end.X && start.Y == end.Y) {
        //        Console.WriteLine("The end was reached!");
        //        Console.WriteLine("Pixel list: ");
        //        for (int i = 0; i < pixelList.Count; i++) {
        //            Console.Write("(" + pixelList[i].X + "," + pixelList[i].Y + ")  ");
        //        }

        //        Console.WriteLine();
        //        Console.WriteLine("The final electrode path: ");
        //        FindElectrodes(pixelBoard, pixelList);
        //    } else if (!ValidateOnBoard(pixelBoard, start) || !ValidateOnBoard(pixelBoard, end)) {
        //        Console.WriteLine("One of the points is not on the board: (" + start.X + "," + start.Y + ") (" + end.X +
        //                          "," + end.Y + ")");

        //        //Move right
        //    } else if (start.X < end.X && pixelBoard[start.X + 1, start.Y].Empty) {
        //        newMove = new Coord(start.X + 1, start.Y);
        //        UpdateVacancy(pixelBoard, start, newMove);
        //        pixelList.Add(newMove);
        //        Move(pixelBoard, newMove, end, pixelList);

        //        //Move left
        //    } else if (start.X > end.X && pixelBoard[start.X - 1, start.Y].Empty) {
        //        newMove = new Coord(start.X - 1, start.Y);
        //        UpdateVacancy(pixelBoard, start, newMove);
        //        pixelList.Add(newMove);
        //        Move(pixelBoard, newMove, end, pixelList);

        //        //Move down
        //    } else if (start.Y < end.Y && pixelBoard[start.X, start.Y + 1].Empty) {
        //        newMove = new Coord(start.X, start.Y + 1);
        //        UpdateVacancy(pixelBoard, start, newMove);
        //        pixelList.Add(newMove);
        //        Move(pixelBoard, newMove, end, pixelList);

        //        //Move up
        //    } else if (start.Y > end.Y && pixelBoard[start.X, start.Y - 1].Empty) {
        //        newMove = new Coord(start.X, start.Y - 1);
        //        UpdateVacancy(pixelBoard, start, newMove);
        //        pixelList.Add(newMove);
        //        Move(pixelBoard, newMove, end, pixelList);

        //    } else {
        //        Console.WriteLine("Something happened at point (" + start.X + "," + start.Y + ")");
        //        Console.WriteLine("The electrode path so far: ");
        //        FindElectrodes(pixelBoard, pixelList);
        //        FindElectrodes(pixelBoard, pixelList);
        //    }
        //}

        public static void UpdateVacancy(Pixels[,] pixelBoard, Coord previous, Coord current) {
            pixelBoard[previous.X, previous.Y].Empty = true;
            pixelBoard[current.X, current.Y].Empty = false;
        }

        public static bool ValidateOnBoard(Pixels[,] pixelBoard, Coord check) {
            return check.X < pixelBoard.GetLength(0) && check.Y < pixelBoard.GetLength(1) && check.X >= 0 &&
                   check.Y >= 0;
        }

        public static int EdgeFind(Pixels[,] pixelBoard, Coord pix, char a) {
            int ans = -1;
            if (ValidateOnBoard(pixelBoard, pix)) {
                switch (a) {
                    case 'L':
                        ans = pixelBoard.GetLength(0);
                        for (int p = 0; p <= pixelBoard[pix.X, pix.Y].XRange; p++) {
                            if (ValidateOnBoard(pixelBoard, new Coord(pix.X - p, pix.Y))
                                && (pixelBoard[pix.X, pix.Y].WhichElectrode.Equals(pixelBoard[pix.X - p, pix.Y].WhichElectrode))) {
                                ans = pix.X - p;
                            }
                        }
                        break;
                    case 'R':
                        for (int p = 0; p <= pixelBoard[pix.X, pix.Y].XRange; p++) {
                            if (ValidateOnBoard(pixelBoard, new Coord(pix.X + p, pix.Y))
                                && (pixelBoard[pix.X, pix.Y].WhichElectrode.Equals(pixelBoard[pix.X + p, pix.Y].WhichElectrode))) {
                                ans = pix.X + p;
                            }
                        }
                        break;
                    case 'T':
                        ans = pixelBoard.GetLength(1);
                        for (int p = 0; p <= pixelBoard[pix.X, pix.Y].YRange; p++) {
                            if (ValidateOnBoard(pixelBoard, new Coord(pix.X, pix.Y - p))
                                && (pixelBoard[pix.X, pix.Y].WhichElectrode.Equals(pixelBoard[pix.X, pix.Y - p].WhichElectrode))) {
                                ans = pix.Y - p;
                            }
                        }
                        break;
                    case 'B':
                        for (int p = 0; p <= pixelBoard[pix.X, pix.Y].YRange; p++) {
                            if (ValidateOnBoard(pixelBoard, new Coord(pix.X, pix.Y + p))
                                && (pixelBoard[pix.X, pix.Y].WhichElectrode.Equals(pixelBoard[pix.X, pix.Y + p].WhichElectrode))) {
                                ans = pix.Y + p;
                            }
                        }
                        break;
                }
            }
            return ans;
        }

        public static List<List<Coord>> ExpandElectrodePath(Pixels[,] pixelBoard, List<Coord> pixelPath, int dropletSize) {
            //TODO
            List<List<Coord>> expandedElectrodeList = new List<List<Coord>>();
            foreach (Coord that in pixelPath) {
                //finding electrode edges for each coordinate in the path
                List<Coord> temp = new List<Coord>() { that };
                int leftoverX = dropletSize - pixelBoard[that.X, that.Y].XRange;
                int leftOverY = dropletSize - pixelBoard[that.X, that.Y].YRange;
                int leftEdge = EdgeFind(pixelBoard, that, 'L');
                int rightEdge = EdgeFind(pixelBoard, that, 'R');
                int topEdge = EdgeFind(pixelBoard, that, 'T');
                int bottomEdge = EdgeFind(pixelBoard, that, 'B');

                int maxLeft = pixelBoard.GetLength(0);
                int maxRight = 0;
                int maxTop = pixelBoard.GetLength(1);
                int maxBottom = 0;
                int xTotal = maxRight - maxLeft + 1;
                int yTotal = maxBottom - maxTop + 1;

                //expanding in both directions if droplet bigger than current electrode range
                if (pixelBoard[that.X, that.Y].XRange < dropletSize || pixelBoard[that.X, that.Y].YRange < dropletSize) {
                    //we only search from the edge to the edge + the leftover to reduce search size
                    for (int k = (leftEdge - leftoverX); k <= (rightEdge + leftoverX); k++) {
                        for (int j = (topEdge - leftOverY); j <= (bottomEdge + leftOverY); j++) {
                            int localLeft = EdgeFind(pixelBoard, new Coord(k, j), 'L');
                            int localRight = EdgeFind(pixelBoard, new Coord(k, j), 'R');
                            int localTop = EdgeFind(pixelBoard, new Coord(k, j), 'T');
                            int localBottom = EdgeFind(pixelBoard, new Coord(k, j), 'B');

                            //Console.WriteLine("(" + that.X + ", " + that.Y + ") " + k + " " + j + " Tot: " + xTotal + " " + yTotal +
                            //                  " Max: " + maxLeft + " " + maxRight + " " + maxTop + " " + maxBottom +
                            //                  " Loc: " + localLeft + " " + localRight + " " + localTop + " " + localBottom);

                            //check total Xrange so far and add if necessary
                            if (xTotal < dropletSize) {
                                //if on the board and not blocked
                                if (ValidateOnBoard(pixelBoard, new Coord(k, j))
                                    && Blockages.BlockageCheck(pixelBoard, new Coord(k, j))) {

                                    //does it increase xrange? if so add
                                    if (localLeft < maxLeft && pixelBoard[k, j].WhichElectrode != -1) {
                                        maxLeft = localLeft;
                                        xTotal = maxRight - maxLeft + 1;
                                        temp.Add(new Coord(k, j));
                                    }

                                    if (localRight > maxRight && pixelBoard[k, j].WhichElectrode != -1) {
                                        maxRight = localRight;
                                        xTotal = maxRight - maxLeft + 1;
                                        temp.Add(new Coord(k, j));
                                    }
                                }
                            }

                            //check total Yrange so far and add if necessary
                            if (yTotal < dropletSize) {
                                //if on the board and not blocked
                                if (ValidateOnBoard(pixelBoard, new Coord(k, j))
                                    && Blockages.BlockageCheck(pixelBoard, new Coord(k, j))) {

                                    //does it increase yrange? if so add.
                                    if (localTop < maxTop && pixelBoard[k, j].WhichElectrode != -1) {
                                        maxTop = localTop;
                                        yTotal = maxBottom - maxTop + 1;
                                        temp.Add(new Coord(k, j));
                                    }

                                    if (localBottom > maxBottom && pixelBoard[k, j].WhichElectrode != -1) {
                                        maxBottom = localBottom;
                                        yTotal = maxBottom - maxTop + 1;
                                        temp.Add(new Coord(k, j));
                                    }
                                }
                            }
                        }
                    }
                }
                expandedElectrodeList.Add(temp);
            }
            return expandedElectrodeList;
        }

        public static int SmallestElectrode(Pixels[,] pixelBoard) {
            int smallsX = 10000;
            int smallsY = 10000;
            for (var k = 0; k < pixelBoard.GetLength(0); k++) {
                for (var j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].XRange < smallsX) { smallsX = pixelBoard[k, j].XRange; }
                    if (pixelBoard[k, j].YRange < smallsY) { smallsY = pixelBoard[k, j].YRange; }
                }
            }
            if (smallsY < smallsX) { smallsX = smallsY; }
            return smallsX;
        }

        public static void FindElectrodes(Pixels[,] pixelBoard, List<Coord> listOfPixels, int dropSize) {
            List<SortedSet<int>> expandedSet = new List<SortedSet<int>>();
            int minElec = SmallestElectrode(pixelBoard);

            if (dropSize > minElec) {
                //expanding the original list into a list of lists
                List<List<Coord>> expandedElecList = ExpandElectrodePath(pixelBoard, listOfPixels, dropSize);
                foreach (var smallList in expandedElecList) {
                    SortedSet<int> finalSet = new SortedSet<int>();
                    for (int i = 0; i < smallList.Count(); i++) {
                        finalSet.Add(pixelBoard[smallList[i].X, smallList[i].Y].WhichElectrode);
                    }
                    expandedSet.Insert(0, finalSet);
                }

                for (int i = 0; i < expandedSet.Count() - 1; i++) {
                    while (expandedSet[i].SetEquals(expandedSet[i + 1])) {
                        expandedSet.RemoveAt(i + 1);
                    }
                }

                //printing the expended list
                expandedSet.Reverse();
                Console.WriteLine("expanded electrode path: ");
                foreach (var that in expandedSet) {
                    foreach (var smallThat in that) {
                        Console.Write(smallThat + " ");
                    }
                    Console.Write(", ");
                }
                Console.WriteLine();
            } else {
                HashSet<int> finalSet = new HashSet<int>();
                //code for single wide corridor
                //cycling through list of pixels and adding electrode# to hashset
                for (int i = 0; i < listOfPixels.Count; i++) {
                    finalSet.Add(pixelBoard[listOfPixels[i].X, listOfPixels[i].Y].WhichElectrode);
                }

                //printing electrode list
                Console.WriteLine("original electrode path: ");
                foreach (int that in finalSet) {
                    Console.Write(that + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
