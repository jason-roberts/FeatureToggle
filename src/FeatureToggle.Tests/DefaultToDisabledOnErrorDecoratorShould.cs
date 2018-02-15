using System;
using Xunit;

// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests
    // ReSharper restore CheckNamespace
{
    public class DefaultToDisabledOnErrorDecoratorShould
    {
        [Fact]
        public void ReturnFalseWhenError()
        {
            var sut = new DefaultToDisabledOnErrorDecorator(new FeatureToggleThatThrowsAnException());

            Assert.False(sut.FeatureEnabled);
        }


        [Fact]
        public void ReturnConfiguredValueWhenNoError()
        {
            var sut = new DefaultToDisabledOnErrorDecorator(new FeatureToggleThatDoesNotThrowAnException());

            Assert.False(sut.FeatureEnabled);
        }


        [Fact]
        public void AllowAccessToWrappedToggle()
        {
            var wrappedToggle = new FeatureToggleThatDoesNotThrowAnException();

            var sut = new DefaultToDisabledOnErrorDecorator(wrappedToggle);

            Assert.Same(wrappedToggle, sut.Toggle);
        }


        [Fact]
        public void CallLoggingActionOnErrorIfSet()
        {
            var wrappedToggle = new FeatureToggleThatThrowsAnException();

            string log = "";

            var sut = new DefaultToDisabledOnErrorDecorator(wrappedToggle, ex => log += ex.Message);



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
                get { return false; }
            }
        }
  
    }
}