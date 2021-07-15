using System;
using System.Collections.Generic;

namespace Commify.FeatureToggle.Interfaces
{
    public interface IDaysOfWeekToggleValueProvider
    {
        IEnumerable<DayOfWeek> GetDaysOfWeek(IFeatureToggle toggle);
    }
}
