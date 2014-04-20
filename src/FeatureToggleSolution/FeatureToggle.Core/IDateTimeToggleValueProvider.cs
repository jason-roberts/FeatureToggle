using System;

namespace FeatureToggle.Core
{
    public interface IDateTimeToggleValueProvider
    {
        DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle);
    }
}