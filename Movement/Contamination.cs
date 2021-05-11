using System.Collections.Generic;
using BachelorProject.Models;

namespace BachelorProject.Movement
{
    public class Contamination
    {
        public static void ContaminateBoard(Pixels[,] pixelBoard, List<Coord> path) {
            foreach (var step in path) {
                if (!pixelBoard[step.X, step.Y].Empty && pixelBoard[step.X, step.Y].BlockageType != "Buffer") continue;
                pixelBoard[step.X, step.Y].Empty = false;
                pixelBoard[step.X, step.Y].BlockageType = "Contaminated";
            }
        }

        public static void Decontaminate(Pixels[,] pixelBoard) {
            for (var k = 0; k < pixelBoard.GetLength(0); k++) {
                for (var j = 0; j < pixelBoard.GetLength(1); j++) {
                    if (pixelBoard[k, j].BlockageType != "Contaminated") continue;
                    pixelBoard[k, j].Empty = true;
                    pixelBoard[k, j].BlockageType = "";
                }
            }
        }
    }
}