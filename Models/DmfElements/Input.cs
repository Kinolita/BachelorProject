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
            System.Console.WriteLine("Board Inputs: ");
            int len = specs.Inputs.Count;
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Input Name: " + specs.Inputs[i].Name);
                System.Console.WriteLine("Id: " + specs.Inputs[i].Id);
                System.Console.WriteLine("Input Id: " + specs.Inputs[i].InputId);
                System.Console.WriteLine("Input Position: (" + specs.Inputs[i].PositionX + "," + specs.Inputs[i].PositionY + ")");
            }
            System.Console.WriteLine();
        }
    }
}
