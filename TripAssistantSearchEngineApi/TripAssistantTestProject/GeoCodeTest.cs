using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class GeoCodeTest
    {
        static private readonly IGeoCodeGenerator geoCodeGenerator = new GeoCodeGenerator();
        private readonly IGeoCode _geoCode = new GeoCode(geoCodeGenerator);
        [Fact]
        public void GeoCodeCityTest()
        {
            string geocode = _geoCode.GetGeoCodeOfCity("pune");
            Assert.Equal("18.5204303,73.8567437", geocode);
        }
    }
}
