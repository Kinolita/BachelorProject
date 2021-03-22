using Newtonsoft.Json;

namespace BachelorProject.Models.DmfElements
{
    public class Information
    {
        [JsonProperty("platform_name")]
        public string PlatformName { get; set; }
        [JsonProperty("platform_type")]
        public string PlatformType { get; set; }
        [JsonProperty("platform_ID")]
        public string PlatformId { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintInformation(Board specs) {
            System.Console.WriteLine("Board Specifications: ");
            int len = specs.Information.Count;

            //really there should be no need for the for loop as there should only be one block of information
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Platform Name: " + specs.Information[i].PlatformName);
                System.Console.WriteLine("Platform Type: " + specs.Information[i].PlatformType);
                System.Console.WriteLine("Platform Id: " + specs.Information[i].PlatformId);
                System.Console.WriteLine("Platform Size: " + specs.Information[i].SizeX + " by " + specs.Information[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}