using FeatureToggle;

namespace AspDotNetCoreExample.ViewModels
{
    public class HomeIndexViewModel
    {
        public IFeatureToggle Printing { get; set; }

        public IFeatureToggle Saving { get; set; }
    }
}
