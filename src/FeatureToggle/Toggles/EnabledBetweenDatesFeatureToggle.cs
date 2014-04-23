//using System;
//#if (WINDOWS_PHONE)
//using JasonRoberts.FeatureToggle.Wp7;
//#endif

using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;

namespace FeatureToggle.Toggles
{
    public abstract class EnabledBetweenDatesFeatureToggle : IFeatureToggle
    {

        protected EnabledBetweenDatesFeatureToggle()
        {
            NowProvider = () => DateTime.Now;
#if (WINDOWS_PHONE)

            ToggleValueProvider = new WindowsPhone7ApplicationResourcesSettingsProvider();
#else

            ToggleValueProvider = new AppSettingsProvider();
#endif
        }



        public Func<DateTime> NowProvider { get; set; }

        public ITimePeriodProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get
            {
                var dates = ToggleValueProvider.EvaluateTimePeriod((this));

                var now = NowProvider.Invoke();

                return (now >= dates.Item1 && now <= dates.Item2);
            }
        }   
    }
}
