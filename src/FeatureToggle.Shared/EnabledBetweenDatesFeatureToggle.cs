using System;
using FeatureToggle;
using FeatureToggle.Internal;

namespace FeatureToggle
{
    public abstract class EnabledBetweenDatesFeatureToggle : IFeatureToggle
    {

        protected EnabledBetweenDatesFeatureToggle()
        {
            NowProvider = () => DateTime.Now;
#if WINDOWS_UWP

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
