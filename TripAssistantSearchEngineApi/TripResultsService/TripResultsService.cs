using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class TripResultsService : ITripResultsService
    {
        IContextAnalyzerService _getcontextResults;
        IActivityTranslator _activityTranslator;
        IHotelTranslator _hotelTranslator;
        public TripResultsService(IContextAnalyzerService contextAnalyzer, IActivityTranslator activityTranslator, IHotelTranslator hotelTranslator)
        {
            _getcontextResults = contextAnalyzer;
            _activityTranslator = activityTranslator;
            _hotelTranslator = hotelTranslator;
        }
        public Response FetchResultsFromAPI(string input, string location)
        {
            Response finalResponseToController = new Response();
            CoreResponse coreResponse = _getcontextResults.GetResultsFromApi(input, location);
            finalResponseToController.Type = coreResponse.Type;
            if (coreResponse.Type == "req")
            {
                finalResponseToController.ActivityList = null;
                finalResponseToController.HotelList = null;
                finalResponseToController.Request = input;
                finalResponseToController.ResponseQuery = coreResponse.ResponseQuery;
            }
            else
            {
                List<Activity> activityList = _activityTranslator.GetFilteredActivity(coreResponse.ActivityList);
                List<Hotel> hotelList = null;
                if (coreResponse.HotelList != null)
                {

                    hotelList = _hotelTranslator.GetFilteredHotel(coreResponse.HotelList);
                }
                finalResponseToController.ActivityList = activityList;
                finalResponseToController.HotelList = hotelList;
                finalResponseToController.Request = null;
                finalResponseToController.ResponseQuery = null;
            }

            return finalResponseToController;
        }
    }

    public interface ITripResultsService
    {
        Response FetchResultsFromAPI(string input, string location);
    }
}
