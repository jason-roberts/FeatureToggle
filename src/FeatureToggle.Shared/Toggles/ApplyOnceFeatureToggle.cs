using FeatureToggle.Core;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Toggles
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// This toggle is activated only one time, it could be created at any time
    /// </summary>
    public abstract class ApplyOnceFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled { get; private set; }


        /// <summary>
        /// Apply the use of the toggle (if it was enabled, disable it)
        /// </summary>
        public void Apply()
        {
            if (FeatureEnabled)
                FeatureEnabled = false;
        }
    }
}
