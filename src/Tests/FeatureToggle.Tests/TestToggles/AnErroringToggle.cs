using FeatureToggle.Core;

namespace FeatureToggle.Tests.TestToggles
{
    public class AnErroringToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { throw new ToggleConfigurationError("Simulated toggle exception"); }
        }
    }
}
