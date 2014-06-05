using System;
using System.Collections.Generic;

namespace FeatureToggle.Core
{
    public interface IDaysOfWeekToggleValueProvider
    {
        IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle);
    }
}
