using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IActivityApi
    {
        List<Activity> GetActivities(string location, string city);
        JObject GetActivitiesByPlaceId(string placeId);
    }
}
