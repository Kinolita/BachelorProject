using System;
using System.Collections.Generic;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;
using BachelorProject.Printers;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        public static List<Coord> CheckInputType(Pixels[,] pixelBoard, Coord start, int finish, Droplet drop) {

            var finalPath = new List<Coord>();
            try {
                var finishCoordinate = PlaceInElectrode(pixelBoard, finish, drop);
                Buffers.RemoveBuffer(pixelBoard);
                Buffers.AddBuffer(pixelBoard, drop);
                finalPath = AStarRouting.Tile.AStar(pixelBoard, start, finishCoordinate, drop);
                //BoardPrint.PrintBoard(pixelBoard);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return finalPath;
        }

        public static Coord PlaceInElectrode(Pixels[,] pixelBoard, int electrode, Droplet drop) {
            Buffers.RemoveBuffer(pixelBoard);
            Buffers.AddBuffer(pixelBoard, drop);
            var putItHere = Coord.FindPixel(pixelBoard, electrode);
            return putItHere;
        }
    }
}
