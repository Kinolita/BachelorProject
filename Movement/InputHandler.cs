using System;
using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        public static void CheckInputType(Pixels[,] pixelBoard, Coord start, Coord finish, int dropletSize) {
            Buffers.RemoveBuffer(pixelBoard);
            Buffers.AddBuffer(pixelBoard, dropletSize);
            AStarRouting.Tile.AStar(pixelBoard, start, finish);
        }

        public static void CheckInputType(Pixels[,] pixelBoard, int start, int finish, int dropletSize) {
            try {
                Buffers.RemoveBuffer(pixelBoard);
                Buffers.AddBuffer(pixelBoard, dropletSize);
                var startCoord = Coord.FindPixel(pixelBoard, start);
                var finishCoord = Coord.FindPixel(pixelBoard, finish);
                AStarRouting.Tile.AStar(pixelBoard, startCoord, finishCoord);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        public static void CheckInputType(Pixels[,] pixelBoard, Coord start, int finish, int dropletSize) {
            try {
                Buffers.RemoveBuffer(pixelBoard);
                Buffers.AddBuffer(pixelBoard, dropletSize);
                var finishCoord = Coord.FindPixel(pixelBoard, finish);
                CheckInputType(pixelBoard, start, finishCoord, dropletSize);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        public static void CheckInputType(Pixels[,] pixelBoard, int start, Coord finish, int dropletSize) {
            try {
                Buffers.RemoveBuffer(pixelBoard);
                Buffers.AddBuffer(pixelBoard, dropletSize);
                var startCoord = Coord.FindPixel(pixelBoard, start);
                CheckInputType(pixelBoard, startCoord, finish, dropletSize);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }


        public static void SizeHandler(Pixels[,] pixelBoard, Coord start, int finish, int dropletSize) {
            if (dropletSize > pixelBoard[start.X, start.Y].XRange || dropletSize > pixelBoard[start.X, start.Y].YRange) {

            }
        }
    }
}
