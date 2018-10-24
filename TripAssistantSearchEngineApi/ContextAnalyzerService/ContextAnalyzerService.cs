using Core.Contracts;
using TripAssistantSearchEngineApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static TripAssistantSearchEngineApi.HotelsApi;

namespace TripAssistantSearchEngineApi
{
    public class ContextAnalyzerService : IContextAnalyzerService
    {
        private readonly IGeoCode _geoCode;
        private readonly IActivityCache _activityCache;
        private readonly IActivityApi _activityApi;
        private readonly IHotelCache _hotelCache;
        private readonly IUserPreferenceService _userPreferenceService;
        private readonly ICoreResponseGenerator _coreResponseGenerator;
        private readonly IHotelApi _hotelApi;
        private readonly IContextCheckerService _contextChecker;
        string typeResponse = "";
        List<Activity> activityResponse = null;
        Task<List<Hotel>> hotelResponse = null;
        List<Hotel> hotels = null;
        string resultantResponse = "";
        string[] response;
        string splitResponse;
        string geoCode;
        public ContextAnalyzerService(IHotelCache hotelCache,IActivityCache activityCache,IUserPreferenceService userPreferenceService,ICoreResponseGenerator coreResponseGenerator, IActivityApi activityApi,IGeoCode geoCode,IContextCheckerService contextChecker, IHotelApi hotelApi)
        {
            _hotelCache = hotelCache;
            _activityCache = activityCache;
            _userPreferenceService = userPreferenceService;
            _hotelApi = hotelApi;
            _activityApi = activityApi;
            _coreResponseGenerator = coreResponseGenerator;
            _contextChecker = contextChecker;
            _geoCode = geoCode;
        }
        public Response GetResultsFromApi(string input, string location)
        {
            splitResponse = _contextChecker.GetFilteredQueryResponse(input);
            response = splitResponse.Split(' ');
            try
            {
                if (response[0].Equals("yes"))
                {
                    typeResponse = "response";
                    string queryString = PerformOperationAgainstCorrectInput(location);
                    string[] keywords = queryString.Split(' ');           
                    activityResponse = _activityCache.GetActivitiesFromCache(keywords[0]);
                    if (activityResponse ==null || activityResponse[0].Type == null || activityResponse[0].ListActivity==null)
                    {
                        geoCode = _geoCode.GetGeoCodeOfCity(keywords[0]);
                        activityResponse = _activityApi.GetActivities(geoCode, keywords[0]);
                        activityResponse = _userPreferenceService.GetFilteredResultsBasedOnUserPreferences(activityResponse);
                        _activityCache.InsertActivitiesInCache(activityResponse, keywords[0]);
                    }                   
                    if (keywords[1] != "1")
                    {
                        hotels = _hotelCache.GetHotelsFromCache(keywords[0]);
                        if (hotels == null)
                        {
                            queryString = _geoCode.GetCumulativeGeoCode(geoCode);
                            hotelResponse = _hotelApi.GetHotelDetails(queryString, keywords[0]);
                            hotels = hotelResponse.Result;
                            _hotelCache.InsertHotelsInCache(hotels, keywords[0]);
                        }
                    }
                }
                else if (response[0].Equals("no"))
                {
                    typeResponse = "request";
                    resultantResponse = PerformOperationAgainstIncorrectInput();
                }
                else
                {
                    typeResponse = "request";
                    resultantResponse = "This Request was beyond my power!!!";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                typeResponse = "request";
                hotelResponse = null;
                activityResponse = null;
                resultantResponse = "This Request is beyond my power!!!";
            }
            Response finalResults = new Response();
            finalResults= _coreResponseGenerator.MakeResponse(input,hotels, activityResponse, typeResponse, resultantResponse);
            return finalResults;
        }
        public string PerformOperationAgainstCorrectInput(string location)
        {
            string queryString = "";
            if (response[response.Length - 1].Equals("current"))
            {
                queryString = location + " " + response[1];
            }
            else
            {
                queryString = response[1] + " " + response[2];
            }
            return queryString;
        }
        public string PerformOperationAgainstIncorrectInput()
        {
            resultantResponse = "";
            for (int index = 1; index < response.Length; index++)
            {
                resultantResponse += response[index] + " ";
            }
            return resultantResponse;
        }
    }    
}
