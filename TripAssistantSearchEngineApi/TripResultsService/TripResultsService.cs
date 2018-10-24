using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class TripResultsService : ITripResultsService
    {
        private readonly IContextAnalyzerService _getcontextResults;
        private readonly ITrainingDataLogger _trainingDataLogger;
        public TripResultsService(IContextAnalyzerService contextAnalyzer, ITrainingDataLogger trainingDataLogger)
        {
            _trainingDataLogger = trainingDataLogger;
            _getcontextResults = contextAnalyzer;
        }
        public Response FetchResultsFromAPI(string input, string location)
        {
            Response finalResponseToController = new Response();
            Response coreResponse = _getcontextResults.GetResultsFromApi(input, location);
            finalResponseToController.Type = coreResponse.Type;
            if (coreResponse.Type == "request")
            {
                finalResponseToController.ActivityList = null;
                finalResponseToController.HotelList = null;
                finalResponseToController.Request = input;
                finalResponseToController.ResponseQuery = coreResponse.ResponseQuery;
                _trainingDataLogger.InsertValuesIntoTrainingData(finalResponseToController.Request, finalResponseToController.ResponseQuery);
            }
            else
            {
                finalResponseToController.ActivityList = coreResponse.ActivityList;
                finalResponseToController.HotelList = coreResponse.HotelList;
                finalResponseToController.Request = null;
                finalResponseToController.ResponseQuery = null;
                _trainingDataLogger.InsertValuesIntoTrainingData(finalResponseToController.Request, "Results coming from API");
            }
            return finalResponseToController;
        }
    }   
}
