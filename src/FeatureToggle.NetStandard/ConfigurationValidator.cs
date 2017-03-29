using System;

namespace FeatureToggle
{
    public class ConfigurationValidator
    {
        public void ValidateStartAndEndDates(DateTime startDate, DateTime endDate, string toggleNameInConfig)
        {
            if (startDate >= endDate)
                throw new ToggleConfigurationError(
                    string.Format("Configuration for {0} is invalid - the start date must be less then the end date",
                        toggleNameInConfig));

        }
    }
}
