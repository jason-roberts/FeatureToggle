#if NETFULL || NETCORE // no Moq support in UWP test projects

using System;
using FeatureToggle;
using FeatureToggle.Internal;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class EnabledBetweenDatesFeatureToggleShould
    {
        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MyEnabledBetweenDatesFeatureToggle();

            Assert.Equal(typeof(AppSettingsProvider), sut.ToggleValueProvider.GetType());
        }

        [Fact]
        public void DisableFeatureAfterToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(-2),
                                                                   expectedNow.AddMilliseconds(-1));

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle
                      {
                          NowProvider = () => expectedNow,
                          ToggleValueProvider = fakeToggleValueProvider.Object
                      };

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void DisableFeatureBeforeToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(1),
                                                                   expectedNow.AddMilliseconds(2));

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle
            {
                NowProvider = () => expectedNow,
                ToggleValueProvider = fakeToggleValueProvider.Object
            };

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureDuringToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(-1),
                                                                   expectedNow.AddMilliseconds(1));

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle
            {
                NowProvider = () => expectedNow,
                ToggleValueProvider = fakeToggleValueProvider.Object
            };

            Assert.True(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureOnEndOfToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow.AddMilliseconds(-1),
                                                                   expectedNow);

            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle
            {
                NowProvider = () => expectedNow,
                ToggleValueProvider = fakeToggleValueProvider.Object
            };

            Assert.True(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureOnStartOfToggleTimePeriod()
        {
            var expectedNow = DateTime.Now;
            var expectedTimePeriod = new Tuple<DateTime, DateTime>(expectedNow,
                                                                   expectedNow.AddMilliseconds(1));


            var fakeToggleValueProvider = new Mock<ITimePeriodProvider>();
            fakeToggleValueProvider.Setup(x => x.EvaluateTimePeriod(It.IsAny<EnabledBetweenDatesFeatureToggle>())).Returns(expectedTimePeriod);

            var sut = new MyEnabledBetweenDatesFeatureToggle
            {
                NowProvider = () => expectedNow,
                ToggleValueProvider = fakeToggleValueProvider.Object
            };

            Assert.True(sut.FeatureEnabled);
        }

        private class MyEnabledBetweenDatesFeatureToggle : EnabledBetweenDatesFeatureToggle { }
    }  
}

#endif