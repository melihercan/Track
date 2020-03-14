using System;
using System.Collections.Generic;
using System.Text;

namespace WebClient.Models
{
    public class DeviceData
    {
        public int Id { get; set; }
        public int GroupId { get; set; }

        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Speed { get; set; }
    }
}
