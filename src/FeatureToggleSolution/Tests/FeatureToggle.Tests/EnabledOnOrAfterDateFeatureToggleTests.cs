using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class EnabledOnOrAfterDateFeatureToggleTests
    {
        [TestMethod]
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

            Assert.IsFalse(sut.FeatureEnabled);
        }

        [TestMethod]
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

            Assert.IsTrue(sut.FeatureEnabled);
        }

        [TestMethod]
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

            Assert.IsTrue(sut.FeatureEnabled);
        }

        private class MyEnabledOnOrAfterDateFeatureToggle : EnabledOnOrAfterDateFeatureToggle { }
    }  
}
