using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AccountTextBox.xaml
    /// </summary>
    public partial class AccountTextBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String _accountText;
        public String AccountText
        {
            set
            {
                _accountText = value;
                OnPropertyChanged("AccountText");
            }
            get
            {
                return _accountText;
            }
        }

        public AccountTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
