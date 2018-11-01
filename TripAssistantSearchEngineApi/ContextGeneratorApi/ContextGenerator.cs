using ApiAiSDK;
using System;
using Core.Contracts;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ContextGenerator: IContextGenerator
    {
        static private ApiAi _apiAi;
        private readonly AppSetting _appSetting;
        public ContextGenerator(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }
        public string GetContextResponse(string contextInput)
        {
            try
            {
                var config = new AIConfiguration(_appSetting.ApiAiConfigurationKey, SupportedLanguage.English);
                _apiAi = new ApiAi(config);
                var response = _apiAi.TextRequest(contextInput);
                string context = response.Result.Fulfillment.Speech;
                return context;
            }
            catch(Exception e)
            {
                return "Exception occured!";
            }
        }
    }
    
}
