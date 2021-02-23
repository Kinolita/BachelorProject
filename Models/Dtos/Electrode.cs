using System.Collections.Generic;

namespace BachelorProject.Models.Dtos
{
    public class Electrode
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int ElectrodeID { get; set; }
        public int DriverID { get; set; }
        public int Shape { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Status { get; set; }
        public IList<IList<int>> Corners { get; set; }
    }
}
