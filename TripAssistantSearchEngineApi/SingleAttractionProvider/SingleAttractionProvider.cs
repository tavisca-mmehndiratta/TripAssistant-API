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
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=100&keyword=" + attraction + "&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
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
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=18000&keyword=amusement%20parks&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Activity activity = new Activity();
                                            activity.Type = "amuesement_park";
                                            activity.ListActivity = _singleAttractionTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=18000&keyword=museum&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) =>
                                    {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            Activity activity = new Activity();
                                            activity.Type = "museum";
                                            activity.ListActivity = _singleAttractionTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=18000&keyword=things%20to%20do&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
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
                                            activity.ListActivity = _singleAttractionTranslator.GetFilteredActivity(result);
                                            searlizedResponse.Add(activity);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=18000&keyword=art%20gallery&key=AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
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
                                            activity.ListActivity = _singleAttractionTranslator.GetFilteredActivity(result);
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
