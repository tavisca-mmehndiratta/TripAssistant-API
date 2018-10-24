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
        private readonly IHotelTranslator _hotelTranslator;
        
        private readonly string _startFieldOfUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
        private readonly string _endFieldOfUrl = "&radius=100000&type=hotels&keyword=hotels&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
        public HotelsApi(IHotelTranslator hotelTranslator)
        {
            _hotelTranslator = hotelTranslator;
        }
        public async Task<List<Hotel>> GetHotelDetails(string queryString, string city)
        {
            string url = "";
            List<Hotel> translatedHotelResult = new List<Hotel>();
            JObject data = new JObject();
            try
            {
                data = null;
                using (var client = new WebClient())
                {
                    url = _startFieldOfUrl + queryString + _endFieldOfUrl;
                    var jsonPrediction = await client.DownloadStringTaskAsync(url);
                    data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                    translatedHotelResult = _hotelTranslator.GetFilteredHotel(data);                   
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                translatedHotelResult = null;
            }
            return translatedHotelResult;
        }
    }        
}

