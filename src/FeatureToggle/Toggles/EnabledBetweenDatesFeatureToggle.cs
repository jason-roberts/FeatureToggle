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
#if (WINDOWS_PHONE || NETFX_CORE)

            ToggleValueProvider = new ApplicationResourcesSettingsProvider();
#else

            ToggleValueProvider = new AppSettingsProvider();
#endif
        }



        public Func<DateTime> NowProvider { get; set; }

        public virtual ITimePeriodProvider ToggleValueProvider { get; set; }


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
