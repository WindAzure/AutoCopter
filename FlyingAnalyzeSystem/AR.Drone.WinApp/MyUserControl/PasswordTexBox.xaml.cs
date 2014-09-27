using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AR.Drone.WinApp.MyUserControl
{
    /// <summary>
    /// Interaction logic for PasswordTexBox.xaml
    /// </summary>
    public partial class PasswordTexBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String _passwordText;
        public String PasswordText
        {
            set
            {
                _passwordText = value;
                _passwordBox.Password = _passwordText;
                OnPropertyChanged("PasswordText");
            }
            get
            {
                _passwordText = _passwordBox.Password;
                return _passwordText;
            }
        }

        public PasswordTexBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void _label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _label.Visibility = Visibility.Hidden;
            _passwordBox.Focus();
        }

        private void _passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_passwordBox.Password.Length == 0)
            {
                _label.Visibility = Visibility.Visible;
            }
        }

        private void _passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_passwordBox.Password.Length == 0)
            {
                _label.Visibility = Visibility.Hidden;
            }
        }
    }
}
