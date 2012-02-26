using System;
using System.Windows;

namespace JasonRoberts.FeatureToggle.Wp7
{
    public class WindowsPhone7ApplicationResourcesSettingsProvider : IBooleanToggleValueProvider, IDateTimeToggleValueProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {            
            var toggleNameInConfig =  toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format("The key '{0}' was not found in Application.Current.Resources", toggleNameInConfig));

            bool toggleValue = (bool) Application.Current.Resources[toggleNameInConfig];

            return toggleValue;
        }


        public DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = toggle.GetType().Name;

            if (!Application.Current.Resources.Contains(toggleNameInConfig))
                throw new Exception(string.Format("The key '{0}' was not found in Application.Current.Resources", toggleNameInConfig));

            DateTime toggleValue = (DateTime)Application.Current.Resources[toggleNameInConfig];

            return toggleValue;
        }
    }
}
