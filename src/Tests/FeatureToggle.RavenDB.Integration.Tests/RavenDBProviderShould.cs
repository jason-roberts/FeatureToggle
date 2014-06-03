using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.RavenDB.Providers;
using FeatureToggle.Toggles;
using Raven.Client.Document;
using Xunit;

namespace FeatureToggle.RavenDB.Integration.Tests
{    
// ReSharper disable InconsistentNaming
    public class RavenDBProviderShould
// ReSharper restore InconsistentNaming
    {

        public RavenDBProviderShould()
        {
            SetupTestData();
        }


        [Fact]
        public void ReadBooleanTrue()
        {
            var sut = new BooleanRavenDBProvider();

            var toggle = new MyRavenDBToggleTrue();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }


        [Fact]
        public void ReadBooleanFalse()
        {
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
        public void ErrorWhenToggleNotInDatabase()
        {
            var sut = new NotInDatabaseToggle();

            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }



        private class MyRavenDBToggleTrue : RavenDBFeatureToggle { }
        private class MyRavenDBToggleFalse : RavenDBFeatureToggle { }
        private class MissingConnectionStringToggle : RavenDBFeatureToggle { }
        private class NotInDatabaseToggle : RavenDBFeatureToggle { }        


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
