#if NETFULL || NETCORE


using System;
//using FeatureToggle;
using FeatureToggle.Internal;
using Xunit;

namespace FeatureToggle.Shared.Tests.Integration
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
        public void ErrorWhenEndDateFormatIsWrong()
        {
            var ex = Assert.Throws<ToggleConfigurationError>(
                () =>
                    new AppSettingsProvider().EvaluateTimePeriod(
                        new FormatInConfigIsWrong()));
#if NETCORE
            Assert.Equal(
                "The value '02/01/2050 04:05:44' cannot be converted to a DateTime as defined in config key 'FormatInConfigIsWrong'. The expected format is: dd-MMM-yyyy HH:mm:ss",
                ex.Message);
#else
            Assert.Equal(
                            "The value '02/01/2050 04:05:44' cannot be converted to a DateTime as defined in config key 'FeatureToggle.FormatInConfigIsWrong'. The expected format is: dd-MMM-yyyy HH:mm:ss",
                            ex.Message);
#endif
        }


        [Fact]
        public void ErrorWhenKeyNotInConfig()
        {
            Assert.Throws<ToggleConfigurationError>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig()));
        }


        private class FormatInConfigIsWrong : EnabledBetweenDatesFeatureToggle
        {
        }

        private class NotInConfig : SimpleFeatureToggle
        {
        }

        private class TimePeriod : EnabledBetweenDatesFeatureToggle
        {
        }
    }
}

#endif