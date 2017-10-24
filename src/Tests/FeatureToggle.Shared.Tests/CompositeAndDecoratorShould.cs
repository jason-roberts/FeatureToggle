using System;
using FeatureToggle;
using Xunit;

namespace FeatureToggle.Tests
{
    public class CompositeAndDecoratorShould
    {
        [Fact]
        public void ReturnFalseWhenOneWrappedToggleIsDisabled()
        {
            var sut = new CompositeAndDecorator(new AnEnabledFeature(), new ADisabledFeature());

            Assert.False(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnFalseWhenAllWrappedTogglesAreDisabled()
        {
            var sut = new CompositeAndDecorator(new ADisabledFeature(), new AnotherDisabledFeature());

            Assert.False(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnTrueWhenAllWrappedTogglesAreEnabled()
        {
            var sut = new CompositeAndDecorator(new AnEnabledFeature(), new AnotherEnabledFeature());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ErrorWhenNullWrappedTogglesSupplied()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeAndDecorator(null));
        }


        [Fact]
        public void ErrorWhenNoWrappedTogglesSupplied()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CompositeAndDecorator());
        }



        private class AnEnabledFeature : AlwaysOnFeatureToggle {}
        private class AnotherEnabledFeature : AlwaysOnFeatureToggle {}
        private class ADisabledFeature : AlwaysOffFeatureToggle {}
        private class AnotherDisabledFeature : AlwaysOffFeatureToggle {}
  
    }
}