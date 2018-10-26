using System;
using Core.Contracts;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TripAssistantSearchEngineApi
{
    public class SingleActivityProvider : ISingleActivityProvider
    {
        private readonly ISingleActivityTranslator _singleActivityTranslator;
        public SingleActivityProvider(ISingleActivityTranslator singleActivityTranslator)
        {
            _singleActivityTranslator = singleActivityTranslator;
        }
        public List<Activity> GetListWithSingleAttraction(string location, string activity)
        {
            List<Activity> combinedResultsFromApi = FetchDataFromAllAPIs(location, activity);
            return combinedResultsFromApi;
        }
        public List<Activity> FetchDataFromAllAPIs(string location, string activity)
        {
            List<Activity> finalList = new List<Activity>();
            string origActivity = activity;
            activity += " places";
            activity = activity.Replace(" ", "%20");
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=50000&keyword="+ activity + "&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
            using (WebClient wc = new WebClient())
            {
                var jobject = wc.DownloadString(url);
                JObject result = JsonConvert.DeserializeObject<JObject>(jobject);
                if (result != null)
                {
                    Activity activity1 = new Activity();
                    activity1.Type = origActivity;
                    activity1.ListActivity = _singleActivityTranslator.GetFilteredActivity(result);
                    finalList.Add(activity1);
                }
            }
            return finalList;
        }
    }
}
