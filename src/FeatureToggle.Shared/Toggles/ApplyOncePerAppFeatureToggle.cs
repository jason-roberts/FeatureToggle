#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
using FeatureToggle.Core;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Toggles
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// This toggle is activated only one time per app, 
    /// Retrieve toggle through Local Settings
    /// </summary>
    public abstract class ApplyOncePerAppFeatureToggle : IFeatureToggle
    {
        // BUG : Local Settings are remove when app is removed (save in Roaming with device ID ?)
        public bool FeatureEnabled
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(Name))
                    return (bool)ApplicationData.Current.LocalSettings.Values[Name];
                return true;
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values[Name] = value;
            }
        }

        public string Name { get; private set; }


        /// <summary>
        /// Apply the use of the toggle (if it was enabled, disable it)
        /// </summary>
        public virtual void Apply()
        {
            if (FeatureEnabled)
                FeatureEnabled = false;
        }
    }
}
#endif