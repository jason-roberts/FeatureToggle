using System;
using FeatureToggle.Core;

namespace JasonRoberts.FeatureToggle
{
    public class NowDateAndTime : INowDateAndTime
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
