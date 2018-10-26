using Core.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class UserPreferencesService : IUserPreferenceService
    {
        List<Activities> activities = new List<Activities>();
        private readonly IUserDetailsProvider _userDetailsProvider;
        public UserPreferencesService(IUserDetailsProvider userDetailsProvider)
        {
            _userDetailsProvider = userDetailsProvider;
        }
        public List<Activity> GetFilteredResultsBasedOnUserPreferences(List<Activity> activityList)
        {
            List<Activity> filteredActivityResult = new List<Activity>();
            string url = "http://tripassistant-user.ap-south-1.elasticbeanstalk.com/api/User?id=Mohit";
            Task<List<Activities>> prefrences = _userDetailsProvider.GetUsersPreferences(url);
            activities = prefrences.Result;
            List<Activities> list =  activities.OrderByDescending(x => x.Points).ToList();
            foreach(Activities activitie in list)
            {
                foreach(Activity activity in activityList)
                {
                    if (activity.Type.Equals(activitie.Type))
                    {
                        filteredActivityResult.Add(activity);
                    }
                }
            }
            return SortResultsBasedOnPastExperience(filteredActivityResult);
        }
        public List<Activity> SortResultsBasedOnPastExperience(List<Activity> activityList)
        {
            List<Activity> filteredActivityResult = new List<Activity>();
            string url = "http://tripassistant-user.ap-south-1.elasticbeanstalk.com/api/PastExperience?id=Mohit";
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
            return filteredActivityResult;
        }
    }
}