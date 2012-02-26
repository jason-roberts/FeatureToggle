using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JasonRoberts.FeatureToggle.Tests
{
    [TestClass]
    public class SimpleFeatureToggleTests
    {
        [TestMethod]
        public void ShouldSetOptionalProviderOnCreation()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(true);

            var sut = new MySimpleFeatureToggle();
            sut.BooleanToggleValueProvider = fakeProvider.Object;

            Assert.AreEqual(true, sut.FeatureEnabled);
        }

        private class MySimpleFeatureToggle : SimpleFeatureToggle { }
    }  
}
