using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;


namespace FeatureToggle.Internal
{
    public sealed class AppSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider, ITimePeriodProvider, IDaysOfWeekToggleValueProvider, IAssemblyVersionProvider
    {
        private const string KeyNotFoundInAppsettingsMessage = "The key '{0}' was not found in AppSettings";



        
        IConfigurationRoot _configuration;

        public IConfigurationRoot Configuration
        { 
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appSettings.json");
                    Configuration = builder.Build();
                }
               
                return _configuration;               
            }
            set
            {
                _configuration = value;
            }
        }


        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            ValidateKeyExists(key);

            var configValue = GetConfigValue(key);



            return ParseBooleanConfigString(configValue, key);  


        }


        /// <summary>
        /// Doing this manually as JavaScriptSerializer doesn't error if bad json parsed
        /// and I don't want to introduce a dependency for users on json.net et al
        /// </summary>
        /// <param name="json">json to verify</param>
        private void AssertValidJson(string json)
        {
            var canonicalised = json.Replace(" ","").Replace(Environment.NewLine, "").Replace("\"", "'").ToLowerInvariant();

            var isValid = canonicalised == "{'enabled':true}" || canonicalised == "{'enabled':false}";

            if (!isValid)
            {
                throw new WebException("The following json is invalid:" + json);
            }
        }


        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            ValidateKeyExists(key);

            var configValue = GetConfigValue(key);            

            var parser = new ConfigurationDateParser();

            return parser.ParseDateTimeConfigString(configValue, key);
        }




        public IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            ValidateKeyExists(key);

            var configValues = GetConfigValue(key).Split(new[] {','}).Select(x => x.Trim());

            foreach (var configValue in configValues)
            {
                DayOfWeek day;

                var isValidDay = DayOfWeek.TryParse(configValue, true, out day);

                if (isValidDay)
                {
                    yield return day;
                }
                else
                {
                    throw new ToggleConfigurationError(
                        string.Format(
                            "The value '{0}' in config key '{1}' is not a valid day of the week. Days should be specified in long format. E.g. Friday and not Fri.",
                            configValue, key));
                }
            }
        }


        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            ValidateKeyExists(key);


            var configValues = GetConfigValue(key).Split(new[] {'|'});

            var parser = new ConfigurationDateParser();

            var startDate = parser.ParseDateTimeConfigString(configValues[0].Trim(), key);
            var endDate = parser.ParseDateTimeConfigString(configValues[1].Trim(), key);

            var v = new ConfigurationValidator();

            v.ValidateStartAndEndDates(startDate, endDate, key);

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }


        public Version EvaluateVersion(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            ValidateKeyExists(key);

            string configuredVersion = GetConfigValue(key);

            return Version.Parse(configuredVersion);
        }


        private void ValidateKeyExists(string key)
        {
            var allKeys = GetAllConfigKeys();
            if (!allKeys.Contains(key))
            {
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage, key));
            }                
        }

        private string ExpectedAppSettingsKeyFor(IFeatureToggle toggle)
        {

            return toggle.GetType().Name;

        }

        private bool ParseBooleanConfigString(string valueToParse, string configKey)
        {
            try
            {
                return Boolean.Parse(valueToParse);
            }
            catch (Exception ex)
            {
                throw new ToggleConfigurationError(
                    string.Format("The value '{0}' cannot be converted to a boolean as defined in config key '{1}'",
                        valueToParse, configKey),
                    ex);
            }
        }

        private string GetConfigValue(string key)
        {


            return Configuration.GetSection(ToggleConfigurationSettings.Prefix.Replace(".", ""))[key];

        }



        private static string AppDirectory  => AppContext.BaseDirectory;




        private string[] GetAllConfigKeys()
        {

            var allToggleSettings = Configuration.GetSection(ToggleConfigurationSettings.Prefix.Replace(".","")).GetChildren();

            var temp= new List<string>();
            foreach (var setting in allToggleSettings)
            {
                temp.Add(setting.Key);
            }

            return temp.ToArray();

        }
    }
}

