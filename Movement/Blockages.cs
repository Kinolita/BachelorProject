using System;
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
            var aa = wantToGo.X - drop.SizeX / 2;
            var bb = (int)Math.Ceiling(wantToGo.X + (float)drop.SizeX / 2);
            var cc = wantToGo.Y - drop.SizeY / 2;
            var dd = (int)Math.Ceiling(wantToGo.Y + (float)drop.SizeY / 2);

            if (aa < 0) aa = 0;
            if (bb > pixelBoard.GetLength(0)) bb = pixelBoard.GetLength(0);
            if (cc < 0) cc = 0;
            if (dd > pixelBoard.GetLength(1)) dd = pixelBoard.GetLength(1);
            
            //searching whole electrodes included in this spread for !Empty that are not buffer or current droplet
            for (var s = aa; s < bb; s++) {
                for (var t = cc; t < dd; t++) {
                    var a = s - pixelBoard[s, t].XRange;
                    if (a < 0) a = 0;
                    var b = s + pixelBoard[s, t].XRange;
                    if (b > pixelBoard.GetLength(0)) b = pixelBoard.GetLength(0);
                    var c = t - pixelBoard[s, t].YRange;
                    if (c < 0) c = 0;
                    var d = t + pixelBoard[s, t].YRange;
                    if (d > pixelBoard.GetLength(1)) d = pixelBoard.GetLength(1);

                    for (var k = a; k < b; k++) {
                        for (var j = c; j < d; j++) {
                            //if in target electrode and if not empty but also not name or buffer
                            if (pixelBoard[k, j].WhichElectrode == pixelBoard[s, t].WhichElectrode && !pixelBoard[k, j].Empty &&
                                 !(pixelBoard[k, j].BlockageType == drop.Name || pixelBoard[k, j].BlockageType == "Buffer")) {
                                result = false;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static void DropletSet(Pixels[,] pixelBoard, Coord destination, Droplet drop) {
            var size = Droplet.MaxSize(drop);
            var extra = 0;
            var a = destination.X - size / 2;
            var b = destination.X + size / 2;
            var c = destination.Y - size / 2;
            var d = destination.Y + size / 2;

            //accounting for large droplets close to the edge
            if (a < 0) {
                extra = 0 - a;
                a = 0;
                b += extra;
            } else if (b > pixelBoard.GetLength(0)) {
                extra = pixelBoard.GetLength(0) - b;
                b = pixelBoard.GetLength(0);
                a += extra;
            }

            if (c < 0) {
                extra = 0 - c;
                c = 0;
                d += extra;
            } else if (d > pixelBoard.GetLength(1)) {
                extra = pixelBoard.GetLength(1) - d;
                d = pixelBoard.GetLength(1);
                c += extra;
            }

            if (size % 2 == 0) {
                for (var k = a; k < b; k++) {
                    for (var j = c; j < d; j++) {
                        if (!Pixels.ValidateOnBoard(pixelBoard, k, j)) continue;
                        pixelBoard[k, j].Empty = false;
                        pixelBoard[k, j].BlockageType = drop.Name;
                    }
                }
            } else {
                for (var k = a; k < b + 1; k++) {
                    for (var j = c; j < d + 1; j++) {
                        if (!Pixels.ValidateOnBoard(pixelBoard, k, j)) continue;
                        pixelBoard[k, j].Empty = false;
                        pixelBoard[k, j].BlockageType = drop.Name;
                    }
                }
            }
        }
    }
}
