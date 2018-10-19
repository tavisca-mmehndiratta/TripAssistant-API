using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IGeoCodeGenerator
    {
        List<double> GetGeoLocation(string url);
    }
}
