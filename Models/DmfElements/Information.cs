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

        public static void PrintInformation(BachelorProject.Models.Dtos.Board Specs) {
            System.Console.WriteLine("Board Specifications: ");
            int len = Specs.Information.Count;
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Platform Name: " + Specs.Information[i].PlatformName);
                System.Console.WriteLine("Platform Type: " + Specs.Information[i].PlatformType);
                System.Console.WriteLine("Platform ID: " + Specs.Information[i].PlatformID);
                System.Console.WriteLine("Platform Size: " + Specs.Information[i].SizeX + " by " + Specs.Information[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}