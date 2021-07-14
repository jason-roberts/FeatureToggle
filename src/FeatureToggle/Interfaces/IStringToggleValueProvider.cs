using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Interfaces
{
    public interface IStringToggleValueProvider
    {
        string EvaluateStringToggleValue(IFeatureToggle toggle);
    }
}
