using FeatureToggle;
using Xunit;

namespace FeatureToggle.Shared.Tests
{    
    public class AlwaysOnFeatureToggleShould
    {
        [Fact]
        public void ReturnAlwaysOn()
        {
            var sut = new MyAlwaysOnFeatureToggle();

            Assert.True(sut.FeatureEnabled);
        }

        private class MyAlwaysOnFeatureToggle : AlwaysOnFeatureToggle {}
    }   
}
