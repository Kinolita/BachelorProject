using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using BachelorProject.Scraps;
using BachelorProject.Movement;
using static BachelorProject.Scraps.PolygonPoints;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\board4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\boardWithEverything.json");
            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] PixelBoard = Pixels.Create(BoardSpecs);

            //have not yet set initial vacancy for start position
            Models.Coord starting = new Models.Coord(62, 58);
            PixelBoard[starting.x, starting.y].Vacancy = false;
            Models.Coord ending = new Models.Coord(22, 18);

            //finding something on the board
            //should i search through pixels to find one with the right label?
            //should i expand the pixelboard to basically carry all the information from the json file?
            //(Droplet presence, Sensor presence, Input/Output presence))

            PixelMapping.FindPath(PixelBoard, starting, ending);

            ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            //Input.PrintInputs(BoardSpecs);
            //Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);

            /////////////////Polygon Point finder////////////////////
            Coord[] polygon1 = {new Coord(0, 0), new Coord(10, 0), new Coord(10, 10), new Coord(0, 10)};
            int n = polygon1.Length;
            Coord p = new Coord(20, 20);
            if (PolygonPoints.IsInside(polygon1, n, p)) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("No");
            }
            p = new Coord(5, 5);
            if (PolygonPoints.IsInside(polygon1, n, p)) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("No");
            }
            Coord[] polygon2 = {new Coord(0, 0), new Coord(5, 5), new Coord(5, 0)};
            p = new Coord(3, 3);
            n = polygon2.Length;
            if (PolygonPoints.IsInside(polygon2, n, p)) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("No");
            }
            p = new Coord(5, 1);
            if (PolygonPoints.IsInside(polygon2, n, p)) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("No");
            }
            p = new Coord(8, 1);
            if (PolygonPoints.IsInside(polygon2, n, p)) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("No");
            }
            Coord[] polygon3 = {new Coord(0, 0), new Coord(10, 0), new Coord(10, 10), new Coord(0, 10)};
            p = new Coord(-1, 10);
            n = polygon3.Length;
            if (PolygonPoints.IsInside(polygon3, n, p)) {
                Console.WriteLine("Yes");
            } else {
                Console.WriteLine("No");
            }
        }
    }
}
