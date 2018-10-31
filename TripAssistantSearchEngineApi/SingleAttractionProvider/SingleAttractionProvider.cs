using System;
using System.Collections.Generic;
using Core.Contracts;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TripAssistantSearchEngineApi
{
    public class SingleAttractionProvider : ISingleAttractionProvider
    {
        private readonly ISingleAttractionTranslator _singleAttractionTranslator;
        UrlBuilderModel builderModel = new UrlBuilderModel();
        public SingleAttractionProvider(ISingleAttractionTranslator singleAttractionTranslator)
        {
            _singleAttractionTranslator = singleAttractionTranslator;
        }
        public List<Activity> GetListWithSingleAttraction(string location, string attraction)
        {
            List<Activity> combinedResultsFromApi = FetchDataFromAllAPIs(location,attraction);
            return combinedResultsFromApi;
        }
        public List<Activity> FetchDataFromAllAPIs(string location, string attraction)
        {
            List<Activity> finalList = new List<Activity>();
            attraction = attraction.Replace(" ", "%20");
            Task<List<Activity>> totalResponse = FetchSerializedData(location,attraction);
            while (totalResponse.Result.Count != 4) ;
            string url = builderModel.baseUri + location + "&radius=100&keyword=" + attraction + "&key=" + builderModel.apiKey;
            using (WebClient wc = new WebClient())
            {
                var jobject = wc.DownloadString(url);
                JObject result = JsonConvert.DeserializeObject<JObject>(jobject);
               if (result != null)
               {
                   Activity activity = new Activity();
                   activity.Type = attraction.Replace("%20", " ");
                   activity.ListActivity = _singleAttractionTranslator.GetFilteredActivity(result);
                   finalList.Add(activity);
               }
            }
            for(int i = 0; i < totalResponse.Result.Count; i++)
            {
                finalList.Add(totalResponse.Result[i]);
            }
            return finalList;
        }
        //Calling multiple apis parallely 
        private async Task<List<Activity>> FetchSerializedData(string location,string attraction)
        {
            List<Activity> searlizedResponse = new List<Activity>();
            string url;
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    Parallel.Invoke
                        (new ParallelOptions() { MaxDegreeOfParallelism = 1 },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=18000&keyword=amusement%20parks&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Activity activity = new Activity
                                            {
                                                Type = "amusement_park",
                                                ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=18000&keyword=museum&key=" + builderModel.apiKey;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Activity activity = new Activity
                                            {
                                                Type = "museum",
                                                ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=18000&keyword=activity&key=" + builderModel.apiKey;
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
                                                ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = builderModel.baseUri + location + "&radius=18000&keyword=art%20gallery&key=" + builderModel.apiKey;
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
                                                ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                            };
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            });

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
