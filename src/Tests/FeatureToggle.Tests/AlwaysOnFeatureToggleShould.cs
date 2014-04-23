using FeatureToggle.Core;
using Xunit;

namespace FeatureToggle.Tests
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
