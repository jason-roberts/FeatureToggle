using System;
using FeatureToggle;
using FeatureToggle.Internal;

// ReSharper disable CheckNamespace
namespace FeatureToggle
// ReSharper restore CheckNamespace
{
    public abstract class SimpleFeatureToggle : IFeatureToggle
    {
        protected SimpleFeatureToggle()
        {
#if WINDOWS_UWP

            ToggleValueProvider = new ApplicationResourcesSettingsProvider();
#else
            ToggleValueProvider = new AppSettingsProvider();
#endif
        }


        public virtual IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return ToggleValueProvider.EvaluateBooleanToggleValue(this); }
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}