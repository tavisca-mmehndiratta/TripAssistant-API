using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class UserPreferencesTest
    {
        static IUserDetailsProvider userDetailsProvider = new UserDetailsProvider();
        static ISingleActivityTranslator singleActivityTranslator = new SingleActivityTranslator();
        static ISingleActivityProvider singleActivityProvider = new SingleActivityProvider(singleActivityTranslator);
        IUserPreferenceService userPreferenceService = new UserPreferencesService(singleActivityProvider, userDetailsProvider);

        [Fact]
        public void CheckUserPreferenceTest()
        {
        }
    }
}
