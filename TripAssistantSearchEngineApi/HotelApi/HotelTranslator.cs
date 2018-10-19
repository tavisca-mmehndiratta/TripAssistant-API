﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;
namespace TripAssistantSearchEngineApi
{
    public class HotelTranslator : IHotelTranslator
    {
        public List<Hotel> GetFilteredHotel(JObject hoteljObject)
        {
            var results = hoteljObject["results"].Value<JArray>();
            Hotel hotel;
            string url = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=";
            List<Hotel> hotelList = new List<Hotel>();

            foreach (JObject res in results)
            {
                hotel = new Hotel();
                JArray photoArray;
                hotel.Name = res["name"].Value<String>();
                hotel.Rating = res["rating"].Value<Double>();
                hotel.Address = res["vicinity"].Value<String>();
                JObject geometry = res["geometry"].Value<JObject>();
                JObject location = geometry["location"].Value<JObject>();
                hotel.Lattitude = location["lat"].Value<Double>();
                hotel.Longitude = location["lng"].Value<Double>();

                if (res["photos"] != null)
                {
                     photoArray = res["photos"].Value<JArray>();

                  JObject  photo = photoArray[0].Value<JObject>();

                    hotel.PhotoUrl = url + photo["photo_reference"].Value<String>() + "&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
                }


              
                hotelList.Add(hotel);
            }


            return hotelList;
        }
    }
    
}
