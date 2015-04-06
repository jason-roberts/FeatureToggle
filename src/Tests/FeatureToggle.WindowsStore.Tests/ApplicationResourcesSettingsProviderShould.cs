using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.WindowsStore.Tests
{
    [Trait("category", "Threaded")]
    public class ApplicationResourcesSettingsProviderShould
    {
        [Fact]
        public async void ReadBooleanTrue()
        {
            var result = false;

            var sut = new ApplicationResourcesSettingsProvider();



            await RunOn.Dispatcher(() =>
            {
                Application.Current.Resources["FeatureToggle.BooleanTrue"] = true;
                result = sut.EvaluateBooleanToggleValue(new BooleanTrue());
            });

            Assert.True(result);
        }


        [Fact]
        public async void ReadBooleanFalse()
        {
            var result = true;

            var sut = new ApplicationResourcesSettingsProvider();

            await RunOn.Dispatcher(() =>
            {
                Application.Current.Resources["FeatureToggle.BooleanFalse"] = false;
                result = sut.EvaluateBooleanToggleValue(new BooleanFalse());
            });


            Assert.False(result);
        }



        [Fact]
        public async void ErrorWhenKeyNotInConfig()
        {
            Exception expectedEx = null;

            try
            {
                await RunOn.Dispatcher(() =>
                {
                    new ApplicationResourcesSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig());
                });
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }


            Assert.True(expectedEx.Message.EndsWith("was not found in Application.Current.Resources"));
        }


        [Fact]
        public async void ReadDate()
        {
            var result = DateTime.MaxValue;

            var sut = new ApplicationResourcesSettingsProvider();

            await RunOn.Dispatcher(() =>
            {
                Application.Current.Resources["FeatureToggle.SimpleToggle"] = "01-Feb-2000 23:22:21";
                result = sut.EvaluateDateTimeToggleValue(new SimpleToggle());
            });

            Assert.Equal(new DateTime(2000, 2, 1, 23, 22, 21), result);
        }


        [Fact]
        public async void ReadDatePeriod()
        {
            var result = Tuple.Create(DateTime.MinValue, DateTime.MaxValue);

            var sut = new ApplicationResourcesSettingsProvider();

            await RunOn.Dispatcher(() =>
            {
                Application.Current.Resources["FeatureToggle.PeriodToggle"] = "01-Jan-2000 23:22:21 | 01-Jan-2001 23:22:21";
                result = sut.EvaluateTimePeriod(new PeriodToggle());
            });

            Assert.Equal(new DateTime(2000, 1, 1, 23, 22, 21), result.Item1);
            Assert.Equal(new DateTime(2001, 1, 1, 23, 22, 21), result.Item2);
        }


        [Fact]
        public async void ReadDaysOfWeek()
        {
            List<DayOfWeek> result = null;

            var sut = new ApplicationResourcesSettingsProvider();

            await RunOn.Dispatcher(() =>
            {
                Application.Current.Resources["FeatureToggle.DaysToggle"] = "Wednesday, Saturday";
                result = sut.GetDaysOfWeek(new DaysToggle()).ToList();
            });

            Assert.Equal(DayOfWeek.Wednesday, result[0]);
            Assert.Equal(DayOfWeek.Saturday, result[1]);
            Assert.Equal(2, result.Count);
        }



        private class BooleanTrue : SimpleFeatureToggle
        {
        }

        private class BooleanFalse : SimpleFeatureToggle
        {
        }

        private class NotInConfig : SimpleFeatureToggle
        {
        }

        private class SimpleToggle : SimpleFeatureToggle
        {
        }


        private class PeriodToggle : SimpleFeatureToggle
        {
        }

        private class DaysToggle : EnabledOnDaysOfWeekFeatureToggle
        {
        }

    }
}
