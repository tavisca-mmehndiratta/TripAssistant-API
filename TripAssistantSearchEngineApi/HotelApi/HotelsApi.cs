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

        UrlBuilderModel urlBuilder = new UrlBuilderModel();
        public HotelsApi(IHotelTranslator hotelTranslator)
        {
            _hotelTranslator = hotelTranslator;
        }
        public async Task<List<Hotel>> GetHotelDetails(string queryString)
        {
            string url = "";
            List<Hotel> translatedHotelResult = new List<Hotel>();
            JObject data = new JObject();
            try
            {
                data = null;
                using (var client = new WebClient())
                {
                    url = urlBuilder.baseUri + queryString + "&radius=100000&type=hotels&keyword=hotels&key=" + urlBuilder.apiKey;
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

