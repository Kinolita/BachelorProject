using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using System.Drawing;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\boardWithEverything.json");
            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);

            ////Print functions for each element of the loaded board
            //Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            //Input.PrintInputs(BoardSpecs);
            //Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);



            ////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("****************************************************");
            // experiment setting the initial board size as a matrix
            // would this Point data type be useful???
            string[,] ElectrodeArray = new string[10, 6];
            string drop1 = "Droplet 1";
            Point p1 = new Point(2, 3);
            ElectrodeArray[p1.X, p1.Y] = drop1;

            // Testing move
            Point p2 = new Point(2, 2);
            Point p3 = new Point(2, 13);
            Point p4 = new Point(-2, 3);
            Point p5 = new Point(2, 6);
            ScrapCode.Move(p1, p2);
            ScrapCode.Move(p1, p5);
            ScrapCode.Move(p4, p2);


            //Experimenting with shortest path algorithm
            /* Let us create the example graph discussed above */
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
            t.dijkstra(graph, 0);
        }
    }
}
