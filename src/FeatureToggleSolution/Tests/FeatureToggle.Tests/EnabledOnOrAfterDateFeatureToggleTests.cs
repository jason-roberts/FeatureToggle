using Xunit;
using Moq;
using System;

namespace JasonRoberts.FeatureToggle.Tests
{
    
    public class EnabledOnOrAfterDateFeatureToggleTests
    {
        [Fact]
        public void ShouldDisableFeatureBeforeToggleDateTime()
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
        public void ShouldEnableFeatureAfterToggleDateTime()
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
        public void ShouldEnableFeatureOnToggleDateTime()
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
