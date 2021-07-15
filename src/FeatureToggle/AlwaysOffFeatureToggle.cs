using Commify.FeatureToggle.Interfaces;

namespace Commify.FeatureToggle
{
    public class AlwaysOffFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { return false; }
        }
    }
}