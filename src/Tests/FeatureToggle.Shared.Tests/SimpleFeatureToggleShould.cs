using System;
using FeatureToggle;
using FeatureToggle.Internal;
using Xunit;

#if NETFULL || NETCORE
using Moq;
#endif

namespace FeatureToggle.Tests
{    
    public class SimpleFeatureToggleShould
    {
        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MySimpleFeatureToggle();
#if NETFULL || NETCORE
            Assert.Equal(typeof(AppSettingsProvider), sut.ToggleValueProvider.GetType());
#else
            Assert.Equal(typeof(ApplicationResourcesSettingsProvider), sut.ToggleValueProvider.GetType());
#endif
        }

#if NETFULL || NETCORE // can't Moq in UWP
        [Fact]
        public void SetOptionalProviderOnCreation()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(true);

            var sut = new MySimpleFeatureToggle();
            sut.ToggleValueProvider = fakeProvider.Object;

            Assert.Equal(true, sut.FeatureEnabled);
        }
#endif

        private class MySimpleFeatureToggle : SimpleFeatureToggle { }
    }  
}
