using System;
using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{
    public class AppSettingsProviderTimePeriodShould
    {
        [Fact]
        public void ReadTimePeriod()
        {
            ITimePeriodProvider sut = new AppSettingsProvider();

            var periodFromConfig = sut.EvaluateTimePeriod(new TimePeriod());

            var expected = new Tuple<DateTime, DateTime>(new DateTime(2050, 1, 2, 4, 5, 8),
                new DateTime(2099, 8, 7, 6, 5, 4));

            Assert.Equal(expected, periodFromConfig);
        }


        [Fact]
        public void ErrorWhenStartDateAfterEndDate()
        {
            var ex = Assert.Throws<ConfigurationErrorsException>(
                () => new AppSettingsProvider().EvaluateTimePeriod(new StartDateAfterEndDate()));

            Assert.Equal(
                "Configuration for FeatureToggle.StartDateAfterEndDate is invalid - the start date must be less then the end date",
                ex.Message);
        }


        [Fact]
        public void ErrorWhenStartDateAndEndDateAreTheSame()
        {
            var ex = Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new StartDateAndEndDateAreTheSame()));

            Assert.Equal(
                "Configuration for FeatureToggle.StartDateAndEndDateAreTheSame is invalid - the start date must be less then the end date",
                ex.Message);
        }


        [Fact]
        public void ErrorWhenEndDateFormatIsWrong()
        {
            var ex = Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new FormatInConfigIsWrong()));

            Assert.Equal(
                "Configuration for FeatureToggle.FormatInConfigIsWrong is invalid - date range should be specified like: '02-Jan-2050 04:05:08 | 07-Aug-2099 06:05:04'",
                ex.Message);            
        }


        [Fact]
        public void ErrorWhenKeyNotInConfig()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig()));
        }




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

        private class NotInConfig : SimpleFeatureToggle
        {
        }
    }
}