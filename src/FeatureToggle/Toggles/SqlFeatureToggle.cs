

// TODO: net core sql support?
using Commify.FeatureToggle.Interfaces;
using Commify.FeatureToggle.Internal;

namespace Commify.FeatureToggle.Toggles
{
    public abstract class SqlFeatureToggle : IFeatureToggle
    {
        protected SqlFeatureToggle()
        {
            ToggleValueProvider = new BooleanSqlServerProvider();
        }


        public virtual IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return ToggleValueProvider.EvaluateBooleanToggleValue(this); }
        }

    }
}