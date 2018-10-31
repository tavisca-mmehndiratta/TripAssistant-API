using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class ActivityTranslator : IActivityTranslator
    {
        readonly string url = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=";

        public ActivityDetails GetFilteredPlace(JObject jObject)
        {
            ActivityDetails activityDetails = new ActivityDetails();
            var results = jObject["result"].Value<JObject>();
            activityDetails.Address = results["formatted_address"].Value<String>();
            activityDetails.Phone = results["international_phone_number"].Value<String>();
            activityDetails.Website = results["website"].Value<String>();
            List<string> list = new List<string>();
            var openingHours = results["opening_hours"].Value<JObject>();
            var listArray = openingHours["weekday_text"].Value<JArray>();
            foreach(string res in listArray)
            {
                list.Add(res);
            }
            activityDetails.OpeningHours = list;
            return activityDetails;
        }
        public List<ActivityList> GetFilteredActivity(JObject activityjObject)
        {
            List<ActivityList> tempData = new List<ActivityList>();
            ActivityList activityList1 = new ActivityList();
            
                var results = activityjObject["results"].Value<JArray>();
                foreach (JObject res in results)
                {
                    activityList1 = SingleFilteredResult(res);
                    tempData.Add(activityList1);  
                }
            return tempData;
        }
        public ActivityList SingleFilteredResult(JObject res)
        {
            ActivityList activity = new ActivityList();
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
