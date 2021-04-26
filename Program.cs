using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using BachelorProject.Models.DmfElements;
using BachelorProject.Models;
using BachelorProject.Movement;
using BachelorProject.Printers;

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
            string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\10x10.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\100x100.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\board10x10FatMaze.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\32x20_mini_maze.json");


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
            //dropletSize = 14;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize); 
            //dropletSize = 15;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //dropletSize = 16;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //dropletSize = 21;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            startElec = 56;
            endElec = 20;
            dropletSize = 4;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            //dropletSize = 2;
            //Pixels[,] pixelBoard2 = Pixels.ScaleDown(pixelBoard1);
            //InputHandler.CheckInputType(pixelBoard2, startElec, endElec, dropletSize);

            //dropletSize = 2;
            //BoardPrint.printBoard(pixelBoard1);
            //BoardPrint.printBoard(pixelBoard2);

            Droplet drop1 = DropletCreation(pixelBoard1, 5, "drop1", 2, true);
            Droplet drop2 = DropletCreation(pixelBoard1, 39, "drop2", 2, false);
            Droplet drop3 = DropletCreation(pixelBoard1, 62, "drop3", 2, true);
            Droplet drop4 = DropletCreation(pixelBoard1, 11, "drop4", 2, true);
            Console.WriteLine(drop1.PositionX + "," + drop1.PositionY + " = " + pixelBoard1[drop1.PositionX, drop1.PositionY].WhichElectrode);
            Console.WriteLine(drop2.PositionX + "," + drop2.PositionY + " = " + pixelBoard1[drop2.PositionX, drop2.PositionY].WhichElectrode);
            Console.WriteLine(drop3.PositionX + "," + drop3.PositionY + " = " + pixelBoard1[drop3.PositionX, drop3.PositionY].WhichElectrode);

            List<Droplet> dropletList = new List<Droplet> { drop1, drop4, drop2, drop3 };
            Dictionary<string, int> endingPoints = new Dictionary<string, int> {
                { drop1.Name, 95 },
                { drop2.Name, 30 },
                { drop3.Name, 59 },
                { drop4.Name, 86 }
            };

            Scheduler.MovingDroplets(pixelBoard1, dropletList, endingPoints);

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

            //JSONCreation.Creator.SampleBoard("whatisthis", 4, 5, 2);
        }
    }
}