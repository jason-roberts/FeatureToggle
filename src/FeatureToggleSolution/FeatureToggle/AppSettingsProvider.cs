using System;
using System.Configuration;
using System.Linq;

namespace JasonRoberts.FeatureToggle
{
    public sealed class AppSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider
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
    }
}