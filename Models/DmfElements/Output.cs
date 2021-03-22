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
            System.Console.WriteLine("Board Outputs: ");
            int len = specs.Outputs.Count;
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Output Name: " + specs.Outputs[i].Name);
                System.Console.WriteLine("Id: " + specs.Outputs[i].Id);
                System.Console.WriteLine("Output Id: " + specs.Outputs[i].OutputId);
                System.Console.WriteLine("Output Position: (" + specs.Outputs[i].PositionX + "," + specs.Outputs[i].PositionY + ")");
            }
            System.Console.WriteLine();
        }
    }
}
