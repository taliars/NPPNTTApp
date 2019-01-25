using System;

namespace NPPNTTApp.Model
{
    public class BaseClass
    {
        public DateTime Date { get; set; }
        public string ObjectA { get; set; }
        public string TypeA { get; set; }
        public string ObjectB { get; set; }
        public string TypeB { get; set; }
        public Direction Direction { get; set; }
        public string Color { get; set; }
        public int Intensity { get; set; }
        public double LatitudeA { get; set; }
        public double LongitudeA { get; set; }
        public double LatitudeB { get; set; }
        public double LongitudeB { get; set; }
    }
}
