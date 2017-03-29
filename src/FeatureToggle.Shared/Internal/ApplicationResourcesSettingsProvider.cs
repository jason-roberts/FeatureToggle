#if WINDOWS_UWP

using System;
using System.Collections.Generic;
using System.Linq;
using FeatureToggle;
using Windows.UI.Xaml;

namespace FeatureToggle.Internal
{
    public class ApplicationResourcesSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider,
        ITimePeriodProvider, IDaysOfWeekToggleValueProvider, IAssemblyVersionProvider
    {
        private const string KeyNotFoundInApplicationResourcesMessage =
            "The key '{0}' was not found in Application.Current.Resources";

        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = CalculateToggleNameAsItShouldAppearInConfig(toggle);

            AssertThatToggleHasConfiguredValue(toggleNameInConfig);

            bool toggleValue = bool.Parse(Application.Current.Resources[toggleNameInConfig].ToString());

            return toggleValue;
        }

        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = CalculateToggleNameAsItShouldAppearInConfig(toggle);

            AssertThatToggleHasConfiguredValue(toggleNameInConfig);

            var parser = new ConfigurationDateParser();

            return parser.ParseDateTimeConfigString((string) Application.Current.Resources[toggleNameInConfig],
                toggleNameInConfig);
        }

        public IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle)
        {
            var toggleNameInConfig = CalculateToggleNameAsItShouldAppearInConfig(toggle);

            AssertThatToggleHasConfiguredValue(toggleNameInConfig);


            var trimmedConfigDays = GetTrimmedDaysFromConfig(toggleNameInConfig);

            foreach (var configValue in trimmedConfigDays)
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

        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var toggleNameInConfig = CalculateToggleNameAsItShouldAppearInConfig(toggle);

            AssertThatToggleHasConfiguredValue(toggleNameInConfig);


            DateTime startDate;
            DateTime endDate;

            var configValues = ((string) Application.Current.Resources[toggleNameInConfig]).Split(new[] {'|'});

            var parser = new ConfigurationDateParser();

            startDate = parser.ParseDateTimeConfigString(configValues[0].Trim(), toggleNameInConfig);
            endDate = parser.ParseDateTimeConfigString(configValues[1].Trim(), toggleNameInConfig);

            var v = new ConfigurationValidator();

            v.ValidateStartAndEndDates(startDate, endDate, toggleNameInConfig);

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        private static void AssertThatToggleHasConfiguredValue(string toggleNameInConfig)
        {
            if (!ConfigurationContains(toggleNameInConfig))
                throw new Exception(string.Format(KeyNotFoundInApplicationResourcesMessage, toggleNameInConfig));
        }

        private static string CalculateToggleNameAsItShouldAppearInConfig(IFeatureToggle toggle)
        {
            return ToggleConfigurationSettings.Prefix + toggle.GetType().Name;
        }

        private static bool ConfigurationContains(string toggleNameInConfig)
        {

            return Application.Current.Resources.ContainsKey(toggleNameInConfig);
        }

        private static IEnumerable<string> GetTrimmedDaysFromConfig(string toggleNameInConfig)
        {
            return ((string) Application.Current.Resources[toggleNameInConfig])
                .Split(new[] {','})
                .Select(x => x.Trim());
        }

        public Version EvaluateVersion(IFeatureToggle toggle)
        {
            var toggleNameInConfig = CalculateToggleNameAsItShouldAppearInConfig(toggle);

            AssertThatToggleHasConfiguredValue(toggleNameInConfig);

            string configuredVersion = (string)Application.Current.Resources[toggleNameInConfig];

            return Version.Parse(configuredVersion);
        }
    }
}

#endif