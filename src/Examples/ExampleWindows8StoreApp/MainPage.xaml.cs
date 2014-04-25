using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ExampleWindows8StoreApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
