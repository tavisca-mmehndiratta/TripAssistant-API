using Newtonsoft.Json.Linq;
using System;

namespace TripAssistantSearchEngineApi
{
    public class CoreResponseGenerator : ICoreResponseGenerator
    {
        CoreResponse coreResponse = new CoreResponse();
        public CoreResponse MakeResponse(JObject hotels, JObject activities, string type, string res)
        {
            coreResponse.HotelList = hotels;
            coreResponse.ActivityList = activities;
            coreResponse.Type = type;
            coreResponse.ResponseQuery = res;
            return coreResponse;
        }
    }
    public interface ICoreResponseGenerator
    {
        CoreResponse MakeResponse(JObject hotels, JObject activities, string type, string res);
    }
}
