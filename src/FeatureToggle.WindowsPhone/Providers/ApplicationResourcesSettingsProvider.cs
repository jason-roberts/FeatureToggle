using System;
using System.Globalization;
using System.Windows;
using FeatureToggle.Core;

namespace FeatureToggle.Providers
{
    public class ApplicationResourcesSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider, ITimePeriodProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = "FeatureToggle." + toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format("The key '{0}' was not found in Application.Current.Resources", toggleNameInConfig));

            bool toggleValue = bool.Parse(Application.Current.Resources[toggleNameInConfig].ToString());

            return toggleValue;
        }


        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format("The key '{0}' was not found in Application.Current.Resources", toggleNameInConfig));

            DateTime toggleValue = (DateTime)Application.Current.Resources[toggleNameInConfig];

            return toggleValue;
        }

        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var toggleNameInConfig = toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format("The key '{0}' was not found in Application.Current.Resources", toggleNameInConfig));


            DateTime startDate;
            DateTime endDate;

            try
            {
                var configValues = ((string)Application.Current.Resources[toggleNameInConfig]).Split(new[] { '|' });

                const string expectedDateFormat = @"dd/MM/yyyy HH:mm:ss";

                startDate = DateTime.ParseExact(configValues[0].Trim(), expectedDateFormat, CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(configValues[1].Trim(), expectedDateFormat, CultureInfo.InvariantCulture);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Configuration for {0} is invalid - date range should be specified like: '02/01/2050 04:05:08 | 07/08/2099 06:05:04'", toggleNameInConfig), ex);
            }

            if (startDate >= endDate)
                throw new Exception(string.Format("Configuration for {0} is invalid - the start date must be less then the end date", toggleNameInConfig));

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}
