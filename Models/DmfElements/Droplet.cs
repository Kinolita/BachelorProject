using System;
using Newtonsoft.Json;

namespace BachelorProject.Models.DmfElements
{
    public class Droplet
    {
        public string Name { get; set; }
        public int Id { get; set; }
        [JsonProperty("substance_name")]
        public string SubstanceName { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string Color { get; set; }
        public float Temperature { get; set; }
        public bool Contamination { get; set; }

        public static void PrintDroplets(Board specs) {
            int len = specs.Droplets.Count;
            Console.WriteLine("Number of droplets on board: " + len);
            for (int i = 0; i < len; i++) {
                Console.WriteLine("Droplet name: " + specs.Droplets[i].Name);
                Console.WriteLine("Droplet ID: " + specs.Droplets[i].Id);
                Console.WriteLine("Droplet Contents: " + specs.Droplets[i].SubstanceName);
                Console.WriteLine("Droplet Position: (" + specs.Droplets[i].PositionX + "," + specs.Droplets[i].PositionY + ")");
                Console.WriteLine("Droplet Size: " + specs.Droplets[i].SizeX + " by " + specs.Droplets[i].SizeY);
                Console.WriteLine("Droplet Color: " + specs.Droplets[i].Color);
                Console.WriteLine("Droplet Temperature: " + specs.Droplets[i].Temperature);
                Console.WriteLine("Droplet Contamination status: " + specs.Droplets[i].Contamination);
            }
            Console.WriteLine();
        }

        public static int MaxSize(Droplet drop) {
            return Math.Max(drop.SizeX, drop.SizeY);
        }
    }
}
