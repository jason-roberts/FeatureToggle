using System;
using JasonRoberts.FeatureToggle;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FeatureToggle.WinRTExtensions
{
    public class FeatureToggleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var toggle = (IFeatureToggle)value;

            return toggle.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
