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
    /// Interaction logic for ImageToggleButton.xaml
    /// </summary>
    public partial class ImageToggleButton : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _isToggled = false;
        public bool IsToggled
        {
            set
            {
                _isToggled = value;
                OnPropertyChanged("IsToggled");
                ToggleChanged();
            }
            get
            {
                return _isToggled;
            }
        }

        private BitmapImage _normalImage;
        private String _normalImagePath;
        public String NormalImagePath
        {
            set
            {
                _normalImagePath = value;
                _normalImage = new BitmapImage(new Uri(_normalImagePath, UriKind.Relative));
                img.Source = _normalImage;
            }
            get
            {
                return _normalImagePath;
            }
        }

        private BitmapImage _downImage;
        private String _downImagePath;
        public String DownImagePath
        {
            set
            {
                _downImagePath = value;
                _downImage = new BitmapImage(new Uri(_downImagePath, UriKind.Relative));
            }
            get
            {
                return _downImagePath;
            }
        }

        public delegate void ImageToggleButtonEvent();
        public event ImageToggleButtonEvent OnToggle = null;

        public ImageToggleButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ToggleChanged()
        {
            if (IsToggled)
            {
                img.Source = _downImage;
            }
            else
            {
                img.Source = _normalImage;
            }
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            IsToggled = !IsToggled;
            ToggleChanged();
            if (OnToggle != null)
            {
                OnToggle();
            }
        }
    }
}
