using JasonRoberts.FeatureToggle;
using Xunit;

namespace FeatureToggle.Integration.Tests
{
    
    public class FeatureToggleAppSettingsTests
    {
        [Fact]
        public void ShouldUseConvetionToGetValueFromAppConfig()
        {
            Assert.True(new ConventionOverConfigurationToggle().FeatureEnabled);
        }

        private class ConventionOverConfigurationToggle : SimpleFeatureToggle {}
    }      
}
