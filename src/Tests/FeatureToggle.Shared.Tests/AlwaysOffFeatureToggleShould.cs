using FeatureToggle;
using Xunit;

namespace FeatureToggle.Shared.Tests
{    
    public class AlwaysOffFeatureToggleShould
    {
        [Fact]
        public void ReturnAlwaysOff()
        {
            var sut = new MyAlwaysOffFeatureToggle();

            Assert.False(sut.FeatureEnabled);
        }

        private class MyAlwaysOffFeatureToggle : AlwaysOffFeatureToggle {}
    }
}
