using Newtonsoft.Json;
using System;
using System.IO;
using BachelorProject.Models.Dtos;
using BachelorProject.Models;
using System.Collections.Generic;

namespace BachelorProject
{
    class Program {
        static void Main(string[] args) {
            string theStringToEndAllStrings = File.ReadAllText(Directory.GetCurrentDirectory() + @"\board4x3.json");
            var BoardSpecs = JsonConvert.DeserializeObject<Board>(theStringToEndAllStrings);

            Pixels.Create(BoardSpecs);
            Coord starting = new Coord(74, 52);
            Coord ending = new Coord(3, 7);
            List<Coord> PixelList = new List<Coord>();

            Move(Pixels.Create(BoardSpecs), starting, ending, PixelList);

            ////Print functions for each element of the loaded board
            Information.PrintInformation(BoardSpecs);
            //Electrode.PrintElectrodes(BoardSpecs);
            //Actuator.PrintActuators(BoardSpecs);
            //Sensor.PrintSensors(BoardSpecs);
            Input.PrintInputs(BoardSpecs);
            Output.PrintOutputs(BoardSpecs);
            //Droplet.PrintDroplets(BoardSpecs);
            //Bubble.PrintBubbles(BoardSpecs);
        }

        public static void FindElectrodes(Pixels[,] PixelBoard, List<Coord> listy) {
            HashSet<int> EList = new HashSet<int>();
            for (int i=0; i<listy.Count; i++) {
                EList.Add(PixelBoard[listy[i].x, listy[i].y].WhichElectrode);
            }
            Console.WriteLine("The electrode path: ");
            foreach (int that in EList) {
                Console.WriteLine(that);
            }
        }

        public static void Move(Pixels[,] PixelBoard, Coord start, Coord end, List<Coord> PixelList) {
            if (start.x == end.x && start.y == end.y) {
                Console.WriteLine("Goal found!");
                FindElectrodes(PixelBoard, PixelList);
            } else if (!Validate(start) || !Validate(end)) {
                Console.WriteLine("One of the points is not on the board: (" + 
                    start.x + "," + start.y + ") (" + 
                    end.x + "," + end.y + ")");
            } else if (start.x < end.x) {
                start.x += 1;
                PixelList.Add(start);
                Move(PixelBoard, start, end, PixelList);
            } else if (start.x > end.x) {
                start.x -= 1;
                PixelList.Add(start);
                Move(PixelBoard, start, end, PixelList);
            } else if (start.y < end.y) {
                start.y += 1;
                PixelList.Add(start);
                Move(PixelBoard, start, end, PixelList);
            } else if (start.y > end.y) {
                start.y -= 1;
                PixelList.Add(start);
                Move(PixelBoard, start, end, PixelList);
            }
        }

        public static bool Validate(Coord check) {
            //need to pass in the actuall board size here
            if (check.x < 80 && check.y < 60 && check.x >= 0 && check.y >= 0) {
                return true;
            }
            return false;
        }
    }
}
