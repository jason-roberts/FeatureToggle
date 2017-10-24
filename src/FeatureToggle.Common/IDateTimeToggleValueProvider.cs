using System;

namespace FeatureToggle
{
    public interface IDateTimeToggleValueProvider
    {
        DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle);
    }
}