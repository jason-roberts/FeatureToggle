using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class EnabledBetweenDatesFeatureToggleTests
    {
        [TestMethod]
        public void ShouldDisableFeatureAfterToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(-2),
                                                                   expectedNow.AddMilliseconds(-1));

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.IsFalse(sut.FeatureEnabled);
        }

        [TestMethod]
        public void ShouldDisableFeatureBeforeToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(1),
                                                                   expectedNow.AddMilliseconds(2));

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.IsFalse(sut.FeatureEnabled);
        }

        [TestMethod]
        public void ShouldEnableFeatureDuringToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(-1),
                                                                   expectedNow.AddMilliseconds(1));

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.IsTrue(sut.FeatureEnabled);
        }

        [TestMethod]
        public void ShouldEnableFeatureOnEndOfToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(-1),
                                                                   expectedNow);

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.IsTrue(sut.FeatureEnabled);
        }

        [TestMethod]
        public void ShouldEnableFeatureOnStartOfToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow,
                                                                   expectedNow.AddMilliseconds(1));

            var fakeNowProvider = new Mock<INowDateAndTime>();

            fakeNowProvider.SetupGet(x => x.Now).Returns(expectedNow);

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();

            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle();
            sut.NowProvider = fakeNowProvider.Object;
            sut.ToggleValueProvider = fakeToggleValueProvider.Object;

            Assert.IsTrue(sut.FeatureEnabled);
        }

        private class MyEnabledBetweenDatesFeatureToggle : EnabledBetweenDatesFeatureToggle { }
    }  
}
