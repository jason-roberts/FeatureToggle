using System.Windows;
using JasonRoberts.FeatureToggle;
using JasonRoberts.FeatureToggle.Wpf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FeatureToggle.Wpf.Tests
{
    [TestClass]
    public class FeatureToggleToVisibilityConverterTests
    {
        [TestMethod]
        public void ShouldCovertTrueToVisisble()
        {
            var mockValueProvider = new Mock<IBooleanToggleValueProvider>();

            mockValueProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(true);

            var toggle = new MyBooleanFeatureToggle
                             {
                                 BooleanToggleValueProvider = mockValueProvider.Object
                             };

            var sut = new FeatureToggleToVisibilityConverter();

            var result = sut.Convert(toggle, typeof(Visibility), null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        private class MyBooleanFeatureToggle : SimpleFeatureToggle { }
    }
}
