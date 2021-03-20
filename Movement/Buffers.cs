using BachelorProject.Models;
using System;

namespace BachelorProject.Movement
{
    class Buffers
    {
        //this is actually almost identicle to a method in BasicRouting
        static bool BufferValidate(int a, int max) {
            if (a >= 0 && a < max) { return true; } else { return false; }
        }

        public static void AddBuffer(Pixels[,] PixelBoard, double dropSize) {
            int Buff = (int)Math.Ceiling(dropSize / 2);
            int kMax = PixelBoard.GetLength(0);
            int jMax = PixelBoard.GetLength(1);
            for (int k = 0; k < kMax; k++) {
                for (int j = 0; j < jMax; j++) {

                    if ((PixelBoard[k, j].Empty == false && PixelBoard[k, j].BlockageType != "Buffer") || k == 0 || j == 0 || k == kMax - 1 || j == jMax - 1) {

                        //cycling through surrounding pixels
                        for (int i = -Buff; i <= Buff; i++) {
                            for (int h = -Buff; h <= Buff; h++) {
                                if (BufferValidate(k + i, kMax) && BufferValidate(j + h, jMax) && PixelBoard[k + i, j + h].Empty == true) {
                                    PixelBoard[k + i, j + h].BlockageType = "Buffer";
                                    PixelBoard[k + i, j + h].Empty = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveBuffer(Pixels[,] PixelBoard) {
            for (int k = 0; k < PixelBoard.GetLength(0); k++) {
                for (int j = 0; j < PixelBoard.GetLength(1); j++) {
                    if (PixelBoard[k, j].BlockageType == "Buffer") {
                        PixelBoard[k, j].Empty = true;
                        PixelBoard[k, j].BlockageType = "";
                    }
                }
            }
        }
    }
}
