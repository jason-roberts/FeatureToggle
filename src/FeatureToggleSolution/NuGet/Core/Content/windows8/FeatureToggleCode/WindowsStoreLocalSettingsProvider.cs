using System;
using JasonRoberts.FeatureToggle;
using Windows.Storage;

namespace FeatureToggle.WinRT
{
    public sealed class WindowsStoreLocalSettingsProvider : IBooleanToggleValueProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var toggleNameInConfig = toggle.GetType().Name;

            var localSettings = ApplicationData.Current.LocalSettings;

            if (! localSettings.Values.ContainsKey(toggleNameInConfig))
            {
                throw new ArgumentOutOfRangeException("toggle",
                    string.Format("No toggle value with the key '{0}' was found in local application settings. Ensure you set a boolean value into ApplicationData.Current.LocalSettings with a key that is the same as the name of your feature toggle class.", toggleNameInConfig));   
            }

            try
            {
                return (bool) localSettings.Values[toggleNameInConfig];
            }
            catch (Exception ex)
            {                                
                throw new Exception(
                    string.Format("The value in local application settings for the feature toggle with the key '{0}' is incorrect, it should be a non-nullable boolean value.", toggleNameInConfig),
                    ex);
            }
        }
    }
}
