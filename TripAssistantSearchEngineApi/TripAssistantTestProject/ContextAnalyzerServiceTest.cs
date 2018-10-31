using System;
using Xunit;
using Core.Contracts;
using TripAssistantSearchEngineApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ContextAnalyzerServiceTest
    {
        static IContextGenerator contextGenerator = new ContextGenerator();
        static IUserDetailsProvider userDetailsProvider = new UserDetailsProvider();
        static IActivityTranslator activityTranslator = new ActivityTranslator();
        static ISingleActivityTranslator singleActivityTranslator = new SingleActivityTranslator();
        static ISingleAttractionTranslator singleAttractionTranslator = new SingleAttractionTranslator();
        static IHotelTranslator hotelTranslator = new HotelTranslator();
        static private readonly ISingleActivityProvider _singleActivityProvider = new SingleActivityProvider(singleActivityTranslator);
        static private readonly IGeoCodeGenerator geoCodeGenerator = new GeoCodeGenerator();
        static private readonly IGeoCode _geoCode = new GeoCode(geoCodeGenerator);
        static private readonly IActivityCache _activityCache = new ActivityCache();
        static private readonly IActivityApi _activityApi = new ActivityApi(activityTranslator);
        static private readonly IHotelCache _hotelCache = new HotelCache();
        static private readonly IUserPreferenceService _userPreferenceService = new UserPreferencesService(_singleActivityProvider, userDetailsProvider);
        static private readonly ISingleAttractionProvider _singleAttractionProvider = new SingleAttractionProvider(singleAttractionTranslator);
        static private readonly ICoreResponseGenerator _coreResponseGenerator = new CoreResponseGenerator();
        static private readonly IHotelApi _hotelApi = new HotelsApi(hotelTranslator);
        static private readonly IContextCheckerService _contextChecker = new ContextCheckerService(contextGenerator);
        IContextAnalyzerService _contextAnalyzerService = new ContextAnalyzerService(_singleActivityProvider,_singleAttractionProvider,_hotelCache,_activityCache,_userPreferenceService,_coreResponseGenerator,_activityApi,_geoCode,_contextChecker,_hotelApi);
        [Fact]
        public void ContextAnalyzeTest()
        {
            Response response = _contextAnalyzerService.GetResultsFromApi("Hello", "pune");
            Assert.Equal("Namaste, How can i help you? trip ", response.ResponseQuery);
        }
    }
}
