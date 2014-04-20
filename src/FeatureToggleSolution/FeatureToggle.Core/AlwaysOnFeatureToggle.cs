namespace FeatureToggle.Core
{
    public class AlwaysOnFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { return true; }
        }
    }
}