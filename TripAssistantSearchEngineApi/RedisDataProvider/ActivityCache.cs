
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripAssistantSearchEngineApi
{
    public class ActivityCache: IActivityCache
    {
        static ConfigurationOptions option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = { "localhost" }
        };
        public ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(option);
    public List<Activity> GetActivitiesFromCache(string city)
        {
            List<Activity> data = new List<Activity>();
            try
            {
                IDatabase db = redis.GetDatabase();
                string val = db.StringGet(city);
                if (val == null)
                {
                    return null;
                }
                data = JsonConvert.DeserializeObject<List<Activity>>(val);
            }
            catch(Exception e)
            {
                data = null;
            }
            return data;
        }
        public void InsertActivitiesInCache(List<Activity> activity, string city)
        {
            try
            {
                IDatabase db = redis.GetDatabase();
                string data = JsonConvert.SerializeObject(activity);
                db.StringSet(city, data, TimeSpan.FromHours(1));
            }
            catch(Exception e)
            {
            }
        }      
    }    
}

