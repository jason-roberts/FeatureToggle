using System;
using System.Configuration;
using System.Linq;

namespace JasonRoberts.FeatureToggle
{
    public sealed class AppSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider, ITimePeriodProvider
    {
        #region IToggleValueProvider Members

        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ConfigurationErrorsException(string.Format("The key '{0}' was not found in AppSettings",
                                                                     toggleNameInConfig));

            var configValue = ConfigurationManager.AppSettings[toggleNameInConfig];

            return ParseConfigString(configValue, toggleNameInConfig);
        }

        #endregion

        private bool ParseConfigString(string valueToParse, string configKey)
        {
            try
            {
                return Boolean.Parse(valueToParse);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(
                    string.Format("The value '{0}' cannot be converted to a boolean as defined in config key '{1}'",
                                  valueToParse, configKey),
                    ex);
            }
        }



        private DateTime ParseDateTimeConfigString(string valueToParse, string configKey)
        {
            try
            {
                return DateTime.Parse(valueToParse);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(
                    string.Format("The value '{0}' cannot be converted to a DateTime as defined in config key '{1}'",
                                  valueToParse, configKey),
                    ex);
            }
        }

        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ConfigurationErrorsException(string.Format("The key '{0}' was not found in AppSettings",
                                                                     toggleNameInConfig));

            var configValue = ConfigurationManager.AppSettings[toggleNameInConfig];

            return ParseDateTimeConfigString(configValue, toggleNameInConfig);
        }


        public Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ConfigurationErrorsException(string.Format("The key '{0}' was not found in AppSettings",
                                                                     toggleNameInConfig));


            DateTime startDate;
            DateTime endDate;            

            try
            {
                var configValues = ConfigurationManager.AppSettings[toggleNameInConfig].Split(new[] { '|' });

                startDate = DateTime.Parse(configValues[0].Trim());
                endDate = DateTime.Parse(configValues[1].Trim());

            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(string.Format("Configuration for {0} is invalid - date range should be specified like: '02/01/2050 04:05:08 | 07/08/2099 06:05:04'", toggleNameInConfig), ex); 
            }

            if (startDate >= endDate)
                throw new ConfigurationErrorsException(string.Format("Configuration for {0} is invalid - the start date must be less then the end date",toggleNameInConfig));

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}