using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FeatureToggle;

namespace ExampleWpfApplication
{
    class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var toggle = (IFeatureToggle) value;

            return toggle.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
