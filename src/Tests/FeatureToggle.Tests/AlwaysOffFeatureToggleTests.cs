using Xunit;

namespace JasonRoberts.FeatureToggle.Tests
{
    
    public class AlwaysOffFeatureToggleTests
    {
        [Fact]
        public void ShouldReturnAlwaysOff()
        {
            var sut = new MyAlwaysOffFeatureToggle();

            Assert.False(sut.FeatureEnabled);
        }

        private class MyAlwaysOffFeatureToggle : AlwaysOffFeatureToggle {}
    }
}
