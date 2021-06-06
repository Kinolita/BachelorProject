using System;
using System.Collections.Generic;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    class InputHandler
    {
        //adds buffers and does routing
        public static List<Coord> RoutingPackage(Pixels[,] pixelBoard, Coord start, Coord finish, Droplet drop) {
            var finalPath = new List<Coord>();
            try {
                Buffers.RemoveBuffer(pixelBoard);
                Buffers.AddBuffer(pixelBoard, drop);
                finalPath = AStarRouting.Tile.AStar(pixelBoard, start, finish, drop);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return finalPath;
        }

        //finds a pixel in an electrode for placement
        public static Coord PlaceInElectrode(Pixels[,] pixelBoard, int electrode, Droplet drop) {
            Buffers.RemoveBuffer(pixelBoard);
            Buffers.AddBuffer(pixelBoard, drop);
            var putItHere = Coord.FindPixel(pixelBoard, electrode);
            return putItHere;
        }
    }
}
