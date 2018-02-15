namespace FeatureToggle
{
    public static class Is<T> where T : IFeatureToggle, new()
    {
        public static bool Enabled => new T().FeatureEnabled;
        public static bool Disabled => ! new T().FeatureEnabled;
    }
}