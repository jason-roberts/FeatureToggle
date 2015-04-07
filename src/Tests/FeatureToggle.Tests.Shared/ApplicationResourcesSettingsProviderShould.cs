// This is pretty messy due to the fact that xUnit 2.0 doesn't support Windows phone Silverlight 8.1
// so as this file is shared between multiple test projects, the Windows phone Silverlight 8.1 uses MSTest
// while the others use xunit. Also async tests don't show in Test Explorer for the Windows phone Silverlight 8.1
// test project, hence the conditional async/awaits 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;

#if NETFX_CORE    
    using Xunit;
#else
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif


// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests.Shared
// ReSharper restore CheckNamespace
{

#if NETFX_CORE
    [Trait("category", "Threaded")]
#else
    [TestClass]
#endif
    public class ApplicationResourcesSettingsProviderShould
    {


#if NETFX_CORE
    [Fact]
    public async void ReadBooleanTrue()
#else
        [TestMethod]
        public void ReadBooleanTrue()
#endif
        
        {
            var result = false;

            var sut = new ApplicationResourcesSettingsProvider();

#if NETFX_CORE
            await 
#endif
            RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.BooleanTrue", true);
                result = sut.EvaluateBooleanToggleValue(new BooleanTrue());
            });

            AssertFacade.True(result);
        }


#if NETFX_CORE
    [Fact]
    public async void ReadBooleanFalse()
#else
    [TestMethod]
    public void ReadBooleanFalse()
#endif        
        {
            var result = true;

            var sut = new ApplicationResourcesSettingsProvider();

#if NETFX_CORE
            await 
#endif
            RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.BooleanFalse", false);
                result = sut.EvaluateBooleanToggleValue(new BooleanFalse());
            });


            AssertFacade.False(result);
        }



#if NETFX_CORE
    [Fact]
    public async void ErrorWhenKeyNotInConfig()
#else
    [TestMethod]
    public void ErrorWhenKeyNotInConfig()
#endif        
        {
            Exception expectedEx = null;

#if NETFX_CORE
            await 
#endif
                RunOn.Dispatcher(() =>
                {
                    try
                    { 
                        new ApplicationResourcesSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig());
                        }
                    catch (Exception ex)
                    {
                        expectedEx = ex;
                    }

                });

            AssertFacade.True(expectedEx.Message.EndsWith("was not found in Application.Current.Resources"));
        }


#if NETFX_CORE
    [Fact]
    public async void ReadDate()
#else
    [TestMethod]
    public void ReadDate()
#endif        
        {
            var result = DateTime.MaxValue;

            var sut = new ApplicationResourcesSettingsProvider();

#if NETFX_CORE
            await 
#endif
            RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.SimpleToggle", "01-Feb-2000 23:22:21");
                result = sut.EvaluateDateTimeToggleValue(new SimpleToggle());
            });

            AssertFacade.Equal(new DateTime(2000, 2, 1, 23, 22, 21), result);
        }


#if NETFX_CORE
    [Fact]
    public async void ReadDatePeriod()
#else
    [TestMethod]
    public void ReadDatePeriod()
#endif        
        {
            var result = Tuple.Create(DateTime.MinValue, DateTime.MaxValue);

            var sut = new ApplicationResourcesSettingsProvider();

#if NETFX_CORE
            await 
#endif
                RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.PeriodToggle", "01-Jan-2000 23:22:21 | 01-Jan-2001 23:22:21");
                result = sut.EvaluateTimePeriod(new PeriodToggle());
            });

            AssertFacade.Equal(new DateTime(2000, 1, 1, 23, 22, 21), result.Item1);
            AssertFacade.Equal(new DateTime(2001, 1, 1, 23, 22, 21), result.Item2);
        }


#if NETFX_CORE
    [Fact]
    public async void ReadDaysOfWeek()
#else
    [TestMethod]
    public void ReadDaysOfWeek()
#endif
        {
            List<DayOfWeek> result = null;

            var sut = new ApplicationResourcesSettingsProvider();

#if NETFX_CORE
            await 
#endif
                RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.DaysToggle", "Wednesday, Saturday");
                result = sut.GetDaysOfWeek(new DaysToggle()).ToList();
            });

            AssertFacade.Equal(DayOfWeek.Wednesday, result[0]);
            AssertFacade.Equal(DayOfWeek.Saturday, result[1]);
            AssertFacade.Equal(2, result.Count);
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
