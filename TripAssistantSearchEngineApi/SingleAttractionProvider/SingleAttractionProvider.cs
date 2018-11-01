using System;
using System.Collections.Generic;
using Core.Contracts;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TripAssistantSearchEngineApi
{
    public class SingleAttractionProvider : ISingleAttractionProvider
    {
        private readonly WebClient _webClient;
        private readonly AppSetting _appSetting;
        private readonly ISingleAttractionTranslator _singleAttractionTranslator;
        UrlBuilderModel builderModel = new UrlBuilderModel();
        public SingleAttractionProvider(IOptions<AppSetting> appSetting,WebClient webClient,ISingleAttractionTranslator singleAttractionTranslator)
        {
            _singleAttractionTranslator = singleAttractionTranslator;
            _webClient = webClient;
            _appSetting = appSetting.Value;
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
            try
            {
                string url = _appSetting.GoogleActivityBaseUrl + location + "&radius=100&keyword=" + attraction + "&key=" + _appSetting.ApiKey;
                var jobject = _webClient.DownloadString(url);
                JObject result = JsonConvert.DeserializeObject<JObject>(jobject);
                if (result != null)
                {
                    Activity activity = new Activity
                    {
                        Type = attraction.Replace("%20", " "),
                        ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                    };
                    finalList.Add(activity);
                }
                for (int i = 0; i < totalResponse.Result.Count; i++)
                {
                    finalList.Add(totalResponse.Result[i]);
                }
                return finalList;
            }
            catch(Exception e)
            {
                return null;
            }
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
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusSingleAttraction + "&keyword=amusement%20parks&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Activity activity = new Activity
                                    {
                                        Type = "amusement_park",
                                        ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusSingleAttraction + "&keyword=museum&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Activity activity = new Activity
                                    {
                                        Type = "museum",
                                        ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusSingleAttraction + "&keyword=things%20to%20do&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Activity activity = new Activity
                                    {
                                        Type = "activity",
                                        ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            },
                            () =>
                            {
                                url = _appSetting.GoogleActivityBaseUrl + location + "&radius=" + _appSetting.RadiusSingleAttraction + "&keyword=art%20gallery&key=" + _appSetting.ApiKey;
                                string serializedResult = _webClient.DownloadString(url);
                                JObject result = JsonConvert.DeserializeObject<JObject>(serializedResult);
                                if (result != null)
                                {
                                    Activity activity = new Activity
                                    {
                                        Type = "art_gallery",
                                        ListActivity = _singleAttractionTranslator.GetFilteredActivity(result)
                                    };
                                    searlizedResponse.Add(activity);
                                }
                            });

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
