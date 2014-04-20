using Xunit;

namespace JasonRoberts.FeatureToggle.Tests
{
    
    public class AlwaysOnFeatureToggleTests
    {
        [Fact]
        public void ShouldReturnAlwaysOn()
        {
            var sut = new MyAlwaysOnFeatureToggle();

            Assert.True(sut.FeatureEnabled);
        }

        private class MyAlwaysOnFeatureToggle : AlwaysOnFeatureToggle {}
    }   
}
