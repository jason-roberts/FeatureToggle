using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
// ReSharper disable InconsistentNaming
    public class RavenDBProviderShould
// ReSharper restore InconsistentNaming
    {
        [Fact]
        public void ReadBooleanTrue()
        {
            var sut = new BooleanRavenDBProvider();

    //        var toggle = new MyRavenDBToggleTrue();

    //        Assert.True(sut.EvaluateBooleanToggleValue(toggle));
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



      //  private class MyRavenDBToggleTrue : RavenDBFeatureToggle { }
     //   private class MyRavenDBToggleFalse : RavenDBFeatureToggle { }
        //private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        //private class MissingSqlStatementSqlServerToggle : SqlFeatureToggle { }        
    }
}
