using System;
using System.Collections.Generic;

namespace BachelorProject.Models.DmfElements
{
    public class Electrode
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ElectrodeId { get; set; }
        public int DriverId { get; set; }
        public int Shape { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Status { get; set; }
        public IList<IList<int>> Corners { get; set; }

        public static void PrintElectrodes(Board specs) {
            int len = specs.Electrodes.Count;
            Console.WriteLine("Number of electrodes on board: " + len);
            string status1;
            for (int i = 0; i < len; i++) {

                Console.WriteLine("Electrode Name: " + specs.Electrodes[i].Name);
                Console.WriteLine("Id: " + specs.Electrodes[i].Id);
                Console.WriteLine("Electrode Id: " + specs.Electrodes[i].ElectrodeId);
                Console.WriteLine("Driver Id: " + specs.Electrodes[i].DriverId);


                if (specs.Electrodes[i].Shape == 0) {
                    Console.WriteLine("Electrode Shape: rectangle");
                    Console.WriteLine("Position of top left corner: (" + specs.Electrodes[i].PositionX + ", " + specs.Electrodes[i].PositionY + ")");
                    Console.Write("Electrode Size: " + specs.Electrodes[i].SizeX + " by " + specs.Electrodes[i].SizeY);

                } else {
                    Console.WriteLine("Electrode Shape: custom polygon");
                    Console.WriteLine("Electrode Origin: (" + specs.Electrodes[i].PositionX + "," + specs.Electrodes[i].PositionY + ")");
                        Console.Write("Electrode corners: ");
                    for (int j = 0; j < specs.Electrodes[i].Corners.Count; j++) {
                        Console.Write("(" + specs.Electrodes[i].Corners[j][0] + "," + specs.Electrodes[i].Corners[j][1] + ") ");
                    }
                }

                if (specs.Electrodes[i].Status == 0) { status1 = "off"; } else { status1 = "on"; }
                Console.WriteLine(Environment.NewLine + "Electrode status: " + status1);
                Console.WriteLine();
            }
        }
    }
}
