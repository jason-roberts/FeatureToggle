using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace UWP10Example
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Feature2Toggle _feature2Toggle;
        private Feature3Toggle _feature3Toggle;               

        public MainPage()
        {
            InitializeComponent();

            Feature2Toggle = new Feature2Toggle();
            Feature3Toggle = new Feature3Toggle();            

            DataContext = this;
        }

        public Feature2Toggle Feature2Toggle
        {
            get { return _feature2Toggle; }
            set
            {
                _feature2Toggle = value;
                Notify("Feature2Toggle");
            }
        }

        public Feature3Toggle Feature3Toggle
        {
            get { return _feature3Toggle; }
            set
            {
                _feature3Toggle = value;
                Notify("Feature3Toggle");
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
