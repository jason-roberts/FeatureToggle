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

            throw new NotImplementedException();
            //ToggleValueProvider = new ApplicationResourcesSettingsProvider();

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