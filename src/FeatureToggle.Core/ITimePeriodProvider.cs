using System;

namespace FeatureToggle.Core
{
    public interface ITimePeriodProvider
    {
        Tuple<DateTime, DateTime> EvaluateTimePeriod(IFeatureToggle toggle);
    }
}
