using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ISingleAttractionTranslator
    {
        List<ActivityList> GetFilteredActivity(JObject activityjObject);
    }
}
