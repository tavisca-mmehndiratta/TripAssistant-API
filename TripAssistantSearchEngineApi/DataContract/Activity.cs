using Newtonsoft.Json.Linq;
using System;

namespace TripAssistantSearchEngineApi
{
    public class Activity
    {
        public string Name { get; set; }
        public JArray PhotosUrl { get; set; }
        public double Rating { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public JArray AvailableDays { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
