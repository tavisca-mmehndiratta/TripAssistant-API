using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.Extensions.Options;
using System.Net;

namespace TripAssistantSearchEngineApi
{
    public class TrainingDataLogger : ITrainingDataLogger
    {
        private readonly WebClient _webClient;
        private readonly AppSetting _appSetting;
        public TrainingDataLogger(IOptions<AppSetting> appSetting,WebClient webClient)
        {
            _webClient = webClient;
            _appSetting = appSetting.Value;
        }
        public void InsertValuesIntoTrainingData(string request, string response)
        {
            try
            {
                Uri uri = new Uri(_appSetting.UsersApiTrainingDataBaseUrl);
                TrainingData trainingData = new TrainingData
                {
                    Request = request,
                    Response = response
                };
                string data = JsonConvert.SerializeObject(trainingData);
                _webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var result = _webClient.UploadString(uri, data);
            }
            catch(Exception e)
            {

            }
        }
    }

    
}
