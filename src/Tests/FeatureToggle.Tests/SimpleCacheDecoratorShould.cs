using System;
using System.Threading;
using FeatureToggle.Core;
using Xunit;

namespace FeatureToggle.Tests
{
    public class SimpleCacheDecoratorShould
    {      
        [Fact]
        public void ErrorWhenNullWrappedTogglesSupplied()
        {
            Assert.Throws<ArgumentNullException>(() => new SimpleCachedDecorator(null, TimeSpan.Zero));
        }


        [Fact]
        public void RetrieveInitialValueFromWrappedToggle()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var sut = new SimpleCachedDecorator(cachedToggle, TimeSpan.FromSeconds(1));

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void MaintainInitialValueWithinCacheDurationWhenUnderlyingToggleValueChanges()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var sut = new SimpleCachedDecorator(cachedToggle, TimeSpan.FromSeconds(1));

            cachedToggle.Disable();

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void UpdateNewValueAfterCacheDurationWhenUnderlyingToggleValueChanged()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var sut = new SimpleCachedDecorator(cachedToggle, TimeSpan.FromSeconds(1));

            cachedToggle.Disable();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Assert.False(sut.FeatureEnabled);
        }

        
        private class CacheTestToggle : IFeatureToggle
        {
            private bool _isEnabled;

            public void Enable()
            {
                _isEnabled = true;
            }

            public void Disable()
            {
                _isEnabled = false;
            }

            public bool FeatureEnabled
            {
                get
                {
                    return _isEnabled;                    
                }
            }
        }
    }

    
}