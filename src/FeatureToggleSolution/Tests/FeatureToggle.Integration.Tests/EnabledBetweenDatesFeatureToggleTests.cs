using System;
using JasonRoberts.FeatureToggle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FeatureToggle.Integration.Tests
{
    [TestClass]
    public class EnabledBetweenDatesFeatureToggleTests
    {
        [TestMethod]
        public void ShouldBeEnabledOnStartDate()
        {
            var mockDate = new Mock<INowDateAndTime>();

            mockDate.SetupGet(x => x.Now).Returns(new DateTime(2012, 1, 1, 0, 0, 0, 0));

            var sut = new AllOf2012 {NowProvider = mockDate.Object};

            Assert.IsTrue(sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeEnabledOnEndDate()
        {
            var mockDate = new Mock<INowDateAndTime>();

            mockDate.SetupGet(x => x.Now).Returns(new DateTime(2012, 12, 31, 23, 59, 59 ));

            var sut = new AllOf2012 { NowProvider = mockDate.Object };

            Assert.IsTrue(sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeEnabledJustBeforeEndDate()
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void ShouldBeEnabledJustAfterStartDate()
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void ShouldBeDisabledJustBeforeStartDate()
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void ShouldBeDisabledJustAfterEndDate()
        {
            Assert.Inconclusive();
        }

       


        private class AllOf2012 : EnabledBetweenDatesFeatureToggle { }

    }
}