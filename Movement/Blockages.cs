using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class Blockages
    {
        // implemented using .XRange and .YRange in Pixels.cs
        // checks the whole electrode of a target pixel to see if there is droplets or bubbles
        public static bool DropletBubbleCheck(Pixels[,] pixelBoard, Coord wantToGo) {
            bool result = true;     //true if the electrode is empty
            var electrodeNumber = pixelBoard[wantToGo.X, wantToGo.Y].WhichElectrode;

            int a = wantToGo.X - pixelBoard[wantToGo.X, wantToGo.Y].XRange;
            if (a < 0) a = 0;
            int b = wantToGo.X + pixelBoard[wantToGo.X, wantToGo.Y].XRange;
            if (b > pixelBoard.GetLength(0)) b = pixelBoard.GetLength(0);
            int c = wantToGo.Y - pixelBoard[wantToGo.X, wantToGo.Y].YRange;
            if (c < 0) c = 0;
            int d = wantToGo.Y + pixelBoard[wantToGo.X, wantToGo.Y].YRange;
            if (d > pixelBoard.GetLength(1)) d = pixelBoard.GetLength(1);

            for (int k = a; k < b; k++) {
                for (int j = c; j < d; j++) {
                    //if in target electrode and not empty and droplet or bubble
                    if (pixelBoard[k, j].WhichElectrode == electrodeNumber && !pixelBoard[k, j].Empty &&
                        (pixelBoard[k, j].BlockageType == "Droplet" || pixelBoard[k, j].BlockageType == "Bubble")) {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool BlockageCheck(Pixels[,] pixelBoard, Coord wantToGo) {
            bool result = true;             //true if electrode is empty
            var electrodeNumber = pixelBoard[wantToGo.X, wantToGo.Y].WhichElectrode;

            int a = wantToGo.X - pixelBoard[wantToGo.X, wantToGo.Y].XRange;
            if (a < 0) a = 0;
            int b = wantToGo.X + pixelBoard[wantToGo.X, wantToGo.Y].XRange;
            if (b > pixelBoard.GetLength(0)) b = pixelBoard.GetLength(0);
            int c = wantToGo.Y - pixelBoard[wantToGo.X, wantToGo.Y].YRange;
            if (c < 0) c = 0;
            int d = wantToGo.Y + pixelBoard[wantToGo.X, wantToGo.Y].YRange;
            if (d > pixelBoard.GetLength(1)) d = pixelBoard.GetLength(1);

            for (int k = a; k < b; k++) {
                for (int j = c; j < d; j++) {
                    //if in target electrode and empty except for buffers
                    if (pixelBoard[k, j].WhichElectrode == electrodeNumber &&
                        !pixelBoard[k, j].Empty && pixelBoard[k,j].BlockageType != "Buffer") {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
