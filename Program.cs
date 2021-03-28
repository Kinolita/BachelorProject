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
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board4x3.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\boardWithEverything.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\mazeBoard.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10Maze.json");
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10FatMaze.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\100x100.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\50x50.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\30x30.json");




            var boardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            //Console.WriteLine("Deserialized done");
            Pixels[,] pixelBoard1 = Pixels.Create(boardSpecs);
            //Console.WriteLine("board creation done");
            Information.PrintInformation(boardSpecs);

            //combining buffer with routing droplets
            int startElec = 0;
            int endElec = 99;
            int dropletSize = 13;
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            Board.printBoard(pixelBoard1);
            dropletSize = 5;
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            //JSONCreation.Class1.SampleBoard("20x20", 20, 20, 10);
        }
    }
}