using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class AlwaysOnFeatureToggleTests
    {
        [TestMethod]
        public void ShouldReturnAlwaysOn()
        {
            var sut = new MyAlwaysOnFeatureToggle();

            Assert.IsTrue(sut.FeatureEnabled);
        }

        private class MyAlwaysOnFeatureToggle : AlwaysOnFeatureToggle {}
    }   
}
