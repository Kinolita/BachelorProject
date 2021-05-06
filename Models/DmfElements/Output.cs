using System;

namespace BachelorProject.Models.DmfElements
{
    public class Output
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string OutputId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public static void PrintOutputs(Board specs) {
            Console.WriteLine("Board Outputs: ");
            var len = specs.Outputs.Count;
            for (var i = 0; i < len; i++) {
                Console.WriteLine("Output name: " + specs.Outputs[i].Name);
                Console.WriteLine("ID: " + specs.Outputs[i].Id);
                Console.WriteLine("Output ID: " + specs.Outputs[i].OutputId);
                Console.WriteLine("Output Position: (" + specs.Outputs[i].PositionX + "," + specs.Outputs[i].PositionY + ")");
            }
            Console.WriteLine();
        }
    }
}
