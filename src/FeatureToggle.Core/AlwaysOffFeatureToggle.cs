namespace FeatureToggle.Core
{
    public class AlwaysOffFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { return false; }
        }
    }
}