using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using JasonRoberts.FeatureToggle;
using Microsoft.Phone.Controls;

namespace ExampleWindowsPhoneApplication
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        private IFeatureToggle _feature2Toggle;

        public MainPage()
        {
            InitializeComponent();
            Feature2Toggle = new Feature2Toggle();
            DateToggle = new MyDateToggle();

            var feature2Key = "Feature2Toggle";
            var dateKey = "MyDateToggle";
                
            Application.Current.Resources.Add(feature2Key, false);
            Application.Current.Resources.Add(dateKey, new DateTime(2100,1,1));
            
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