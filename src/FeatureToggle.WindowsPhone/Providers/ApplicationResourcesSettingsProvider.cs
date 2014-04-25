using System;
using System.Windows;
using FeatureToggle.Core;

namespace FeatureToggle.Providers
{
    public class ApplicationResourcesSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider, ITimePeriodProvider
    {
        private const string ConfigPrefix = "FeatureToggle.";
        private const string KeyNotFoundInApplicationResourcesMessage = "The key '{0}' was not found in Application.Current.Resources";

        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));

            bool toggleValue = bool.Parse(Application.Current.Resources[toggleNameInConfig].ToString());

            return toggleValue;
        }


        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));

            var parser = new ConfigurationDateParser();

            return parser.ParseDateTimeConfigString((string) Application.Current.Resources[toggleNameInConfig],
                toggleNameInConfig);
        }

        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));


            DateTime startDate;
            DateTime endDate;

            var configValues = ((string) Application.Current.Resources[toggleNameInConfig]).Split(new[] {'|'});

            var parser = new ConfigurationDateParser();

            startDate = parser.ParseDateTimeConfigString(configValues[0].Trim(), toggleNameInConfig);
            endDate = parser.ParseDateTimeConfigString(configValues[1].Trim(), toggleNameInConfig);


            if (startDate >= endDate)
                throw new Exception(
                    string.Format("Configuration for {0} is invalid - the start date must be less then the end date",
                        toggleNameInConfig));

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}
