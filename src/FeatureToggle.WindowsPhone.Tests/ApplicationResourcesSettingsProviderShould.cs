using System;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FeatureToggle.WindowsPhone.Tests
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
        
    }


 
}
