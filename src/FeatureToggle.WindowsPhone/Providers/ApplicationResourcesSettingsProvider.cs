using System;
using System.Collections.Generic;
using System.Linq;
using FeatureToggle.Core;


#if NETFX_CORE
    using Windows.UI.Xaml;
#else
    using System.Windows;
#endif


namespace FeatureToggle.Providers
{
    public class ApplicationResourcesSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider, ITimePeriodProvider, IDaysOfWeekToggleValueProvider
    {
        private const string ConfigPrefix = "FeatureToggle.";
        private const string KeyNotFoundInApplicationResourcesMessage = "The key '{0}' was not found in Application.Current.Resources";

        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!ConfigurationContains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));

            bool toggleValue = bool.Parse(Application.Current.Resources[toggleNameInConfig].ToString());

            return toggleValue;
        }

        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!ConfigurationContains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));

            var parser = new ConfigurationDateParser();

            return parser.ParseDateTimeConfigString((string) Application.Current.Resources[toggleNameInConfig],
                toggleNameInConfig);
        }

        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!ConfigurationContains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));


            DateTime startDate;
            DateTime endDate;

            var configValues = ((string) Application.Current.Resources[toggleNameInConfig]).Split(new[] {'|'});

            var parser = new ConfigurationDateParser();

            startDate = parser.ParseDateTimeConfigString(configValues[0].Trim(), toggleNameInConfig);
            endDate = parser.ParseDateTimeConfigString(configValues[1].Trim(), toggleNameInConfig);

            var v = new ConfigurationValidator();

            v.ValidateStartAndEndDates(startDate,endDate, toggleNameInConfig);

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        private static bool ConfigurationContains(string toggleNameInConfig)
        {
#if NETFX_CORE
            return Application.Current.Resources.ContainsKey(toggleNameInConfig);
#else
    return Application.Current.Resources.Contains(toggleNameInConfig);
#endif

            
        }

        public IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ConfigPrefix + toggle.GetType().Name;

            if (!ConfigurationContains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));

            var configValues = ((string)Application.Current.Resources[toggleNameInConfig]).Split(new[] {','}).Select(x => x.Trim());
            

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
                            configValue, toggleNameInConfig));
                }
            }
        }
    }
}
