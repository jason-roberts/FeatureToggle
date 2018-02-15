using System;
using System.Reflection;
using FeatureToggle;
using FeatureToggle.Internal;

// ReSharper disable CheckNamespace
namespace FeatureToggle
// ReSharper restore CheckNamespace
{
    public abstract class EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle : IFeatureToggle
    {
        public EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle()
        {

            ToggleValueProvider = new AppSettingsProvider();

        }

        public virtual IAssemblyVersionProvider ToggleValueProvider { get; set; }

        public bool FeatureEnabled
        {
            get
            {
                var assemblyVersionOfDerivedToggle = GetAssemblyVersionOfDerivedToggle();

                Version configuredToggleVersion = GetConfiguredVersion();


                return assemblyVersionOfDerivedToggle >= configuredToggleVersion;
            }
        }

        private Version GetAssemblyVersionOfDerivedToggle()
        {
            var assemblyName = this.GetType().GetTypeInfo().Assembly.FullName;

            var assembly = new AssemblyName(assemblyName);

            return assembly.Version;
        }

        private Version GetConfiguredVersion()
        {
            return ToggleValueProvider.EvaluateVersion(this);
        }
    }
}