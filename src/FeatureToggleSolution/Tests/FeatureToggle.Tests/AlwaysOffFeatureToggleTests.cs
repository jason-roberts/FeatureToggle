using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class AlwaysOffFeatureToggleTests
    {
        [TestMethod]
        public void ShouldReturnAlwaysOff()
        {
            var sut = new MyAlwaysOffFeatureToggle();

            Assert.IsFalse(sut.FeatureEnabled);
        }

        private class MyAlwaysOffFeatureToggle : AlwaysOffFeatureToggle {}
    }
}
