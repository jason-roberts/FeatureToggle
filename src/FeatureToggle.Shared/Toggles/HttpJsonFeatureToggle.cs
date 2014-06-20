using System;
#if (FEATURETOGGLE_FULL)
using FeatureToggle.Core;
using FeatureToggle.Providers;

namespace FeatureToggle.Toggles
{
    public abstract class HttpJsonFeatureToggle : IFeatureToggle
    {
        protected HttpJsonFeatureToggle()
        {
            //ToggleValueProvider = new BooleanSqlServerProvider();
        }


        public virtual IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get
            {
                throw new NotImplementedException();
                //return ToggleValueProvider.EvaluateBooleanToggleValue(this);
            }
        }

    }
}

#endif