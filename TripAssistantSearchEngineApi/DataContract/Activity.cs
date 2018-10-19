using Newtonsoft.Json.Linq;
using System;

namespace Data.Contract

{
    public class Activity
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string PlaceId { get; set; }
        public double Rating { get; set; }
        public string Type { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
