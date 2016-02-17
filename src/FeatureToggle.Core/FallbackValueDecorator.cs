using System;

namespace FeatureToggle.Core
{

    public class FallbackValueDecorator : IFeatureToggle
    {
        public IFeatureToggle PrimaryToggle { get; private set; }
        public IFeatureToggle FallbackToggle { get; private set; }
        private readonly Action<Exception> _logAction;

        public FallbackValueDecorator(IFeatureToggle primaryToggle, IFeatureToggle fallbackToggle, Action<Exception> logAction = null)
        {
            if (primaryToggle == null)
            {
                throw new ArgumentNullException(nameof(primaryToggle));
            }

            if (fallbackToggle == null)
            {
                throw new ArgumentNullException(nameof(fallbackToggle));
            }

            PrimaryToggle = primaryToggle;
            FallbackToggle = fallbackToggle;
            _logAction = logAction;
        }

        public bool FeatureEnabled
        {
            get
            {
                try
                {
                    return PrimaryToggle.FeatureEnabled;
                }
                catch (Exception ex)
                {                    
                    return FallbackToggle.FeatureEnabled;                    
                }
            }
        }
    }
}
