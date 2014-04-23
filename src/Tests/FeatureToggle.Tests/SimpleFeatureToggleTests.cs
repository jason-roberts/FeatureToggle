using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class SimpleFeatureToggleShould
    {
        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MySimpleFeatureToggle();

            Assert.Equal(typeof(AppSettingsProvider), sut.ToggleValueProvider.GetType());
        }

        [Fact]
        public void SetOptionalProviderOnCreation()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(true);

            var sut = new MySimpleFeatureToggle();
            sut.ToggleValueProvider = fakeProvider.Object;

            Assert.Equal(true, sut.FeatureEnabled);
        }

        private class MySimpleFeatureToggle : SimpleFeatureToggle { }
    }  
}
