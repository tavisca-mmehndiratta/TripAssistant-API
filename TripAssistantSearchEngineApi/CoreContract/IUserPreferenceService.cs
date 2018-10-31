using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IUserPreferenceService
    {
        List<Activity> GetFilteredResultsBasedOnUserPreferences(string geoCode, List<Activity> listActivity);
    }
}
