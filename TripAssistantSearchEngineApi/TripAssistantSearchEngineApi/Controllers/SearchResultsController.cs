using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TripAssistantSearchEngineApi
{
    [Produces("application/json")]
    [Route("api/SearchResults")]
    public class SearchResultsController : Controller
    {
        private ITripResultsService _tripResults;
        public SearchResultsController(ITripResultsService iTripResult)
        {
            _tripResults = iTripResult;
        }
        [HttpGet]
        public Response GetSearchResults([FromQuery] string input, [FromQuery] string location)
        {
            Response response = new Response();
            response = _tripResults.FetchResultsFromAPI(input, location);
            return response;
        }

    }
}