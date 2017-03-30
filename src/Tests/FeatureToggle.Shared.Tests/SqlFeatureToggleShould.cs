#if NETFULL // sql toggle only on full framework for now

using FeatureToggle;
using FeatureToggle.Internal;
using Moq;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class SqlFeatureToggleShould
    {
        [Fact]
        public void HaveDefaultProvider()
        {
            var sut = new MySqlFeatureToggle();

            Assert.Equal(typeof(BooleanSqlServerProvider), sut.ToggleValueProvider.GetType());
        }


        [Fact]
        public void DisableFeatureWhenToggleValueIsFalse()
        {
            var fakeProvider = new Mock<IBooleanToggleValueProvider>();

            fakeProvider.Setup(x => x.EvaluateBooleanToggleValue(It.IsAny<SqlFeatureToggle>())).Returns(false);

            var sut = new MySqlFeatureToggle();
            sut.ToggleValueProvider = fakeProvider.Object;

            Assert.False(sut.FeatureEnabled);
        }

        [Fact]
        public void EnableFeatureWhenToggleValueIsTrue()
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

#endif