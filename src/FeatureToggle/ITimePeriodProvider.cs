using System;

namespace JasonRoberts.FeatureToggle
{
    public interface ITimePeriodProvider
    {
        Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle);
    }
}
