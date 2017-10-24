using System;

namespace FeatureToggle
{
    public class FixedTimeCacheDecorator : IFeatureToggle
    {
        private readonly TimeSpan _cacheDuration;
        private bool _cachedValue;

        public FixedTimeCacheDecorator(IFeatureToggle toggleToWrap, TimeSpan cacheDuration,
            Func<DateTime> alternativeNowProvider = null)
        {
            if (toggleToWrap == null)
            {
                throw new ArgumentNullException("toggleToWrap");
            }

            WrappedToggle = toggleToWrap;

            _cacheDuration = cacheDuration;

            if (alternativeNowProvider == null)
            {
                NowProvider = () => DateTime.Now;
            }
            else
            {
                NowProvider = alternativeNowProvider;
            }

            SetCachedValue();
        }

        public DateTime CachedValueLastUpdatedTime { get; private set; }
        public DateTime CacheExpiryTime { get; private set; }
        public IFeatureToggle WrappedToggle { get; private set; }
        public Func<DateTime> NowProvider { get; set; }

        public bool FeatureEnabled
        {
            get
            {
                var cacheHasExpired = NowProvider() > CacheExpiryTime;

                if (cacheHasExpired)
                {
                    SetCachedValue();
                }

                return _cachedValue;
            }
        }

        private void SetCachedValue()
        {
            _cachedValue = WrappedToggle.FeatureEnabled;

            CachedValueLastUpdatedTime = NowProvider();
            CacheExpiryTime = CachedValueLastUpdatedTime.Add(_cacheDuration);
        }
    }
}