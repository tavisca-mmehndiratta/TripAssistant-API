using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ISingleActivityProvider
    {
        List<Activity> GetListWithSingleAttraction(string location, string activity);
        Activity GetActivityForUserPastExperience(string geocode, string activity);
    }
}
