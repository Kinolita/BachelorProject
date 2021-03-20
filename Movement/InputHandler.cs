using System;
using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        //public static void CheckInputType(Pixels[,] PixelBoard, object start, object finish) {
        //    Type s = start.GetType();
        //    Type t = finish.GetType();
        //    if (s.Equals(typeof(int))) {
        //        start = Coord.FindPixel(PixelBoard, (int)start);
        //    }
        //    if (t.Equals(typeof(int))) {
        //        finish = Coord.FindPixel(PixelBoard, (int)finish);
        //    }
        //    AStarRouting.Tile.AStar(PixelBoard, (Coord)start, (Coord)finish);
        //}

        public static void CheckInputType(Pixels[,] PixelBoard, Coord start, Coord finish) {
            AStarRouting.Tile.AStar(PixelBoard, start, finish);
        }

        public static void CheckInputType(Pixels[,] PixelBoard, int start, int finish) {
            var startCoord = Coord.FindPixel(PixelBoard, start);
            var finishCoord = Coord.FindPixel(PixelBoard, finish);
            CheckInputType(PixelBoard, startCoord, finishCoord);
        }

        public static void CheckInputType(Pixels[,] PixelBoard, Coord start, int finish) {
            var finishCoord = Coord.FindPixel(PixelBoard, finish);
            CheckInputType(PixelBoard, start, finishCoord);
        }

        public static void CheckInputType(Pixels[,] PixelBoard, int start, Coord finish) {
            var startCoord = Coord.FindPixel(PixelBoard, start);
            CheckInputType(PixelBoard, startCoord, finish);
        }
    }
}
