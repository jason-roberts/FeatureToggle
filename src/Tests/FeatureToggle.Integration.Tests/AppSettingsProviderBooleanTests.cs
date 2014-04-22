using System;
using System.Configuration;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{
    public class AppSettingsProviderBooleanTests
    {
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


        [Fact]
        public void ShouldErrorWhenCannotConvertConfig()
        {
            var ex = Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotABooleanValue()));

            Assert.Equal(typeof(FormatException), ex.InnerException.GetType());
        }


        [Fact]
        public void ShouldErrorWhenKeyNotInConfig()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () =>
                    new AppSettingsProvider().EvaluateBooleanToggleValue(new NotInConfig()));
        }        


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