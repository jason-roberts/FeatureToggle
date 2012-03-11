using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class AppSettingsProviderTests
    {
        [TestMethod]
        public void ShouldReadBooleanTrueFromConfig()
        {
            Assert.IsTrue(new AppSettingsProvider().EvaluateBooleanToggleValue( new SimpleFeatureTrue()));
        }


        [TestMethod]
        public void ShouldReadBooleanFalseFromConfig()
        {
            Assert.IsFalse(new AppSettingsProvider().EvaluateBooleanToggleValue(new SimpleFeatureFalse()));
        }


        [TestMethod]
        public void ShouldReadTimePeriodFromConfig()
        {
            ITimePeriodProvider sut = new AppSettingsProvider();

            var periodFromConfig = sut.EvaluateTimePeriod(new AppSettingsProviderTestsTimePeriod());

            var expected = new Tuple<DateTime, DateTime>(new DateTime(2050, 1, 2, 4, 5, 8),
                                                         new DateTime(2099, 8, 7, 6, 5, 4));

            Assert.AreEqual(expected, periodFromConfig);
        }


        [TestMethod]
        public void ShouldErrorWhenStartDateAfterEndDate()
        {
            Assert.Inconclusive();            
        }

        [TestMethod]
        public void ShouldErrorWhenStartDateAndEndDateAreTheSame()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldErrorWhenFormatInConfigIsWrong()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldErrorWhenStartDateNotAValidDate()
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void ShouldErrorWhenEndDateNotAValidDate()
        {
            Assert.Inconclusive();
        }

        [TestMethod, ExpectedException(typeof(ConfigurationErrorsException))]
        public void ShouldErrorWhenKeyNotInConfig()
        {
            new AppSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig());            
        }


        [TestMethod, ExpectedException(typeof(ConfigurationErrorsException))]
        public void ShouldErrorWhenCannotConvertConfig()
        {
            new AppSettingsProvider().EvaluateBooleanToggleValue(new NotASimpleValue());
        }


        private class SimpleFeatureTrue : SimpleFeatureToggle{}
        private class SimpleFeatureFalse : SimpleFeatureToggle { }
        private class NotInConfig : SimpleFeatureToggle { }
        private class NotASimpleValue : SimpleFeatureToggle { }

        private class AppSettingsProviderTestsTimePeriod : EnabledBetweenDatesFeatureToggle { }     
        
    }
}
