using System.Windows;
using FeatureToggle.Core;
using Microsoft.Phone.Controls;

namespace ExamplePhone8App
{
    public partial class MainPage : PhoneApplicationPage
    {
        private FeatureB _featureB = new FeatureB();
        private FridayImInLoveFeature _fri = new FridayImInLoveFeature();

        public MainPage()
        {
            InitializeComponent();
 

            // Using in code
            var fa = new FeatureA();
            FeatureA.Visibility = fa.FeatureEnabled ? Visibility.Visible : Visibility.Collapsed;

            // using in databinding
            this.DataContext = this;
        }

        public FeatureB  Fb
        {
            get { return _featureB; }
            set { _featureB = value; }
        }

        public FridayImInLoveFeature Fri
        {
            get { return _fri; }
            set { _fri = value; }
        }
    }
}