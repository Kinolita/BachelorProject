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
            if (dropSize > 0) { dropSize -= 1; }
            var kMax = pixelBoard.GetLength(0);
            var jMax = pixelBoard.GetLength(1);

            //even droplet size
            if (dropSize % 2 == 0) {
                var buff = (int)dropSize / 2;
                for (var k = 0; k < kMax; k++) {
                    for (var j = 0; j < jMax; j++) {
                        //if occupied but not a buffer
                        if ((!pixelBoard[k, j].Empty && pixelBoard[k, j].BlockageType != "Buffer")) {
                            //cycling through surrounding pixels
                            for (int i = -buff; i <= buff; i++) {
                                for (int h = -buff; h <= buff; h++) {
                                    //check if on the board and empty
                                    if (BufferValidate(k + i, kMax) && BufferValidate(j + h, jMax) && pixelBoard[k + i, j + h].Empty) {
                                        pixelBoard[k + i, j + h].BlockageType = "Buffer";
                                        pixelBoard[k + i, j + h].Empty = false;
                                    }
                                }
                            }
                        }

                        // if an edge and not occupied
                        if (pixelBoard[k, j].Empty && k == 0) {
                            for (int i = 0; i < buff; i++) {
                                pixelBoard[k + i, j].BlockageType = "Buffer";
                                pixelBoard[k + i, j].Empty = false;
                            }
                        }
                        if (pixelBoard[k, j].Empty && j == 0) {
                            for (int i = 0; i < buff; i++) {
                                pixelBoard[k, j + i].BlockageType = "Buffer";
                                pixelBoard[k, j + i].Empty = false;
                            }
                        }
                        if (pixelBoard[k, j].Empty && k == kMax - 1) {
                            for (int i = 0; i < buff; i++) {
                                pixelBoard[k - i, j].BlockageType = "Buffer";
                                pixelBoard[k - i, j].Empty = false;
                            }
                        }
                        if (pixelBoard[k, j].Empty && j == jMax - 1) {
                            for (int i = 0; i < buff; i++) {
                                pixelBoard[k, j - i].BlockageType = "Buffer";
                                pixelBoard[k, j - i].Empty = false;
                            }
                        }
                    }
                }

                //odd droplet size
            } else {
                var buff = (int)Math.Floor(dropSize / 2);
                for (var k = 0; k < kMax; k++) {
                    for (var j = 0; j < jMax; j++) {
                        //if occupied but not a buffer
                        if ((!pixelBoard[k, j].Empty && pixelBoard[k, j].BlockageType != "Buffer")) {
                            //cycling through surrounding pixels
                            for (int i = -buff; i <= buff + 1; i++) {
                                for (int h = -buff; h <= buff + 1; h++) {
                                    //check if on the board and empty
                                    if (BufferValidate(k + i, kMax) && BufferValidate(j + h, jMax) && pixelBoard[k + i, j + h].Empty) {
                                        pixelBoard[k + i, j + h].BlockageType = "Buffer";
                                        pixelBoard[k + i, j + h].Empty = false;
                                    }
                                }
                            }
                        }

                        // if an edge and not occupied
                        if (pixelBoard[k, j].Empty && k == 0) {
                            for (int i = 0; i < buff + 1; i++) {
                                pixelBoard[k + i, j].BlockageType = "Buffer";
                                pixelBoard[k + i, j].Empty = false;
                            }
                        }
                        if (pixelBoard[k, j].Empty && j == 0) {
                            for (int i = 0; i < buff + 1; i++) {
                                pixelBoard[k, j + i].BlockageType = "Buffer";
                                pixelBoard[k, j + i].Empty = false;
                            }
                        }
                        if (pixelBoard[k, j].Empty && k == kMax - 1) {
                            for (int i = 0; i < buff; i++) {
                                pixelBoard[k - i, j].BlockageType = "Buffer";
                                pixelBoard[k - i, j].Empty = false;
                            }
                        }
                        if (pixelBoard[k, j].Empty && j == jMax - 1) {
                            for (int i = 0; i < buff; i++) {
                                pixelBoard[k, j - i].BlockageType = "Buffer";
                                pixelBoard[k, j - i].Empty = false;
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
