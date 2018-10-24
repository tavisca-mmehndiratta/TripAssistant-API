using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Models
{
    public class Users
    {
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long PhoneNo { get; set; }
        public Dictionary<string, int> Preferences { get; set; }


    }
}
