namespace BachelorProject.Models.Dtos
{
    public class Input
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string InputID { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public static void PrintInputs(BachelorProject.Models.Dtos.Board Specs) {
            System.Console.WriteLine("Board Inputs: ");
            int len = Specs.Inputs.Count;
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Input Name: " + Specs.Inputs[i].Name);
                System.Console.WriteLine("ID: " + Specs.Inputs[i].ID);
                System.Console.WriteLine("Input ID: " + Specs.Inputs[i].InputID);
                System.Console.WriteLine("Input Position: (" + Specs.Inputs[i].PositionX + "," + Specs.Inputs[i].PositionY + ")");
            }
            System.Console.WriteLine();
        }
    }
}
