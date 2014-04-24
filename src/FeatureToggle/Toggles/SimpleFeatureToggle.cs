//#if (NETFX_CORE)
//    using FeatureToggle.WinRT;
//#endif

//#if (WINDOWS_PHONE)
//    using JasonRoberts.FeatureToggle.Wp7;
//#endif

using FeatureToggle.Core;
using FeatureToggle.Providers;

namespace FeatureToggle.Toggles
{
    public abstract class SimpleFeatureToggle : IFeatureToggle
    {
        protected SimpleFeatureToggle()
        {
#if (WINDOWS_PHONE)

            ToggleValueProvider = new ApplicationResourcesSettingsProvider();

//#elif (NETFX_CORE)

//            ToggleValueProvider = new WindowsStoreLocalSettingsProvider();

#else

            ToggleValueProvider = new AppSettingsProvider();
#endif
        }


        public IBooleanToggleValueProvider ToggleValueProvider { get; set; }


        public bool FeatureEnabled
        {
            get { return ToggleValueProvider.EvaluateBooleanToggleValue(this); }
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}