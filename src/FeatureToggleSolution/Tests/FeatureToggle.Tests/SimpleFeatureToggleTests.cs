using Xunit;
using Moq;

namespace JasonRoberts.FeatureToggle.Tests
{
    
    public class SimpleFeatureToggleTests
    {
        [Fact]
        public void ShouldSetOptionalProviderOnCreation()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(true);

            var sut = new MySimpleFeatureToggle();
            sut.BooleanToggleValueProvider = fakeProvider.Object;

            Assert.Equal(true, sut.FeatureEnabled);
        }

        private class MySimpleFeatureToggle : SimpleFeatureToggle { }
    }  
}
