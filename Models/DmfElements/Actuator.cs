using System;

namespace BachelorProject.Models.DmfElements

{
    public class Actuator
    {
        public string name { get; set; }
        public int ID { get; set; }
        public string actuatorId { get; set; }
        public string type { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int sizeX { get; set; }
        public int sizeY { get; set; }

        public static void PrintActuators(Board specs) {
            int len = specs.Actuators.Count;
            Console.WriteLine("Number of actuators on board: " + len);
            for (int i = 0; i < len; i++) {
                Console.WriteLine("Actuator name: " + specs.Actuators[i].name);
                Console.WriteLine("ID: " + specs.Actuators[i].ID);
                Console.WriteLine("Actuator ID: " + specs.Actuators[i].actuatorId);
                Console.WriteLine("Actuator type " + specs.Actuators[i].type);
                Console.WriteLine("Actuator Position: (" + specs.Actuators[i].positionX + "," + specs.Actuators[i].positionY + ")");
                Console.WriteLine("Actuator Size: " + specs.Actuators[i].sizeX + " by " + specs.Actuators[i].sizeY);
            }
            Console.WriteLine();
        }
    }
}
