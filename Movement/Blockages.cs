using BachelorProject.Models;

namespace BachelorProject.Movement
{
    class Blockages
    {
        //this slows down the program quite a bit as it has to search every pixel on the board everytime it wants to move 1 pixel
        //suggestion for later:
        //search by max electrode size on either side of the current
        public static bool DropletBubbleCheck(Pixels[,] PixelBoard, Coord WantToGO) {
            bool result = true;     //true if the electrode is empty
            int elec = PixelBoard[WantToGO.x, WantToGO.y].WhichElectrode;
            for (int k = 0; k < PixelBoard.GetLength(0); k++) {
                for (int j = 0; j < PixelBoard.GetLength(1); j++) {
                    if (PixelBoard[k,j].WhichElectrode == elec && PixelBoard[k, j].Empty == false && 
                        (PixelBoard[k,j].BlockageType == "Droplet" || PixelBoard[k,j].BlockageType == "Bubble")) {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
