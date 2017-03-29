using System;
using FeatureToggle;
using FeatureToggle.Shared.Tests.TestToggles;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FallbackValueDecoratorShould
    {
        [Fact]
        public void ReturnValueOfPrimaryToggleIfAvailable()
        {
            var sut = new FallbackValueDecorator(new AnEnabledFeature(), new ADisabledFeature());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnValueOfFallbackToggleIfPrimaryToggleNotConfiguredOrErrors()
        {
            var sut = new FallbackValueDecorator(new AnErroringToggle(), new AnEnabledFeature());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ErrorIfFallbackToggleErrors()
        {
            var sut = new FallbackValueDecorator(new AnErroringToggle(), new AnErroringToggle());

            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);
        }


        [Fact]
        public void ErrorWhenNullPrimaryToggleSupplied()
        {
            Assert.Throws<ArgumentNullException>(() => new FallbackValueDecorator(null, new AnErroringToggle()));
        }


        [Fact]
        public void ErrorWhenNullFallbackToggleSupplied()
        {
            Assert.Throws<ArgumentNullException>(() => new FallbackValueDecorator(new AnEnabledFeature(), null));
        }


        [Fact]
        public void CallLoggingActionOnPrimaryToggleFailIfConfigured()
        {
            var actionWasCalled = false;

            var sut = new FallbackValueDecorator(new AnErroringToggle(), new AnEnabledFeature(),
                ex => actionWasCalled = true);

            var isEnabled = sut.FeatureEnabled;

            Assert.True(actionWasCalled);
        }


        private class AnEnabledFeature : AlwaysOnFeatureToggle
        {
        }

        private class ADisabledFeature : AlwaysOffFeatureToggle
        {
        }


    }
}