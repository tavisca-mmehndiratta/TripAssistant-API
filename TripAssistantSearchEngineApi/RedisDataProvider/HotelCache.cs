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
        static ConfigurationOptions option = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            EndPoints = { "localhost" }
        };
        public ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(option);
        public List<Hotel> GetHotelsFromCache (string city)
        {
            List<Hotel> data = new List<Hotel>();
            try
            {
                IDatabase db = redis.GetDatabase();
                city += "h";
                string val = db.StringGet(city);
                if (val == null)
                {
                    return null;
                }
                data = JsonConvert.DeserializeObject<List<Hotel>>(val);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                data = null;
            }
            return data;
        }
        public void InsertHotelsInCache(List<Hotel> hotel, string city)
        {
            try
            {
                IDatabase db = redis.GetDatabase();
                string data = JsonConvert.SerializeObject(hotel);
                city += "h";
                db.StringSet(city, data, TimeSpan.FromHours(1));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
