using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using BachelorProject.Models.DmfElements;
using BachelorProject.Models;
using BachelorProject.Movement;
using BachelorProject.Printers;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));

            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\boardWithEverything.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\mazeBoard4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\board10x10Maze.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\50x50.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\30x30.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\20x20.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\10x10.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\100x100.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\board10x10FatMaze.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\32x20_mini_maze.json");
            var jsonString = File.ReadAllText(path + @"\JSONBoards\standard32x20.json");

            var boardSpecs = JsonConvert.DeserializeObject<Board>(jsonString);
            var pixelBoard1 = Pixels.Create(boardSpecs);
            Information.PrintInformation(boardSpecs);

            //note: droplets larger than 1 electrode should be placed in the center or top left of their range
            //int startElectrode = 33;
            //int endElectrode = 606;

            //int dropletSize = 7;
            //InputHandler.RoutingPackage(pixelBoard1, startElectrode, endElectrode, dropletSize);
            //dropletSize = 8;
            //InputHandler.RoutingPackage(pixelBoard1, startElectrode, endElectrode, dropletSize);

            //startElectrode = 56;
            //endElectrode = 20;
            //dropletSize = 4;
            //InputHandler.RoutingPackage(pixelBoard1, startElectrode, endElectrode, dropletSize);

            //dropletSize = 2;
            //Pixels[,] pixelBoard2 = Pixels.ScaleDown(pixelBoard1);
            //InputHandler.RoutingPackage(pixelBoard2, startElectrode, endElectrode, dropletSize);
            //BoardPrint.PrintBoard(pixelBoard1);
            //BoardPrint.PrintBoard(pixelBoard2);

            var drop1 = DropletCreation(pixelBoard1, 108, "drop1", 001, 7, false);
            var drop2 = DropletCreation(pixelBoard1, 256, "drop2", 002,11, true);
            var drop3 = DropletCreation(pixelBoard1, 422, "drop3", 003,25, true);
            var drop4 = DropletCreation(pixelBoard1, 99, "drop4", 004,15, true);

            var dropletList = new List<Droplet> { drop1, drop2, drop3, drop4 };
            var endingPoints = new Dictionary<int, int> {
                { drop1.Id, 620 },
                { drop2.Id, 286 },
                { drop3.Id, 315 },
                { drop4.Id, 560 }
            };

            try {
                var output = Scheduler.MovingDroplets(pixelBoard1, dropletList, endingPoints);
                CollisionPrint.PrintCollisions(output);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            //Helper method to create droplets
            static Droplet DropletCreation(Pixels[,] pixelBoard, int location, string name, int id, int size, bool isContaminating) {
                var coordinates = new Coord();
                try {
                    coordinates = Coord.FindPixel(pixelBoard, location);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
                var thisDroplet = new Droplet {
                    Name = name,
                    Id = id,
                    SizeY = size,
                    SizeX = size,
                    Contamination = isContaminating,
                    PositionX = coordinates.X,
                    PositionY = coordinates.Y
                };
                return thisDroplet;
            }

            //Method to create json files for custom board sizes
            //JSONCreation.Creator.SampleBoard("blank32x20", 32, 20, 10);
            Console.Beep();
        }
    }
}