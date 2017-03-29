using System;
using System.Globalization;

namespace FeatureToggle
{
    public class ConfigurationDateParser
    {
        private const string ExpectedDateFormat = @"dd-MMM-yyyy HH:mm:ss";

        public DateTime ParseDateTimeConfigString(string valueToParse, string configKey)
        {
            try
            {
                return DateTime.ParseExact(valueToParse, ExpectedDateFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The value '{0}' cannot be converted to a DateTime as defined in config key '{1}'. The expected format is: {2}",
                        valueToParse, configKey, ExpectedDateFormat),
                    ex);
            }
        }
    }
}
