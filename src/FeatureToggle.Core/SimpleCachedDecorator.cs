using System;

namespace FeatureToggle.Core
{
    public class SimpleCachedDecorator : IFeatureToggle
    {
        private readonly TimeSpan _cacheDuration;
        private bool _cachedValue;
        private DateTime _cachedValueLastUpdatedTime;
        private DateTime _cacheExpiryTime;
        public IFeatureToggle WrappedToggle { get; private set; }

        public SimpleCachedDecorator(IFeatureToggle toggleToWrap, TimeSpan cacheDuration)
        {            
            if (toggleToWrap == null)
            {
                throw new ArgumentNullException("toggleToWrap");
            }

            WrappedToggle = toggleToWrap;
            _cacheDuration = cacheDuration;

            SetCachedValue();
        }

        private void SetCachedValue()
        {
            _cachedValue = WrappedToggle.FeatureEnabled;

            _cachedValueLastUpdatedTime = DateTime.Now;
            _cacheExpiryTime = _cachedValueLastUpdatedTime.Add(_cacheDuration);
        }


        public bool FeatureEnabled
        {
            get
            {
                var cacheHasExpired = DateTime.Now > _cacheExpiryTime;

                if (cacheHasExpired)
                {
                    SetCachedValue();
                }

                return _cachedValue;
            }
        }
    }
}