using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IHotelCache
    {
        void InsertHotelsInCache(List<Hotel> hotel, string city);
        List<Hotel> GetHotelsFromCache(string city);

    }
}
