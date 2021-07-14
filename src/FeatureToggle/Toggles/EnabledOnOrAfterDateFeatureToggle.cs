using System;
using FeatureToggle;
using FeatureToggle.Interfaces;
using FeatureToggle.Internal;

namespace FeatureToggle
{
    public abstract class EnabledOnOrAfterDateFeatureToggle : IFeatureToggle
    {        
        
        protected EnabledOnOrAfterDateFeatureToggle()
        {
            NowProvider = () => DateTime.Now;

            ToggleValueProvider = new AppSettingsProvider();

        }



        public Func<DateTime> NowProvider { get; set; }

        public virtual IDateTimeToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return NowProvider.Invoke() >= ToggleValueProvider.EvaluateDateTimeToggleValue(this); }
        }   
    }
}
