#if NETFX_CORE

using System.Threading.Tasks;
using FeatureToggle;
    using Xunit;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Shared.Tests
// ReSharper restore CheckNamespace
{


    [Trait("category", "Threaded")]
    public class EnabledOnOrAfterToggleAssemblyVersionShould
    {        
        // The assembly info requires a specific value for these tests



        [Fact]
        public async Task BeEnabledOnExactMatchingVersion()
        
        {
            var result = false;

            var sut = new MyVersionToggleFor_v0_0_2_1();

            await RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_1", "0.0.2.1");
                result = sut.FeatureEnabled;
            });

            Assert.True(result);
        }



        [Fact]
        public async Task BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
        
        {
            var result = false;

            var sut = new MyVersionToggleFor_v0_0_2_2();

            await RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_2", "0.0.2.2");
                result = sut.FeatureEnabled;
            });


            Assert.False(result);
        }


        [Fact]
        public async Task BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
        {
            var result = false;

            var sut = new MyVersionToggleFor_v0_0_2_0();
            
            await RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_0", "0.0.2.0");    
                result = sut.FeatureEnabled;
            });

            Assert.True(result);
        }

// ReSharper disable InconsistentNaming
        private class MyVersionToggleFor_v0_0_2_0 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_1 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_2 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
// ReSharper restore InconsistentNaming
    }
}
#endif