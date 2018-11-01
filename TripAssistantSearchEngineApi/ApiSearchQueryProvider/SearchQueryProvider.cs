using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TripAssistantSearchEngineApi
{
    public class SearchQueryProvider: ISearchQueryProvider
    {
        public string GetSearchQuery(int duration, double distance)
        {
            string url = "";
            JObject result=new JObject();
            try
            {
                using (var client = new WebClient())
                {
                    url = String.Format("http://172.16.14.216:5000/getDataForTraining?distance={0}&duration={1}", distance, duration);
                    var jobject = client.DownloadString(url);
                    return jobject;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

           return "NoProvidedApi";
        }
    }

}
