using System;

namespace JasonRoberts.FeatureToggle
{
    public interface IDateTimeToggleValueProvider
    {
        DateTime EvaluateDateTimeToggleValue(IFeatureToggle toggle);
    }
}