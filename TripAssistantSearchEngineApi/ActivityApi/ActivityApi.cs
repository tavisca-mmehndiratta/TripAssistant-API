using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ActivityApi : IActivityApi
    {
        JObject activityResult;
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

        public async Task<JObject> GetActivitiesByPlaceId(string placeId)
        {
            try
            {
                string url = "https://maps.googleapis.com/maps/api/place/details/json?place_id=" + placeId + "&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                using (WebClient client = new WebClient())
                {
                    string jsonPrediction = await client.DownloadStringTaskAsync(url);
                    activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                activityResult = null;

            }
            return activityResult;
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
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=things%20to%20do&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "activity";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=attractions&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "attractions";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=amusement%20parks&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "amusement_park";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=aquarium&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "aquarium";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=art%20gallery&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "art_gallery";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=church&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "church";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=hindu%20temple&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "hindu_temple";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=mosque&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "mosque";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=museum&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "museum";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=park&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "park";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=shopping%20mall&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "shopping_mall";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=zoo&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "zoo";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=natural%20feature&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "natural_feature";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=38000&keyword=adventures&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Core.Contracts.Activity activity = new Core.Contracts.Activity();
                                            activity.Type = "adventures";
                                            activity.ListActivity = _activityTranslator.GetFilteredActivity(result);
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
