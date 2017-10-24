using System;
using FeatureToggle;
using FeatureToggle.Internal;

namespace FeatureToggle
{
    public abstract class EnabledOnOrAfterDateFeatureToggle : IFeatureToggle
    {        
        
        protected EnabledOnOrAfterDateFeatureToggle()
        {
            NowProvider = () => DateTime.Now;
#if WINDOWS_UWP

            ToggleValueProvider = new ApplicationResourcesSettingsProvider();
#else
            ToggleValueProvider = new AppSettingsProvider();
#endif
        }



        public Func<DateTime> NowProvider { get; set; }

        public virtual IDateTimeToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return NowProvider.Invoke() >= ToggleValueProvider.EvaluateDateTimeToggleValue(this); }
        }   
    }
}
