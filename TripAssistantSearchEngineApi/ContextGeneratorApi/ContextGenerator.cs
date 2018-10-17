using ApiAiSDK;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ContextGenerator: IContextGenerator
    {
        static private ApiAi apiAi;
        public string GetContextResponse(string contextInput)
        {
            var config = new AIConfiguration("ada088ff76ae462fb2a17e5ee0df4c9b", SupportedLanguage.English);
            apiAi = new ApiAi(config);
            var response = apiAi.TextRequest(contextInput);
            string context = response.Result.Fulfillment.Speech;
            return context;
        }
    }
    public interface IContextGenerator
    {
        string GetContextResponse(string input);
    }
}
