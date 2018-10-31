using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ISingleActivityTranslator
    {
        List<ActivityList> GetFilteredActivity(JObject activityjObject);
    }
}
