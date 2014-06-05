using System.Collections.Generic;
#if (FEATURETOGGLE_FULL)
using System;
using System.Configuration;
using System.Linq;
using FeatureToggle.Core;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Providers
// ReSharper restore CheckNamespace
{
    public sealed class AppSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider,
        ITimePeriodProvider, IDaysOfWeekToggleValueProvider
    {
        private const string KeyNotFoundInAppsettingsMessage = "The key '{0}' was not found in AppSettings";


        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    key));

            var configValue = ConfigurationManager.AppSettings[key];

            return ParseConfigString(configValue, key);
        }

        private static string ExpectedAppSettingsKeyFor(IFeatureToggle toggle)
        {
            return AppSettingsKeys.Prefix + "." + toggle.GetType().Name;
        }


        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    key));

            var configValue = ConfigurationManager.AppSettings[key];

            var parser = new ConfigurationDateParser();

            return parser.ParseDateTimeConfigString(configValue, key);            
        }


        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    key));


            var configValues = ConfigurationManager.AppSettings[key].Split(new[] {'|'});

            var parser = new ConfigurationDateParser();

            var startDate = parser.ParseDateTimeConfigString(configValues[0].Trim(), key);
            var endDate = parser.ParseDateTimeConfigString(configValues[1].Trim(), key);

            var v = new ConfigurationValidator();

            v.ValidateStartAndEndDates(startDate, endDate, key);

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        private bool ParseConfigString(string valueToParse, string configKey)
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


        public IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle)
        {
            var key = ExpectedAppSettingsKeyFor(toggle);

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    key));

            var configValues = ConfigurationManager.AppSettings[key].Split(new[] { ',' }).Select(x => x.Trim());

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
    }
}

#endif