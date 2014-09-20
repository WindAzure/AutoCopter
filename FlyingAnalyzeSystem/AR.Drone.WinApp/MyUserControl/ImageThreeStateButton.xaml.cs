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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AR.Drone.WinApp.MyUserControl
{
    /// <summary>
    /// Interaction logic for ImageThreeStateButton.xaml
    /// </summary>
    public partial class ImageThreeStateButton : UserControl
    {
        private bool _isHover = false;
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

        private BitmapImage _hoverImage;
        private String _hoverImagePath;
        public String HoverImagePath
        {
            set
            {
                _hoverImagePath = value;
                _hoverImage = new BitmapImage(new Uri(_hoverImagePath, UriKind.Relative));
            }
            get
            {
                return _hoverImagePath;
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

        public delegate void ImageThreeStateButtonEvent();
        public event ImageThreeStateButtonEvent OnClick = null;

        public ImageThreeStateButton()
        {
            InitializeComponent();
        }
        
        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;
            img.Source = _downImage;
        }

        private void MouseLeftButtonUpImage(object sender, MouseButtonEventArgs e)
        {
            if (_isDown)
            {
                _isDown = false;
                if (OnClick != null)
                {
                    OnClick();
                }
            }

            if (_isHover)
            {
                img.Source = _hoverImage;
            }
            else
            {
                img.Source = _normalImage;
            }
        }

        private void MouseEnterImage(object sender, MouseEventArgs e)
        {
            _isHover = true;
            img.Source = _hoverImage;
        }

        private void MouseLeaveImage(object sender, MouseEventArgs e)
        {
            img.Source = _normalImage;
            _isHover = false;
            _isDown = false;
        }
    }
}