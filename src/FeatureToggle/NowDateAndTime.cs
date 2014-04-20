using System;
using FeatureToggle.Core;

namespace FeatureToggle
{
    public class NowDateAndTime : INowDateAndTime
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
