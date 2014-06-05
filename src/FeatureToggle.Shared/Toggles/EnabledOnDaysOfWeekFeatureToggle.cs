using System;
using System.Linq;
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
                var dayToday= NowProvider.Invoke().DayOfWeek;

                var daysShouldBeEnabled = ToggleValueProvider.GetDaysOfWeek(this);

                return daysShouldBeEnabled.Contains(dayToday);
            }
        }   
    }
}
