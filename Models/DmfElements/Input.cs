using System;

namespace BachelorProject.Models.DmfElements
{
    public class Input
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string InputId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public static void PrintInputs(Board specs) {
            Console.WriteLine("Board Inputs: ");
            var len = specs.Inputs.Count;
            for (var i = 0; i < len; i++) {
                Console.WriteLine("Input name: " + specs.Inputs[i].Name);
                Console.WriteLine("ID: " + specs.Inputs[i].Id);
                Console.WriteLine("Input ID: " + specs.Inputs[i].InputId);
                Console.WriteLine("Input Position: (" + specs.Inputs[i].PositionX + "," + specs.Inputs[i].PositionY + ")");
            }
            Console.WriteLine();
        }
    }
}
