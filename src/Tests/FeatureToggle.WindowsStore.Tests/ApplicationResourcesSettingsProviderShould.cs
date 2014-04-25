// These tests are commented out until I figure out how to have app.xaml in this test project


//using System;
//using FeatureToggle.Providers;
//using FeatureToggle.Toggles;
//using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
//using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
//using Assert = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert;

//namespace FeatureToggle.WindowsStore.Tests
//{
//    [TestClass]
//    public class ApplicationResourcesSettingsProviderShould
//    {
//        [UITestMethod]        
//        public void ReadBooleanTrue()
//        {
//            var result = false;

//            var sut = new ApplicationResourcesSettingsProvider();

//            result = sut.EvaluateBooleanToggleValue(new BooleanTrue());

//            Assert.IsTrue(result);
//        }


//        [UITestMethod]
//        public void ReadBooleanFalse()
//        {
//            var result = true;

//            var sut = new ApplicationResourcesSettingsProvider();

//            result = sut.EvaluateBooleanToggleValue(new BooleanFalse());         

//            Assert.IsFalse(result);
//        }



//        [UITestMethod]
//        public void ErrorWhenKeyNotInConfig()
//        {
//            Exception expectedEx = null;
        
//            try
//            {
//                new ApplicationResourcesSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig());
//            }
//            catch (Exception ex)
//            {
//                expectedEx = ex;
//            }


//            Assert.IsNotNull(expectedEx, "Exception expected");
//        }


//        [UITestMethod]
//        public void ReadDate()
//        {
//            var result = DateTime.MaxValue;

//            var sut = new ApplicationResourcesSettingsProvider();

//            result = sut.EvaluateDateTimeToggleValue(new SimpleToggle());

//            Assert.AreEqual(new DateTime(2000, 1, 1, 23, 22, 21), result);
//        }


//        [UITestMethod]
//        public void ReadDatePeriod()
//        {
//            var result = Tuple.Create(DateTime.MinValue, DateTime.MaxValue);

//            var sut = new ApplicationResourcesSettingsProvider();

//            result = sut.EvaluateTimePeriod(new PeriodToggle());

//            Assert.AreEqual(new DateTime(2000, 1, 1, 23, 22, 21), result.Item1);
//            Assert.AreEqual(new DateTime(2001, 1, 1, 23, 22, 21), result.Item2);
//        }



//        private class BooleanTrue : SimpleFeatureToggle
//        {
//        }

//        private class BooleanFalse : SimpleFeatureToggle
//        {
//        }

//        private class NotInConfig : SimpleFeatureToggle
//        {
//        }
        
//        private class SimpleToggle : SimpleFeatureToggle
//        {
//        }


//        private class PeriodToggle : SimpleFeatureToggle
//        {
//        }
        
//    }


 
//}
