using System;
using FeatureToggle.Core;
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
        public void ReturnValueOfFallbackToggleIfPrimaryToggleNotCOnfiguredOrErrors()
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





        private class AnEnabledFeature : AlwaysOnFeatureToggle {}
        private class ADisabledFeature : AlwaysOffFeatureToggle {}

        private class AnErroringToggle : IFeatureToggle
        {
            public bool FeatureEnabled
            {
                get
                {
                    throw new ToggleConfigurationError("Simulated toggle exception");                     
                }
            }
        }

  
    }
}