using System.Collections.Generic;
using System.Linq;
using BachelorProject.Models.DmfElements;

namespace BachelorProject.Models
{
    public class Pixels
    {
        public bool Empty { get; set; }
        public string BlockageType { get; set; }
        // (Contaminated, OoB, bubble, droplet name, etc.)
        public int WhichElectrode { get; set; }
        public int XRange { get; set; }
        public int YRange { get; set; }

        private static List<int> MaxRange(IList<IList<int>> corners, int axis) {
            var track = new List<int>();
            var theRange = new List<int>();
            foreach (var corner in corners) {
                track.Add(corner[axis]);
            }
            theRange.Add(track.Min());
            theRange.Add(track.Max());
            return theRange;
        }
        public static Pixels[,] Create(Board specs) {
            var pixelBoard1 = new Pixels[specs.Information[0].SizeX, specs.Information[0].SizeY];

            // going through each pixel one at a time
            for (var k = 0; k < specs.Information[0].SizeX; k++) {
                for (var j = 0; j < specs.Information[0].SizeY; j++) {
                    var pix = new Pixels();
                    pixelBoard1[k, j] = pix;
                    pix.Empty = false;
                    pix.BlockageType = "OoB";
                    pix.WhichElectrode = -1;
                }
            }

            //going through each of the electrodes
            foreach (var electrode in specs.Electrodes) {
                switch (electrode.shape) {
                    // rectangle electrodes
                    case 0:
                        for (var p = electrode.positionX; p < electrode.positionX + electrode.sizeX; p++) {
                            for (var q = electrode.positionY; q < electrode.positionY + electrode.sizeY; q++) {
                                pixelBoard1[p, q].WhichElectrode = electrode.ID;
                                pixelBoard1[p, q].Empty = true;
                                pixelBoard1[p, q].BlockageType = "";
                                pixelBoard1[p, q].XRange = electrode.sizeX;
                                pixelBoard1[p, q].YRange = electrode.sizeY;
                            }
                        }
                        break;
                    // custom polygon electrodes
                    case 1:
                        var minX = MaxRange(electrode.corners, 0)[0] + electrode.positionX;
                        var maxX = MaxRange(electrode.corners, 0)[1] + electrode.positionX;
                        var minY = MaxRange(electrode.corners, 1)[0] + electrode.positionY;
                        var maxY = MaxRange(electrode.corners, 1)[1] + electrode.positionY;

                        var nn = electrode.corners.Count;
                        var polygon2 = new Coord[nn];
                        for (var i = 0; i < nn; i++) {
                            polygon2[i].X = (electrode.corners[i][0] + electrode.positionX);
                            polygon2[i].Y = (electrode.corners[i][1] + electrode.positionY);
                        }

                        for (var p = minX; p < maxX; p++) {
                            for (var q = minY; q < maxY; q++) {
                                var r = new Coord(p, q);
                                if (!PolygonPoints.IsInside(polygon2, nn, r)) continue;
                                pixelBoard1[p, q].WhichElectrode = electrode.ID;
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
                for (var q = t.PositionX; q < t.PositionX + t.SizeX; q++) {
                    for (var r = t.PositionY; r < t.PositionY + t.SizeY; r++) {
                        pixelBoard1[q, r].Empty = false;
                        pixelBoard1[q, r].BlockageType = t.Id.ToString();
                    }
                }

            if (specs.Bubbles == null) return pixelBoard1;
            foreach (var t in specs.Bubbles) {
                for (var q = t.PositionX; q < t.PositionX + t.SizeX; q++) {
                    for (var r = t.PositionY; r < t.PositionY + t.SizeY; r++) {
                        pixelBoard1[q, r].Empty = false;
                        pixelBoard1[q, r].BlockageType = t.Name;
                    }
                }
            }

            return pixelBoard1;
        }

        public static Pixels[,] ScaleDown(Pixels[,] pixelBoard, out bool isScaled) {
            //this will only work for boards with even dimensioned electrodes
            var square = new List<Coord> { new Coord(1, 0), new Coord(0, 1), new Coord(1, 1) };
            var scalable = true;

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
                var pixelBoard2 = new Pixels[pixelBoard.GetLength(0) / 2, pixelBoard.GetLength(1) / 2];
                for (var k = 0; k < pixelBoard2.GetLength(0); k++) {
                    for (var j = 0; j < pixelBoard2.GetLength(1); j++) {
                        pixelBoard2[k, j] = pixelBoard[k * 2, j * 2];
                        pixelBoard2[k, j].XRange = pixelBoard[k * 2, j * 2].XRange / 2;
                        pixelBoard2[k, j].YRange = pixelBoard[k * 2, j * 2].YRange / 2;
                    }
                }
                isScaled = true;
                return pixelBoard2;
            }
            isScaled = false;
            return pixelBoard;
        }

        public static bool ValidateOnBoard(Pixels[,] pixelBoard, int k, int j) {
            return k >= 0 && j >= 0 && k < pixelBoard.GetLength(0) && j < pixelBoard.GetLength(1);
        }
    }
}

