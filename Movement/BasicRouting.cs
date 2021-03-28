using BachelorProject.Models;
using System;
using System.Collections.Generic;

namespace BachelorProject.Movement
{
    class BasicRouting
    {
        public static void FindPath(Pixels[,] pixelBoard, Coord start, Coord end) {
            List<Coord> pixelList = new List<Coord>();
            BasicRouting.Move(pixelBoard, start, end, pixelList);
        }

        public static void Move(Pixels[,] pixelBoard, Coord start, Coord end, List<Coord> pixelList) {
            Coord newMove;
            if (start.X == end.X && start.Y == end.Y) {
                Console.WriteLine("The end was reached!");
                Console.WriteLine("Pixel list: ");
                for (int i = 0; i < pixelList.Count; i++) {
                    Console.Write("(" + pixelList[i].X + "," + pixelList[i].Y + ")  ");
                }
                Console.WriteLine();
                Console.WriteLine("The final electrode path: ");
                FindElectrodes(pixelBoard, pixelList);
            } else if (!ValidateOnBoard(pixelBoard, start) || !ValidateOnBoard(pixelBoard, end)) {
                Console.WriteLine("One of the points is not on the board: (" + start.X + "," + start.Y + ") (" + end.X + "," + end.Y + ")");

                //Move right
            } else if (start.X < end.X && pixelBoard[start.X + 1, start.Y].Empty) {
                newMove = new Coord(start.X + 1, start.Y);
                UpdateVacancy(pixelBoard, start, newMove);
                pixelList.Add(newMove);
                Move(pixelBoard, newMove, end, pixelList);

                //Move left
            } else if (start.X > end.X && pixelBoard[start.X - 1, start.Y].Empty) {
                newMove = new Coord(start.X - 1, start.Y);
                UpdateVacancy(pixelBoard, start, newMove);
                pixelList.Add(newMove);
                Move(pixelBoard, newMove, end, pixelList);

                //Move down
            } else if (start.Y < end.Y && pixelBoard[start.X, start.Y + 1].Empty) {
                newMove = new Coord(start.X, start.Y + 1);
                UpdateVacancy(pixelBoard, start, newMove);
                pixelList.Add(newMove);
                Move(pixelBoard, newMove, end, pixelList);

                //Move up
            } else if (start.Y > end.Y && pixelBoard[start.X, start.Y - 1].Empty) {
                newMove = new Coord(start.X, start.Y - 1);
                UpdateVacancy(pixelBoard, start, newMove);
                pixelList.Add(newMove);
                Move(pixelBoard, newMove, end, pixelList);

            } else {
                Console.WriteLine("Something happened at point (" + start.X + "," + start.Y + ")");
                Console.WriteLine("The electrode path so far: ");
                FindElectrodes(pixelBoard, pixelList);
                FindElectrodes(pixelBoard, pixelList);
            }
        }

        public static bool ValidateOnBoard(Pixels[,] pixelBoard, Coord check) {
            return check.X < pixelBoard.GetLength(0) && check.Y < pixelBoard.GetLength(1) && check.X >= 0 && check.Y >= 0;
        }

        public static void UpdateVacancy(Pixels[,] pixelBoard, Coord previous, Coord current) {
            pixelBoard[previous.X, previous.Y].Empty = true;
            pixelBoard[current.X, current.Y].Empty = false;
        }

        public static void FindElectrodes(Pixels[,] pixelBoard, List<Coord> listOfPixels) {
            HashSet<int> finalList = new HashSet<int>();
            for (int i = 0; i < listOfPixels.Count; i++) {
                finalList.Add(pixelBoard[listOfPixels[i].X, listOfPixels[i].Y].WhichElectrode);
            }
            foreach (int that in finalList) {
                Console.WriteLine(that);
            }
        }
    }
}
