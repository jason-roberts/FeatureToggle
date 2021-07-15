using Commify.FeatureToggle.Interfaces;

namespace Commify.FeatureToggle
{
    public class AlwaysOnFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { return true; }
        }
    }
}