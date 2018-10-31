using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TripAssistantSearchEngineApi
{
    [Produces("application/json")]
    [Route("api/PlaceDetails")]
    public class PlaceDetailsController : Controller
    {
        private IActivityApi _activityApi;
        public PlaceDetailsController(IActivityApi activityApi)
        {
            _activityApi = activityApi;
        }
        public IActionResult GetPlaceDetails([FromQuery] string placeId)
        {
            Task<ActivityDetails> activityDetails = _activityApi.GetActivitiesByPlaceId(placeId);
            return Ok(activityDetails.Result);
        }
    }
}