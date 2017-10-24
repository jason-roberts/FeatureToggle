#if NETFULL || NETCORE // no Moq support in UWP test projects

using System;
using FeatureToggle;
using FeatureToggle.Internal;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class EnabledOnOrBeforeDateFeatureToggleShould
    {
        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MyEnabledOnOrBeforeDateFeatureToggle();

            Assert.Equal(typeof(AppSettingsProvider), sut.ToggleValueProvider.GetType());
        }

        [Fact]
        public void DisableFeatureAfterToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();            
            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrBeforeDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(-1));

            var sut = new MyEnabledOnOrBeforeDateFeatureToggle
                      {
                          ToggleValueProvider = fakeToggleValueProvider.Object,
                          NowProvider = () => expectedNow
                      };

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureBeforeToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrBeforeDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(1));

            var sut = new MyEnabledOnOrBeforeDateFeatureToggle
            {
                ToggleValueProvider = fakeToggleValueProvider.Object,
                NowProvider = () => expectedNow
            };

            Assert.True(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureOnToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrBeforeDateFeatureToggle>())).Returns(expectedNow);

            var sut = new MyEnabledOnOrBeforeDateFeatureToggle
            {
                ToggleValueProvider = fakeToggleValueProvider.Object,
                NowProvider = () => expectedNow
            };

            Assert.True(sut.FeatureEnabled);
        }

        private class MyEnabledOnOrBeforeDateFeatureToggle : EnabledOnOrBeforeDateFeatureToggle { }
    }  
}
#endif