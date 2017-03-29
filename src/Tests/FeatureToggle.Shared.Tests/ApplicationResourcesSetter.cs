#if NETFX_CORE    

using Windows.UI.Xaml;
// ReSharper disable CheckNamespace
namespace FeatureToggle.Shared.Tests
// ReSharper restore CheckNamespace
{
    public static class ApplicationResourcesSetter
    {
        public static void Set(string key, object value)
        {
         


      Application.Current.Resources[key] = value;

        }
    }
}
#endif