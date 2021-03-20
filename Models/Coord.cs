using BachelorProject.Models;

namespace BachelorProject.Models
{
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y) {
            this.x = x;
            this.y = y;
        }

        //for now we are returning a random point inside the electrode. 
        //this should not be a problem as the buffer will be added to obstacles 
        //so it doesnt matter if the  pixel droplet is centered
        public static Coord FindPixel(Pixels[,] PixelBoard, int a) {
            Coord inside = new Coord(-1,-1);
            for (int k = 0; k < PixelBoard.GetLength(0); k++) {
                for (int j = 0; j < PixelBoard.GetLength(1); j++) {
                    if (PixelBoard[k, j].WhichElectrode == a && PixelBoard[k,j].Empty == true) {
                        inside.x = k;
                        inside.y = j;
                    }
                }
            }
            return inside;
        }
    }
}
