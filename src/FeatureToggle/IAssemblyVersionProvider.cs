using System;

namespace FeatureToggle
{
    public interface IAssemblyVersionProvider
    {
        Version EvaluateVersion(IFeatureToggle toggle);
    }
}