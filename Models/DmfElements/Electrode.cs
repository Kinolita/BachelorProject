using System;
using System.Collections.Generic;

namespace BachelorProject.Models.Dtos
{
    public class Electrode
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int ElectrodeID { get; set; }
        public int DriverID { get; set; }
        public int Shape { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Status { get; set; }
        public IList<IList<int>> Corners { get; set; }

        public static void PrintElectrodes(BachelorProject.Models.Dtos.Board Specs) {
            int len = Specs.Electrodes.Count;
            System.Console.WriteLine("Number of electrodes on board: " + len);
            string status1;
            for (int i = 0; i < len; i++) {

                System.Console.WriteLine("Electrode Name: " + Specs.Electrodes[i].Name);
                System.Console.WriteLine("ID: " + Specs.Electrodes[i].ID);
                System.Console.WriteLine("Electrode ID: " + Specs.Electrodes[i].ElectrodeID);
                System.Console.WriteLine("Driver ID: " + Specs.Electrodes[i].DriverID);


                if (Specs.Electrodes[i].Shape == 0) {
                    System.Console.WriteLine("Electrode Shape: rectangle");
                    System.Console.WriteLine("Position of top left corner: (" + Specs.Electrodes[i].PositionX + ", " + Specs.Electrodes[i].PositionY + ")");
                    System.Console.Write("Electrode Size: " + Specs.Electrodes[i].SizeX + " by " + Specs.Electrodes[i].SizeY);

                } else {
                    System.Console.WriteLine("Electrode Shape: custom polygon");
                    System.Console.WriteLine("Electrode Origin: (" + Specs.Electrodes[i].PositionX + "," + Specs.Electrodes[i].PositionY + ")");
                        System.Console.Write("Electrode corners: ");
                    for (int j=0; j< Specs.Electrodes[i].Corners.Count; j++) {
                        System.Console.Write("(" + Specs.Electrodes[i].Corners[j][0] + "," + Specs.Electrodes[i].Corners[j][1] + ") ");
                    }
                };

                if (Specs.Electrodes[i].Status == 0) { status1 = "off"; } else { status1 = "on"; };
                System.Console.WriteLine(Environment.NewLine + "Electrode status: " + status1);
                System.Console.WriteLine();
            }
        }
    }
}
