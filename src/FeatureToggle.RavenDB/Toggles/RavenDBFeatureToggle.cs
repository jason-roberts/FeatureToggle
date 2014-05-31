using FeatureToggle.Core;
using FeatureToggle.Providers;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Toggles
// ReSharper restore CheckNamespace
{
// ReSharper disable InconsistentNaming
    public abstract class RavenDBFeatureToggle : IFeatureToggle
// ReSharper restore InconsistentNaming
    {
        protected RavenDBFeatureToggle()
        {
            ToggleValueProvider = new BooleanRavenDBProvider();
        }


        public virtual IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return ToggleValueProvider.EvaluateBooleanToggleValue(this); }
        }

    }
}

