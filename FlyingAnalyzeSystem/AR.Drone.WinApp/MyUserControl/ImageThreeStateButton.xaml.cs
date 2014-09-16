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
        private Storyboard _board = new Storyboard();

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

        private Boolean _isActiveHoverAnimation;
        public Boolean IsActiveHoverAnimation
        {
            set
            {
                _isActiveHoverAnimation = value;
            }
            get
            {
                return _isActiveHoverAnimation;
            }
        }

        private ObjectAnimationUsingKeyFrames _mouseHoverAnimation;
        public ObjectAnimationUsingKeyFrames MouseHoverAnimation
        {
            set
            {
                _mouseHoverAnimation = value;
            }
            get
            {
                return _mouseHoverAnimation;
            }
        }

        public delegate void ImageThreeStateButtonEvent();
        public event ImageThreeStateButtonEvent OnClick = null;

        public ImageThreeStateButton()
        {
            InitializeComponent();
            IsActiveHoverAnimation = false;
            MouseHoverAnimation = null;
            _board.Completed += CompletedBoard;
        }

        void CompletedBoard(object sender, EventArgs e)
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            _board.Children.Clear();
            _board.Children.Add(MouseHoverAnimation);
            Storyboard.SetTarget(MouseHoverAnimation, img);
            Storyboard.SetTargetProperty(MouseHoverAnimation, new PropertyPath(Image.SourceProperty));
            _board.Begin();
        }

        private void EndAnimation()
        {
            _board.Stop();
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            EndAnimation();
            _isDown = true;
            img.Source = _downImage;
        }

        private void MouseLeftButtonUpImage(object sender, MouseButtonEventArgs e)
        {
            if (_isDown)
            {
                _isDown = false;
                if(OnClick!=null)
                {
                    OnClick();
                }
            }

            if (_isHover)
            {
                EndAnimation();
                StartAnimation();
            }
            else
            {
                img.Source = _normalImage;
            }
        }

        private void MouseEnterImage(object sender, MouseEventArgs e)
        {
            if (!IsActiveHoverAnimation)
            {
                img.Source = _hoverImage;
            }
            else
            {
                _isHover = true;
                EndAnimation();
                StartAnimation();
            }
        }

        private void MouseLeaveImage(object sender, MouseEventArgs e)
        {
            EndAnimation();
            img.Source = _normalImage;
            _isHover = false;
            _isDown = false;
        }
    }
}
