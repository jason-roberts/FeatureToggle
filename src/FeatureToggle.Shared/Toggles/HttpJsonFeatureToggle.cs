#if (FEATURETOGGLE_FULL)
using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;

namespace FeatureToggle.Toggles
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