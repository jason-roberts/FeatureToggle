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
#if WINDOWS_UWP

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
#if WINDOWS_UWP

            var assemblyName = this.GetType().GetTypeInfo().Assembly.FullName;

            var assembly = new AssemblyName(assemblyName);

            return assembly.Version;
#elif NETCORE
            throw new NotImplementedException();
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