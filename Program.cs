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

            ////Print functions for each element of the loaded board
            Information.PrintInformation(boardSpecs);

            //combining buffer with routing droplets
            int startElec = 0;
            int endElec = 99;
            int dropletSize = 15;
            Buffers.AddBuffer(pixelBoard1, dropletSize);
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec);
            Buffers.RemoveBuffer(pixelBoard1);
            dropletSize = 5;
            Buffers.AddBuffer(pixelBoard1, dropletSize);
            InputHandler.CheckInputType(pixelBoard1, startElec, endElec);



            printBoard(pixelBoard1);

            void printBoard(Pixels[,] pixelBoard) {
                Console.WriteLine("Printing the board:");
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                    Console.Write("\n");
                    for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                        if (pixelBoard[k, j].Empty == false && pixelBoard[k, j].BlockageType == "Buffer") {
                            Console.Write("B");
                        } else if (pixelBoard[k, j].Empty == false) {
                            Console.Write("X");
                        } else { Console.Write("o"); }
                    }
                }
            }

            ////////////////example of throwing an exception//////////////////
            //static void CheckInput(object that) {
            //    Type t = that.GetType();
            //    //if (!t.Equals(typeof(Coord)) && !t.Equals(typeof(int))) throw new ArgumentException("this aint neither one");

            //    try {
            //        if (t.Equals(typeof(int))) {
            //            Console.WriteLine("this is an electrode");
            //        } else if (t.Equals(typeof(Coord))) {
            //            Console.WriteLine("this is a coord");
            //        } else {
            //            throw new ArgumentException("this aint neither one");
            //        }
            //    } catch (ArgumentException) {
            //        Console.WriteLine("this aint neither one");
            //    }
            //}
        }
    }
}