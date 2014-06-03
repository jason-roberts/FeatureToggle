using System;
using System.Linq;
using FeatureToggle.Core;
using FeatureToggle.RavenDB.Providers;
using Raven.Client.Document;
using Raven.Client.Linq;

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
            // TODO: need to consider not creating a new document store each time for performance reasons

            var connectionNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name;

            var documentStore = new DocumentStore
            {
                ConnectionStringName = connectionNameInConfig
            };

            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                var t = session.Load<BooleanToggleSetting>(toggle.GetType().Name);
                return t.Enabled;
            }

        }
    }
}
