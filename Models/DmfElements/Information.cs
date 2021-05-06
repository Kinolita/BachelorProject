using Newtonsoft.Json;
using System;

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
        [JsonProperty("sizeX")]
        public int SizeX { get; set; }
        [JsonProperty("sizeY")]
        public int SizeY { get; set; }

        public static void PrintInformation(Board specs) {
            Console.WriteLine("Board Specifications: ");
            var len = specs.Information.Count;

            //really there should be no need for the for loop as there should only be one block of information
            for (var i = 0; i < len; i++) {
                Console.WriteLine("Platform name: " + specs.Information[i].PlatformName);
                Console.WriteLine("Platform type: " + specs.Information[i].PlatformType);
                Console.WriteLine("Platform ID: " + specs.Information[i].PlatformId);
                Console.WriteLine("Platform Size: " + specs.Information[i].SizeX + " by " + specs.Information[i].SizeY);
            }
            Console.WriteLine();
        }
    }
}