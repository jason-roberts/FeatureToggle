using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;

namespace FeatureToggle.Toggles
{
    public abstract class EnabledOnDaysOfWeekFeatureToggle : IFeatureToggle
    {        
        
        protected EnabledOnDaysOfWeekFeatureToggle()
        {
            NowProvider = () => DateTime.Now;
#if (WINDOWS_PHONE || NETFX_CORE)

            ToggleValueProvider = new ApplicationResourcesSettingsProvider();
#else

            ToggleValueProvider = new AppSettingsProvider();
#endif
        }



        public Func<DateTime> NowProvider { get; set; }

        public virtual IDaysOfWeekToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get
            {
                return false;// NowProvider.Invoke() >= ToggleValueProvider.EvaluateDateTimeToggleValue(this); 
            }
        }   
    }
}
