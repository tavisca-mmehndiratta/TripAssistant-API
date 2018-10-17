using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace TripAssistantSearchEngineApi
{
    public class GeoCodeGenerator : IGeoCodeGenerator
    {
        public List<double> GetGeoLocation(string url)
        {
            int count = 0;
            List<double> locations = new List<double>();
            JObject value, location, geometry;
            double latitude = 0;
            double longitude = 0;

            using (var client = new WebClient())
            {
                var jsonPrediction = client.DownloadString(url);
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
            locations.Add(latitude);
            locations.Add(longitude);
            locations.Add(count);
            return locations;
        }
    }
    public interface IGeoCodeGenerator
    {
        List<double> GetGeoLocation(string url);
    }
   
}
