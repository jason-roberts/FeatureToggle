//using System;
//#if (WINDOWS_PHONE)
//using JasonRoberts.FeatureToggle.Wp7;
//#endif

using FeatureToggle.Core;
using FeatureToggle.Providers;

namespace FeatureToggle
{
    public abstract class EnabledBetweenDatesFeatureToggle : IFeatureToggle
    {

        protected EnabledBetweenDatesFeatureToggle()
        {
            NowProvider = new NowDateAndTime();
#if (WINDOWS_PHONE)

            ToggleValueProvider = new WindowsPhone7ApplicationResourcesSettingsProvider();
#else

            ToggleValueProvider = new AppSettingsProvider();
#endif
        }



        public INowDateAndTime NowProvider { get; set; }

        public ITimePeriodProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get
            {
                var dates = ToggleValueProvider.EvaluateTimePeriod((this));

                return (NowProvider.Now >= dates.Item1 && NowProvider.Now <= dates.Item2);
            }
        }   
    }
}
