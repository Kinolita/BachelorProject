using System;
using System.Collections.Generic;

namespace BachelorProject.Models.DmfElements
{
    public class Board
    {
        public IList<Information> Information { get; set; }
        public IList<Electrode> Electrodes { get; set; }
        public IList<Actuator> Actuators { get; set; }
        public IList<Sensor> Sensors { get; set; }
        public IList<Input> Inputs { get; set; }
        public IList<Output> Outputs { get; set; }
        public IList<Droplet> Droplets { get; set; }
        public IList<Bubble> Bubbles { get; set; }
        public object Unclassified { get; set; }

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
                        case (false, _):
                            Console.Write("X");
                            break;
                        default:
                            Console.Write("o");
                            break;
                    }
                }
            }
        }
    }
}
