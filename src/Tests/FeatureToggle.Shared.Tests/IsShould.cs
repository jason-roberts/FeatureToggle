using FeatureToggle;
using FeatureToggle.Shared.Tests.TestToggles;
using Xunit;

namespace FeatureToggle.Tests
{    
    public class IsShould
    {
        [Fact]
        public void ReturnEnabled()
        {
            Assert.True(Is<Printing>.Enabled);
            Assert.False(Is<Printing>.Disabled);


            // Additional convoluted test code below so as to visualize how the fluent syntax reads

            if (Is<Printing>.Enabled)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }            
        }

        [Fact]
        public void ReturnDisabled()
        {
            Assert.True(Is<Saving>.Disabled);
            Assert.False(Is<Saving>.Enabled);
            

            // Additional convoluted test code below so as to visualize how the fluent syntax reads

            if (Is<Saving>.Disabled)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }


        [Fact]
        public void ThrowToggleErrors()
        {
            Assert.Throws<ToggleConfigurationError>(() => Is<AnErroringToggle>.Enabled);
        }

        private class Printing : AlwaysOnFeatureToggle {}
        private class Saving : AlwaysOffFeatureToggle {}
    }   
}
