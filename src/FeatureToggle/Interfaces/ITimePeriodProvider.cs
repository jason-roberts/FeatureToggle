using System;

namespace FeatureToggle.Interfaces
{
    public interface ITimePeriodProvider
    {
        Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle);
    }
}
