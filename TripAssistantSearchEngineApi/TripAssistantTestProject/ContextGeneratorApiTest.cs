using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class ContextGeneratorApiTest
    {
        IContextGenerator _contextGenerator = new ContextGenerator();
        [Fact]
        public void GetContextResponseTest()
        {
            string testOutput = _contextGenerator.GetContextResponse("hello");
            Assert.Equal("no hello activity Namaste, How can i help you? trips", testOutput);
        }
    }
}
