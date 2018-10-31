using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public class ActivityDetails
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public List<string> OpeningHours { get; set; }
    }
}
