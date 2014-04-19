using System;
using System.Configuration;
using Xunit;

namespace JasonRoberts.FeatureToggle.Tests
{
    
    public class AppSettingsProviderTests
    {

        #region "boolean toggle tests"

        [Fact]
        public void ShouldReadBooleanTrueFromConfig()
        {
            Assert.True(new AppSettingsProvider().EvaluateBooleanToggleValue( new SimpleFeatureTrue()));
        }


        [Fact]
        public void ShouldReadBooleanFalseFromConfig()
        {
            Assert.False(new AppSettingsProvider().EvaluateBooleanToggleValue(new SimpleFeatureFalse()));
        }


        #endregion




        #region "single date tests"

        [Fact]
        public void ShouldErrorWhenBadDateFormat()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () => new AppSettingsProvider().EvaluateDateTimeToggleValue(new InvalidDateFormat()));
        }


        #endregion




        #region "time range config test"

        [Fact]
        public void ShouldReadTimePeriodFromConfig()
        {
            ITimePeriodProvider sut = new AppSettingsProvider();

            var periodFromConfig = sut.EvaluateTimePeriod(new AppSettingsProviderTestsTimePeriod());

            var expected = new Tuple<DateTime, DateTime>(new DateTime(2050, 1, 2, 4, 5, 8),
                                                         new DateTime(2099, 8, 7, 6, 5, 4));

            Assert.Equal(expected, periodFromConfig);
        }


        [Fact]
        public void ShouldErrorWhenStartDateAfterEndDate()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new AppSettingsProviderTestsShouldErrorWhenStartDateAfterEndDate()));

        }


        [Fact]
        public void ShouldErrorWhenStartDateAndEndDateAreTheSame()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new AppSettingsProviderTestsShouldErrorWhenStartDateAndEndDateAreTheSame()));
        }


        [Fact]
        public void ShouldErrorWhenFormatInConfigIsWrong()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new AppSettingsProviderTestsShouldErrorWhenFormatInConfigIsWrong()));
        }


        [Fact]
        public void ShouldErrorWhenKeyNotInConfig()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig()));
        }


        [Fact]
        public void ShouldErrorWhenCannotConvertConfig()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotASimpleValue()));
        }


        #endregion

        private class SimpleFeatureTrue : SimpleFeatureToggle{}
        private class SimpleFeatureFalse : SimpleFeatureToggle { }
        private class NotInConfig : SimpleFeatureToggle { }
        private class NotASimpleValue : SimpleFeatureToggle { }

        private class InvalidDateFormat : EnabledOnOrAfterDateFeatureToggle{};

        private class AppSettingsProviderTestsTimePeriod : EnabledBetweenDatesFeatureToggle { }
        private class AppSettingsProviderTestsShouldErrorWhenStartDateAfterEndDate : EnabledBetweenDatesFeatureToggle { }
        private class AppSettingsProviderTestsShouldErrorWhenStartDateAndEndDateAreTheSame : EnabledBetweenDatesFeatureToggle { }
        private class AppSettingsProviderTestsShouldErrorWhenFormatInConfigIsWrong : EnabledBetweenDatesFeatureToggle { }                            
    }
}
