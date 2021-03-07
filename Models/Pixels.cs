using BachelorProject.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BachelorProject.Models
{
    public class Pixels
    {
        public bool Vacancy { get; set; }
        public string BlockageType { get; set; }
        // (Contaminated, OoB, occupied, etc.)
        public int WhichElectrode { get; set; }

        public static Pixels[,] Create(Board Specs) {
            Pixels[,] PixelBoard1 = new Pixels[Specs.Information[0].SizeX, Specs.Information[0].SizeY];
            for (int k = 0; k < Specs.Information[0].SizeX; k++) {
                for (int j = 0; j < Specs.Information[0].SizeY; j++) {
                    Pixels pix = new Pixels();
                    PixelBoard1[k, j] = pix;

                    pix.Vacancy = true;
                    pix.BlockageType = "";
                    for (int m = 0; m < Specs.Electrodes.Count; m++) {
                        if (k >= Specs.Electrodes[m].PositionX &&
                           k < (Specs.Electrodes[m].PositionX + Specs.Electrodes[m].SizeX) &&
                           j >= Specs.Electrodes[m].PositionY &&
                           j < (Specs.Electrodes[m].PositionY + Specs.Electrodes[m].SizeY)) {
                            pix.WhichElectrode = Specs.Electrodes[m].ID;
                        }
                    }
                }
            }
            return PixelBoard1;
            //Console.WriteLine("One: " + PixelBoard1[45, 15].WhichElectrode); //2
            //Console.WriteLine("Two: " + PixelBoard1[22, 23].WhichElectrode);//5
            //Console.WriteLine("3: " + PixelBoard1[44, 55].WhichElectrode);//10
        }
    }
}
