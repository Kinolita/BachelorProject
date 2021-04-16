using System;
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
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\board10x10FatMaze.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\100x100.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\50x50.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\30x30.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\20x20.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\10x10.json");
            //string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\4x3.json");
            string theStringToEndAllStrings = File.ReadAllText(path + @"\JSONBoards\32x20_mini_maze.json");


            var boardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] pixelBoard1 = Pixels.Create(boardSpecs);
            Information.PrintInformation(boardSpecs);

            //combining buffer with routing droplets
            //node: droplets larger than 1 electrode should start away from the border
            int startElec = 33;
            int endElec = 606;
            int dropletSize = 16;
            Console.WriteLine("Droplet size: " + dropletSize);
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);
            //dropletSize = 10;
            //Console.WriteLine("Droplet size: " + dropletSize);
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);
            //dropletSize = 11;
            //Console.WriteLine("Droplet size: " + dropletSize);
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);

            //dropletSize = 20;
            //Console.WriteLine("Droplet size: " + dropletSize);
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);
            //dropletSize = 21;
            //Console.WriteLine("Droplet size: " + dropletSize);
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);
            //dropletSize = 22;
            //Console.WriteLine("Droplet size: " + dropletSize);
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);


            //JSONCreation.Class1.SampleBoard("whatisthis", 4, 5, 2);
        }
    }
}