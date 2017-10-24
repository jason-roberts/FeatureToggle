#if NETFULL

using System;
using FeatureToggle;
using FeatureToggle.Internal;

namespace FeatureToggle
{
    public abstract class HttpJsonFeatureToggle : IFeatureToggle
    {
        protected HttpJsonFeatureToggle()
        {
            ToggleValueProvider = new AppSettingsProvider();
        }


        public virtual IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get
            {
                return ToggleValueProvider.EvaluateBooleanToggleValue(this);
            }
        }

    }
}

#endif