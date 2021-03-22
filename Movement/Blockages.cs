using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class Blockages
    {
        //this slows down the program quite a bit as it has to search every pixel on the board every time it wants to move 1 pixel
        //suggestion for later:
        //search by max electrode size on either side of the current
        public static bool DropletBubbleCheck(Pixels[,] pixelBoard, Coord wantToGo) {
            bool result = true;     //true if the electrode is empty
            var electrodeNumber = pixelBoard[wantToGo.X, wantToGo.Y].WhichElectrode;
            for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k,j].WhichElectrode == electrodeNumber && pixelBoard[k, j].Empty == false && 
                        (pixelBoard[k,j].BlockageType == "Droplet" || pixelBoard[k,j].BlockageType == "Bubble")) {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
