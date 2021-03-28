using BachelorProject.Models;
using System;

namespace BachelorProject.Movement
{
    class Buffers
    {
        //this is actually almost identical to a method in BasicRouting
        static bool BufferValidate(int a, int max) {
            return a >= 0 && a < max;
        }

        public static void AddBuffer(Pixels[,] pixelBoard, double dropSize) {
            var buff = (int)Math.Ceiling(dropSize / 2);
            var kMax = pixelBoard.GetLength(0);
            var jMax = pixelBoard.GetLength(1);
            for (var k = 0; k < kMax; k++) {
                for (var j = 0; j < jMax; j++) {
                    if ((pixelBoard[k, j].Empty == false && pixelBoard[k, j].BlockageType != "Buffer") || k == 0 || j == 0 || k == kMax - 1 || j == jMax - 1) {
                        //cycling through surrounding pixels
                        for (int i = -buff; i <= buff; i++) {
                            for (int h = -buff; h <= buff; h++) {
                                if (BufferValidate(k + i, kMax) && BufferValidate(j + h, jMax) && pixelBoard[k + i, j + h].Empty) {
                                    pixelBoard[k + i, j + h].BlockageType = "Buffer";
                                    pixelBoard[k + i, j + h].Empty = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveBuffer(Pixels[,] pixelBoard) {
            for (int k = 0; k < pixelBoard.GetLength(0); k++) {
                for (int j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].BlockageType != "Buffer") continue;
                    pixelBoard[k, j].Empty = true;
                    pixelBoard[k, j].BlockageType = "";
                }
            }
        }
    }
}
