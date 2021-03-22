
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

        //for now we are returning a random point inside the electrode. 
        //this should not be a problem as the buffer will be added to obstacles 
        //so it doesn't matter if the  pixel droplet is centered
        public static Coord FindPixel(Pixels[,] pixelBoard, int a) {
            Coord inside = new Coord(-1,-1);
            for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].WhichElectrode == a && pixelBoard[k,j].Empty) {
                        inside.X = k;
                        inside.Y = j;
                    }
                }
            }
            return inside;
        }
    }
}
