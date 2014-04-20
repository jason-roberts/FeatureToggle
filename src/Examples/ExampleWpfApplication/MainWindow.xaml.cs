using System.ComponentModel;
using System.Windows;

namespace ExampleWpfApplication
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Feature2Toggle _feature2Toggle;

        public MainWindow()
        {
            InitializeComponent();
            Feature2Toggle = new Feature2Toggle();
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}