using System;
using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{
    public class AppSettingsProviderTests
    {
        #region "boolean toggle tests"

        [Fact]
        public void ShouldReadBooleanTrueFromConfig()
        {
            Assert.True(new AppSettingsProvider().EvaluateBooleanToggleValue(new SimpleFeatureTrue()));
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

            var periodFromConfig = sut.EvaluateTimePeriod(new TimePeriod());

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
                        new StartDateAfterEndDate()));
        }


        [Fact]
        public void ShouldErrorWhenStartDateAndEndDateAreTheSame()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new StartDateAndEndDateAreTheSame()));
        }


        [Fact]
        public void ShouldErrorWhenFormatInConfigIsWrong()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new FormatInConfigIsWrong()));
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
            var ex = Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotABooleanValue()));

            Assert.Equal(typeof(FormatException), ex.InnerException.GetType());
        }

        #endregion

        private class FormatInConfigIsWrong : EnabledBetweenDatesFeatureToggle
        {
        }

        private class StartDateAfterEndDate : EnabledBetweenDatesFeatureToggle
        {
        }

        private class StartDateAndEndDateAreTheSame :
            EnabledBetweenDatesFeatureToggle
        {
        }

        private class TimePeriod : EnabledBetweenDatesFeatureToggle
        {
        }

        private class InvalidDateFormat : EnabledOnOrAfterDateFeatureToggle
        {
        };

        private class NotABooleanValue : SimpleFeatureToggle
        {
        }

        private class NotInConfig : SimpleFeatureToggle
        {
        }

        private class SimpleFeatureFalse : SimpleFeatureToggle
        {
        }

        private class SimpleFeatureTrue : SimpleFeatureToggle
        {
        }
    }
}