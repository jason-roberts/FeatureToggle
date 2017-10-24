// TODO: net core

#if NETFULL

using System.Configuration;
using System.Net;
using FeatureToggle.Shared.Tests.Integration.TestApiServer;
using FeatureToggle.Internal;
using FeatureToggle;
using Microsoft.Owin.Hosting;
using Xunit;

namespace FeatureToggle.Shared.Tests.Integration
{    
    public class BooleanHttpJsonProviderShould
    {
        private const string Url = "http://localhost:8084";
        private const string AlternateUrl = "http://localhost:8089";

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
        public void ErrorWhenInvalidJsonReceived()
        {
            using (WebApp.Start<Startup>(Url))
            {
                var sut = new AppSettingsProvider();

                var toggle = new InvalidHttpJsonToggle();

                Assert.Throws<WebException>(() => sut.EvaluateBooleanToggleValue(toggle));
            }
        }


        [Fact]
        public void ErrorWhen404()
        {
            using (WebApp.Start<Startup>(AlternateUrl))
            {
                var sut = new AppSettingsProvider();

                var toggle = new InvalidHttpJsonToggle();

                Assert.Throws<WebException>(() => sut.EvaluateBooleanToggleValue(toggle));
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
        private class InvalidHttpJsonToggle : HttpJsonFeatureToggle { }

    }
}
#endif