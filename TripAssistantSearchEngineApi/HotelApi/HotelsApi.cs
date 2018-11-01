using Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class HotelsApi : IHotelApi
    {
        private readonly IHotelTranslator _hotelTranslator;
        private readonly WebClient _webClient;
        private readonly AppSetting _appSetting;
        UrlBuilderModel urlBuilder = new UrlBuilderModel();
        public HotelsApi(IOptions<AppSetting> appSetting,WebClient webClient,IHotelTranslator hotelTranslator)
        {
            _hotelTranslator = hotelTranslator;
            _webClient = webClient;
            _appSetting = appSetting.Value;
        }
        public async Task<List<Hotel>> GetHotelDetails(string queryString)
        {
            string url = "";
            List<Hotel> translatedHotelResult = new List<Hotel>();
            JObject data = new JObject();
            try
            {
                data = null;
                url = _appSetting.GoogleActivityBaseUrl + queryString + "&radius=" + _appSetting.RadiusActivityApi + "&type=hotels&keyword=hotels&key=" + _appSetting.ApiKey;
                var jsonPrediction = await _webClient.DownloadStringTaskAsync(url);
                data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                translatedHotelResult = _hotelTranslator.GetFilteredHotel(data);
            }
            catch(Exception e)
            {
                translatedHotelResult = null;
            }
            return translatedHotelResult;
        }
    }        
}

