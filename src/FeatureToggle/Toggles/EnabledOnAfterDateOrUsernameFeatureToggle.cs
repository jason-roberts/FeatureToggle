using Commify.FeatureToggle.Interfaces;
using Commify.FeatureToggle.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commify.FeatureToggle.Toggles
{
    public class EnabledOnAfterDateOrUsernameFeatureToggle : IFeatureToggle
    {
        private readonly string _username;
        public EnabledOnAfterDateOrUsernameFeatureToggle(string username) : this()
        {
            _username = username;
        }
        protected EnabledOnAfterDateOrUsernameFeatureToggle()
        {
            NowProvider = () => DateTime.Now;
            ToggleStringValueProvider = new AppSettingsProvider();
            ToggleDateValueProvider = new AppSettingsProvider();
        }

        public Func<DateTime> NowProvider { get; set; }
        public virtual IStringToggleValueProvider ToggleStringValueProvider { get; set; }
        public virtual IDateTimeToggleValueProvider ToggleDateValueProvider { get; set; }

        public bool FeatureEnabled
        {
            get { return _username.EndsWith(ToggleStringValueProvider.EvaluateStringToggleValue(this)) || (NowProvider.Invoke() >= ToggleDateValueProvider.EvaluateDateTimeToggleValue(this)); }
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
