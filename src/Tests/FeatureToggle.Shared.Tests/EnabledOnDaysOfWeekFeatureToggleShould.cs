#if NETFULL || NETCORE // no Moq support in UWP test projects

using System;
using FeatureToggle;
using FeatureToggle.Internal;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class EnabledOnDaysOfWeekFeatureToggleShould
    {

        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MyEnabledOnDaysFeatureToggle();

            Assert.Equal(typeof(AppSettingsProvider), sut.ToggleValueProvider.GetType());
        }

        [Fact]
        public void BeEnabledOnlyOnSpecifiedDaysOfWeek()
        {
            var aMonday = new DateTime(2014, 6, 2);
            var aFriday = new DateTime(2014, 6, 6);
            var aSaturday= new DateTime(2014, 6, 7);

            var fakeToggleValueProvider = new Mock<IDaysOfWeekToggleValueProvider>();

            fakeToggleValueProvider.Setup(x => x.GetDaysOfWeek(It.IsAny<EnabledOnDaysOfWeekFeatureToggle>()))
                .Returns(new[] {DayOfWeek.Monday, DayOfWeek.Friday});

            var sut = new MyEnabledOnDaysFeatureToggle
                      {
                          ToggleValueProvider = fakeToggleValueProvider.Object,                          
                      };

            sut.NowProvider = () => aMonday;
            Assert.True(sut.FeatureEnabled);

            sut.NowProvider = () => aFriday;
            Assert.True(sut.FeatureEnabled);

            sut.NowProvider = () => aSaturday;
            Assert.False(sut.FeatureEnabled);
        }


        private class MyEnabledOnDaysFeatureToggle : EnabledOnDaysOfWeekFeatureToggle { }
    }  
}

#endif