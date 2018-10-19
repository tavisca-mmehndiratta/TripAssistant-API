using Core.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class CoreResponseGenerator : ICoreResponseGenerator
    {
        Response coreResponse = new Response();
        public Response MakeResponse(string input,List<Hotel> hotels, List<Activity> activities, string type, string res)
        {
            coreResponse.HotelList = hotels;
            coreResponse.ActivityList = activities;
            coreResponse.Type = type;
            coreResponse.ResponseQuery = res;
            return coreResponse;
        }
    }
    
}
