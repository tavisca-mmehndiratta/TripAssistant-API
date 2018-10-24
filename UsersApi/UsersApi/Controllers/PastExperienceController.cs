using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using UsersApi.Services;
using Microsoft.AspNetCore.Http;

namespace UsersApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PastExperience")]
    public class PastExperienceController : Controller
    {
        [HttpGet]
        public Dictionary<string, int> GetUserPastExperience(string id)
        {
            UserPastExperienceProvider ob = new UserPastExperienceProvider();
            return ob.GetUserPastExperience(id);
        }
        [HttpPost]

        public void InsertUsersPastExperiences([FromBody] UserPastExperiences userPastExperiences)
        {
            UserPastExperienceProvider user = new UserPastExperienceProvider();

            user.InsertUserPastExperience(userPastExperiences);

        }
    }
}