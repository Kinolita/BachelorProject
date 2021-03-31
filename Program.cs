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
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\boardWithEverything.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\mazeBoard4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10Maze.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10FatMaze.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\100x100.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\50x50.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\30x30.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\20x20.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\10x10.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\4x3.json");
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\standard32x20.json");

            var boardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            //Console.WriteLine("Deserialized done");
            Pixels[,] pixelBoard1 = Pixels.Create(boardSpecs);
            //Console.WriteLine("board creation done");
            Information.PrintInformation(boardSpecs);

            //combining buffer with routing droplets
            int startElec = 0;
            int endElec = 639;
            int dropletSize = 4;
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            //Board.printBoard(pixelBoard1);
            dropletSize = 5;
            //InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            //JSONCreation.Class1.SampleBoard("check32x20", 32, 20, 10);
        }
    }
}