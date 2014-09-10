using System;
using System.Collections.Generic;
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
        Storyboard _bigCircle = new Storyboard();
        Storyboard _middleCircle = new Storyboard();
        Storyboard _signInUpCircle = new Storyboard();
        Storyboard _signInDownCircle = new Storyboard();
        Storyboard _bigCircleDownCircle = new Storyboard();

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

            _bigCircle.Completed += CompletedBigCircleAnimation;
            Rotate(_bigCircle, _bigCircleImage, 0, 360, 20000);

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

        void CompletedBigCircleAnimation(object sender, EventArgs e)
        {
            Rotate(_bigCircle, _bigCircleImage, 0, 360, 20000);
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
    }
}
