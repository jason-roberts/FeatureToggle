using System;

namespace FeatureToggle
{
    /// <summary>
    /// This decorator should be used with care or not at all. It will essentially hide any configuration errors
    /// or other problems with the wrapped toggle knowing if it should enable the feature or not.
    /// This feature is provided as a response to a user request.
    /// </summary>
    public class DefaultToEnabledOnErrorDecorator : IFeatureToggle
    {
        private readonly Action<Exception> _logAction;
        public IFeatureToggle Toggle { get; private set; }

        /// <summary>
        /// Decorator constructor that allows the wrapping of another toggle
        /// </summary>
        /// <param name="toggle">The toggle to wrap (decorate)</param>
        /// <param name="logAction">Optional action that gets called if an exception is thrown by the wrapped toggle before returning enabled = true</param>
        public DefaultToEnabledOnErrorDecorator(IFeatureToggle toggle, Action<Exception> logAction = null)
        {            
            Toggle = toggle;
            _logAction = logAction;
        }

        public bool FeatureEnabled
        {
            get
            {
                try
                {
                    return Toggle.FeatureEnabled;
                }
                catch (Exception ex)
                {
                    if (_logAction != null)
                    {
                        _logAction(ex);
                    }

                    return true;
                }
            }
        }
    }
}
