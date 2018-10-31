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
                finalResponseToController.Selected = coreResponse.Selected;
                string[] checker = coreResponse.ResponseQuery.Split(" ");
                if (checker[checker.Length-2].Equals("trip"))
                {
                    finalResponseToController.Request = "";
                }
                else
                {
                    finalResponseToController.Request = input;
                }
                string responseQuery = "";
                for (int i = 0; i < checker.Length; i++)
                {
                    if (checker[i].Equals("trip"))
                    {
                        break;
                    }
                    responseQuery += checker[i] + " ";
                }
                finalResponseToController.ResponseQuery = responseQuery;
                _trainingDataLogger.InsertValuesIntoTrainingData(input, finalResponseToController.ResponseQuery);
            }
            else
            {
                finalResponseToController.Selected = coreResponse.Selected;
                finalResponseToController.ActivityList = coreResponse.ActivityList;
                finalResponseToController.HotelList = coreResponse.HotelList;
                finalResponseToController.Request = null;
                finalResponseToController.ResponseQuery = null;
                _trainingDataLogger.InsertValuesIntoTrainingData(input, "Results coming from API");
            }
            return finalResponseToController;
        }
    }   
}
