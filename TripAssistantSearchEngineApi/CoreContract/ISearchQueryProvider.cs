using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ISearchQueryProvider
    {
        string GetSearchQuery(int duration, double distance);
    }
}
