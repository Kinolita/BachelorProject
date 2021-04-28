using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using BachelorProject.Models.DmfElements;
using BachelorProject.Models;
using BachelorProject.Movement;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));

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
            string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\standard32x20.json");

            //TODO - routing is working but contamination paths are still overlapping. need to check droplet and assignment variables
            //go through and make variable names more professional and relevant
            // organize the methods a bit better
            // get rid of redundant code

            var boardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] pixelBoard1 = Pixels.Create(boardSpecs);
            Information.PrintInformation(boardSpecs);

            //note: droplets larger than 1 electrode should start away from the border
            int startElec = 33;
            int endElec = 606;

            int dropletSize = 7;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //dropletSize = 8;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            startElec = 56;
            endElec = 20;
            dropletSize = 4;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            //dropletSize = 2;
            //Pixels[,] pixelBoard2 = Pixels.ScaleDown(pixelBoard1);
            //InputHandler.CheckInputType(pixelBoard2, startElec, endElec, dropletSize);
            //BoardPrint.printBoard(pixelBoard1);
            //BoardPrint.printBoard(pixelBoard2);

            Droplet drop1 = DropletCreation(pixelBoard1, 108, "drop1", 7, true);
            Droplet drop2 = DropletCreation(pixelBoard1, 256, "drop2", 11, true);
            Droplet drop3 = DropletCreation(pixelBoard1, 422, "drop3", 25, true);
            Droplet drop4 = DropletCreation(pixelBoard1, 99, "drop4", 15, true);

            List<Droplet> dropletList = new List<Droplet> { drop1, drop4, drop2, drop3 };
            Dictionary<string, int> endingPoints = new Dictionary<string, int> {
                { drop1.Name, 620 },
                { drop2.Name, 286 },
                { drop3.Name, 315 },
                { drop4.Name, 590 }
            };

            var that = Scheduler.MovingDroplets(pixelBoard1, dropletList, endingPoints);
            Console.WriteLine("successful path:");
            foreach (var thatone in that) {
                Console.Write(thatone.Name + " ");
            }

            //Helper method to create droplets
            static Droplet DropletCreation(Pixels[,] pixelBoard, int location, string name, int size, bool contam) {
                Coord coordinates = new Coord();
                try {
                    coordinates = Coord.FindPixel(pixelBoard, location);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }

                int centerX = coordinates.X + pixelBoard[coordinates.X, coordinates.Y].XRange / 2;
                int centerY = coordinates.Y + pixelBoard[coordinates.X, coordinates.Y].YRange / 2;

                Droplet thisDroplet = new Droplet {
                    Name = name,
                    SizeY = size,
                    SizeX = size,
                    Contamination = contam,
                    PositionX = centerX,
                    PositionY = centerY
                };
                return thisDroplet;
            }
            Console.Beep();
            dropletSize = 2;

            //JSONCreation.Creator.SampleBoard("blank32x20", 32, 20, 10);
        }
    }
}