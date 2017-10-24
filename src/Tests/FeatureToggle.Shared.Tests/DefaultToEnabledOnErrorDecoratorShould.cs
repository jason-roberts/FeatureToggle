using System;
using FeatureToggle;
using Xunit;

namespace FeatureToggle.Tests
{
    public class DefaultToEnabledOnErrorDecoratorShould
    {
        [Fact]
        public void ReturnTrueWhenError()
        {
            var sut = new DefaultToEnabledOnErrorDecorator(new FeatureToggleThatThrowsAnException());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnConfiguredValueWhenNoError()
        {
            var sut = new DefaultToEnabledOnErrorDecorator(new FeatureToggleThatDoesNotThrowAnException());

            Assert.True(sut.FeatureEnabled);
        }


        [Fact]
        public void AllowAccessToWrappedToggle()
        {
            var wrappedToggle = new FeatureToggleThatDoesNotThrowAnException();

            var sut = new DefaultToEnabledOnErrorDecorator(wrappedToggle);

            Assert.Same(wrappedToggle, sut.Toggle);
        }


        [Fact]
        public void CallLoggingActionOnErrorIfSet()
        {
            var wrappedToggle = new FeatureToggleThatThrowsAnException();

            string log = "";

            var sut = new DefaultToEnabledOnErrorDecorator(wrappedToggle, ex => log += ex.Message);



            try
            {
                var isEnabled = sut.FeatureEnabled;
            }
            catch
            {
                // ignore exception so we can assert that the specified action was called
            }



            Assert.Equal("Exception for testing purposes", log);
        }


        private class FeatureToggleThatThrowsAnException : IFeatureToggle {
            public bool FeatureEnabled
            {
                get
                {
                    throw new Exception("Exception for testing purposes");
                }
            }
        }


        private class FeatureToggleThatDoesNotThrowAnException : IFeatureToggle
        {
            public bool FeatureEnabled
            {
                get { return true; }
            }
        }
  
    }
}