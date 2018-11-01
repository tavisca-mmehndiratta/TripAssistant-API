using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public class AppSetting
    {
        public string GoogleActivityBaseUrl { get; set; }
        public string GooglePlaceIdBaseUrl { get; set; }
        public string ClassificationAlgoBaseUrl { get; set; }
        public string ApiAiConfigurationKey { get; set; }
        public string ApiKey { get; set; }
        public string UsersApiTrainingDataBaseUrl { get; set; }
        public string UsersPreferencesBaseUrl { get; set; }
        public string UsersPastExperienceBaseUrl { get; set; }
        public string RadiusActivityApi { get; set; }
        public string RadiusSingleThing { get; set; }
        public string RadiusSingleAttraction { get; set; }
    }
}
