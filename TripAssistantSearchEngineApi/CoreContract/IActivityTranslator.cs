using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IActivityTranslator
    {
        List<ActivityList> GetFilteredActivity(JObject activityjObject);
        ActivityDetails GetFilteredPlace(JObject jObject);
    }
}
