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

        //returning the center most point in the electrode that is not blocked. 
        public static Coord FindPixel(Pixels[,] pixelBoard, int a) {
            var inside = new Coord(-1, -1);
            var check = "";
            //locating a pixel in that electrode
            for (var k = 0; k < pixelBoard.GetLength(0); k++) {
                for (var j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].WhichElectrode != a) continue;
                    var centerX = pixelBoard[k, j].XRange / 2 + k;
                    var centerY = pixelBoard[k, j].YRange / 2 + j;

                    //searching from the center outwards for an open spot
                    for (var s = 0; s < pixelBoard[k, j].XRange / 2; s++) {
                        var ss = centerX + s * Flip(s);
                        for (var t = 0; t < pixelBoard[k, j].YRange / 2; t++) {
                            var tt = centerY + t * Flip(t);

                            if (Pixels.ValidateOnBoard(pixelBoard, ss, tt) && pixelBoard[ss, tt].BlockageType == "Buffer") { check = "B"; }
                            if (Pixels.ValidateOnBoard(pixelBoard, ss, tt) && pixelBoard[ss, tt].BlockageType == "OoB") { check = "O"; }
                            if (!Pixels.ValidateOnBoard(pixelBoard, ss, tt) ||
                                pixelBoard[ss, tt].WhichElectrode != a) continue;
                            inside.X = ss;
                            inside.Y = tt;
                            goto LoopEnd;
                        }
                    }
                }
            }
            LoopEnd:
            return inside.X switch {
                -1 when check == "B" => throw new ElectrodeException("This electrode is blocked: " + a),
                -1 when check == "O" => throw new ElectrodeException("This electrode does not exist: " + a),
                _ => inside
            };
        }

        private static int Flip(int a) {
            if (a % 2 == 0) {
                return -1;
            }
            return 1;
        }
    }
}
