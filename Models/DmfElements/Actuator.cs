namespace BachelorProject.Models.Dtos
{
    public class Actuator
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string actuatorID { get; set; }
        public string type { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintActuators(BachelorProject.Models.Dtos.Board Specs) {
            int len = Specs.Actuators.Count;
            System.Console.WriteLine("Number of actuators on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Actuator Name: " + Specs.Actuators[i].Name);
                System.Console.WriteLine("ID: " + Specs.Actuators[i].ID);
                System.Console.WriteLine("Actuator ID: " + Specs.Actuators[i].actuatorID);
                System.Console.WriteLine("Actuator Type " + Specs.Actuators[i].type);
                System.Console.WriteLine("Actuator Position: (" + Specs.Actuators[i].PositionX + "," + Specs.Actuators[i].PositionY + ")");
                System.Console.WriteLine("Actuator Size: " + Specs.Actuators[i].SizeX + " by " + Specs.Actuators[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}
