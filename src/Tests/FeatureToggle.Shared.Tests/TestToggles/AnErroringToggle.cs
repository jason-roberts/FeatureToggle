using FeatureToggle;

namespace FeatureToggle.Shared.Tests.TestToggles
{
    public class AnErroringToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { throw new ToggleConfigurationError("Simulated toggle exception"); }
        }
    }
}
