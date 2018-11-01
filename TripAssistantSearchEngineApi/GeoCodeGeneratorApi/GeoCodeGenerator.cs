using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using GoogleMaps.LocationServices;
using Core.Contracts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class GeoCodeGenerator : IGeoCodeGenerator
    {
        private readonly WebClient _webClient;
        private readonly AppSetting _appSetting;
        public GeoCodeGenerator(IOptions<AppSetting> appSetting,WebClient webClient)
        {
            _webClient = webClient;
            _appSetting = appSetting.Value;
        }
        public List<double> GetGeoCode(string location)
        {
            List<double> locations = new List<double>();
            var locationService = new GoogleLocationService(apikey: _appSetting.ApiKey);
            var Point = locationService.GetLatLongFromAddress(location);
            locations.Add(Point.Latitude);
            locations.Add(Point.Longitude);
            return locations;
        }
        public async Task<List<double>> GetGeoLocation(string url)
        {
            int count = 0;
            List<double> locations = new List<double>();
            JObject location, geometry;
            double latitude = 0;
            double longitude = 0;
            try
            {
                string jsonPrediction = await _webClient.DownloadStringTaskAsync(url);
                var data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                var results = data["results"].Value<JArray>();
                foreach (JObject res in results)
                {
                    count++;
                    geometry = res["geometry"].Value<JObject>();
                    location = geometry["location"].Value<JObject>();
                    latitude += location["lat"].Value<Double>();
                    longitude += location["lng"].Value<Double>();
                }
            }
            catch(Exception e)
            {
                latitude = 0;
                longitude = 0;
                count = 0;
            }
            locations.Add(latitude);
            locations.Add(longitude);
            locations.Add(count);
            return locations;
        }
    }
   
   
}
