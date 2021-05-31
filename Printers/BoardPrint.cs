using System;

using BachelorProject.Models;

namespace BachelorProject.Printers
{
    class BoardPrint
    {
        public static void PrintBoard(Pixels[,] pixelBoard) {
            Console.WriteLine("Printing the board:");
            for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                Console.Write("\n");
                for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                    bool one = pixelBoard[k, j].Empty;
                    string two = pixelBoard[k, j].BlockageType;

                    switch (one, two) {
                        case (false, "Buffer"):
                            Console.Write("B");
                            break;
                        case (false, "Contaminated"):
                            Console.Write("C");
                            break;
                        case (false, "1"):
                            Console.Write("1");
                            break;
                        case (false, "2"):
                            Console.Write("2");
                            break;
                        case (false, "3"):
                            Console.Write("3");
                            break;
                        case (false, "4"):
                            Console.Write("4");
                            break;
                        case (false, _):
                            Console.Write("X");
                            break;
                        default:
                            Console.Write("o");
                            break;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
