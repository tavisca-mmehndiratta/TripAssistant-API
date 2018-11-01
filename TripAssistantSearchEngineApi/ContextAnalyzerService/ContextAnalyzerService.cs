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
        private readonly ISingleAttractionProvider _singleAttractionProvider;
        private readonly ICoreResponseGenerator _coreResponseGenerator;
        private readonly ISingleActivityProvider _singleActivityProvider;
        private readonly IHotelApi _hotelApi;
        private readonly IContextCheckerService _contextChecker;
        private readonly ISearchQueryProvider _searchQueryProvider;
        private readonly IDistanceCalculator _distanceCalculator;

        string typeResponse = "";
        //JObject selectApi = new JObject();
        string selectApi = "";
        ActivityBasedSearch activityBasedSearch = new ActivityBasedSearch();
        AttractionBasedSearch attractionBasedSearch = new AttractionBasedSearch();
        CityBasedSearch cityBasedSearch = new CityBasedSearch();
        List<Activity> activityResponse = null;
        Task<List<Hotel>> hotelResponse = null;
        List<Hotel> hotels = null;
        string resultantResponse = "";
        string[] response;
        string splitResponse;
        double dist = 0;
      
        string currentgeoCode;
        string geoCode;
        List<Activity> selected = new List<Activity>();
        public ContextAnalyzerService(ISingleActivityProvider singleActivityProvider,ISingleAttractionProvider singleAttractionProvider,IHotelCache hotelCache,IActivityCache activityCache,IUserPreferenceService userPreferenceService,IDistanceCalculator distanceCalculator, ICoreResponseGenerator coreResponseGenerator, IActivityApi activityApi,IGeoCode geoCode,IContextCheckerService contextChecker, IHotelApi hotelApi, ISearchQueryProvider searchQueryProvider)
        {
            _singleActivityProvider = singleActivityProvider;
            _singleAttractionProvider = singleAttractionProvider;
            _hotelCache = hotelCache;
            _activityCache = activityCache;
            _userPreferenceService = userPreferenceService;
            _hotelApi = hotelApi;
            _activityApi = activityApi;
            _coreResponseGenerator = coreResponseGenerator;
            _contextChecker = contextChecker;
            _geoCode = geoCode;
            _searchQueryProvider = searchQueryProvider;
            _distanceCalculator = distanceCalculator;
        }
        public Response GetResultsFromApi(string input, string location)
        {
            splitResponse = _contextChecker.GetFilteredQueryResponse(input);
            response = splitResponse.Split(' ');
            try
            {
                if (response[0].Equals("yes"))
                {
                    currentgeoCode = _geoCode.GetGeoCodeOfCity(location);

                    typeResponse = "response";
                    PerformOperationAgainstCorrectInput(location);
                    if (cityBasedSearch != null)
                    {
                        activityResponse = _activityCache.GetActivitiesFromCache(cityBasedSearch.City);
                        if (activityResponse == null || activityResponse[0].Type == null || activityResponse[0].ListActivity == null)
                        {
                            geoCode = _geoCode.GetGeoCodeOfCity(cityBasedSearch.City);
                             dist = _distanceCalculator.distance(geoCode, currentgeoCode,'K');
                            selectApi=(_searchQueryProvider.GetSearchQuery(cityBasedSearch.Duration, dist)).ToString();
                            activityResponse = _activityApi.GetActivities(geoCode, cityBasedSearch.City);
                            activityResponse = _userPreferenceService.GetFilteredResultsBasedOnUserPreferences(geoCode,activityResponse);
                            _activityCache.InsertActivitiesInCache(activityResponse, cityBasedSearch.City);
                        }
                        if (cityBasedSearch.Duration != 1)
                        {
                            hotels = _hotelCache.GetHotelsFromCache(cityBasedSearch.City);
                            if (hotels == null)
                            {
                                string queryString = _geoCode.GetCumulativeGeoCode(geoCode);
                                hotelResponse = _hotelApi.GetHotelDetails(queryString);
                                hotels = hotelResponse.Result;
                                _hotelCache.InsertHotelsInCache(hotels, cityBasedSearch.City);
                            }
                        }
                        selected = null;
                    }
                    else if (activityBasedSearch != null)
                    {
                        geoCode = _geoCode.GetGeoCodeOfCity(activityBasedSearch.City);
                        activityResponse = _singleActivityProvider.GetListWithSingleAttraction(geoCode, activityBasedSearch.ActivityName);
                        selected = null;
                    }
                    else if (attractionBasedSearch != null)
                    {
                        geoCode = _geoCode.GetGeoCodeOfCity(attractionBasedSearch.AttractionName);
                        dist = _distanceCalculator.distance(geoCode, currentgeoCode, 'K');
                        selectApi=(_searchQueryProvider.GetSearchQuery(cityBasedSearch.Duration, dist)).ToString();
                        activityResponse = _singleAttractionProvider.GetListWithSingleAttraction(geoCode, attractionBasedSearch.AttractionName);
                        if(attractionBasedSearch.Duration != 1)
                        {
                            hotelResponse = _hotelApi.GetHotelDetails(geoCode);
                            hotels = hotelResponse.Result;
                        }
                        selected.Add(activityResponse[0]);
                        activityResponse.RemoveAt(0);
                    }                  
                }
                else if (response[0].Equals("no"))
                {
                    typeResponse = "request";
                    selected = null;
                    resultantResponse = PerformOperationAgainstIncorrectInput();
                }
                else
                {
                    typeResponse = "request";
                    selected = null;
                    resultantResponse = "This Request was beyond my power!!! trip";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                typeResponse = "request";
                hotelResponse = null;
                selected = null;
                activityResponse = null;
                resultantResponse = "This Request was beyond my power!!! trip";
            }
            Response finalResults = new Response();
            finalResults= _coreResponseGenerator.MakeResponse(input,selected,hotels, activityResponse, typeResponse, resultantResponse);
            return finalResults;
        }


        
        public void PerformOperationAgainstCorrectInput(string location)
        {
            if (response[response.Length - 1].Equals("current"))
            {
                cityBasedSearch.City = location;
                int index = Array.IndexOf(response, "duration");
                index++;
                cityBasedSearch.Duration = int.Parse(response[index]);
                activityBasedSearch = null;
                attractionBasedSearch = null;
            }
            else
            {
                if (response[1].Equals("ok"))
                {
                    int index = Array.IndexOf(response, "city");
                    index++;
                    string city = "";
                    while (response[index] != "duration")
                    {
                        city += response[index] + " ";
                        index++;
                    }
                    cityBasedSearch.City = city;
                    index++;
                    cityBasedSearch.Duration = int.Parse(response[index]);
                    activityBasedSearch = null;
                    attractionBasedSearch = null;
                }
                else if (response[1].Equals("activity"))
                {
                    int index = 2;
                    string add = "";
                    while (response[index] != "city")
                    {
                        add += response[index] + " ";
                        index++;
                    }
                    activityBasedSearch.ActivityName = add;
                    add = "";
                    index++;
                    while (index < response.Length)
                    {
                        add += response[index] + " ";
                        index++;
                    }
                    activityBasedSearch.City = add;
                    cityBasedSearch = null;
                    attractionBasedSearch = null;
                }
                else if (response[1].Equals("attraction"))
                {
                    int index = 2;
                    string add = "";
                    while (response[index] != "duration")
                    {
                        add += response[index] + " ";
                        index++;
                    }
                    index++;
                    attractionBasedSearch.AttractionName = add;
                    attractionBasedSearch.Duration = int.Parse(response[index]);
                    activityBasedSearch.City = add;
                    cityBasedSearch = null;
                    activityBasedSearch = null;
                }
            }
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
