using System;

namespace FeatureToggle.Core
{
    public interface IAssemblyVersionProvider
    {
        Version EvaluateVersion(IFeatureToggle toggle);
    }
}