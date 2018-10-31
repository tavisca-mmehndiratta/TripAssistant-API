using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ISingleActivityProvider
    {
        List<Activity> GetListWithSingleAttraction(string location, string activity);
    }
}
