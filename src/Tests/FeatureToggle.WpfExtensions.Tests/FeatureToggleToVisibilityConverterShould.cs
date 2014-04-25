using System.Windows;
using FeatureToggle.Core;
using FeatureToggle.Toggles;
using Moq;
using Xunit;

namespace FeatureToggle.WpfExtensions.Tests
{
    public class FeatureToggleToVisibilityConverterShould
    {
        [Fact]
        public void CovertTrueToVisisble()
        {
            var mockValueProvider = new Mock<IBooleanToggleValueProvider>();
            mockValueProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(true);

            var toggle = new MyBooleanFeatureToggle
                             {
                                 ToggleValueProvider = mockValueProvider.Object
                             };

            var sut = new FeatureToggleToVisibilityConverter();

            var result = sut.Convert(toggle, typeof(Visibility), null, null);

            Assert.Equal(Visibility.Visible, result);
        }

        [Fact]
        public void CovertFalseToCollapsed()
        {
            var mockValueProvider = new Mock<IBooleanToggleValueProvider>();
            mockValueProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SimpleFeatureToggle>())).Returns(false);

            var toggle = new MyBooleanFeatureToggle
            {
                ToggleValueProvider = mockValueProvider.Object
            };

            var sut = new FeatureToggleToVisibilityConverter();

            var result = sut.Convert(toggle, typeof(Visibility), null, null);

            Assert.Equal(Visibility.Collapsed, result);
        }

        private class MyBooleanFeatureToggle : SimpleFeatureToggle { }
    }
}
