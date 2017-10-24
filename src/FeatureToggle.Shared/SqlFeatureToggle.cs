#if NETFULL


// TODO: net core sql support?
using FeatureToggle.Internal;

namespace FeatureToggle
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

#endif