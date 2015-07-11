#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
using FeatureToggle.Core;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Toggles
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// This toggle is activated only one time per user, per all device family of the user
    /// Example : Explains how the app works only one time (no matter how many devices user have)
    /// Retrieve toggle through Roaming Settings
    /// </summary>
    public abstract class ApplyOncePerUserFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get
            {
                if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(Name))
                    return (bool)ApplicationData.Current.RoamingSettings.Values[Name];
                return true;
            }
            private set
            {
                ApplicationData.Current.RoamingSettings.Values[Name] = value;
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