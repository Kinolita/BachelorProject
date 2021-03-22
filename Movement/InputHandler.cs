using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        public static void CheckInputType(Pixels[,] pixelBoard, Coord start, Coord finish) {
            AStarRouting.Tile.AStar(pixelBoard, start, finish);
        }

        public static void CheckInputType(Pixels[,] pixelBoard, int start, int finish) {
            var startCoord = Coord.FindPixel(pixelBoard, start);
            var finishCoord = Coord.FindPixel(pixelBoard, finish);
            CheckInputType(pixelBoard, startCoord, finishCoord);
        }

        public static void CheckInputType(Pixels[,] pixelBoard, Coord start, int finish) {
            var finishCoord = Coord.FindPixel(pixelBoard, finish);
            CheckInputType(pixelBoard, start, finishCoord);
        }

        public static void CheckInputType(Pixels[,] pixelBoard, int start, Coord finish) {
            var startCoord = Coord.FindPixel(pixelBoard, start);
            CheckInputType(pixelBoard, startCoord, finish);
        }
    }
}
