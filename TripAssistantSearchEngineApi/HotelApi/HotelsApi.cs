using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class HotelsApi : IHotelApi
    {
        private IHotelTranslator _hotelTranslator;
        private IHotelCache _hotelCache;
        public HotelsApi(IHotelTranslator hotelTranslator, IHotelCache hotelCache)
        {
            _hotelCache = hotelCache;
            _hotelTranslator = hotelTranslator;
        }
        public List<Hotel> GetHotelDetails(string queryString, string city)
        {
            string url = "";
            List<Hotel> translatedHotelResult = new List<Hotel>();
            JObject data = new JObject();
            using (var client = new WebClient())
            {
                data = _hotelCache.GetHotelsFromCache(city);
                if (data != null)
                {
                    translatedHotelResult = _hotelTranslator.GetFilteredHotel(data);
                }
                else
                {
                    url = "  https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + queryString + "&radius=100000&type=hotels&keyword=hotels&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";

                    var jsonPrediction = client.DownloadString(url);

                    data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                    _hotelCache.InsertHotelsInCache(data, city);
                    translatedHotelResult = _hotelTranslator.GetFilteredHotel(data);

                }
                return translatedHotelResult;
            }
        }
    }
        
    }

