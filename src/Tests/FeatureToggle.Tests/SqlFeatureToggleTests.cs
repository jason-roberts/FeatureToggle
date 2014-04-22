using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class SqlFeatureToggleTests
    {
        [Fact]
        public void ShouldHaveDefaultProvider()
        {
            var sut = new MySqlFeatureToggle();

            Assert.Equal(typeof(BooleanSqlServerProvider), sut.ToggleValueProvider.GetType());
        }


        [Fact]
        public void ShouldDisableFeatureWhenToggleValueIsFalse()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SqlFeatureToggle>())).Returns(false);

            var sut = new MySqlFeatureToggle();
            sut.ToggleValueProvider = fakeProvider.Object;

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void ShouldEnableFeatureWhenToggleValueIsTrue()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SqlFeatureToggle>())).Returns(true);

            var sut = new MySqlFeatureToggle();
            sut.ToggleValueProvider = fakeProvider.Object;

            Assert.True(sut.FeatureEnabled);
        }

        private class MySqlFeatureToggle : SqlFeatureToggle { }
    }  
}
