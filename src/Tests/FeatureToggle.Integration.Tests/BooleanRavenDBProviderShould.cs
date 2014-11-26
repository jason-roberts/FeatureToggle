using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.RavenDB.Providers;
using FeatureToggle.Toggles;
using Raven.Client.Document;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
// ReSharper disable InconsistentNaming
    public class BooleanRavenDBProviderShould
// ReSharper restore InconsistentNaming
    {

        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]
        public void ReadBooleanTrue()
        {
            SetupTestData();

            var sut = new BooleanRavenDBProvider();

            var toggle = new MyRavenDBToggleTrue();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }


        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]
        public void ReadBooleanFalse()
        {
            SetupTestData();

            var sut = new BooleanRavenDBProvider();

            var toggle = new MyRavenDBToggleFalse();

            Assert.False(sut.EvaluateBooleanToggleValue(toggle));
        }


        [Fact]
        public void ErrorWhenConnectionsStringNotInConfig()
        {
            var sut = new MissingConnectionStringToggle();

            var ex = Assert.Throws<ArgumentException>(() => sut.FeatureEnabled);

            Assert.Equal("Could not find connection string name: 'FeatureToggle.MissingConnectionStringToggle'", ex.Message);
        }


        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]
        public void ErrorWhenToggleNotInDatabase()
        {
            SetupTestData();

            var sut = new NotInDatabaseToggle();

            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }



        private class MyRavenDBToggleTrue : BooleanRavenDBFeatureToggle { }
        private class MyRavenDBToggleFalse : BooleanRavenDBFeatureToggle { }
        private class MissingConnectionStringToggle : BooleanRavenDBFeatureToggle { }
        private class NotInDatabaseToggle : BooleanRavenDBFeatureToggle { }        


        private static void SetupTestData()
        {
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "FeatureToggle.MyRavenDBToggleTrue"
            };

            documentStore.Initialize();


            using (var session = documentStore.OpenSession())
            {
                var existing = session.Query<BooleanToggleSetting>();

                foreach (var booleanToggleSetting in existing)
                {
                    session.Delete(booleanToggleSetting);
                }

                session.SaveChanges();


                session.Store(new BooleanToggleSetting { Id = "MyRavenDBToggleTrue", Enabled = true });
                session.Store(new BooleanToggleSetting { Id = "MyRavenDBToggleFalse", Enabled = false });

                session.SaveChanges();
            }
        }    
    }
}
