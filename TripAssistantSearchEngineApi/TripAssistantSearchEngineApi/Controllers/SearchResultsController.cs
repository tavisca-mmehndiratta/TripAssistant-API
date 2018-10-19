using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreContract = Core.Contracts;
using DataContract = Data.Contract;
using AutoMapper;

namespace TripAssistantSearchEngineApi
{
    [Produces("application/json")]
    [Route("api/SearchResults")]
    public class SearchResultsController : Controller
    {
        private CoreContract.ITripResultsService _tripResults;
        private readonly IMapper _mapper;
        public SearchResultsController(CoreContract.ITripResultsService iTripResult, IMapper mapper)
        {
            _tripResults = iTripResult;
            _mapper = mapper;
        }
        [HttpGet]
        public DataContract.Response GetSearchResults([FromQuery] string input, [FromQuery] string location)
        {
            CoreContract.Response response = new CoreContract.Response();
            response = _tripResults.FetchResultsFromAPI(input, location);
            return _mapper.Map<DataContract.Response>(response);
        }

    }
}