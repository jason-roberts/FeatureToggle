using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class BooleanSqlServerProviderTests
    {
        [TestMethod]
        public void ShouldReadBooleanTrueFromSqlServer()
        {            
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleTrue();

            Assert.IsTrue(sut.EvaluateBooleanToggleValue(toggle));
        }


        [TestMethod]
        public void ShouldReadBooleanFalseFromSqlServer()
        {
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleFalse();

            Assert.IsTrue(sut.EvaluateBooleanToggleValue(toggle));
        }



        [TestMethod, ExpectedException(typeof(ConfigurationErrorsException))]
        public void ShouldErrorWhenConnectionsStringNotInConfig()
        {
            var sut = new MissingConnectionStringSqlServerToggle();

            var x = sut.FeatureEnabled;
        }


        [TestMethod, ExpectedException(typeof(ConfigurationErrorsException))]
        public void ShouldErrorWhenSqlStatementNotInConfig()
        {
            var sut = new MissingSqlStatementSqlServerToggle();

            var x = sut.FeatureEnabled;
        }

   

        private class MySqlServerToggleTrue : SqlFeatureToggle{}
        private class MySqlServerToggleFalse : SqlFeatureToggle { }
        private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        private class MissingSqlStatementSqlServerToggle : SqlFeatureToggle { }        
    }
}
