using System.Collections.Generic;

namespace BachelorProject.Models.DmfElements
{
    public class Board
    {
        public IList<Information> Information { get; set; }
        public IList<Electrode> Electrodes { get; set; }
        public IList<Actuator> Actuators { get; set; }
        public IList<Sensor> Sensors { get; set; }
        public IList<Input> Inputs { get; set; }
        public IList<Output> Outputs { get; set; }
        public IList<Droplet> Droplets { get; set; }
        public IList<Bubble> Bubbles { get; set; }
        public object Unclassified { get; set; }
    }
}
