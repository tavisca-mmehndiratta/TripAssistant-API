using System;
using Core.Contracts;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TripAssistantSearchEngineApi
{
    public class SingleActivityProvider : ISingleActivityProvider
    {
        private readonly ISingleActivityTranslator _singleActivityTranslator;
        private readonly AppSetting _appSetting;
        UrlBuilderModel builderModel = new UrlBuilderModel();
        private readonly WebClient _webClient;
        public SingleActivityProvider(IOptions<AppSetting> appSetting,WebClient webClient,ISingleActivityTranslator singleActivityTranslator)
        {
            _singleActivityTranslator = singleActivityTranslator;
            _webClient = webClient;
            _appSetting = appSetting.Value;
        }
        public List<Activity> GetListWithSingleAttraction(string location, string activity)
        {
            List<Activity> combinedResultsFromApi = FetchDataFromAllAPIs(location, activity);
            return combinedResultsFromApi;
        }
        public List<Activity> FetchDataFromAllAPIs(string location, string activity)
        {
            List<Activity> finalList = new List<Activity>();
            try
            {
                string origActivity = activity;
                activity += " places";
                activity = activity.Replace(" ", "%20");
                string url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusSingleThing + "&keyword=" + activity + "&key=" + _appSetting.ApiKey;
                var jobject = _webClient.DownloadString(url);
                JObject result = JsonConvert.DeserializeObject<JObject>(jobject);
                if (result != null)
                {
                    Activity activity1 = new Activity();
                    activity1.Type = origActivity;
                    activity1.ListActivity = _singleActivityTranslator.GetFilteredActivity(result);
                    finalList.Add(activity1);
                }
                return finalList;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public Activity GetActivityForUserPastExperience(string geocode, string activity)
        {
            Activity finalActivity = new Activity();
            try
            {
                string origActivity = activity;
                activity += " places";
                activity = activity.Replace(" ", "%20");
                string url = _appSetting.GoogleActivityBaseUrl + geocode + "&radius=" + _appSetting.RadiusSingleThing + "&keyword=" + activity + "&key=" + _appSetting.ApiKey;
                var jobject = _webClient.DownloadString(url);
                JObject result = JsonConvert.DeserializeObject<JObject>(jobject);
                if (result != null)
                {
                    finalActivity.Type = origActivity;
                    finalActivity.ListActivity = _singleActivityTranslator.GetFilteredActivity(result);
                }
                return finalActivity;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
