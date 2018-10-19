using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IHotelApi
    {
        List<Hotel> GetHotelDetails(string queryString, string city);
    }
}
