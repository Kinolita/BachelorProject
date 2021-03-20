using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using BachelorProject.Movement;
using static BachelorProject.Movement.AStarRouting;

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
            //3x4 maze
            //Models.Coord starting = new Models.Coord(70, 5);
            //Models.Coord ending = new Models.Coord(30, 15);
            //10x10 maze
            Models.Coord starting = new Models.Coord(5, 25);
            Models.Coord ending = new Models.Coord(95, 65);


            //testing input with electrodes vs coordinates
            int startElec = 20;
            int endElec = 69;


            //the question is: should we add buffers right after we create the board?
            //what if theres different sized droplets on the same board and we are alternating movements?
            //possible to make 2 different buffer layers, one for each droplet thats interchangeable with the board?
            int dropletSize = 15;

            static bool BufferValidate(int a, int max) {
                if (a >= 0 && a < max) { return true; } else { return false; }
            }

            static void AddBuffer(Pixels[,] PixelBoard, double dropSize) {
                int Buff = (int)Math.Ceiling(dropSize / 2);
                int kMax = PixelBoard.GetLength(0);
                int jMax = PixelBoard.GetLength(1);
                for (int k = 0; k < kMax; k++) {
                    for (int j = 0; j < jMax; j++) {

                        if ((PixelBoard[k, j].Empty == false && PixelBoard[k, j].BlockageType != "Buffer") || k == 0 || j == 0 || k == kMax - 1 || j == jMax - 1) {

                            //cycling through surrounding pixels
                            for (int i = -Buff; i <= Buff; i++) {
                                for (int h = -Buff; h <= Buff; h++) {
                                    if (BufferValidate(k + i, kMax) && BufferValidate(j + h, jMax) && PixelBoard[k + i, j + h].Empty == true) {
                                        PixelBoard[k + i, j + h].BlockageType = "Buffer";
                                        PixelBoard[k + i, j + h].Empty = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }



            //printBoard(PixelBoard);
            AddBuffer(PixelBoard, dropletSize);
            printBoard(PixelBoard);




            //InputHandler.CheckInputType(PixelBoard, startElec, endElec);
            //InputHandler.CheckInputType(PixelBoard, starting, ending);
            //InputHandler.CheckInputType(PixelBoard, starting, endElec);


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



            //had to suppress this for CanWeMoveThere or it sees the one its in as not empty and wont move
            //PixelBoard[starting.x, starting.y].Empty = false;

            //Console.WriteLine("Starting point: (" + starting.x + "," + starting.y + ")");
            //Console.WriteLine("Ending point: (" + ending.x + "," + ending.y + ")");
            //BasicRouting.FindPath(PixelBoard, starting, ending);
            //Tile.AStar(PixelBoard, starting, ending);





            //CheckInput(startElec);
            //CheckInput(starting);
            //CheckInput(theStringToEndAllStrings);

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