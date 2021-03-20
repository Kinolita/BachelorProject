using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using BachelorProject.Movement;

namespace BachelorProject
{
    class Program
    {
        static void Main(string[] args) {
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board4x3.json");
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\boardWithEverything.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\mazeBoard.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10Maze.json");
            //string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\JSONBoards\board10x10FatMaze.json");


            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);
            Pixels[,] PixelBoard = Pixels.Create(BoardSpecs);

            ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
     
            //A* routing on 3x4 maze
            //Models.Coord starting = new Models.Coord(70, 5);
            //Models.Coord ending = new Models.Coord(30, 15);
            //10x10 maze
            Models.Coord starting = new Models.Coord(5, 25);
            Models.Coord ending = new Models.Coord(95, 65);


            //testing input with electrodes vs coordinates
            int startElec = 4;
            int endElec = 3;
            int dropletSize = 15;
            Buffers.AddBuffer(PixelBoard, dropletSize);
            InputHandler.CheckInputType(PixelBoard, startElec, endElec);
            //Console.WriteLine("Starting point: (" + starting.x + "," + starting.y + ")");
            //Console.WriteLine("Ending point: (" + ending.x + "," + ending.y + ")");
            
            //printBoard(PixelBoard);
    
            void printBoard(Pixels[,] PixelBoard) {
                Console.WriteLine("Printing the board:");
                for (int j = 0; j < PixelBoard.GetLength(1); j++) {
                    Console.Write("\n");
                    for (int k = 0; k < PixelBoard.GetLength(0); k++) {
                        if (PixelBoard[k, j].Empty == false && PixelBoard[k, j].BlockageType == "Buffer") {
                            Console.Write("B");
                        } else if (PixelBoard[k, j].Empty == false) {
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