using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BachelorProject.Models.DmfElements
{
    public class Board
    {
        [JsonProperty("information")]
        public IList<Information> Information { get; set; }
        [JsonProperty("electrodes")]
        public IList<Electrode> Electrodes { get; set; }
        [JsonProperty("actuators")]
        public IList<Actuator> Actuators { get; set; }
        [JsonProperty("sensors")]
        public IList<Sensor> Sensors { get; set; }
        [JsonProperty("inputs")]
        public IList<Input> Inputs { get; set; }
        [JsonProperty("outputs")]
        public IList<Output> Outputs { get; set; }
        [JsonProperty("droplets")]
        public IList<Droplet> Droplets { get; set; }
        [JsonProperty("bubbles")]
        public IList<Bubble> Bubbles { get; set; }
        [JsonProperty("unclassified")]
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
            Console.WriteLine();
        }
    }
}
