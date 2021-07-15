

using System;
using FeatureToggle;
using Commify.FeatureToggle.Interfaces;
using Commify.FeatureToggle.Internal;

namespace Commify.FeatureToggle.Toggles
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