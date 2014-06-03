using System;
using FeatureToggle.Core;
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
            throw new NotImplementedException();

            // TODO: need to consider not creating a new document store each time for performance reasons
            
            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080"
            };

            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                // Using the session
            }
        }
    }
}
