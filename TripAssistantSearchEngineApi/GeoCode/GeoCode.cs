using System;
using System.Collections.Generic;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class GeoCode : IGeoCode
    {
        List<double> locations = new List<double>();
        string geoCode = "";
        private IGeoCodeGenerator _geoCodeGenerator;
        public GeoCode(IGeoCodeGenerator geoCodeGenerator)
        {
            _geoCodeGenerator = geoCodeGenerator;
        }

        public string GetCumulativeGeoCode(string geocode)
        {
            geoCode = "";
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+geocode+"&radius=300000&keyword=point%20of%20interest&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
            locations = _geoCodeGenerator.GetGeoLocation(url);
            geoCode += (locations[0] / locations[2]).ToString()+ ",";
            geoCode += (locations[1] / locations[2]).ToString();
            return geoCode;
        }

        public string GetGeoCodeOfCity(string city)
        {
            geoCode = "";
            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + city + "&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
            locations = _geoCodeGenerator.GetGeoLocation(url);
            geoCode += locations[0].ToString() + ",";
            geoCode += locations[1].ToString();
            return geoCode;
        }
    }
    
}
