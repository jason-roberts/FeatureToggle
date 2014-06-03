using FeatureToggle.Core;
using FeatureToggle.RavenDB.Providers;
using Raven.Client.Document;

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
            var toggleName = toggle.GetType().Name;

            var connectionNameInConfig = AppSettingsKeys.Prefix + "." + toggleName;

            // TODO: need to consider not creating a new document store each time for performance reasons
            var documentStore = new DocumentStore
            {
                ConnectionStringName = connectionNameInConfig
            };

            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                var t = session.Load<BooleanToggleSetting>(toggleName);

                if (t == null)
                {
                    throw new ToggleConfigurationError("No document was found for toggle " + toggleName);
                }

                return t.Enabled;
            }
        }
    }
}
