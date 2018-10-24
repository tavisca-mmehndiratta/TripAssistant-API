using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;

namespace UsersApi
{
    [Produces("application/json")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("api/[controller]")]
        public void InsertUsers([FromBody]Users user)
        {
            UserDataBaseProvider ob = new UserDataBaseProvider();
            ob.InsertUser(user);

        }
        
        [HttpGet("{id}")]

        [Route("api/[controller]")]

        public Dictionary<string, int> UserGets(string id)
        {
            UserDataBaseProvider ob = new UserDataBaseProvider();
           return ob.GetUser(id);

        }
    }
}