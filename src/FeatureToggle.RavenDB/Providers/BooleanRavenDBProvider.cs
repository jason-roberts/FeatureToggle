using System;
using FeatureToggle.Core;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Providers
// ReSharper restore CheckNamespace
{
// ReSharper disable InconsistentNaming
    public class BooleanRavenDBProvider : IBooleanToggleValueProvider
// ReSharper restore InconsistentNaming
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            throw new NotImplementedException();
        }
    }
}
