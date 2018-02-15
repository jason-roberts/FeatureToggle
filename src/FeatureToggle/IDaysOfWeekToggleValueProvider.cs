using System;
using System.Collections.Generic;

namespace FeatureToggle
{
    public interface IDaysOfWeekToggleValueProvider
    {
        IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle);
    }
}
