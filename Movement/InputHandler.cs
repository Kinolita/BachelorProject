using System;
using System.Collections.Generic;
using BachelorProject.Models;
using BachelorProject.Printers;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, Coord start, Coord finish, int dropletSize) {
            Buffers.RemoveBuffer(pixelBoard);
            Pixels.ScaleDown(pixelBoard);
            Buffers.AddBuffer(pixelBoard, dropletSize);
            List<Coord> that = AStarRouting.Tile.AStar(pixelBoard, start, finish);
            //List<SortedSet<int>> finalPath = PixelElectrodeConversion.FindElectrodes(pixelBoard, that, dropletSize);
            //PathPrint.FinalPrint(finalPath);
            return that;
        }

        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, int start, int finish, int dropletSize) {
            var startCoord = PlaceInElectrode(pixelBoard, start, dropletSize);
            var finishCoord = PlaceInElectrode(pixelBoard, finish, dropletSize);
            List<Coord> finalPath = CheckInputType(pixelBoard, startCoord, finishCoord, dropletSize);
            return finalPath;
        }

        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, Coord start, int finish, int dropletSize) {
            var finishCoord = PlaceInElectrode(pixelBoard, finish, dropletSize);
            List<Coord> finalPath = CheckInputType(pixelBoard, start, finishCoord, dropletSize);
            return finalPath;

        }

        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, int start, Coord finish, int dropletSize) {
            var startCoord = PlaceInElectrode(pixelBoard, start, dropletSize);
            List<Coord> finalPath = CheckInputType(pixelBoard, startCoord, finish, dropletSize);
            return finalPath;
        }

        public static Coord PlaceInElectrode(Pixels[,] pixelBoard, int elec, int dropletSize) {
            Buffers.RemoveBuffer(pixelBoard);
            Buffers.AddBuffer(pixelBoard, dropletSize);
            Coord putItHere = new Coord();
            try {
                putItHere = Coord.FindPixel(pixelBoard, elec);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
            return putItHere;
        }
    }
}
