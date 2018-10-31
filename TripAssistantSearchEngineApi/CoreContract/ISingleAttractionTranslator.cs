using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ISingleAttractionTranslator
    {
        List<ActivityList> GetFilteredActivity(JObject activityjObject);
    }
}
