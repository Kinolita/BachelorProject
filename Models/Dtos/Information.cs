using Newtonsoft.Json;

namespace BachelorProject.Models.Dtos
{
    public class Information
    {
        [JsonProperty("platform_name")]
        public string PlatformName { get; set; }
        [JsonProperty("platform_type")]
        public string PlatformType { get; set; }
        [JsonProperty("platform_ID")]
        public string PlatformID { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }
}