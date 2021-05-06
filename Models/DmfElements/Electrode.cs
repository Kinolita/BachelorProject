using System;
using System.Collections.Generic;

namespace BachelorProject.Models.DmfElements
{
    public class Electrode
    {
        public string name { get; set; }
        public int ID { get; set; }
        public int electrodeID { get; set; }
        public int driverID { get; set; }
        public int shape { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int sizeX { get; set; }
        public int sizeY { get; set; }
        public int status { get; set; }
        public IList<IList<int>> corners { get; set; }

        public static void PrintElectrodes(Board specs) {
            var len = specs.Electrodes.Count;
            Console.WriteLine("Number of electrodes on board: " + len);
            for (var i = 0; i < len; i++) {

                Console.WriteLine("Electrode name: " + specs.Electrodes[i].name);
                Console.WriteLine("ID: " + specs.Electrodes[i].ID);
                Console.WriteLine("Electrode ID: " + specs.Electrodes[i].electrodeID);
                Console.WriteLine("Driver ID: " + specs.Electrodes[i].driverID);


                if (specs.Electrodes[i].shape == 0) {
                    Console.WriteLine("Electrode shape: rectangle");
                    Console.WriteLine("Position of top left corner: (" + specs.Electrodes[i].positionX + ", " + specs.Electrodes[i].positionY + ")");
                    Console.Write("Electrode Size: " + specs.Electrodes[i].sizeX + " by " + specs.Electrodes[i].sizeY);

                } else {
                    Console.WriteLine("Electrode shape: custom polygon");
                    Console.WriteLine("Electrode Origin: (" + specs.Electrodes[i].positionX + "," + specs.Electrodes[i].positionY + ")");
                    Console.Write("Electrode corners: ");
                    foreach (var corner in specs.Electrodes[i].corners) {
                        Console.Write("(" + corner[0] + "," + corner[1] + ") ");
                    }
                }

                var status1 = specs.Electrodes[i].status == 0 ? "off" : "on";
                Console.WriteLine(Environment.NewLine + "Electrode status: " + status1);
                Console.WriteLine();
            }
        }
    }
}
