using Newtonsoft.Json;
using System.Collections.Generic;

namespace BachelorProject.Models.DmfElements
{
    public class Board
    {
        [JsonProperty("information")]
        public IList<Information> Information { get; set; }
        [JsonProperty("electrodes")]
        public IList<Electrode> Electrodes { get; set; }
        [JsonProperty("actuators")]
        public IList<Actuator> Actuators { get; set; }
        [JsonProperty("sensors")]
        public IList<Sensor> Sensors { get; set; }
        [JsonProperty("inputs")]
        public IList<Input> Inputs { get; set; }
        [JsonProperty("outputs")]
        public IList<Output> Outputs { get; set; }
        [JsonProperty("droplets")]
        public IList<Droplet> Droplets { get; set; }
        [JsonProperty("bubbles")]
        public IList<Bubble> Bubbles { get; set; }
        [JsonProperty("unclassified")]
        public object Unclassified { get; set; }
    }
}
