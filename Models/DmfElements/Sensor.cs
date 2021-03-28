using System;

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
            Console.WriteLine("Number of sensors on board: " + len);
            for (int i = 0; i < len; i++) {
                Console.WriteLine("Sensor name: " + specs.Sensors[i].Name);
                Console.WriteLine("ID: " + specs.Sensors[i].Id);
                Console.WriteLine("Sensor ID: " + specs.Sensors[i].SensorId);
                Console.WriteLine("Sensor type " + specs.Sensors[i].Type);
                Console.WriteLine("Sensor Position: (" + specs.Sensors[i].PositionX + "," + specs.Sensors[i].PositionY + ")");
                Console.WriteLine("Sensor Size: " + specs.Sensors[i].SizeX + " by " + specs.Sensors[i].SizeY);
            }
            Console.WriteLine();
        }
    }
}
