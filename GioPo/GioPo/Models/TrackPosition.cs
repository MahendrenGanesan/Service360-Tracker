using System;
using System.Collections.Generic;
using System.Text;

namespace GioPo.Models
{
    public class TrackPosition
    {
        public string TrackerId { get; set; }

        public string DisplayName { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public DateTime TrackTimeStamp { get; set; }
    }
}
