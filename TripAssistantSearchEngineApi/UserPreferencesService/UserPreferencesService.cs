using Core.Contracts;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TripAssistantSearchEngineApi
{
    public class UserPreferencesService : IUserPreferenceService
    {
        List<Activities> activities = new List<Activities>();
        private readonly AppSetting _appSetting;
        private readonly IUserDetailsProvider _userDetailsProvider;
        private readonly ISingleActivityProvider _singleActivityProvider;
        public UserPreferencesService(IOptions<AppSetting> appSetting,ISingleActivityProvider singleActivityProvider,IUserDetailsProvider userDetailsProvider)
        {
            _singleActivityProvider = singleActivityProvider;
            _userDetailsProvider = userDetailsProvider;
            _appSetting = appSetting.Value;
        }
        public List<Activity> GetFilteredResultsBasedOnUserPreferences(string geoCode, List<Activity> activityList)
        {
            List<Activity> filteredActivityResult = new List<Activity>();
            string url = _appSetting.UsersPreferencesBaseUrl;
            try
            {
                Task<List<Activities>> prefrences = _userDetailsProvider.GetUsersPreferences(url);
                activities = prefrences.Result;
                List<Activities> list = activities.OrderByDescending(x => x.Points).ToList();
                foreach (Activities activitie in list)
                {
                    foreach (Activity activity in activityList)
                    {
                        if (activity.Type.Equals(activitie.Type))
                        {
                            filteredActivityResult.Add(activity);
                        }
                    }
                }
                return SortResultsBasedOnPastExperience(geoCode, filteredActivityResult);
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public List<Activity> SortResultsBasedOnPastExperience(string geoCode, List<Activity> activityList)
        {
            List<string> activityStatic = new List<string>()
            {
                "angling",
                "bamboo_rafting",
                "trekking",
                "paraglidng",
                "river_rafting",
                "sky_diving",
                "scuba_diving",
                "hot_air_balloon",
                "waterfall",
                "cliff_jumping",
                "heli_skiing",
                "caving",
                "snorkelling",
                "rafting",
                "bunjee_jumping",
                "swimming",
                "climbing",
                "biking",
                "sightseeings",
                "wildlife_safari",
                "underwater_walk",
                "kayaking",
                "surfing",
                "zorbing",
                "microlight_flying",
                "para_sailing",
                "camping",
                "dune_bashing",
                "safari",
                "camel_safari",
                "flying_fox",
                "giant_swing",
                "cable_car"
            };
            List<Activity> filteredActivityResult = new List<Activity>();
            string url = _appSetting.UsersPastExperienceBaseUrl;
            try
            {
                Task<List<Activities>> prefrences = _userDetailsProvider.GetUsersPastExperience(url);
                activities = prefrences.Result;
                List<Activities> list = activities.OrderByDescending(x => x.Points).ToList();
                foreach (Activities activitie in list)
                {
                    int flag = 0;
                    foreach (Activity activity in activityList)
                    {
                        if (activity.Type.Equals(activitie.Type))
                        {
                            flag = 1;
                            filteredActivityResult.Add(activity);
                        }
                    }
                    if (flag == 0)
                    {
                        if (activitie.Points > 0)
                        {
                            if (activityStatic.Contains(activitie.Type))
                            {
                                string act = activitie.Type;
                                act = activitie.Type.Replace("_", " ");
                                Activity activity = _singleActivityProvider.GetActivityForUserPastExperience(geoCode, act);
                                filteredActivityResult.Add(activity);
                            }
                        }
                    }
                }
                return filteredActivityResult;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}