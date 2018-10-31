using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class SingleAttractionProviderTest
    {
        static ISingleAttractionTranslator singleAttractionTranslator = new SingleAttractionTranslator();
        ISingleAttractionProvider _singleAttractionProvider = new SingleAttractionProvider(singleAttractionTranslator);
        [Fact]
        public void SingleAttractionTest()
        {
            List<Activity> testResult = _singleAttractionProvider.GetListWithSingleAttraction("27.1750,78.0422", "taj mahal");
            int count = testResult.Count;
            Assert.Equal(5, count);
        }
    }
}
