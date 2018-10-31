using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IActivityCache
    {
        List<Activity> GetActivitiesFromCache(string city);
        void InsertActivitiesInCache(List<Activity> activity, string city);

    }
}
