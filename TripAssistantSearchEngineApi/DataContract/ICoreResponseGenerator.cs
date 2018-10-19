using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ICoreResponseGenerator
    {
        Response MakeResponse(string input, List<Hotel> hotels, List<Activity> activities, string type, string res);
    }
}
