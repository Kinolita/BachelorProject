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

            /////////////////New code//////////////////
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

            /////////////////old version that cant do 100x100///////////////////////
            //// going through each pixel one at a time
            //        //going through each of the electrodes
            //        for (int m = 0; m < specs.Electrodes.Count; m++) {
            //            Coord p = new Coord(k, j);

            //            // corners for rectangle electrodes
            //            if (specs.Electrodes[m].shape == 0) {
            //                int n = 4;
            //                Coord[] polygon1 = {new Coord(specs.Electrodes[m].positionX, specs.Electrodes[m].positionY),
            //                    new Coord(specs.Electrodes[m].positionX + specs.Electrodes[m].sizeX-1, specs.Electrodes[m].positionY),
            //                    new Coord(specs.Electrodes[m].positionX + specs.Electrodes[m].sizeX-1, specs.Electrodes[m].positionY + specs.Electrodes[m].sizeY-1),
            //                    new Coord(specs.Electrodes[m].positionX, specs.Electrodes[m].positionY + specs.Electrodes[m].sizeY-1)};
            //                if (PolygonPoints.IsInside(polygon1, n, p)) {
            //                    pix.WhichElectrode = specs.Electrodes[m].ID;
            //                    pix.Empty = true;
            //                    pix.BlockageType = "";

            //                }
            //                //corners for non-rectangle electrodes
            //            } else if (specs.Electrodes[m].shape == 1) {
            //                int nn = specs.Electrodes[m].corners.Count;
            //                Coord[] polygon2 = new Coord[nn];
            //                for (int i = 0; i < nn; i++) {
            //                    polygon2[i].X = (specs.Electrodes[m].corners[i][0] + specs.Electrodes[m].positionX);
            //                    polygon2[i].Y = (specs.Electrodes[m].corners[i][1] + specs.Electrodes[m].positionY);
            //                }
            //                if (PolygonPoints.IsInside(polygon2, nn, p)) {
            //                    pix.WhichElectrode = specs.Electrodes[m].ID;
            //                    pix.Empty = true;
            //                    pix.BlockageType = "";
            //                }
            //            }
            //        }
            //    }
            //}

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
    }
}

