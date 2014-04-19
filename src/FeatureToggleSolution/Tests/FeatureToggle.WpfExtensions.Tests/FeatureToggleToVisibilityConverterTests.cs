using System.Windows;
using JasonRoberts.FeatureToggle;
using JasonRoberts.FeatureToggle.WpfExtensions;
using Moq;
using Xunit;

namespace FeatureToggle.Wpf.Tests
{
    public class FeatureToggleToVisibilityConverterTests
    {
        [Fact]
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

            Assert.Equal(Visibility.Visible, result);
        }

        private class MyBooleanFeatureToggle : SimpleFeatureToggle { }
    }
}
