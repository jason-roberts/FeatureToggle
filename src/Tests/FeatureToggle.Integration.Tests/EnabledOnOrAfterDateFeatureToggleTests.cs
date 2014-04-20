using System;
using JasonRoberts.FeatureToggle;
using Xunit;
using Moq;

namespace FeatureToggle.Integration.Tests
{
    
    public class EnabledOnOrAfterDateFeatureToggleTests
    {
        [Fact]
        public void ShouldBeEnabledOnDate()
        {
            var mockDate = new Mock<INowDateAndTime>();
            mockDate.SetupGet(x => x.Now).Returns(new DateTime(2000, 1, 1, 0, 0, 0, 0));
            var sut = new NewYearsDay2000 {NowProvider = mockDate.Object};

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ShouldBeEnabledAfterDate()
        {
            var mockDate = new Mock<INowDateAndTime>();

            mockDate.SetupGet(x => x.Now).Returns(new DateTime(2000, 1, 1, 0, 0, 0, 1));

            var sut = new NewYearsDay2000 {NowProvider = mockDate.Object};

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ShouldBeDisabledBeforeDate()
        {
            var mockDate = new Mock<INowDateAndTime>();
            mockDate.SetupGet(x => x.Now).Returns(
                new DateTime(2000, 1, 1, 0, 0, 0, 0).Subtract(TimeSpan.FromMilliseconds(1)));

            var sut = new NewYearsDay2000 {NowProvider = mockDate.Object};

            Assert.False(sut.FeatureEnabled);
        }


        private class NewYearsDay2000 : EnabledOnOrAfterDateFeatureToggle{}

    }
}