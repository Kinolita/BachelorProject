using BachelorProject.Models;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    class Blockages
    {
        // implemented using .XRange and .YRange in Pixels.cs
        // checks the whole electrode of a target pixel to see if there is anything besides current droplet or buffers
        public static bool DropletBubbleCheck(Pixels[,] pixelBoard, Coord wantToGo, string name) {
            bool result = true; //true if the electrode is empty
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
                    //if in target electrode and if not empty but also not name or buffer
                    if (pixelBoard[k, j].WhichElectrode == electrodeNumber &&
                        (!pixelBoard[k, j].Empty &&
                         (pixelBoard[k, j].BlockageType != name || pixelBoard[k, j].BlockageType != "Buffer"))){
                        result = false;
                    }
                }
            }

            return result;
        }

        public static bool BlockageCheck(Pixels[,] pixelBoard, Coord wantToGo) {
            bool result = true; //true if electrode is empty
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
                        !pixelBoard[k, j].Empty && pixelBoard[k, j].BlockageType != "Buffer") {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static void DropletSet(Pixels[,] pixelBoard, Coord destination, Droplet drop) {
            int size = Droplet.MaxSize(drop);
            if (size % 2 == 0) {
                for (var k = destination.X - size / 2; k < destination.X + size / 2; k++) {
                    for (var j = destination.Y - size / 2; j < destination.Y + size / 2; j++) {
                        if (Pixels.ValidateOnBoard(pixelBoard, k, j)) {
                            pixelBoard[k, j].Empty = false;
                            pixelBoard[k, j].BlockageType = drop.Name;
                        }
                    }
                }
            } else {
                for (var k = destination.X - size / 2; k < destination.X + size / 2 + 1; k++) {
                    for (var j = destination.Y - size / 2; j < destination.Y + size / 2 + 1; j++) {
                        if (Pixels.ValidateOnBoard(pixelBoard, k, j)) {
                            pixelBoard[k, j].Empty = false;
                            pixelBoard[k, j].BlockageType = drop.Name;
                        }
                    }
                }
            }
        }
    }
}
