using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Options;
using System.Text;

namespace TripAssistantSearchEngineApi
{
    public class SearchQueryProvider: ISearchQueryProvider
    {
        private readonly AppSetting _appSetting;
        private readonly WebClient _webClient;
        public SearchQueryProvider(WebClient webClient,IOptions<AppSetting> appSetting)
        {
            _webClient = webClient;
            _appSetting = appSetting.Value;
        }
        public string GetSearchQuery(int duration, double distance)
        {
            string url = "";
            JObject result=new JObject();
            try
            {
                url = String.Format(_appSetting.ClassificationAlgoBaseUrl, distance, duration);
                var jobject = _webClient.DownloadString(url);
                return jobject;
            }
            catch (Exception e)
            {
            }

           return "NoProvidedApi";
        }
    }

}
