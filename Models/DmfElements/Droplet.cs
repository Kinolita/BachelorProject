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

        public static void PrintDroplets(Board specs) {
            int len = specs.Droplets.Count;
            System.Console.WriteLine("Number of droplets on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Droplet Name: " + specs.Droplets[i].Name);
                System.Console.WriteLine("Droplet Id: " + specs.Droplets[i].Id);
                System.Console.WriteLine("Droplet Contents: " + specs.Droplets[i].SubstanceName);
                System.Console.WriteLine("Droplet Position: (" + specs.Droplets[i].PositionX + "," + specs.Droplets[i].PositionY + ")");
                System.Console.WriteLine("Droplet Size: " + specs.Droplets[i].SizeX + " by " + specs.Droplets[i].SizeY);
                System.Console.WriteLine("Droplet Color: " + specs.Droplets[i].Color);
                System.Console.WriteLine("Droplet Temperature: " + specs.Droplets[i].Temperature);
            }
            System.Console.WriteLine();
        }
    }
}
