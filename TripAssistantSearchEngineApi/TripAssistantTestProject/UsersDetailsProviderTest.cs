using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class UsersDetailsProviderTest
    {
        IUserDetailsProvider _userDetailsProvider = new UserDetailsProvider();
        [Fact]
        public void UserPreferencesTest()
        {
            Task<List<Activities>> testResponse = _userDetailsProvider.GetUsersPreferences("http://tripassistant-user.ap-south-1.elasticbeanstalk.com/api/User?id=Mohit");
            int count = testResponse.Result.Count;
            Assert.Equal(14, count);
        }
        [Fact]
        public void UserPastExperienceTest()
        {
            Task<List<Activities>> testResponse = _userDetailsProvider.GetUsersPastExperience("http://tripassistant-user.ap-south-1.elasticbeanstalk.com/api/PastExperience?id=Mohit");
            int count = testResponse.Result.Count;
            Assert.Equal(47, count);
        }
    }
}
