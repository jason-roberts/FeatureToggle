using FeatureToggle.Toggles;
using Xunit;

namespace FeatureToggle.Integration.Tests
{
    public class EnabledOnOrAfterToggleAssemblyVersionShould
    {

        // assembly info has specific values for this test project

        [Fact]
        public void BeEnabledOnExactMatchingVersion()
        {
            var sut = new MyVersionToggleFor_v0_0_2_1();

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
        {
            var sut = new MyVersionToggleFor_v0_0_2_2();

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
        {
            var sut = new MyVersionToggleFor_v0_0_2_0();

            Assert.True(sut.FeatureEnabled);
        }

// ReSharper disable InconsistentNaming
        private class MyVersionToggleFor_v0_0_2_0 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_1 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_2 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
// ReSharper restore InconsistentNaming
    }
}
