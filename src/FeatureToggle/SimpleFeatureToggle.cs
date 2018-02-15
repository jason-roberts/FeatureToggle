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

            ToggleValueProvider = new AppSettingsProvider();

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