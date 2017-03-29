using System;
using System.Globalization;
using FeatureToggle;


#if WINDOWS_UWP
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
#elif WINDOWS_PHONE
    using System.Windows;
    using System.Windows.Data;
#endif

namespace FeatureToggle
{    
    #if (WINDOWS_UWP || WINDOWS_PHONE)
    public class FeatureToggleToVisibilityConverter : IValueConverter
    {
#if (WINDOWS_UWP)
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return EvaluateVisibility(value);
        }  

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EvaluateVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

#endif

        private static object EvaluateVisibility(object value)
        {
            var toggle = (IFeatureToggle)value;

            return toggle.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

    }

#endif
}
