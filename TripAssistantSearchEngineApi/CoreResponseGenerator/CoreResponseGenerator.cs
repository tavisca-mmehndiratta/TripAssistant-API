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
        public Response MakeResponse(string input,List<Activity> selected,List<Hotel> hotels, List<Activity> activities, string type, string res)
        {
            try
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
                coreResponse.Request = input;
                coreResponse.Type = type;
                coreResponse.ResponseQuery = res;
                return coreResponse;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
    
}
