using System;
using FeatureToggle;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FixedTimeCacheDecoratorShould
    {      
        [Fact]
        public void ErrorWhenNoWrappedToggleSupplied()
        {
            Assert.Throws<ArgumentNullException>(() => new FixedTimeCacheDecorator(null, TimeSpan.Zero));
        }


        [Fact]
        public void RetrieveInitialValueFromWrappedToggle()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var sut = new FixedTimeCacheDecorator(cachedToggle, TimeSpan.FromSeconds(1));

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void MaintainInitialValueWithinCacheDurationEvenWhenUnderlyingToggleValueChanges()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var sut = new FixedTimeCacheDecorator(cachedToggle, TimeSpan.FromSeconds(1));

            cachedToggle.Disable();

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void UpdateNewValueAfterCacheExpires()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var creationTime = new DateTime(2000, 1, 25);
            const int durationTicks = 1;

            var sut = new FixedTimeCacheDecorator(cachedToggle, TimeSpan.FromTicks(durationTicks), () => creationTime);


            cachedToggle.Disable();


            sut.NowProvider = () => creationTime.AddTicks(durationTicks + 1);
            Assert.False(sut.FeatureEnabled);
        }


        [Fact]
        public void CacheTimesShouldBeInitialisedOnCreation()
        {
            var cachedToggle = new CacheTestToggle();
            var duration = TimeSpan.FromMilliseconds(42);

            var sut = new FixedTimeCacheDecorator(cachedToggle, duration, () => new DateTime(2000, 1, 25));

            Assert.Equal(new DateTime(2000, 1, 25), sut.CachedValueLastUpdatedTime);
            Assert.Equal(new DateTime(2000, 1, 25).Add(duration), sut.CacheExpiryTime);
        }


        [Fact]
        public void CacheTimesShouldBeUpdatedAfterExpiry()
        {
            var cachedToggle = new CacheTestToggle();
            cachedToggle.Enable();

            var creationTime = new DateTime(2000, 1, 25);
            const int durationTicks = 1;

            var sut = new FixedTimeCacheDecorator(cachedToggle, TimeSpan.FromTicks(durationTicks), () => creationTime);


            cachedToggle.Disable();


            const int passedTimeInTicks = durationTicks + 1;

            sut.NowProvider = () => creationTime.AddTicks(passedTimeInTicks);

            var dontCare = sut.FeatureEnabled;

            Assert.Equal(creationTime.AddTicks(passedTimeInTicks), sut.CachedValueLastUpdatedTime);
            Assert.Equal(creationTime.AddTicks(passedTimeInTicks).AddTicks(durationTicks), sut.CacheExpiryTime);
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