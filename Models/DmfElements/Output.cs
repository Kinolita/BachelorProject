namespace BachelorProject.Models.Dtos
{
    public class Output
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string OutputID { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public static void PrintOutputs(BachelorProject.Models.Dtos.Board Specs) {
            System.Console.WriteLine("Board Inputs: ");
            int len = Specs.Outputs.Count;
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Input Name: " + Specs.Outputs[i].Name);
                System.Console.WriteLine("ID: " + Specs.Outputs[i].ID);
                System.Console.WriteLine("Input ID: " + Specs.Outputs[i].OutputID);
                System.Console.WriteLine("Input Position: (" + Specs.Outputs[i].PositionX + "," + Specs.Outputs[i].PositionY + ")");
            }
            System.Console.WriteLine();
        }
    }
}
