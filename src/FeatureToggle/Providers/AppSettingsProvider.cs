using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using FeatureToggle.Core;

namespace FeatureToggle.Providers
{
    public sealed class AppSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider,
        ITimePeriodProvider
    {
        private const string KeyNotFoundInAppsettingsMessage = "The key '{0}' was not found in AppSettings";


        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    toggleNameInConfig));

            var configValue = ConfigurationManager.AppSettings[toggleNameInConfig];

            return ParseConfigString(configValue, toggleNameInConfig);
        }


        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    toggleNameInConfig));

            var configValue = ConfigurationManager.AppSettings[toggleNameInConfig];

            var parser = new ConfigurationDateParser();

            return parser.ParseDateTimeConfigString(configValue, toggleNameInConfig);            
        }


        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ToggleConfigurationError(string.Format(KeyNotFoundInAppsettingsMessage,
                    toggleNameInConfig));


            var configValues = ConfigurationManager.AppSettings[toggleNameInConfig].Split(new[] {'|'});

            var parser = new ConfigurationDateParser();

            var startDate = parser.ParseDateTimeConfigString(configValues[0].Trim(), toggleNameInConfig);
            var endDate = parser.ParseDateTimeConfigString(configValues[1].Trim(), toggleNameInConfig);

            var v = new ConfigurationValidator();

            v.ValidateStartAndEndDates(startDate, endDate, toggleNameInConfig);

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



    }
}