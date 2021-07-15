using System;

namespace Commify.FeatureToggle.Interfaces
{
    public interface IAssemblyVersionProvider
    {
        Version EvaluateVersion(IFeatureToggle toggle);
    }
}