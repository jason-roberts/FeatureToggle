// This is pretty messy due to the fact that xUnit 2.0 doesn't support Windows phone Silverlight 8.1
// so as this file is shared between multiple test projects, the Windows phone Silverlight 8.1 uses MSTest
// while the others use xunit. Also async tests don't show in Test Explorer for the Windows phone Silverlight 8.1
// test project, hence the conditional async/awaits 


using FeatureToggle.Toggles;


#if NETFX_CORE    
    using Xunit;
#else
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif

// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests.Shared
// ReSharper restore CheckNamespace
{

#if NETFX_CORE
    [Trait("category", "Threaded")]
#else
    [TestClass]
#endif
    public class EnabledOnOrAfterToggleAssemblyVersionShould
    {        
        // assembly info has specific value


#if NETFX_CORE
        [Fact]
        public async void BeEnabledOnExactMatchingVersion()
#else
        [TestMethod]
        public void BeEnabledOnExactMatchingVersion()
#endif
        
        {
            var result = false;

            var sut = new MyVersionToggleFor_v0_0_2_1();


#if NETFX_CORE
            await 
#endif
            RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_1", "0.0.2.1");
                result = sut.FeatureEnabled;
            });

            AssertFacade.True(result);
        }


#if NETFX_CORE
        [Fact]
        public async void BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
#else
        [TestMethod]
        public void BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
#endif
        
        {
            var result = false;

            var sut = new MyVersionToggleFor_v0_0_2_2();


#if NETFX_CORE
            await 
#endif
            RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_2", "0.0.2.2");
                result = sut.FeatureEnabled;
            });


            AssertFacade.False(result);
        }

#if NETFX_CORE
        [Fact]
        public async void BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
#else
        [TestMethod]
        public void BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
#endif
        
        {
            var result = false;

            var sut = new MyVersionToggleFor_v0_0_2_0();


#if NETFX_CORE
            await 
#endif
            RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_0", "0.0.2.0");
                result = sut.FeatureEnabled;
            });

            AssertFacade.True(result);
        }

// ReSharper disable InconsistentNaming
        private class MyVersionToggleFor_v0_0_2_0 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_1 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_2 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
// ReSharper restore InconsistentNaming
    }
}
