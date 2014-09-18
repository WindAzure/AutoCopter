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
    /// Interaction logic for MapImageView.xaml
    /// </summary>
    public partial class MapImageView : UserControl,INotifyPropertyChanged
    {
        private Boolean _isClickDown = false;
        private Boolean _isDown = false;
        private Point _DownPoint = new Point();
        private BitmapImage _imagePath = new BitmapImage();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public BitmapImage ImagePath
        {
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
            get
            {
                return _imagePath;
            }
        }

        public MapImageView()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        public void Initialize()
        {
            _scale.ScaleX = 1;
            _scale.ScaleY = 1;
            _trans.X = 0;
            _trans.Y = 0;
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
            _isClickDown = true;
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
            _isClickDown = false;
        }

        private void OnMouseUpMapImageBack(object sender, MouseButtonEventArgs e)
        {
            if (_isClickDown)
            {
                Initialize();
                _isClickDown = false; 
            }
            _isDown = false;
        }
    }
}
