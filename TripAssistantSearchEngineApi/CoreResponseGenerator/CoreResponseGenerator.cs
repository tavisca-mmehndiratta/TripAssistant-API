using Core.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class CoreResponseGenerator : ICoreResponseGenerator
    {
        Response coreResponse = new Response();
        public Response MakeResponse(string input,string selected,List<Hotel> hotels, List<Activity> activities, string type, string res)
        {
            if (hotels != null)
            {
                coreResponse.HotelList = hotels;
            }
            else
            {
                coreResponse.HotelList = null;
            }
            coreResponse.ActivityList = activities;
            coreResponse.Selected = selected;
            coreResponse.Type = type;
            coreResponse.ResponseQuery = res;
            return coreResponse;
        }
    }
    
}
