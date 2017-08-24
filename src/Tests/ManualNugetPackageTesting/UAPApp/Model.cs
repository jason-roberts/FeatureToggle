using FeatureToggle;

namespace UAPApp
{
    public class Model
    {
        public IFeatureToggle Print { get; set; } = new Printing();
    }
}