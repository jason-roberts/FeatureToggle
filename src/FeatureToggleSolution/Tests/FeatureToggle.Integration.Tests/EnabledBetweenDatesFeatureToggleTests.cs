using System;
using JasonRoberts.FeatureToggle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FeatureToggle.Integration.Tests
{
    [TestClass]
    public class EnabledBetweenDatesFeatureToggleTests
    {
        private readonly DateTime _startDate = new DateTime(2012, 1, 1, 0, 0, 0, 0);
        private readonly DateTime _endDate = new DateTime(2012, 12, 31, 23, 59, 59);

        private static readonly Mock<INowDateAndTime> MockDate = new Mock<INowDateAndTime>();

        // sut = system under test, i.e. the thing we are testing
        private readonly AllOf2012 _sut = new AllOf2012 {NowProvider = MockDate.Object};


        [TestMethod]
        public void ShouldBeEnabledOnStartDate()
        {            
            MockDate.SetupGet(x => x.Now).Returns(_startDate);

            Assert.IsTrue(_sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeEnabledOnEndDate()
        {
            MockDate.SetupGet(x => x.Now).Returns(_endDate);

            Assert.IsTrue(_sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeEnabledJustBeforeEndDate()
        {
            MockDate.SetupGet(x => x.Now).Returns(_endDate.AddMilliseconds(-1));

            Assert.IsTrue(_sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeEnabledJustAfterStartDate()
        {
            MockDate.SetupGet(x => x.Now).Returns(_startDate.AddMilliseconds(1));

            Assert.IsTrue(_sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeDisabledJustBeforeStartDate()
        {
            MockDate.SetupGet(x => x.Now).Returns(_startDate.AddMilliseconds(-1));

            Assert.IsFalse(_sut.FeatureEnabled);
        }


        [TestMethod]
        public void ShouldBeDisabledJustAfterEndDate()
        {
            MockDate.SetupGet(x => x.Now).Returns(_endDate.AddMilliseconds(1));

            Assert.IsFalse(_sut.FeatureEnabled);
        }
      

        private class AllOf2012 : EnabledBetweenDatesFeatureToggle { }

    }
}