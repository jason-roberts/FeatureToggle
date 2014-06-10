using System;

namespace FeatureToggle.Core
{
    public class RandomFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get
            {
                

                
                return true;
            }
        }
    }
}