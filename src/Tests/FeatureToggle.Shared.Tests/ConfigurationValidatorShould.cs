using System;
using FeatureToggle;
using Xunit;

namespace FeatureToggle.Tests
{
    public class ConfigurationValidatorShould
    {
        [Fact]
        public void ErrorWhenStartDateAfterEndDate()
        {
            var sut = new ConfigurationValidator();

            var ex = Assert.Throws<ToggleConfigurationError>(
                () => sut.ValidateStartAndEndDates(DateTime.MinValue.AddMilliseconds(1), DateTime.MinValue, "FeatureToggle.StartDateAfterEndDate"));

            Assert.Equal(
                "Configuration for FeatureToggle.StartDateAfterEndDate is invalid - the start date must be less then the end date",
                ex.Message);
        }

        [Fact]
        public void ErrorWhenStartDateAndEndDateAreTheSame()
        {
            var sut = new ConfigurationValidator();

            var ex = Assert.Throws<ToggleConfigurationError>(
                () => sut.ValidateStartAndEndDates(DateTime.MinValue, DateTime.MinValue, "FeatureToggle.StartDateAfterEndDate"));

            Assert.Equal(
                "Configuration for FeatureToggle.StartDateAfterEndDate is invalid - the start date must be less then the end date",
                ex.Message);
        }
    }
}