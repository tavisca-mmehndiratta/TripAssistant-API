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
                Activities activities = new Activities
                {
                    Type = "activity",
                    Points = activityResult["activity"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "amusement_park",
                    Points = activityResult["amusement_park"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "adventures",
                    Points = activityResult["adventures"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "aquarium",
                    Points = activityResult["aquarium"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "art_gallery",
                    Points = activityResult["art_gallery"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "attractions",
                    Points = activityResult["attractions"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "church",
                    Points = activityResult["church"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "hindu_temple",
                    Points = activityResult["hindu_temple"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "mosque",
                    Points = activityResult["mosque"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "museum",
                    Points = activityResult["museum"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "natural_feature",
                    Points = activityResult["natural_feature"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "park",
                    Points = activityResult["park"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "shopping_mall",
                    Points = activityResult["shopping_mall"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "zoo",
                    Points = activityResult["zoo"].Value<int>()
                };
                userPref.Add(activities);               
            }
            return userPref;
        }
        public async Task<List<Activities>> GetUsersPastExperience(string url)
        {
            List<Activities> userPref = new List<Activities>();
            StaticUserTypes userPreference = new StaticUserTypes();
            using (WebClient client = new WebClient())
            {
                string jsonPrediction = await client.DownloadStringTaskAsync(url);
                JObject activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                Activities activities = new Activities
                {
                    Type = "activity",
                    Points = activityResult["activity"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "amusement_park",
                    Points = activityResult["amusement_park"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "adventures",
                    Points = activityResult["adventures"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "aquarium",
                    Points = activityResult["aquarium"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "art_gallery",
                    Points = activityResult["art_gallery"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "attractions",
                    Points = activityResult["attractions"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "church",
                    Points = activityResult["church"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "hindu_temple",
                    Points = activityResult["hindu_temple"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "mosque",
                    Points = activityResult["mosque"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "museum",
                    Points = activityResult["museum"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "natural_feature",
                    Points = activityResult["natural_feature"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "park",
                    Points = activityResult["park"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "shopping_mall",
                    Points = activityResult["shopping_mall"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "zoo",
                    Points = activityResult["zoo"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "angling",
                    Points = activityResult["angling"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "bamboo_rafting",
                    Points = activityResult["bamboo_rafting"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "biking",
                    Points = activityResult["biking"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "bunjee_jumping",
                    Points = activityResult["bunjee_jumping"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "cable_car",
                    Points = activityResult["cable_car"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "camel_safari",
                    Points = activityResult["camel_safari"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "camping",
                    Points = activityResult["camping"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "caving",
                    Points = activityResult["caving"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "kayaking",
                    Points = activityResult["kayaking"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "cliff_jumping",
                    Points = activityResult["cliff_jumping"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "climbing",
                    Points = activityResult["climbing"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "dune_bashing",
                    Points = activityResult["dune_bashing"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "flying_fox",
                    Points = activityResult["flying_fox"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "giant_swing",
                    Points = activityResult["giant_swing"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "heli_skiing",
                    Points = activityResult["heli_skiing"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "hot_air_balloon",
                    Points = activityResult["hot_air_balloon"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "microlight_flying",
                    Points = activityResult["microlight_flying"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "para_sailing",
                    Points = activityResult["para_sailing"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "paragliding",
                    Points = activityResult["paragliding"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "rafting",
                    Points = activityResult["rafting"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "river_rafting",
                    Points = activityResult["river_rafting"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "safari",
                    Points = activityResult["safari"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "scuba_diving",
                    Points = activityResult["scuba_diving"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "sightseeings",
                    Points = activityResult["sightseeings"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "sky_diving",
                    Points = activityResult["sky_diving"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "snorkelling",
                    Points = activityResult["snorkelling"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "surfing",
                    Points = activityResult["surfing"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "swimming",
                    Points = activityResult["swimming"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "trekking",
                    Points = activityResult["trekking"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "underwater_walk",
                    Points = activityResult["underwater_walk"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "waterfall",
                    Points = activityResult["waterfall"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "wildlife_safari",
                    Points = activityResult["wildlife_safari"].Value<int>()
                };
                userPref.Add(activities);
                activities = new Activities
                {
                    Type = "zorbing",
                    Points = activityResult["zorbing"].Value<int>()
                };
                userPref.Add(activities);
            }
            return userPref;
        }
    }
    
}
