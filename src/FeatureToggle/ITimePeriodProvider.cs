using System;

namespace FeatureToggle
{
    public interface ITimePeriodProvider
    {
        Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle);
    }
}
