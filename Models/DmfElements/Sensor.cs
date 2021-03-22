namespace BachelorProject.Models.DmfElements
{
    public class Sensor
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string SensorId { get; set; }
        public string Type { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintSensors(Board specs) {
            int len = specs.Sensors.Count;
            System.Console.WriteLine("Number of sensors on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Sensor Name: " + specs.Sensors[i].Name);
                System.Console.WriteLine("Id: " + specs.Sensors[i].Id);
                System.Console.WriteLine("Sensor Id: " + specs.Sensors[i].SensorId);
                System.Console.WriteLine("Sensor Type " + specs.Sensors[i].Type);
                System.Console.WriteLine("Sensor Position: (" + specs.Sensors[i].PositionX + "," + specs.Sensors[i].PositionY + ")");
                System.Console.WriteLine("Sensor Size: " + specs.Sensors[i].SizeX + " by " + specs.Sensors[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}
