using System;

using BachelorProject.Models;

namespace BachelorProject.Printers
{
    class BoardPrint
    {
        public static void printBoard(Pixels[,] pixelBoard) {
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
                        case (false, "Droplet"):
                            Console.Write("D");
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
