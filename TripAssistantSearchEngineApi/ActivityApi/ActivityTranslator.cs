using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class ActivityTranslator : IActivityTranslator
    {
        readonly string url = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=";

        public List<Activity> GetFilteredActivity(List<JObject> activityjObject)
        {
            List<Activity> activityList = new List<Activity>();
            Activity activity = null;
            
            foreach (JObject rest in activityjObject)
            {
                var results = rest["results"].Value<JArray>();
                foreach(JObject res in results)
                {
                    activity = SingleFilteredResult(res);
                    activityList.Add(activity);
                } 
            }
            return activityList;
        }
        public Activity SingleFilteredResult(JObject res)
        {
            Activity activity = new Activity();
            try {
                
                JObject activityDetails;
                string placeId = res["place_id"].Value<String>();
                activityDetails = res;
                activity.Name = activityDetails["name"].Value<String>();
                if (activityDetails["rating"] != null)
                {
                    activity.Rating = activityDetails["rating"].Value<Double>();
                }
                JObject photo = null;
                JObject geometry = activityDetails["geometry"].Value<JObject>();
                JObject location = geometry["location"].Value<JObject>();
                activity.Lattitude = location["lat"].Value<Double>();
                activity.Longitude = location["lng"].Value<Double>();
                if (activityDetails["photos"] != null)
                {
                    JArray photoArray = res["photos"].Value<JArray>();
                    photo = photoArray[0].Value<JObject>();
                    activity.PhotoUrl = url + photo["photo_reference"].Value<String>() + "&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
                }
                JArray types = new JArray();
                types = activityDetails["types"].Value<JArray>();
                activity.Type = (String)types[0];
                activity.PlaceId = placeId;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                activity = null;
            }
            return activity;
        }

    }
    
}
