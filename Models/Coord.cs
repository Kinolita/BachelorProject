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
        //changing this to pick the center point of the electrode
        public static Coord FindPixel(Pixels[,] pixelBoard, int a) {
            Coord inside = new Coord(-1, -1);
            string check = "";
            //locating a pixel in that electrode
            for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].WhichElectrode == a) {
                        var centerX = pixelBoard[k, j].XRange / 2 + k;
                        var centerY = pixelBoard[k, j].YRange / 2 + j;

                        //searching from the center outwards for an open spot
                        for (var s = 0; s < pixelBoard[k, j].XRange / 2; s++) {
                            var ss = centerX + s * StupidFunctionCauseICouldntFindAnyOtherWay(s);
                            for (var t = 0; t < pixelBoard[k, j].YRange / 2; t++) {
                                var tt = centerY + t * StupidFunctionCauseICouldntFindAnyOtherWay(t);

                                if (Pixels.ValidateOnBoard(pixelBoard, ss, tt) && pixelBoard[ss, tt].BlockageType == "Buffer") { check = "B"; }
                                if (Pixels.ValidateOnBoard(pixelBoard, ss, tt) && pixelBoard[ss, tt].BlockageType == "OoB") { check = "O"; }

                                if (Pixels.ValidateOnBoard(pixelBoard, ss, tt) && pixelBoard[ss, tt].WhichElectrode == a) {
                                    inside.X = ss;
                                    inside.Y = tt;
                                    goto LoopEnd;
                                }
                            }
                        }
                    }
                }
            }
            LoopEnd:
            //BoardPrint.PrintBoard(pixelBoard);
            if (inside.X == -1 && check == "B") throw new ElectrodeException("This electrode is blocked: " + a);
            //if (inside.X == -1) throw new ElectrodeException("This electrode does not exist: " + a);
            if (inside.X == -1 && check == "O") throw new ElectrodeException("This electrode does not exist: " + a);

            return inside;
        }

        public static int StupidFunctionCauseICouldntFindAnyOtherWay(int a) {
            if (a % 2 == 0) {
                return -1;
            }
            return 1;
        }
    }
}
