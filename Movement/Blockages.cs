using System;
using BachelorProject.Exceptions;
using BachelorProject.Models;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Movement
{
    class Blockages
    {
        // checks the whole electrode of a target pixel to see if there is any blockage besides buffers or the current droplet
        public static bool DropletBubbleCheck(Pixels[,] pixelBoard, Coord wantToGo, Droplet drop) {
            var result = true; // true if the electrode is empty or the current droplet or a buffer

            //boundaries of possible contamination spread
            var leftBoundary = wantToGo.X - drop.SizeX / 2;
            var rightBoundary = (int)Math.Ceiling(wantToGo.X + (float)drop.SizeX / 2);
            var topBoundary = wantToGo.Y - drop.SizeY / 2;
            var bottomBoundary = (int)Math.Ceiling(wantToGo.Y + (float)drop.SizeY / 2);

            if (leftBoundary < 0) leftBoundary = 0;
            if (rightBoundary > pixelBoard.GetLength(0)) rightBoundary = pixelBoard.GetLength(0);
            if (topBoundary < 0) topBoundary = 0;
            if (bottomBoundary > pixelBoard.GetLength(1)) bottomBoundary = pixelBoard.GetLength(1);

            //searching whole electrodes included in this spread for !Empty that are not buffer or current droplet
            for (var s = leftBoundary; s < rightBoundary; s++) {
                for (var t = topBoundary; t < bottomBoundary; t++) {
                    var left = s - pixelBoard[s, t].XRange;
                    if (left < 0) left = 0;
                    var right = s + pixelBoard[s, t].XRange;
                    if (right > pixelBoard.GetLength(0)) right = pixelBoard.GetLength(0);
                    var up = t - pixelBoard[s, t].YRange;
                    if (up < 0) up = 0;
                    var down = t + pixelBoard[s, t].YRange;
                    if (down > pixelBoard.GetLength(1)) down = pixelBoard.GetLength(1);

                    for (var k = left; k < right; k++) {
                        for (var j = up; j < down; j++) {
                            //if in target electrode and if not empty but also not name or buffer
                            if (pixelBoard[k, j].WhichElectrode == pixelBoard[s, t].WhichElectrode && !pixelBoard[k, j].Empty &&
                                 !(pixelBoard[k, j].BlockageType == drop.Id.ToString() || pixelBoard[k, j].BlockageType == "Buffer")) {
                                result = false;
                            }
                        }
                    }
                }
            }
            return result;
        }

        //marks droplet location according to size and start pixel
        public static void DropletSet(Pixels[,] pixelBoard, Coord destination, Droplet drop) {
            if (destination.X == -1 || destination.Y == -1) {
                throw new ElectrodeException("The position for " + drop.Name + " is not valid.");
            }

            var size = Droplet.MaxSize(drop);
            var left = destination.X - size / 2;
            var right = destination.X + size / 2;
            var up = destination.Y - size / 2;
            var down = destination.Y + size / 2;

            //accounting for large droplets close to the edge
            if (left < 0) {
                int extra = 0 - left;
                left = 0;
                right += extra;
            } else if (right > pixelBoard.GetLength(0)) {
                int extra = pixelBoard.GetLength(0) - right;
                right = pixelBoard.GetLength(0);
                left += extra;
            }

            if (up < 0) {
                int extra = 0 - up;
                up = 0;
                down += extra;
            } else if (down > pixelBoard.GetLength(1)) {
                int extra = pixelBoard.GetLength(1) - down;
                down = pixelBoard.GetLength(1);
                up += extra;
            }

            if (size % 2 == 0) {
                for (var k = left; k < right; k++) {
                    for (var j = up; j < down; j++) {
                        if (!Pixels.ValidateOnBoard(pixelBoard, k, j)) continue;
                        pixelBoard[k, j].Empty = false;
                        pixelBoard[k, j].BlockageType = drop.Id.ToString();
                    }
                }
            } else {
                for (var k = left; k < right + 1; k++) {
                    for (var j = up; j < down + 1; j++) {
                        if (!Pixels.ValidateOnBoard(pixelBoard, k, j)) continue;
                        pixelBoard[k, j].Empty = false;
                        pixelBoard[k, j].BlockageType = drop.Id.ToString();
                    }
                }
            }
        }
    }
}
