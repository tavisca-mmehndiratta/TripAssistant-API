using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ISingleAttractionProvider
    {
        List<Activity> GetListWithSingleAttraction(string location, string attraction);
    }
}
