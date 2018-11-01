using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace TripAssistantSearchEngineApi
{
    public class ActivityApi : IActivityApi
    {
        JObject activityResult;
        UrlBuilderModel builderModel = new UrlBuilderModel();
        private readonly IActivityTranslator _activityTranslator;
        public ActivityApi(IActivityTranslator activityTranslator)
        {
            _activityTranslator = activityTranslator;
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
                string url = builderModel.baseUriPlaceId + placeId + "&key=" + builderModel.apiKey;
                using (WebClient client = new WebClient())
                {
                    //TODO: change name of the variable
                    string jsonPrediction = await client.DownloadStringTaskAsync(url);
                    activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                    activityDetails = _activityTranslator.GetFilteredPlace(activityResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                activityResult = null;
            }
            return activityDetails;
        }
        public Task<List<Core.Contracts.Activity>> FetchDataFromAllAPIs(string location)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task<List<Core.Contracts.Activity>> totalResponse = FetchSerializedData(location);
            while (totalResponse.Result.Count != 13) ;
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
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=things%20to%20do&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "activity",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=attractions&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "attractions",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=amusement%20park&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "amusement_park",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=aquarium&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "aquarium",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=art%20gallery&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "art_gallery",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=church&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "church",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=hindu%20temple&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "hindu_temple",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=mosque&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "mosque",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=museum&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "museum",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=park&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "park",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=shopping%20mall&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "shopping_mall",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=zoo&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "zoo",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=natural%20feature&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "natural_feature",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=" + builderModel.radius + "&keyword=adventures&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity
                                            {
                                                Type = "adventures",
                                                ListActivity = _activityTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            }
                        );

                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                searlizedResponse = null;
            }
            return searlizedResponse;
        }
    }

}
