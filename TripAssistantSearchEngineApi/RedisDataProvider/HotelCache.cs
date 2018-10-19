using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripAssistantSearchEngineApi
{
    public class HotelCache: IHotelCache
    {
        public ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");


        public JObject GetHotelsFromCache (string city)
        {

            IDatabase db = redis.GetDatabase();
           
            string val = db.StringGet(city);
            if (val == null)
            {
                return null;
            }

            JObject ob = JsonConvert.DeserializeObject<JObject>(val);



            //Console.WriteLine (val);

            return ob;
        }

        public void InsertHotelsInCache(JObject hotel, string city)
        {

            IDatabase db = redis.GetDatabase();
            string data = JsonConvert.SerializeObject(hotel);

            if (db.StringSet(city, data))
            {
                var val = db.StringGet(city);

                Console.WriteLine(val);
            }
        }
    }

    
}
