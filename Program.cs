using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using BachelorProject.Scraps;
using BachelorProject.Movement;
using static BachelorProject.Scraps.PolygonPoints;
using System.Collections.Generic;
using static BachelorProject.Movement.AStarRouting;
using System.Linq;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\boardWithEverything.json");
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\mazeBoard.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10.json");


            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] PixelBoard = Pixels.Create(BoardSpecs);

                       ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            //Input.PrintInputs(BoardSpecs);
            //Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);


            //Dummy navigation
            //Models.Coord starting = new Models.Coord(10, 15);
            //Models.Coord ending = new Models.Coord(70, 55);

            //testing custom polygon electrodes
            //Models.Coord starting = new Models.Coord(70, 58);
            //Models.Coord ending = new Models.Coord(10, 15);

            //testing navigation with droplet/bubble detection
            //Models.Coord starting = new Models.Coord(23, 10);
            //Models.Coord ending = new Models.Coord(70, 55);
            //Models.Coord starting = new Models.Coord(50, 33);
            //Models.Coord ending = new Models.Coord(23, 10);

            //A* demo (work in progress)
            Models.Coord starting = new Models.Coord(70, 5);
            Models.Coord ending = new Models.Coord(30, 15);


            PixelBoard[starting.x, starting.y].Vacancy = false;
            Console.WriteLine("Starting point: (" + starting.x + "," + starting.y + ")");
            Console.WriteLine("Ending point: (" + ending.x + "," + ending.y + ")");
            BasicRouting.FindPath(PixelBoard, starting, ending);


            /////////////////A* Routing////////////////////
            //C# code found at:
            //https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/

            var start = new Tile();
            start.X = starting.x;
            start.Y = starting.y;

            var finish = new Tile();
            finish.X = ending.x;
            finish.Y = ending.y;

            Tile.AStar(PixelBoard, start, finish);
        }
    }
}
