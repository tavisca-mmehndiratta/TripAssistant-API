using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using UsersApi.DataBaseAccess;

namespace UsersApi.Controllers
{
    [Produces("application/json")]
    public class TrainingDataController : Controller
    {
        [HttpPost]
        [Route("api/[controller]")]
        public void InsertUsersTrainingData([FromBody] UserTrainedData userTrainedData)
        {
            TrainingDataProvider ob = new TrainingDataProvider();
            ob.InsertTrainingData(userTrainedData);

        }
        [HttpGet]
        [Route("api/[controller]")]
        public List<UserTrainedData> GetUsersTrainingData()
        {
            TrainingDataProvider ob = new TrainingDataProvider();
            return ob.GetTrainingData();

        }

    }
}