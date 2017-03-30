#if NETFX_CORE    

using Windows.UI.Xaml;
// ReSharper disable CheckNamespace
namespace FeatureToggle.Shared.Tests
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Used to set resources in XAML/UWP app so that tests can configure resource values
    /// </summary>
    public static class ApplicationResourcesSetter
    {
        public static void Set(string key, object value)
        {       
            Application.Current.Resources[key] = value;
        }
    }
}
#endif