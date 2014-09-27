using AR.Drone.WinApp.CommandToServer;
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
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : UserControl
    {
        private bool _isDown = false;
        private Storyboard _bigCircleIn = new Storyboard();
        private Storyboard _bigCircleOut = new Storyboard();
        private Storyboard _middleCircle = new Storyboard();
        private Storyboard _signInUpCircle = new Storyboard();
        private Storyboard _signInDownCircle = new Storyboard();
        private Storyboard _bigCircleDownCircle = new Storyboard();

        public delegate void LoginPanelEvent();
        public event LoginPanelEvent SignInSuccess = null;

        public void Rotate(Storyboard board, DependencyObject obj, int from, int to, int milliSecond)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = TimeSpan.FromMilliseconds(milliSecond);
            board.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.Angle"));
            Storyboard.SetTarget(animation, obj);
            board.Begin();
        }

        public LoginPanel()
        {
            InitializeComponent();

            _bigCircleIn.Completed += CompletedBigCircleInAnimation;
            Rotate(_bigCircleIn, _bigCircleInImage, 0, 360, 30000);

            _bigCircleOut.Completed += CompletedBigCircleOutAnimation;
            Rotate(_bigCircleOut, _bigCircleOutImage, 360, 0, 30000);

            _middleCircle.Completed += CompletedMiddleCircleAnimation;
            Rotate(_middleCircle, _middleCircleImage, 0, 360, 16000);

            _signInUpCircle.Completed += CompletedSignInUpCircleAnimation;
            Rotate(_signInUpCircle, _signInUpCircleImage, 360, 0, 2000);

            _signInDownCircle.Completed += CompletedSignInDownCircleAnimation;
            Rotate(_signInDownCircle, _signInDownCircleImage, 360, 0, 4000);

            _bigCircleDownCircle.Completed += CompletedBigCircleDownAnimation;
            Rotate(_bigCircleDownCircle, _bigCircleDownImage, 360, 0, 4000);
        }

        void CompletedMiddleCircleAnimation(object sender, EventArgs e)
        {
            Rotate(_middleCircle, _middleCircleImage, 0, 360, 16000);
        }

        void CompletedBigCircleInAnimation(object sender, EventArgs e)
        {
            Rotate(_bigCircleIn, _bigCircleInImage, 0, 360, 30000);
        }

        void CompletedBigCircleOutAnimation(object sender, EventArgs e)
        {
            Rotate(_bigCircleOut, _bigCircleOutImage, 360, 0, 30000);
        }

        void CompletedSignInUpCircleAnimation(object sender, EventArgs e)
        {
            Rotate(_signInUpCircle, _signInUpCircleImage, 360, 0, 2000);
        }

        void CompletedSignInDownCircleAnimation(object sender, EventArgs e)
        {
            Rotate(_signInDownCircle, _signInDownCircleImage, 360, 0, 4000);
        }

        void CompletedBigCircleDownAnimation(object sender, EventArgs e)
        {
            Rotate(_bigCircleDownCircle, _bigCircleDownImage, 360, 0, 4000);
        }

        private void MouseDownSignInImage(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;
            _signInImage.Focus();
            _signInImage.Opacity = 0.5;
        }

        private void MouseEnterSignInImage(object sender, MouseEventArgs e)
        {
            _signInImage.Opacity = 0.8;
        }

        private void MouseLeaveSignInImage(object sender, MouseEventArgs e)
        {
            _isDown = false;
            _signInImage.Opacity = 1;
        }

        private void MouseUpSignInImage(object sender, MouseButtonEventArgs e)
        {
            _signInImage.Opacity = 1;
            if (_isDown)
            {
                if (Commands.IsAdmin(_accountTextBox.AccountText, _passwordTextBox.PasswordText) == false)
                {
                    MessageBox.Show("Invalid ID or password", "Error");
                }
                else
                {
                    if (SignInSuccess != null)
                    {
                        SignInSuccess();
                    }
                }
            }
            _isDown = false;
        }
    }
}
