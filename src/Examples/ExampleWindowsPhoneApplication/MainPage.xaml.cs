using System;
using System.ComponentModel;
using System.Windows;
using JasonRoberts.FeatureToggle;
using Microsoft.Phone.Controls;

namespace ExampleWindowsPhoneApplication
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        private IFeatureToggle _feature2Toggle;
        private IFeatureToggle _dateRangeToggle;

        public MainPage()
        {
            InitializeComponent();
            Feature2Toggle = new Feature2Toggle();
            DateToggle = new MyDateToggle();
            DateRangeToggle = new MyDateRangeToggle();

            var feature2Key = "Feature2Toggle";
            var dateKey = "MyDateToggle";
            var dateRangeKey = "MyDateRangeToggle";
                
            Application.Current.Resources.Add(feature2Key, false);
            Application.Current.Resources.Add(dateKey, new DateTime(2100,1,1));
            Application.Current.Resources.Add(dateRangeKey, "01/01/2012 00:00:00 | 31/12/2012 23:59:59");
            
            
            DataContext = this;            
        }



        public IFeatureToggle DateToggle
        {
            get { return _feature2Toggle; }
            set
            {
                _feature2Toggle = value;
                Notify("DateToggle");
            }
        }

        public IFeatureToggle Feature2Toggle
        {
            get { return _feature2Toggle; }
            set
            {
                _feature2Toggle = value;
                Notify("Feature2Toggle");
            }
        }

        public IFeatureToggle DateRangeToggle
        {
            get { return _dateRangeToggle; }
            set
            {
                _dateRangeToggle = value;
                Notify("DateRangeToggle");
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