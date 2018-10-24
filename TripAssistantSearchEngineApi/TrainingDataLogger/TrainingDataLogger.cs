using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace TripAssistantSearchEngineApi
{
    public class TrainingDataLogger : ITrainingDataLogger
    {
        public void InsertValuesIntoTrainingData(string request, string response)
        {
            Uri uri = new Uri("http://172.16.14.216:54004/api/TrainingData");
            TrainingData trainingData = new TrainingData();
            trainingData.Request = request;
            trainingData.Response = response;
            string data = JsonConvert.SerializeObject(trainingData);
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var result = client.UploadString(uri,data);
        }
    }

    
}
