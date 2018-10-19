using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IHotelApi
    {
        List<Hotel> GetHotelDetails(string queryString, string city);
    }
}
