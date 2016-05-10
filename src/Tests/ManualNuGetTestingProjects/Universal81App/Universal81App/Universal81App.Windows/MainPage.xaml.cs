using Windows.UI.Xaml.Controls;
using FeatureToggle.Core.Fluent;
using FeatureToggle.Toggles;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Universal81App
{
    class TestToggle : SimpleFeatureToggle { }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var x = new TestToggle().FeatureEnabled;

            var y = Is<TestToggle>.Enabled;
        }
    }
}
