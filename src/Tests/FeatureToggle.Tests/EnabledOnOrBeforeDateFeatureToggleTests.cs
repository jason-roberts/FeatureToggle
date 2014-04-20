using Xunit;
using Moq;
using System;

namespace JasonRoberts.FeatureToggle.Tests
{
    
    public class EnabledOnOrBeforeDateFeatureToggleTests
    {
        [Fact]
        public void ShouldDisableFeatureAfterToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrBeforeDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(-1));

            var sut = new MyEnabledOnOrBeforeDateFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void ShouldEnableFeatureBeforeToggleDateTime()
        {
            var expectedNow = DateTime.Now;

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<IDateTimeToggleValueProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrBeforeDateFeatureToggle>())).Returns(expectedNow.AddMilliseconds(1));

            var sut = new MyEnabledOnOrBeforeDateFeatureToggle();
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

            fakeToggleValueProvider.Setup(x => x.EvaluateDateTimeToggleValue(It.IsAny<EnabledOnOrBeforeDateFeatureToggle>())).Returns(expectedNow);

            var sut = new MyEnabledOnOrBeforeDateFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.True(sut.FeatureEnabled);
        }

        private class MyEnabledOnOrBeforeDateFeatureToggle : EnabledOnOrBeforeDateFeatureToggle { }
    }  
}
