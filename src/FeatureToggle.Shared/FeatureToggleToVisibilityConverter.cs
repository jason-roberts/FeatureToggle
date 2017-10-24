#if WINDOWS_UWP

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;


// ReSharper disable CheckNamespace
namespace FeatureToggle
    // ReSharper restore CheckNamespace
{    
    public class FeatureToggleToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return EvaluateVisibility(value);
        }  

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }


        private static object EvaluateVisibility(object value)
        {
            var toggle = (IFeatureToggle)value;

            return toggle.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
#endif