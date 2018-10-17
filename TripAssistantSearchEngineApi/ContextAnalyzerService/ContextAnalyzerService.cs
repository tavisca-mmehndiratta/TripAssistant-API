using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ContextAnalyzerService : IContextAnalyzerService
    {
        private IGeoCode _geoCode;
        private IActivityApi _activityApi;
        private ICoreResponseGenerator _coreResponseGenerator;
        private IHotelApi _hotelApi;
        private IContextCheckerService _contextChecker;
        string typeResponse = "";
        JObject activityResponse = null;
        JObject hotelResponse = null;
        string resultantResponse = "";
        string[] response;
        string splitResponse;
        string geoCode;

        public ContextAnalyzerService(ICoreResponseGenerator coreResponseGenerator, IActivityApi activityApi,IGeoCode geoCode,IContextCheckerService contextChecker, IHotelApi hotelApi)
        {
            _hotelApi = hotelApi;
            _activityApi = activityApi;
            _coreResponseGenerator = coreResponseGenerator;
            _contextChecker = contextChecker;
            _geoCode = geoCode;
        }
        public CoreResponse GetResultsFromApi(string input, string location)
        {
            splitResponse = _contextChecker.GetFilteredQueryResponse(input);
            response = splitResponse.Split(' ');

            if (response[0].Equals("yes"))
            {
                typeResponse = "res";
                string queryString = "";
                if (response[response.Length - 1].Equals("current"))
                {
                    queryString = location + " " + response[1];
                }
                else
                {
                    queryString = response[1] + " " + response[2];
                }

                string[] keywords = queryString.Split(' ');
                geoCode = _geoCode.GetGeoCodeOfCity(keywords[0]);
                activityResponse = _activityApi.GetActivities(geoCode);
                if (keywords[1] != "1")
                {
                    queryString = _geoCode.GetCumulativeGeoCode();
                    hotelResponse = _hotelApi.GetHotelDetails(queryString);
                }
            }
            else if (response[0].Equals("no"))
            {
                typeResponse = "req";
                resultantResponse = "";
                for (int index = 1; index < response.Length; index++)
                {
                    resultantResponse += response[index] + " ";
                }
            }
            else
            {
                typeResponse = "req";
                resultantResponse = "This Request was beyond my power!!!";
            }
            CoreResponse finalResults = _coreResponseGenerator.MakeResponse(hotelResponse, activityResponse, typeResponse, resultantResponse);
            return finalResults;
        }
    }
    public interface IContextAnalyzerService
    {
        CoreResponse GetResultsFromApi(string input, string location);
    }
}
