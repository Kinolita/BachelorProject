using System;
using System.Collections.Generic;
using System.Linq;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;
using BachelorProject.Printers;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        public static List<Coord> CheckInputType(ref Pixels[,] pixelBoard, Coord start, Coord finish, Droplet drop) {
            bool scaled;
            int dropletSize = Droplet.MaxSize(drop);
            Buffers.RemoveBuffer(pixelBoard);
            //pixelBoard = Pixels.ScaleDown(pixelBoard, out scaled);
            //TODO
            //if (scaled) {
            //    dropletSize /= 2;
            //    start.X /= 2;
            //    start.Y /= 2;
            //    finish.X /= 2;
            //    finish.Y /= 2;
            //}

            Buffers.AddBuffer(pixelBoard, drop);
            var that = AStarRouting.Tile.AStar(pixelBoard, start, finish, drop);

            if (that.Any()) {
                var thatone = that.Count - 1;
                Blockages.DropletSet(pixelBoard, that[thatone], drop);
            }
            return that;
        }

        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, int start, int finish, Droplet drop) {
            int dropletSize = Droplet.MaxSize(drop);
            var startCoord = PlaceInElectrode(pixelBoard, start, drop);
            var finishCoord = PlaceInElectrode(pixelBoard, finish, drop);
            List<Coord> finalPath = CheckInputType(ref pixelBoard, startCoord, finishCoord, drop);
            return finalPath;
        }

        public static List<Coord> CheckInputType(ref Pixels[,] pixelBoard, Coord start, int finish, Droplet drop) {

            var finalPath = new List<Coord>();
            try {
                var finishCoord = PlaceInElectrode(pixelBoard, finish, drop);
                finalPath = CheckInputType(ref pixelBoard, start, finishCoord, drop);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return finalPath;
        }

        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, int start, Coord finish, Droplet drop) {
            int dropletSize = Droplet.MaxSize(drop);
            var startCoord = PlaceInElectrode(pixelBoard, start, drop);
            List<Coord> finalPath = CheckInputType(ref pixelBoard, startCoord, finish, drop);
            return finalPath;
        }

        public static Coord PlaceInElectrode(Pixels[,] pixelBoard, int elec, Droplet drop) {
            Buffers.RemoveBuffer(pixelBoard);
            Buffers.AddBuffer(pixelBoard, drop);
            var putItHere = new Coord();
            putItHere = Coord.FindPixel(pixelBoard, elec);
            return putItHere;
        }
    }
}
