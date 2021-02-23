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
    }
}
