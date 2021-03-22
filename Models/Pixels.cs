using BachelorProject.Models.DmfElements;

namespace BachelorProject.Models
{
    public class Pixels
    {
        public bool Empty { get; set; }
        public string BlockageType { get; set; }
        // (Contaminated, OoB, bubble, droplet, etc.)
        public int WhichElectrode { get; set; }

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

                    for (int m = 0; m < specs.Electrodes.Count; m++) {
                        Coord p = new Coord(k, j);

                        // corners for rectangle electrodes
                        if (specs.Electrodes[m].Shape == 0) {
                            int n = 4;
                            Coord[] polygon1 = {new Coord(specs.Electrodes[m].PositionX, specs.Electrodes[m].PositionY),
                                new Coord(specs.Electrodes[m].PositionX + specs.Electrodes[m].SizeX-1, specs.Electrodes[m].PositionY),
                                new Coord(specs.Electrodes[m].PositionX + specs.Electrodes[m].SizeX-1, specs.Electrodes[m].PositionY + specs.Electrodes[m].SizeY-1),
                                new Coord(specs.Electrodes[m].PositionX, specs.Electrodes[m].PositionY + specs.Electrodes[m].SizeY-1)};
                            if (PolygonPoints.IsInside(polygon1, n, p)) {
                                pix.WhichElectrode = specs.Electrodes[m].Id;
                                pix.Empty = true;
                                pix.BlockageType = "";

                            }
                            //corners for non-rectangle electrodes
                        } else if (specs.Electrodes[m].Shape == 1) {
                            int nn = specs.Electrodes[m].Corners.Count;
                            Coord[] polygon2 = new Coord[nn];
                            for (int i = 0; i < nn; i++) {
                                polygon2[i].X = (specs.Electrodes[m].Corners[i][0] + specs.Electrodes[m].PositionX);
                                polygon2[i].Y = (specs.Electrodes[m].Corners[i][1] + specs.Electrodes[m].PositionY);
                            }
                            if (PolygonPoints.IsInside(polygon2, nn, p)) {
                                pix.WhichElectrode = specs.Electrodes[m].Id;
                                pix.Empty = true;
                                pix.BlockageType = "";
                            }
                        }
                    }
                }
            }

            //program can recognize but not successfully navigate around bubbles and droplets now
            //assigning vacancy(bubbles and droplets)
            if (specs.Droplets != null) {
                for (int m = 0; m < specs.Droplets.Count; m++) {
                    for (int q = specs.Droplets[m].PositionX; q < specs.Droplets[m].PositionX + specs.Droplets[m].SizeX; q++) {
                        for (int r = specs.Droplets[m].PositionY; r < specs.Droplets[m].PositionY + specs.Droplets[m].SizeY; r++) {
                            pixelBoard1[q, r].Empty = false;
                            pixelBoard1[q, r].BlockageType = "Droplet";
                        }
                    }
                }
            }
            if (specs.Bubbles != null) {
                for (int m = 0; m < specs.Bubbles.Count; m++) {
                    for (int q = specs.Bubbles[m].PositionX; q < specs.Bubbles[m].PositionX + specs.Bubbles[m].SizeX; q++) {
                        for (int r = specs.Bubbles[m].PositionY; r < specs.Bubbles[m].PositionY + specs.Bubbles[m].SizeY; r++) {
                            pixelBoard1[q, r].Empty = false;
                            pixelBoard1[q, r].BlockageType = "Bubble";
                        }
                    }
                }
            }
            return pixelBoard1;
        }
    }
}

