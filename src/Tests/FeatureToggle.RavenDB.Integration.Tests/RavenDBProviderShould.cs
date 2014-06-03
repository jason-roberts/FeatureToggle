using System;
using System.Threading;
using FeatureToggle.Providers;
using FeatureToggle.RavenDB.Providers;
using FeatureToggle.Toggles;
using Raven.Abstractions.Data;
using Raven.Client;
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

       //     var toggle = new MyRavenDBToggleFalse();

      //      Assert.False(sut.EvaluateBooleanToggleValue(toggle));
        }



        [Fact]
        public void ErrorWhenConnectionsStringNotInConfig()
        {
            Assert.True(false);
            //var sut = new MissingConnectionStringSqlServerToggle();

            //Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }


        [Fact]
        public void ErrorWhenSqlStatementNotInConfig()
        {
            Assert.True(false);
            //var sut = new MissingSqlStatementSqlServerToggle();

            //Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }



        private class MyRavenDBToggleTrue : RavenDBFeatureToggle { }
     //   private class MyRavenDBToggleFalse : RavenDBFeatureToggle { }
        //private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        //private class MissingSqlStatementSqlServerToggle : SqlFeatureToggle { }        


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

                session.SaveChanges();
            }
        }    
    }
}
