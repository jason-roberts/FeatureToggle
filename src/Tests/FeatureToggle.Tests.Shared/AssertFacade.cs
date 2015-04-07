
#if NETFX_CORE    
    using Xunit;
#else
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif

// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests.Shared
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// xUnit 2.0 doesn't support Windows Phone 8.1 Silverlight so in that project still need to use MSTest.
    /// This class provides a level of indirection regarding the underlying assert that gets called (MSTest or xUnit)
    /// </summary>
    static class AssertFacade
    {
        public static void True(bool actual)
        {
#if NETFX_CORE    
            Assert.True(actual);
#else
            Assert.IsTrue(actual);
#endif
        }


        public static void False(bool actual)
        {
#if NETFX_CORE    
            Assert.False(actual);
#else
            Assert.IsFalse(actual);
#endif
        }

        public static void Equal<T>(T expected, T actual)
        {
#if NETFX_CORE
            Assert.Equal<T>(expected, actual);
#else
            Assert.AreEqual(expected, actual);            
#endif

        }
    }
}
