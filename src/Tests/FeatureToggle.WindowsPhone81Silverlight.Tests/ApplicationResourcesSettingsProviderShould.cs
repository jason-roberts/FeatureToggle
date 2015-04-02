using System;
using System.Collections.Generic;
using System.Linq;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FeatureToggle.WindowsPhone81Silverlight.Tests
{
    [TestClass]
    public class ApplicationResourcesSettingsProviderShould
    {
        [TestMethod]        
        public void ReadBooleanTrue()
        {
            var result = false;

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.EvaluateBooleanToggleValue(new BooleanTrue()); });

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void ReadBooleanFalse()
        {
            var result = true;

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.EvaluateBooleanToggleValue(new BooleanFalse()); });            

            Assert.IsFalse(result);
        }



        [TestMethod]
        public void ErrorWhenKeyNotInConfig()
        {
            Exception expectedEx =  null;

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

            Assert.IsNotNull(expectedEx, "Exception expected");           
        }


        [TestMethod]
        public void ReadDate()
        {
            var result = DateTime.MaxValue;

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.EvaluateDateTimeToggleValue(new SimpleToggle()); });

            Assert.AreEqual(new DateTime(2000, 1, 1, 23, 22, 21), result);
        }


        [TestMethod]
        public void ReadDatePeriod()
        {
            var result = Tuple.Create(DateTime.MinValue, DateTime.MaxValue);

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.EvaluateTimePeriod(new PeriodToggle()); });

            Assert.AreEqual(new DateTime(2000, 1, 1, 23, 22, 21), result.Item1);
            Assert.AreEqual(new DateTime(2001, 1, 1, 23, 22, 21), result.Item2);
        }


        [TestMethod]
        public void ReadDaysOfWeek()
        {
            List<DayOfWeek> result = null;

            var sut = new ApplicationResourcesSettingsProvider();

            RunOn.Dispatcher(() => { result = sut.GetDaysOfWeek(new DaysToggle()).ToList(); });

            Assert.AreEqual(DayOfWeek.Wednesday, result[0]);
            Assert.AreEqual(DayOfWeek.Saturday, result[1]);
            Assert.AreEqual(2, result.Count);   
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
