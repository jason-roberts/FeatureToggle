using System;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
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

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrAfterDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(1));

            var sut = new MyEnabledOnOrAfterDateFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureAfterToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrAfterDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(-1));

            var sut = new MyEnabledOnOrAfterDateFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.True(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureOnToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrAfterDateFeatureToggle>())).Returns(expectedNow);

            var sut = new MyEnabledOnOrAfterDateFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.True(sut.FeatureEnabled);
        }

        private class MyEnabledOnOrAfterDateFeatureToggle : EnabledOnOrAfterDateFeatureToggle { }
    }  
}
