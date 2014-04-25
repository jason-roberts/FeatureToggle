using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace ExampleWindows8StoreApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var fa = new FeatureA();
            FeatureA.Visibility = fa.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;


            var fb = new FeatureB();
            FeatureB.Visibility = fb.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
