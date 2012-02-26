using JasonRoberts.FeatureToggle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureToggle.Integration.Tests
{
    [TestClass]
    public class FeatureToggleAppSettingsTests
    {
        [TestMethod]
        public void ShouldUseConvetionToGetValueFromAppConfig()
        {
            Assert.IsTrue(new ConventionOverConfigurationToggle().FeatureEnabled);
        }

        private class ConventionOverConfigurationToggle : SimpleFeatureToggle {}
    }      
}
