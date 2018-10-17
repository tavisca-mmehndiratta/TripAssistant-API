using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ActivityApi : IActivityApi
    {
        JObject activityResult;
        public JObject GetActivities(string location)
        {
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+location+"&radius=300000&keyword=amusement%20parks&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
            using (WebClient client = new WebClient()) {
                var jsonPrediction = client.DownloadString(url);
                activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
            }
            return activityResult;
        }

        public JObject GetActivitiesByPlaceId(string placeId)
        {
            string url = "https://maps.googleapis.com/maps/api/place/details/json?place_id=" + placeId + "&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
            using (WebClient client = new WebClient())
            {
                var jsonPrediction = client.DownloadString(url);
                activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
            }
            return activityResult;
        }
    }
    public interface IActivityApi
    {
        JObject GetActivities(string location);
        JObject GetActivitiesByPlaceId(string placeId);
    }
}
