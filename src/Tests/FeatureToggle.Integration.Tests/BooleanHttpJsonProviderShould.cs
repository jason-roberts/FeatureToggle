using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
    public class BooleanHttpJsonProviderShould
    {
        //[Fact]
        //public void ReadBooleanTrueFromSqlServer()
        //{            
        //    var sut = new BooleanSqlServerProvider();

        //    var toggle = new MySqlServerToggleTrue();

        //    Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        //}


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

    }
}
