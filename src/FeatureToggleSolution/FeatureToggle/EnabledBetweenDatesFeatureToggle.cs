using System;

#if (WINDOWS_PHONE)
using JasonRoberts.FeatureToggle.Wp7;
#endif

namespace JasonRoberts.FeatureToggle
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

        public IDateTimeToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return NowProvider.Now >= ToggleValueProvider.EvaluateDateTimeToggleValue(this); }
        }   
    }
}
