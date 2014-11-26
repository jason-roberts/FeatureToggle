using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
    public class BooleanSqlServerProviderShould
    {
        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]                
        public void ReadBooleanTrueFromSqlServer()
        {            
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleTrue();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }


        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]
        public void ReadBooleanFalseFromSqlServer()
        {
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleFalse();

            Assert.False(sut.EvaluateBooleanToggleValue(toggle));
        }



        [Fact]
        public void ErrorWhenConnectionsStringNotInConfig()
        {
            var sut = new MissingConnectionStringSqlServerToggle();

            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }


        [Fact]
        public void ErrorWhenSqlStatementNotInConfig()
        {
            var sut = new MissingSqlStatementSqlServerToggle();

            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }

   

        private class MySqlServerToggleTrue : SqlFeatureToggle{}
        private class MySqlServerToggleFalse : SqlFeatureToggle { }
        private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        private class MissingSqlStatementSqlServerToggle : SqlFeatureToggle { }        
    }
}
