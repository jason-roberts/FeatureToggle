using System.ComponentModel;
using System.Windows;
using FeatureToggle;

namespace ExampleWpfApplication
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Feature2Toggle _feature2Toggle;
        private Feature3Toggle _feature3Toggle;       
        private IFeatureToggle _feature5;

        public MainWindow()
        {
            InitializeComponent();

            Feature2Toggle = new Feature2Toggle();
            Feature3Toggle = new Feature3Toggle();            

            Feature5 = new CompositeAndDecorator(new FeatureXToggle(), new FeatureYToggle());

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



        public IFeatureToggle Feature5
        {
            get { return _feature5; }
            set
            {
                _feature5 = value;
                Notify("Feature5");
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