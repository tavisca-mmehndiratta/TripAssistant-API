using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class ActivityApiProviderTest
    {
        static IActivityTranslator activityTranslator = new ActivityTranslator();
        IActivityApi _activityApi = new ActivityApi(activityTranslator);
        [Fact]
        public void GetActivitiesTest()
        {
            List<Activity> testActivityOutput = _activityApi.GetActivities("18.5204,73.8567", "pune");
            int count = testActivityOutput.Count;
            Assert.Equal(13, count);
        }
    }
}
