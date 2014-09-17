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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AR.Drone.WinApp.MyUserControl
{
    /// <summary>
    /// Interaction logic for MapImageView.xaml
    /// </summary>
    public partial class MapImageView : UserControl
    {
        private Boolean _isDown = false;
        private Point _DownPoint = new Point();

        public MapImageView()
        {
            InitializeComponent();
        }

        private void OnMouseWheelMapImageBack(object sender, MouseWheelEventArgs e)
        {
            var point = e.GetPosition(_mapImageBack);
            var pointToContent = _group.Inverse.Transform(point);

            _scale.ScaleX += e.Delta * 0.001;
            _scale.ScaleY += e.Delta * 0.001;

            _trans.X = -1 * ((pointToContent.X * _scale.ScaleX) - point.X);
            _trans.Y = -1 * ((pointToContent.Y * _scale.ScaleY) - point.Y);
        }

        private void OnMouseDownMapImageBack(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;
            _DownPoint = e.GetPosition(_mapImageBack);
        }

        private void OnMouseMoveMapImageBack(object sender, MouseEventArgs e)
        {
            if (_isDown)
            {
                _trans.X += (e.GetPosition(_mapImageBack).X - _DownPoint.X);
                _trans.Y += (e.GetPosition(_mapImageBack).Y - _DownPoint.Y);
                _DownPoint = e.GetPosition(_mapImageBack);
            }
        }

        private void OnMouseUpMapImageBack(object sender, MouseButtonEventArgs e)
        {
            _isDown = false;
        }
    }
}
