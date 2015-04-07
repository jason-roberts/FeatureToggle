#if NETFX_CORE    
    using Windows.UI.Xaml;
#endif

// ReSharper disable CheckNamespace
namespace FeatureToggle.Tests.Shared
// ReSharper restore CheckNamespace
{
    public static class ApplicationResourcesSetter
    {
        public static void Set(string key, object value)
        {
// Windows phone Silverlight 8.1 has resources pre-set in app.xaml
#if NETFX_CORE
      Application.Current.Resources[key] = value;
#endif
        }
    }
}
