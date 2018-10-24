using System;
using Core.Contracts;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class UserDetailsProvider : IUserDetailsProvider
    {
        public async Task<List<Activities>> GetUsersPreferences(string url)
        {
            List<Activities> userPref = new List<Activities>();
            StaticUserTypes userPreference = new StaticUserTypes();
            using (WebClient client = new WebClient())
            {
                string jsonPrediction = await client.DownloadStringTaskAsync(url);
                JObject activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                Activities activities = new Activities();               
                activities.Type = "activity";
                activities.Points = activityResult["activity"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "amusement_park";
                activities.Points = activityResult["amusement_park"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "adventures";
                activities.Points = activityResult["adventures"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "aquarium";
                activities.Points = activityResult["aquarium"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "art_gallery";
                activities.Points = activityResult["art_gallery"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "attractions";
                activities.Points = activityResult["attractions"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "church";
                activities.Points = activityResult["church"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "hindu_temple";
                activities.Points = activityResult["hindu_temple"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "mosque";
                activities.Points = activityResult["mosque"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "museum";
                activities.Points = activityResult["museum"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "natural_feature";
                activities.Points = activityResult["natural_feature"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "park";
                activities.Points = activityResult["park"].Value<int>();
                userPref.Add(activities);                
                activities = new Activities();
                activities.Type = "shopping_mall";
                activities.Points = activityResult["shopping_mall"].Value<int>();
                userPref.Add(activities);
                activities = new Activities();
                activities.Type = "zoo";
                activities.Points = activityResult["zoo"].Value<int>();
                userPref.Add(activities);               
            }
            return userPref;
        }
    }
    
}
