namespace BachelorProject.Models.DmfElements

{
    public class Actuator
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string ActuatorId { get; set; }
        public string Type { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintActuators(Board specs) {
            int len = specs.Actuators.Count;
            System.Console.WriteLine("Number of actuators on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Actuator Name: " + specs.Actuators[i].Name);
                System.Console.WriteLine("Id: " + specs.Actuators[i].Id);
                System.Console.WriteLine("Actuator Id: " + specs.Actuators[i].ActuatorId);
                System.Console.WriteLine("Actuator Type " + specs.Actuators[i].Type);
                System.Console.WriteLine("Actuator Position: (" + specs.Actuators[i].PositionX + "," + specs.Actuators[i].PositionY + ")");
                System.Console.WriteLine("Actuator Size: " + specs.Actuators[i].SizeX + " by " + specs.Actuators[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}
