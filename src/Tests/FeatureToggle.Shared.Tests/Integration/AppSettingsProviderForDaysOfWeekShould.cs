using System;
using System.Linq;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{
    public class AppSettingsProviderForDaysOfWeekShould
    {
        [Fact]
        public void ReadDaysTimePeriod()
        {
            IDaysOfWeekToggleValueProvider sut = new AppSettingsProvider();

            var daysFromConfig = sut.GetDaysOfWeek(new MondayAndFridayToggle());

            var expected = new[] {DayOfWeek.Monday, DayOfWeek.Friday};

            Assert.Equal(expected, daysFromConfig);
        }

        
        [Fact]
        public void InvalidDayInConfig()
        {
            var sut = new AppSettingsProvider();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.GetDaysOfWeek(new InvalidDayToggle()).ToList());

            Assert.Equal("The value 'Sun' in config key 'FeatureToggle.InvalidDayToggle' is not a valid day of the week. Days should be specified in long format. E.g. Friday and not Fri.", ex.Message);
        }
    

        [Fact]
        public void ErrorWhenKeyNotInConfig()
        {
            Assert.Throws<ToggleConfigurationError>(
                () =>
                    new AppSettingsProvider().GetDaysOfWeek(new NotInConfig()).ToList());
        }


        private class MondayAndFridayToggle : EnabledOnDaysOfWeekFeatureToggle {}
        private class InvalidDayToggle : EnabledOnDaysOfWeekFeatureToggle {}
        private class NotInConfig : EnabledOnDaysOfWeekFeatureToggle {}

      
    }
}