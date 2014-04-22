using System.Configuration;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
    public class BooleanSqlServerProviderTests
    {
        [Fact]
        public void ShouldReadBooleanTrueFromSqlServer()
        {            
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleTrue();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }


        [Fact]
        public void ShouldReadBooleanFalseFromSqlServer()
        {
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleFalse();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }



        [Fact]
        public void ShouldErrorWhenConnectionsStringNotInConfig()
        {
            var sut = new MissingConnectionStringSqlServerToggle();

            Assert.Throws<ConfigurationErrorsException>(() => sut.FeatureEnabled);            
        }


        [Fact]
        public void ShouldErrorWhenSqlStatementNotInConfig()
        {
            var sut = new MissingSqlStatementSqlServerToggle();

            Assert.Throws<ConfigurationErrorsException>(() => sut.FeatureEnabled);            
        }

   

        private class MySqlServerToggleTrue : SqlFeatureToggle{}
        private class MySqlServerToggleFalse : SqlFeatureToggle { }
        private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        private class MissingSqlStatementSqlServerToggle : SqlFeatureToggle { }        
    }
}
