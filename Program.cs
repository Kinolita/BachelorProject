using Newtonsoft.Json;
using System;
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
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10Maze.json");
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10FatMaze.json");


            var boardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] pixelBoard1 = Pixels.Create(boardSpecs);
            Information.PrintInformation(boardSpecs);

            //combining buffer with routing droplets
            int startElec = 0;
            int endElec = 15;
            int dropletSize = 15;
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);
            dropletSize = 5;
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec, dropletSize);

            //Board.printBoard(pixelBoard1);
            

            ////////////////example of throwing an exception//////////////////
            //static void CheckInput(object that) {
            //    Type t = that.GetType();
            //    //if (!t.Equals(typeof(Coord)) && !t.Equals(typeof(int))) throw new ArgumentException("this ain't neither one");

            //    try {
            //        if (t.Equals(typeof(int))) {
            //            Console.WriteLine("this is an electrode");
            //        } else if (t.Equals(typeof(Coord))) {
            //            Console.WriteLine("this is a coord");
            //        } else {
            //            throw new ArgumentException("this ain't neither one");
            //        }
            //    } catch (ArgumentException) {
            //        Console.WriteLine("this ain't neither one");
            //    }
            //}
        }
    }
}