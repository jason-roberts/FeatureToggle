using System;

namespace FeatureToggle.Interfaces
{
    public interface IAssemblyVersionProvider
    {
        Version EvaluateVersion(IFeatureToggle toggle);
    }
}