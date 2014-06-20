using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
    public class BooleanHttpJsonProviderShould
    {
        [Fact]
        public void ReadBooleanTrueFromHttpJsonEndpoint()
        {
            var sut = new AppSettingsProvider();

            var toggle = new HttpJsonTrueToggle();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }


        //[Fact]
        //public void ReadBooleanFalseFromSqlServer()
        //{
        //    var sut = new BooleanSqlServerProvider();

        //    var toggle = new MySqlServerToggleFalse();

        //    Assert.False(sut.EvaluateBooleanToggleValue(toggle));
        //}



        [Fact]
        public void ErrorWhenUrlNotInConfig()
        {
            var sut = new MissingUrlToggle();
            
            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }



        private class MissingUrlToggle : HttpJsonFeatureToggle { }
        private class HttpJsonTrueToggle : HttpJsonFeatureToggle { }

    }
}
