using BachelorProject.Models;
using System;
using System.Collections.Generic;

namespace BachelorProject.Movement
{
    class BasicRouting
    {
        public static void FindPath(Pixels[,] PixelBoard, Coord start, Coord end) {
            List<Coord> PixelList = new List<Coord>();
            BasicRouting.Move(PixelBoard, start, end, PixelList);
        }

        public static void Move(Pixels[,] PixelBoard, Coord start, Coord end, List<Coord> PixelList) {
            Coord NewMove;
            //Console.WriteLine("moving: ");
            if (start.x == end.x && start.y == end.y) {
                Console.WriteLine("The end was reached!");
                Console.WriteLine("Pixel list: ");
                for (int i = 0; i < PixelList.Count; i++) {
                    Console.Write("(" + PixelList[i].x + "," + PixelList[i].y + ")  ");
                }
                Console.WriteLine();
                Console.WriteLine("The final electrode path: ");
                FindElectrodes(PixelBoard, PixelList);
            } else if (!ValidateOnBoard(PixelBoard, start) || !ValidateOnBoard(PixelBoard, end)) {
                Console.WriteLine("One of the points is not on the board: (" + start.x + "," + start.y + ") (" + end.x + "," + end.y + ")");

                //Move right
            } else if (start.x < end.x && PixelBoard[start.x + 1, start.y].Vacancy) {
                NewMove = new Coord(start.x + 1, start.y);
                UpdateVacancy(PixelBoard, start, NewMove);
                PixelList.Add(NewMove);
                Move(PixelBoard, NewMove, end, PixelList);

                //Move left
            } else if (start.x > end.x && PixelBoard[start.x - 1, start.y].Vacancy) {
                NewMove = new Coord(start.x - 1, start.y);
                UpdateVacancy(PixelBoard, start, NewMove);
                PixelList.Add(NewMove);
                Move(PixelBoard, NewMove, end, PixelList);

                //Move down
            } else if (start.y < end.y && PixelBoard[start.x, start.y + 1].Vacancy) {
                NewMove = new Coord(start.x, start.y + 1);
                UpdateVacancy(PixelBoard, start, NewMove);
                PixelList.Add(NewMove);
                Move(PixelBoard, NewMove, end, PixelList);

                //Move up
            } else if (start.y > end.y && PixelBoard[start.x, start.y - 1].Vacancy) {
                NewMove = new Coord(start.x, start.y - 1);
                UpdateVacancy(PixelBoard, start, NewMove);
                PixelList.Add(NewMove);
                Move(PixelBoard, NewMove, end, PixelList);

            } else {
                Console.WriteLine("Something happened at point (" + start.x + "," + start.y + ")");
                Console.WriteLine("The electrode path so far: ");
                FindElectrodes(PixelBoard, PixelList);
            }
        }

        public static bool ValidateOnBoard(Pixels[,] PixelBoard, Coord check) {
            if (check.x < PixelBoard.GetLength(0) && check.y < PixelBoard.GetLength(1) && check.x >= 0 && check.y >= 0) {
                return true;
            }
            return false;
        }

        public static void UpdateVacancy(Pixels[,] PixelBoard, Coord old, Coord neww) {
            PixelBoard[old.x, old.y].Vacancy = true;
            PixelBoard[neww.x, neww.y].Vacancy = false;
        }

        public static void FindElectrodes(Pixels[,] PixelBoard, List<Coord> listy) {
            HashSet<int> EList = new HashSet<int>();
            for (int i = 0; i < listy.Count; i++) {
                EList.Add(PixelBoard[listy[i].x, listy[i].y].WhichElectrode);
            }
            foreach (int that in EList) {
                Console.WriteLine(that);
            }
        }
    }
}
