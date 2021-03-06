using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\board4x3.json");
            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);

            Create(BoardSpecs);

            ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            Input.PrintInputs(BoardSpecs);
            Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);



            ////////////////////////////////////Other Stuff////////////////////////////////////////////
            //Experimenting with shortest path algorithm
            int[,] graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                      { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                      { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                      { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                      { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                      { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                                      { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                                      { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                      { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
            Dijkstra t = new Dijkstra();
            //t.dijkstra(graph, 0);
        }

        public static void Create(Board Specs) {
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
            Console.WriteLine("One: " + PixelBoard1[45, 15].WhichElectrode); //2
            Console.WriteLine("Two: " + PixelBoard1[22, 23].WhichElectrode);//5
            Console.WriteLine("3: " + PixelBoard1[44, 55].WhichElectrode);//10
        }

        public static void Move(Pixels[,] PixelBoard, int startX, int startY, int endX, int endY) {

        }
    }
}
