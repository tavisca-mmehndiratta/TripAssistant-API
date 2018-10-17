using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class HotelsApi : IHotelApi
    {
        public JObject GetHotelDetails(string queryString)
        {
            string url = "";
            JObject data = new JObject();
            using (var client = new WebClient())
            {
                url = "  https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + queryString + "&radius=100000&type=hotels&keyword=hotels&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";

                var jsonPrediction = client.DownloadString(url);

                data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);

            }
            return data;
        }
    }
    public interface IHotelApi
    {
        JObject GetHotelDetails(string queryString);
    }
}
