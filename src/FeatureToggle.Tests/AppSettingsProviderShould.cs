using System;
using System.Reflection;
using FeatureToggle.Internal;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FeatureToggle.NetCore.Tests
{
    public class AppSettingsProviderShould
    {

        [Fact]
        public void HaveDefaultConfiguration()
        {
            Assert.True(new A().FeatureEnabled);
        }

        [Fact]
        public void SetOptionalConfiguration()
        {
            var customConfig = BuildCustomConfig();

            var sut = new AppSettingsProvider{Configuration = customConfig};

            Assert.Same(customConfig, sut.Configuration); 
        }
        
        [Fact]
        public void UseOptionalConfigurationWhenReadingConfigValues()
        {
            var provider = new AppSettingsProvider{Configuration = BuildCustomConfig()};

            var b = new B();
            b.ToggleValueProvider = provider;

            Assert.True(b.FeatureEnabled);
        }

        private IConfigurationRoot BuildCustomConfig()
        {
            string testDir = System.IO.Path.GetDirectoryName(this.GetType().GetTypeInfo().Assembly.Location);
            var builder = new ConfigurationBuilder().SetBasePath(testDir).AddJsonFile("customSettings.json");
            IConfigurationRoot customConfig = builder.Build();
            return customConfig;
        }

        class A : SimpleFeatureToggle {}
        class B : SimpleFeatureToggle {}
    }
}
