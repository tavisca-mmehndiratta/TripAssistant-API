using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ICoreResponseGenerator
    {
        Response MakeResponse(string input, List<Hotel> hotels, List<Activity> activities, string type, string res);
    }
}
