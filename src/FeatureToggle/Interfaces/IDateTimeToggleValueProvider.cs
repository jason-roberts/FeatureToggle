using System;

namespace FeatureToggle.Interfaces
{
    public interface IDateTimeToggleValueProvider
    {
        DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle);
    }
}