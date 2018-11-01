using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class CoreResponseGeneratorTest
    {
        private readonly ICoreResponseGenerator coreResponseGenerator = new CoreResponseGenerator();
        [Fact]
        public void CoreResponseTest()
        {
            Response outputResponse = coreResponseGenerator.MakeResponse("Hello",null,null,null,"request", "no Namaste, How can i help you? trip");
            Response inputResponse = new Response();
            Assert.Null(outputResponse.ActivityList);
            Assert.Null(outputResponse.HotelList);
            Assert.Null(outputResponse.Selected);
            Assert.Equal( "request", outputResponse.Type);
            Assert.Equal("Hello", outputResponse.Request);
            Assert.Equal("no Namaste, How can i help you? trip", outputResponse.ResponseQuery);
        }
    }
}
