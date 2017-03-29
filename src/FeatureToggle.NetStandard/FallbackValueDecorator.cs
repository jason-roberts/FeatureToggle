using System;

namespace FeatureToggle
{
    public class FallbackValueDecorator : IFeatureToggle
    {
        private readonly Action<Exception> _logAction;

        public FallbackValueDecorator(IFeatureToggle primaryToggle, IFeatureToggle fallbackToggle,
            Action<Exception> logAction = null)
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

        public IFeatureToggle PrimaryToggle { get; }
        public IFeatureToggle FallbackToggle { get; }

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
                    _logAction?.Invoke(ex);

                    return FallbackToggle.FeatureEnabled;
                }
            }
        }
    }
}