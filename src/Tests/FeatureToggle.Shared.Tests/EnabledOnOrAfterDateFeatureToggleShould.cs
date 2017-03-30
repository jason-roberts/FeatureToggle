#if NETFULL || NETCORE // no Moq support in UWP test projects

using System;
using FeatureToggle;
using FeatureToggle.Internal;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class EnabledOnOrAfterDateFeatureToggleShould
    {

        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MyEnabledOnOrAfterDateFeatureToggle();

            Assert.Equal(typeof(AppSettingsProvider), sut.ToggleValueProvider.GetType());
        }

        [Fact]
        public void DisableFeatureBeforeToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrAfterDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(1));

            var sut = new MyEnabledOnOrAfterDateFeatureToggle
                      {
                          ToggleValueProvider = fakeToggleValueProvider.Object,
                          NowProvider = () => expectedNow
                      };

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureAfterToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrAfterDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(-1));

            var sut = new MyEnabledOnOrAfterDateFeatureToggle
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
            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrAfterDateFeatureToggle>())).Returns(expectedNow);

            var sut = new MyEnabledOnOrAfterDateFeatureToggle
            {
                ToggleValueProvider = fakeToggleValueProvider.Object,
                NowProvider = () => expectedNow
            };

            Assert.True(sut.FeatureEnabled);
        }

        private class MyEnabledOnOrAfterDateFeatureToggle : EnabledOnOrAfterDateFeatureToggle { }
    }  
}

#endif