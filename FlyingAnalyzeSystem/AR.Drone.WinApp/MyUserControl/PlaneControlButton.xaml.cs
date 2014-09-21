using System;
using System.Collections.Generic;
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
    /// Interaction logic for PlaneControlButton.xaml
    /// </summary>
    public partial class PlaneControlButton : UserControl
    {
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

        private ObjectAnimationUsingKeyFrames _mouseDownAnimation = null;
        public ObjectAnimationUsingKeyFrames MouseDownAnimation
        {
            set
            {
                _mouseDownAnimation = value;
            }
            get
            {
                return _mouseDownAnimation;
            }
        }

        public delegate void PlaneControlButtonEvent();
        public event PlaneControlButtonEvent OnButtonMouseDown = null;
        public event PlaneControlButtonEvent OnButtonMouseUp = null;

        public PlaneControlButton()
        {
            InitializeComponent();
            MouseDownAnimation = null;
            _board.Completed += CompletedBoard;
        }

        void CompletedBoard(object sender, EventArgs e)
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            _board.Children.Clear();
            _board.Children.Add(MouseDownAnimation);
            Storyboard.SetTarget(MouseDownAnimation, img);
            Storyboard.SetTargetProperty(MouseDownAnimation, new PropertyPath(Image.SourceProperty));
            _board.Begin();
        }

        private void EndAnimation()
        {
            _board.Stop();
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;
            if (OnButtonMouseDown != null)
            {
                OnButtonMouseDown();
            }
            StartAnimation();
        }

        private void MouseLeftButtonUpImage(object sender, MouseButtonEventArgs e)
        {
            if (_isDown)
            {
                EndAnimation();
                _isDown = false;
                if (OnButtonMouseUp != null)
                {
                    OnButtonMouseUp();
                }
            }
            img.Source = _normalImage;
        }

        private void MouseLeaveImage(object sender, MouseEventArgs e)
        {
            if (_isDown)
            {
                EndAnimation();
                _isDown = false;
                if (OnButtonMouseUp != null)
                {
                    OnButtonMouseUp();
                }
            }
            img.Source = _normalImage;
        }
    }
}
