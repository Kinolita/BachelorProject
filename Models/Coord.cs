using System;
using BachelorProject.Exceptions;

namespace BachelorProject.Models
{
    public struct Coord
    {
        public int X;
        public int Y;

        public Coord(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        //we are returning the top and leftmost point in the electrode that is not blocked. 
        public static Coord FindPixel(Pixels[,] pixelBoard, int a) {
            Coord inside = new Coord(-1, -1);
            string check = "";
            for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {

                    if (pixelBoard[k, j].WhichElectrode == a && pixelBoard[k, j].BlockageType == "Buffer") { check = "B"; }
                    if (pixelBoard[k, j].WhichElectrode == a && pixelBoard[k, j].Empty) {
                        inside.X = k;
                        inside.Y = j;
                        goto LoopEnd;
                    }
                }
            }
            LoopEnd:
            if (inside.X == -1 && check == "B") throw new ElectrodeException("This electrode is blocked: " + a);
            if (inside.X == -1) throw new ElectrodeException("This electrode does not exist: " + a);
            return inside;
        }
    }
}
