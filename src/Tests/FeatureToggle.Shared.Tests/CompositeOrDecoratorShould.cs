using System;
using FeatureToggle;
using Xunit;

namespace FeatureToggle.Tests
{
    public class CompositeOrDecoratorShould
    {
        [Fact]
        public void ReturnTrueWhenOneWrappedToggleIsDisabled()
        {
            var sut = new CompositeOrDecorator(new AnEnabledFeature(), new ADisabledFeature());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnFalseWhenAllWrappedTogglesAreDisabled()
        {
            var sut = new CompositeOrDecorator(new ADisabledFeature(), new AnotherDisabledFeature());

            Assert.False(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnTrueWhenAllWrappedTogglesAreEnabled()
        {
            var sut = new CompositeOrDecorator(new AnEnabledFeature(), new AnotherEnabledFeature());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ErrorWhenNullWrappedTogglesSupplied()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeOrDecorator(null));
        }


        [Fact]
        public void ErrorWhenNoWrappedTogglesSupplied()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CompositeOrDecorator());
        }



        private class AnEnabledFeature : AlwaysOnFeatureToggle {}
        private class AnotherEnabledFeature : AlwaysOnFeatureToggle {}
        private class ADisabledFeature : AlwaysOffFeatureToggle {}
        private class AnotherDisabledFeature : AlwaysOffFeatureToggle {}
  
    }
}