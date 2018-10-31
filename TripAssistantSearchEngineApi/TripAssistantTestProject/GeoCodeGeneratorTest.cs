using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class GeoCodeGeneratorTest
    {
        IGeoCodeGenerator _geoCodeGenerator = new GeoCodeGenerator();
        [Fact]
        public void GetGeoCodeTest()
        {
            List<double> testOutput = _geoCodeGenerator.GetGeoCode("pune");
            int count = testOutput.Count;
            Assert.Equal(2, count);
        }
    }
}
