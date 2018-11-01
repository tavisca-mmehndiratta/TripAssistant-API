using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;
using System.Diagnostics;
using Core.Contracts;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace TripAssistantSearchEngineApi
{
    public class ActivityApi : IActivityApi
    {
        JObject activityResult;
        private readonly WebClient _webClient;
        private readonly AppSetting _appSetting;
        UrlBuilderModel builderModel = new UrlBuilderModel();
        private readonly IActivityTranslator _activityTranslator;
        public ActivityApi(WebClient webClient, IActivityTranslator activityTranslator, IOptions<AppSetting> appSetting)
        {
            _activityTranslator = activityTranslator;
            _webClient = webClient;
            _appSetting = appSetting.Value;
        }
        //Getting activities from the api and sending the filtered response.
        public List<Core.Contracts.Activity> GetActivities(string location, string city)
        {
            Task<List<Core.Contracts.Activity>> combinedResultsFromApi = FetchDataFromAllAPIs(location);
            return combinedResultsFromApi.Result;
        }
        public async Task<ActivityDetails> GetActivitiesByPlaceId(string placeId)
        {
            ActivityDetails activityDetails = new ActivityDetails();
            try
            {
                string url = _appSetting.GooglePlaceIdBaseUrl + placeId + "&key=" + _appSetting.ApiKey;
                string jsonSerializedResult = await _webClient.DownloadStringTaskAsync(url);
                activityResult = (JObject)JsonConvert.DeserializeObject(jsonSerializedResult);
                activityDetails = _activityTranslator.GetFilteredPlace(activityResult);
            }
            catch (Exception e)
            {
                activityResult = null;
            }
            return activityDetails;
        }
        public Task<List<Core.Contracts.Activity>> FetchDataFromAllAPIs(string location)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task<List<Core.Contracts.Activity>> totalResponse = FetchSerializedData(location);
            while (totalResponse.Result.Count != 14) ;
            sw.Stop();
            return totalResponse;
        }
        //Calling multiple apis parallely 
        private async Task<List<Core.Contracts.Activity>> FetchSerializedData(string location)
        {
            List<Core.Contracts.Activity> searlizedResponse = new List<Core.Contracts.Activity>();
            string url;
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    Parallel.Invoke
                        (new ParallelOptions() { MaxDegreeOfParallelism = 1 },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=things%20to%20do&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "activity",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=attractions&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "attractions",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=amusement%20parks&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "amusement_park",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=aquarium&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "aquarium",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=art%20gallery&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "art_gallery",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=church&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "church",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=hindu%20temple&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "hindu_temple",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=mosque&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "mosque",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=museum&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "museum",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=park&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "park",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=shopping%20mall&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "shopping_mall",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=zoo&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "zoo",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=natural%20feature&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "natural_feature",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusActivityApi + "&keyword=adventures&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Core.Contracts.Activity activity = new Core.Contracts.Activity
                                    {
                                        Type = "adventures",
                                        ListActivity = _activityTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            }
                        );

                });
            }
            catch (Exception e)
            {
                searlizedResponse = null;
            }
            return searlizedResponse;
        }
    }

}
