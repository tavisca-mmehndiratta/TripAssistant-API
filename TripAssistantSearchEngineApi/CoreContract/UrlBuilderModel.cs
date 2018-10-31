using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public class UrlBuilderModel
    {
        public string baseUri = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
        public string apiKey = "AIzaSyA9v-ByUMauD8TazXdViq_f7RF-EHru86A";
        public string baseUriPlaceId = "https://maps.googleapis.com/maps/api/place/details/json?place_id=";
        public string radius = "38000";
    }
}
