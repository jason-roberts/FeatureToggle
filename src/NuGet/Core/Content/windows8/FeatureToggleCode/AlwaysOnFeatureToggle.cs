namespace JasonRoberts.FeatureToggle
{
    public class AlwaysOnFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get { return true; }
        }
    }
}