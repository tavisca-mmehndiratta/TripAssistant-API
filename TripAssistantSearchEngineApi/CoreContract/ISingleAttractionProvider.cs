using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ISingleAttractionProvider
    {
        List<Activity> GetListWithSingleAttraction(string location, string attraction);
    }
}
