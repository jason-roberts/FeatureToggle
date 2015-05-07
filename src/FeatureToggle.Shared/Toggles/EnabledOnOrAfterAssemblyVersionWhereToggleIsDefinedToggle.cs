using System;
using System.Reflection;
using FeatureToggle.Core;
using FeatureToggle.Providers;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Toggles
// ReSharper restore CheckNamespace
{
    public abstract class EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle : IFeatureToggle
    {
        public EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle()
        {
#if (WINDOWS_PHONE || NETFX_CORE)

            ToggleValueProvider = new ApplicationResourcesSettingsProvider();

#else

            ToggleValueProvider = new AppSettingsProvider();
#endif
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
#if (WINDOWS_PHONE || NETFX_CORE)

            var assemblyName = this.GetType().GetTypeInfo().Assembly.FullName;

            var assembly = new AssemblyName(assemblyName);

            return assembly.Version;
#else
            return new AssemblyName(GetType().Assembly.FullName).Version;
#endif
        }

        private Version GetConfiguredVersion()
        {
            return ToggleValueProvider.EvaluateVersion(this);
        }
    }
}