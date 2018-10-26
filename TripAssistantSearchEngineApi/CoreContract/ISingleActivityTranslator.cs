using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ISingleActivityTranslator
    {
        List<ActivityList> GetFilteredActivity(JObject activityjObject);
    }
}
