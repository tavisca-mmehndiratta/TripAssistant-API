using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IGeoCodeGenerator
    {
        List<double> GetGeoLocation(string url);
    }
}
