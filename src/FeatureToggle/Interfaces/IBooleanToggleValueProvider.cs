namespace FeatureToggle.Interfaces
{
    public interface IBooleanToggleValueProvider
    {
        bool EvaluateBooleanToggleValue(IFeatureToggle toggle);
    }
}
