using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class Blockages
    {
        //this slows down the program quite a bit as it has to search every pixel on the board every time it wants to move 1 pixel
        //suggestion for later:
        //search by max electrode size on either side of the current 

        // implemented using .XRange and .YRange in Pixels.cs
        public static bool DropletBubbleCheck(Pixels[,] pixelBoard, Coord wantToGo) {
            bool result = true;     //true if the electrode is empty
            var electrodeNumber = pixelBoard[wantToGo.X, wantToGo.Y].WhichElectrode;

            ///////////////////////New Code////////////////////////////
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
                    if (pixelBoard[k, j].WhichElectrode == electrodeNumber && pixelBoard[k, j].Empty == false &&
                        (pixelBoard[k, j].BlockageType == "Droplet" || pixelBoard[k, j].BlockageType == "Bubble")) {
                        result = false;
                    }
                }
            }

            //////////////////////Old code that's slow and checks the entire board//////////////
            //for (int k = 0; k < pixelBoard.GetLength(0); k++) {
            //    for (int j = 0; j < pixelBoard.GetLength(1); j++) {
            //        if (pixelBoard[k, j].WhichElectrode == electrodeNumber && pixelBoard[k, j].Empty == false &&
            //            (pixelBoard[k, j].BlockageType == "Droplet" || pixelBoard[k, j].BlockageType == "Bubble")) {
            //            result = false;
            //        }
            //    }
            //}

            return result;
        }
    }
}
