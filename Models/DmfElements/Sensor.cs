namespace BachelorProject.Models.Dtos
{
    public class Sensor
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string SensorID { get; set; }
        public string type { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public static void PrintSensors(BachelorProject.Models.Dtos.Board Specs) {
            int len = Specs.Sensors.Count;
            System.Console.WriteLine("Number of sensors on board: " + len);
            for (int i = 0; i < len; i++) {
                System.Console.WriteLine("Sensor Name: " + Specs.Sensors[i].Name);
                System.Console.WriteLine("ID: " + Specs.Sensors[i].ID);
                System.Console.WriteLine("Sensor ID: " + Specs.Sensors[i].SensorID);
                System.Console.WriteLine("Sensor Type " + Specs.Sensors[i].type);
                System.Console.WriteLine("Sensor Position: (" + Specs.Sensors[i].PositionX + "," + Specs.Sensors[i].PositionY + ")");
                System.Console.WriteLine("Sensor Size: " + Specs.Sensors[i].SizeX + " by " + Specs.Sensors[i].SizeY);
            }
            System.Console.WriteLine();
        }
    }
}
