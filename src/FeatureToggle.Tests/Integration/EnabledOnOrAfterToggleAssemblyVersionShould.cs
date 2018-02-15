using System.Threading.Tasks;
using Xunit;


// ReSharper disable CheckNamespace
namespace FeatureToggle.Shared.Tests.Integration
// ReSharper restore CheckNamespace
{
    [Trait("category", "Threaded")]
    public class EnabledOnOrAfterToggleAssemblyVersionShould
    {
        // Test assembly version should be set to 0.0.2.1

        [Fact]
        public async Task BeEnabledOnExactMatchingVersion()
        {
            var result = false;
            var sut = new MyVersionToggleFor_v0_0_2_1();


            result = await Task.Run(() => sut.FeatureEnabled);


            Assert.True(result);
        }



        [Fact]
        public async Task BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
        {
            var result = true;
            var sut = new MyVersionToggleFor_v0_0_2_2();



            result = await Task.Run(() => sut.FeatureEnabled);


            Assert.False(result);
        }


        [Fact]
        public async Task BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
        {
            var result = false;
            var sut = new MyVersionToggleFor_v0_0_2_0();


            result = await Task.Run(() => sut.FeatureEnabled);

            Assert.True(result);
        }

        // ReSharper disable InconsistentNaming
        private class MyVersionToggleFor_v0_0_2_0 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_1 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_2 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        // ReSharper restore InconsistentNaming
    }
}