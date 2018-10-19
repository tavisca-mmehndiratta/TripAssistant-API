using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IActivityApi
    {
        List<Activity> GetActivities(string location, string city);
        JObject GetActivitiesByPlaceId(string placeId);
    }
}
