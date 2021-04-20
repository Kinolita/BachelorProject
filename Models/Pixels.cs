using System.Collections.Generic;
using System.Linq;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Models
{
    public class Pixels
    {
        public bool Empty { get; set; }
        public string BlockageType { get; set; }
        // (Contaminated, OoB, bubble, droplet, etc.)
        public int WhichElectrode { get; set; }
        public int XRange { get; set; }
        public int YRange { get; set; }

        private static List<int> MaxRange(IList<IList<int>> corners, int axis) {
            List<int> track = new List<int>();
            List<int> theRange = new List<int>();
            for (int i = 0; i < corners.Count; i++) {
                track.Add(corners[i][axis]);
            }
            theRange.Add(track.Min());
            theRange.Add(track.Max());
            return theRange;
        }
        public static Pixels[,] Create(Board specs) {
            Pixels[,] pixelBoard1 = new Pixels[specs.Information[0].SizeX, specs.Information[0].SizeY];

            // going through each pixel one at a time
            for (int k = 0; k < specs.Information[0].SizeX; k++) {
                for (int j = 0; j < specs.Information[0].SizeY; j++) {
                    Pixels pix = new Pixels();
                    pixelBoard1[k, j] = pix;
                    pix.Empty = false;
                    pix.BlockageType = "OoB";
                    pix.WhichElectrode = -1;
                }
            }

            //going through each of the electrodes
            for (int m = 0; m < specs.Electrodes.Count; m++) {
                switch (specs.Electrodes[m].shape) {
                    // rectangle electrodes
                    case 0:
                        for (int p = specs.Electrodes[m].positionX; p < specs.Electrodes[m].positionX + specs.Electrodes[m].sizeX; p++) {
                            for (int q = specs.Electrodes[m].positionY; q < specs.Electrodes[m].positionY + specs.Electrodes[m].sizeY; q++) {
                                pixelBoard1[p, q].WhichElectrode = specs.Electrodes[m].ID;
                                pixelBoard1[p, q].Empty = true;
                                pixelBoard1[p, q].BlockageType = "";
                                pixelBoard1[p, q].XRange = specs.Electrodes[m].sizeX;
                                pixelBoard1[p, q].YRange = specs.Electrodes[m].sizeY;
                            }
                        }
                        break;
                    // custom polygon electrodes
                    case 1:
                        int minX = MaxRange(specs.Electrodes[m].corners, 0)[0] + specs.Electrodes[m].positionX;
                        int maxX = MaxRange(specs.Electrodes[m].corners, 0)[1] + specs.Electrodes[m].positionX;
                        int minY = MaxRange(specs.Electrodes[m].corners, 1)[0] + specs.Electrodes[m].positionY;
                        int maxY = MaxRange(specs.Electrodes[m].corners, 1)[1] + specs.Electrodes[m].positionY;

                        int nn = specs.Electrodes[m].corners.Count;
                        Coord[] polygon2 = new Coord[nn];
                        for (int i = 0; i < nn; i++) {
                            polygon2[i].X = (specs.Electrodes[m].corners[i][0] + specs.Electrodes[m].positionX);
                            polygon2[i].Y = (specs.Electrodes[m].corners[i][1] + specs.Electrodes[m].positionY);
                        }

                        for (int p = minX; p < maxX; p++) {
                            for (int q = minY; q < maxY; q++) {
                                Coord r = new Coord(p, q);
                                if (!PolygonPoints.IsInside(polygon2, nn, r)) continue;
                                pixelBoard1[p, q].WhichElectrode = specs.Electrodes[m].ID;
                                pixelBoard1[p, q].Empty = true;
                                pixelBoard1[p, q].BlockageType = "";
                                pixelBoard1[p, q].XRange = maxX - minX;
                                pixelBoard1[p, q].YRange = maxY - minY;
                            }
                        }
                        break;
                }
            }

            //assigning vacancy(bubbles and droplets)
            if (specs.Droplets == null) return pixelBoard1;
            foreach (var t in specs.Droplets)
                for (int q = t.PositionX; q < t.PositionX + t.SizeX; q++) {
                    for (int r = t.PositionY; r < t.PositionY + t.SizeY; r++) {
                        pixelBoard1[q, r].Empty = false;
                        pixelBoard1[q, r].BlockageType = "Droplet";
                    }
                }

            if (specs.Bubbles == null) return pixelBoard1;
            foreach (var t in specs.Bubbles) {
                for (int q = t.PositionX; q < t.PositionX + t.SizeX; q++) {
                    for (int r = t.PositionY; r < t.PositionY + t.SizeY; r++) {
                        pixelBoard1[q, r].Empty = false;
                        pixelBoard1[q, r].BlockageType = "Bubble";
                    }
                }
            }

            return pixelBoard1;
        }

        //public static Pixels[,] ScaleDown(Pixels[,] pixelBoard) {
        //    int minRange = pixelBoard[0, 0].XRange;
        //    //int minYrange = pixelBoard[0, 0].YRange;
        //    int minDropletBubbleSize = 0;

        //    for (var k = 0; k < pixelBoard.GetLength(0); k++) {
        //        for (var j = 0; j < pixelBoard.GetLength(1); j++) {
        //            if (pixelBoard[k, j].XRange < minRange) { minRange = pixelBoard[k, j].XRange; }
        //            if (pixelBoard[k, j].YRange < minRange) { minRange = pixelBoard[k, j].YRange; }

        //            if (!pixelBoard[k, j].Empty && (pixelBoard[k, j].BlockageType == "Bubble" ||
        //                                            pixelBoard[k, j].BlockageType == "Droplet")) {
        //                int temp = 0;
        //                while (pixelBoard[k, j + temp].BlockageType == "Bubble" || pixelBoard[k, j].BlockageType == "Droplet") {
        //                    temp += 1;
        //                }
        //                if (temp < minDropletBubbleSize) { minDropletBubbleSize = temp; }
        //                break;
        //            }
        //            //actuators and sensors are not present at the pixel level
        //            // how should those be considered for scaling down?
        //        }
        //    }

        //    //GCD
        //    while (minDropletBubbleSize != 0) {
        //        int remainder = minRange % minDropletBubbleSize;
        //        minRange = minDropletBubbleSize;
        //        minDropletBubbleSize = remainder;
        //    }

        //    if (minRange > 4) {
        //        Pixels[,] pixelBoard2 = new Pixels[pixelBoard.GetLength(0) / minRange, pixelBoard.GetLength(1) / minRange];
        //        // going through each pixel one at a time
        //        for (int k = 0; k < pixelBoard2.GetLength(0); k++) {
        //            for (int j = 0; j < pixelBoard2.GetLength(1); j++) {
        //                Pixels pix = new Pixels();
        //                pixelBoard2[k, j] = pix;
        //                pix.Empty = pixelBoard[k,j].Empty;
        //                pix.BlockageType = pixelBoard[k, j].BlockageType;
        //                pix.WhichElectrode = -1;
        //            }
        //        }
        //    }
        //    return pixelBoard2;
        //}


        public static Pixels[,] ScaleDown(Pixels[,] pixelBoard) {
        //this will only work for boards with even dimensioned electrodes
            List<Coord> square = new List<Coord>();
            square.Add(new Coord(1, 0));
            square.Add(new Coord(0, 1));
            square.Add(new Coord(1, 1));
            bool scalable = true;

            for (var k = 0; k < pixelBoard.GetLength(0); k += 2) {
                for (var j = 0; j < pixelBoard.GetLength(1); j += 2) {
                    foreach (var corner in square) {
                        if (pixelBoard[k, j].Empty != pixelBoard[k + corner.X, j + corner.Y].Empty ||
                            pixelBoard[k, j].WhichElectrode != pixelBoard[k + corner.X, j + corner.Y].WhichElectrode ||
                            pixelBoard[k, j].BlockageType != pixelBoard[k + corner.X, j + corner.Y].BlockageType)
                            scalable = false;
                    }
                }
            }

            if (scalable) {
                Pixels[,] pixelBoard2 = new Pixels[pixelBoard.GetLength(0) / 2, pixelBoard.GetLength(1) / 2];
                for (var k = 0; k < pixelBoard2.GetLength(0); k ++) {
                    for (var j = 0; j < pixelBoard2.GetLength(1); j ++) {
                        pixelBoard2[k, j] = pixelBoard[k * 2, j * 2];
                    }
                }
                return pixelBoard2;
                // still need to add in preexisting droplets and input/outputs
            }
            return pixelBoard;
        }
    }
}

