using Newtonsoft.Json;

namespace BachelorProject.Models.Dtos
{
    public class Droplet
    {
        public string Name { get; set; }
        public int ID { get; set; }
        [JsonProperty("substance_name")]
        public string SubstanceName { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string Color { get; set; }
        public float Temperature { get; set; }

        public static void PrintDroplets(BachelorProject.Models.Dtos.Board Specs) {
            int len = Specs.Droplets.Count;
            System.Console.WriteLine("Number of droplets on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Droplet Name: " + Specs.Droplets[i].Name);
                System.Console.WriteLine("Droplet ID: " + Specs.Droplets[i].ID);
                System.Console.WriteLine("Droplet Contents: " + Specs.Droplets[i].SubstanceName);
                System.Console.WriteLine("Droplet Position: (" + Specs.Droplets[i].PositionX + "," + Specs.Droplets[i].PositionY + ")");
                System.Console.WriteLine("Droplet Size: " + Specs.Droplets[i].SizeX + " by " + Specs.Droplets[i].SizeY);
                System.Console.WriteLine("Droplet Color: " + Specs.Droplets[i].Color);
                System.Console.WriteLine("Droplet Temperature: " + Specs.Droplets[i].Temperature);
            }
            System.Console.WriteLine();
        }
    }
}
