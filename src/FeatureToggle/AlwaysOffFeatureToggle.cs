using FeatureToggle.Interfaces;

namespace FeatureToggle
{
    public class AlwaysOffFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { return false; }
        }
    }
}