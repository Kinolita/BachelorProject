using BachelorProject.Models.Dtos;
using BachelorProject.Scraps;

namespace BachelorProject.Models
{
    public class Pixels
    {
        public bool Empty { get; set; }
        public string BlockageType { get; set; }
        // (Contaminated, OoB, bubble, droplet, etc.)
        public int WhichElectrode { get; set; }

        public static Pixels[,] Create(Board Specs) {
            Pixels[,] PixelBoard1 = new Pixels[Specs.Information[0].SizeX, Specs.Information[0].SizeY];

            // going through each pixel one at a time
            for (int k = 0; k < Specs.Information[0].SizeX; k++) {
                for (int j = 0; j < Specs.Information[0].SizeY; j++) {
                    Pixels pix = new Pixels();
                    PixelBoard1[k, j] = pix;
                    pix.Empty = false;
                    pix.BlockageType = "OoB";
                    pix.WhichElectrode = -1;

                    for (int m = 0; m < Specs.Electrodes.Count; m++) {
                        Coord p = new Coord(k, j);

                        // corners for rectangle electrodes
                        if (Specs.Electrodes[m].Shape == 0) {
                            int n = 4;
                            Coord[] polygon1 = {new Coord(Specs.Electrodes[m].PositionX, Specs.Electrodes[m].PositionY),
                                new Coord(Specs.Electrodes[m].PositionX + Specs.Electrodes[m].SizeX-1, Specs.Electrodes[m].PositionY),
                                new Coord(Specs.Electrodes[m].PositionX + Specs.Electrodes[m].SizeX-1, Specs.Electrodes[m].PositionY + Specs.Electrodes[m].SizeY-1),
                                new Coord(Specs.Electrodes[m].PositionX, Specs.Electrodes[m].PositionY + Specs.Electrodes[m].SizeY-1)};
                            if (PolygonPoints.IsInside(polygon1, n, p)) {
                                pix.WhichElectrode = Specs.Electrodes[m].ID;
                                pix.Empty = true;
                                pix.BlockageType = "";

                            }
                            //corners for non-rectangle electrodes
                        } else if (Specs.Electrodes[m].Shape == 1) {
                            int nn = Specs.Electrodes[m].Corners.Count;
                            Coord[] polygon2 = new Coord[nn];
                            for (int i = 0; i < nn; i++) {
                                polygon2[i].x = (Specs.Electrodes[m].Corners[i][0] + Specs.Electrodes[m].PositionX);
                                polygon2[i].y = (Specs.Electrodes[m].Corners[i][1] + Specs.Electrodes[m].PositionY);
                            }
                            if (PolygonPoints.IsInside(polygon2, nn, p)) {
                                pix.WhichElectrode = Specs.Electrodes[m].ID;
                                pix.Empty = true;
                                pix.BlockageType = "";
                            }
                        }
                    }
                }
            }

            //program can recognize but not successfully navigate around bubbles and droplets now
            //assigning vacancy(bubbles and droplets)
            if (Specs.Droplets != null) {
                for (int m = 0; m < Specs.Droplets.Count; m++) {
                    for (int q = Specs.Droplets[m].PositionX; q < Specs.Droplets[m].PositionX + Specs.Droplets[m].SizeX; q++) {
                        for (int r = Specs.Droplets[m].PositionY; r < Specs.Droplets[m].PositionY + Specs.Droplets[m].SizeY; r++) {
                            PixelBoard1[q, r].Empty = false;
                            PixelBoard1[q, r].BlockageType = "Droplet";
                        }
                    }
                }
            }
            if (Specs.Bubbles != null) {
                for (int m = 0; m < Specs.Bubbles.Count; m++) {
                    for (int q = Specs.Bubbles[m].PositionX; q < Specs.Bubbles[m].PositionX + Specs.Bubbles[m].SizeX; q++) {
                        for (int r = Specs.Bubbles[m].PositionY; r < Specs.Bubbles[m].PositionY + Specs.Bubbles[m].SizeY; r++) {
                            PixelBoard1[q, r].Empty = false;
                            PixelBoard1[q, r].BlockageType = "Bubble";
                        }
                    }
                }
            }
            return PixelBoard1;
        }
    }
}

