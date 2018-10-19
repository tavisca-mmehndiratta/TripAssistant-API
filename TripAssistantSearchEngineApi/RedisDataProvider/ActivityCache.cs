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
            public ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");


            public List<JObject> GetActivitiesFromCache(string city)
            {

                IDatabase db = redis.GetDatabase();
               
                string val = db.StringGet(city);
                if (val == null)
                {
                    return null;
                }

                List<JObject> ob = JsonConvert.DeserializeObject<List<JObject>>(val);



                //Console.WriteLine (val);

                return ob;
            }

            public void InsertActivitiesInCache(List<JObject> activity, string city)
            {

                IDatabase db = redis.GetDatabase();
                string data = JsonConvert.SerializeObject(activity);

                if (db.StringSet(city, data))
                {
                    var val = db.StringGet(city);

                    Console.WriteLine(val);
                }
            }
        public void Remove(string key)
        {
            IDatabase db = redis.GetDatabase();
            db.KeyDelete(key);

        }
            
    }

    
}

