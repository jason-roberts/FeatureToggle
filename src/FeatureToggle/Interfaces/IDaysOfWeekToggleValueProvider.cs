using System;
using System.Collections.Generic;

namespace FeatureToggle.Interfaces
{
    public interface IDaysOfWeekToggleValueProvider
    {
        IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle);
    }
}
