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
                if (coreResponse.ResponseQuery.Equals("This Request was beyond my power!!!") || coreResponse.ResponseQuery.Equals("I cannot process this request! ") || coreResponse.ResponseQuery.Equals("My master says abusing is a bad habit!! One should not abuse. :) ")||coreResponse.ResponseQuery.Equals("Namaste, How can i help you? ")||coreResponse.ResponseQuery.Equals("Hi, i'm your Trip Assistant, I'm doing great. How can i help you? "))
                {
                    finalResponseToController.Request = "";
                }
                else
                {
                    finalResponseToController.Request = input;
                }
                finalResponseToController.ResponseQuery = coreResponse.ResponseQuery;
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
