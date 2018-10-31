using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class ContextCheckerServiceTest
    {
        static private readonly IContextGenerator _contextGenerator = new ContextGenerator();
        private readonly IContextCheckerService _contextCheckerService = new ContextCheckerService(_contextGenerator);
        [Fact]
        public void FilteredQueryTest()
        {
            string queryResponse = _contextCheckerService.GetFilteredQueryResponse("yes pune only");
            Assert.Equal("yes ok city Pune duration 1", queryResponse);
        }
    }
}
