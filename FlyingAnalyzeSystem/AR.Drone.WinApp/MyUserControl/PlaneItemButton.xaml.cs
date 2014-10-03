
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
    /// Interaction logic for PlaneItemButton.xaml
    /// </summary>
    public partial class PlaneItemButton : UserControl, INotifyPropertyChanged
    {
        private bool _isSelected = false;
        private bool _isDown = false;

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

        private BitmapImage _selectedImage;
        private String _selectedImagePath;
        public String SelectedImagePath
        {
            set
            {
                _selectedImagePath = value;
                _selectedImage = new BitmapImage(new Uri(_selectedImagePath, UriKind.Relative));
            }
            get
            {
                return _selectedImagePath;
            }
        }

        private String _itemText;
        public String ItemText
        {
            set
            {
                _itemText = value;
                OnPropertyChanged("ItemText");
            }
            get
            {
                return _itemText;
            }
        }

        public delegate void ImageThreeStateButtonEvent(object sender);
        public event ImageThreeStateButtonEvent OnClick = null;
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PlaneItemButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void InitializeToNormalImage()
        {
            _isSelected = false;
            img.Source = _normalImage;
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;
            _isSelected = false;
            img.Source = _downImage;
        }

        private void MouseLeftButtonUpImage(object sender, MouseButtonEventArgs e)
        {
            if (_isDown)
            {
                _isDown = false;
                if (OnClick != null)
                {
                    _isSelected = true;
                    img.Source = _selectedImage;
                    OnClick(this);
                }
            }

            if (!_isSelected)
            {
                img.Source = _normalImage;
            }
        }

        private void MouseLeaveImage(object sender, MouseEventArgs e)
        {
            if (!_isSelected)
            {
                img.Source = _normalImage;
            }
            _isDown = false;
        }
    }
}
