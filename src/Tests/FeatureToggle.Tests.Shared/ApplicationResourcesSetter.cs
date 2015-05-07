#if NETFX_CORE    
    using Windows.UI.Xaml;
#endif
#if WINDOWS_PHONE
    using System.Windows;
#endif
// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests.Shared
// ReSharper restore CheckNamespace
{
    public static class ApplicationResourcesSetter
    {
        public static void Set(string key, object value)
        {
#if WINDOWS_PHONE
            // cannot do this in win phone 8.1: Application.Current.Resources[key] = value;

            // the expected settings must be in the actual app.xaml resources
#endif           

#if NETFX_CORE
      Application.Current.Resources[key] = value;
#endif
        }
    }
}
