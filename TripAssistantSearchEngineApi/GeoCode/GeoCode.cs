using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class GeoCode : IGeoCode
    {
        List<double> locations = new List<double>();
        string geoCode = "";
        private readonly AppSetting _appSetting;
        private readonly IGeoCodeGenerator _geoCodeGenerator;

        public GeoCode(IOptions<AppSetting> appSetting,IGeoCodeGenerator geoCodeGenerator)
        {
            _geoCodeGenerator = geoCodeGenerator;
            _appSetting = appSetting.Value;
        }

        public string GetCumulativeGeoCode(string geocode)
        {
            geoCode = "";
            try
            {
                string url = _appSetting.GoogleActivityBaseUrl + geocode + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=point%20of%20interest&key=" + _appSetting.ApiKey;
                Task<List<double>> location = _geoCodeGenerator.GetGeoLocation(url);
                locations = location.Result;
                geoCode += (locations[0] / locations[2]).ToString() + ",";
                geoCode += (locations[1] / locations[2]).ToString();
            }
            catch(Exception e)
            {
                geocode = "0,0";
            }
            return geoCode;
        }

        public string GetGeoCodeOfCity(string city)
        {
            geoCode = "";
            try
            {
                List<double> location = _geoCodeGenerator.GetGeoCode(city);
                geoCode += location[0].ToString() + "," + location[1].ToString();
            }
            catch(Exception e)
            {
                geoCode = "0,0";
            }
            return geoCode;
        }
    }
    
}
