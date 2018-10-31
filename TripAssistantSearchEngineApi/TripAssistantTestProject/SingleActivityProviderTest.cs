using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class SingleActivityProviderTest
    {
        static ISingleActivityTranslator singleActivityTranslator = new SingleActivityTranslator();
        ISingleActivityProvider _singleActivityProvider = new SingleActivityProvider(singleActivityTranslator);
        [Fact]
        public void SingleActivityTest()
        {
            List<Activity> testResult = _singleActivityProvider.GetListWithSingleAttraction("18.5204,73.8567", "trekking");
            int count = testResult.Count;
            Assert.Equal(1, count);
        }
    }
}
