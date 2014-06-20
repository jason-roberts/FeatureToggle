using System.Configuration;
using System.Net;
using FeatureToggle.Core;
using FeatureToggle.Integration.Tests.TestApiServer;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Microsoft.Owin.Hosting;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
    public class BooleanHttpJsonProviderShould
    {
        private const string Url = "http://localhost:8084";

        [Fact]
        public void ReadBooleanTrueFromHttpJsonEndpoint()
        {
            using (WebApp.Start<Startup>(Url))
            {
                var sut = new AppSettingsProvider();

                var toggle = new HttpJsonTrueToggle();

                Assert.True(sut.EvaluateBooleanToggleValue(toggle));
            }
        }

        [Fact]
        public void ReadBooleanFalseFromHttpJsonEndpoint()
        {
            using (WebApp.Start<Startup>(Url))
            {
                var sut = new AppSettingsProvider();

                var toggle = new HttpJsonFalseToggle();

                Assert.False(sut.EvaluateBooleanToggleValue(toggle));
            }
        }


        [Fact]
        public void ErrorWhenUrlNotInConfig()
        {
            var sut = new MissingUrlToggle();
            
            Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);            
        }


        private class MissingUrlToggle : HttpJsonFeatureToggle { }
        private class HttpJsonTrueToggle : HttpJsonFeatureToggle { }
        private class HttpJsonFalseToggle : HttpJsonFeatureToggle { }

    }
}
