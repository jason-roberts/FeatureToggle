using System.Windows;
using Microsoft.Phone.Controls;

namespace ExamplePhone8App
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
 
            var fa = new FeatureA();
            FeatureA.Visibility = fa.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;


            var fb = new FeatureB();
            FeatureB.Visibility = fb.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;

        }     
    }
}