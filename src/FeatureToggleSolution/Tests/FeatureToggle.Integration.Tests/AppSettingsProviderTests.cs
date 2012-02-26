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
    }
}
