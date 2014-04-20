namespace FeatureToggle.Core
{
    public interface IBooleanToggleValueProvider
    {
        bool EvaluateBooleanToggleValue(IFeatureToggle toggle);
    }
}
