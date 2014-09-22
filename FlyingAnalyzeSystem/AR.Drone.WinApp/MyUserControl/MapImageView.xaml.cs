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
    public partial class MapImageView : UserControl, INotifyPropertyChanged
    {
        private Boolean _isLeftButtonClickDown = false;
        private Boolean _isLeftButtonDown = false;
        private Boolean _isRightButtonClickDown = false;
        private Point _DownPoint = new Point();
        private Line _hint = new Line();
        private Boolean _hasLine = false;
        private List<Line> _lineList = new List<Line>();
        private List<double> _timeList = new List<double>();
        private int _index = 0;
        private Ellipse _nowPosition = new Ellipse();
        private Boolean _searching = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private BitmapImage _imagePath = null;
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

            _nowPosition.Stroke = System.Windows.Media.Brushes.Blue;
            _nowPosition.Fill = System.Windows.Media.Brushes.Blue;
            _hint.Stroke = System.Windows.Media.Brushes.Red;
            _mapImageView.Children.Add(_hint);
            _nowPosition.Height = 10;
            _nowPosition.Width = 10;
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

        private void OnMouseLeftButtonDownMapImageBack(object sender, MouseButtonEventArgs e)
        {
            _isLeftButtonDown = true;
            _isLeftButtonClickDown = true;
            _DownPoint = e.GetPosition(_mapImageBack);
        }

        private void OnMouseMoveMapImageBack(object sender, MouseEventArgs e)
        {
            if (_isLeftButtonDown)
            {
                _trans.X += (e.GetPosition(_mapImageBack).X - _DownPoint.X);
                _trans.Y += (e.GetPosition(_mapImageBack).Y - _DownPoint.Y);
                _DownPoint = e.GetPosition(_mapImageBack);
            }
            _isLeftButtonClickDown = false;
            if (_isRightButtonClickDown)
            {
                _hint.X2 = e.GetPosition(_mapImageView).X;
                _hint.Y2 = e.GetPosition(_mapImageView).Y;
            }
        }

        private void OnMouseLeftButtonUpMapImageBack(object sender, MouseButtonEventArgs e)
        {
            if (_isLeftButtonClickDown)
            {
                Initialize();
                _isLeftButtonClickDown = false;
            }
            _isLeftButtonDown = false;
        }

        private void OnMouseRightButtonDownMapImageBack(object sender, MouseButtonEventArgs e)
        {
            _isRightButtonClickDown = true;
            if (_hasLine)
            {
                _hint.X1 = _hint.X2;
                _hint.Y1 = _hint.Y2;
            }
            else
            {
                _hint.X1 = e.GetPosition(_mapImageView).X;
                _hint.Y1 = e.GetPosition(_mapImageView).Y;
            }
        }

        private void OnMouseRightButtonUpMapImageBack(object sender, MouseButtonEventArgs e)
        {
            if (_isRightButtonClickDown)
            {
                _hint.X2 = e.GetPosition(_mapImageView).X;
                _hint.Y2 = e.GetPosition(_mapImageView).Y;
                Line line = new Line();
                line.Stroke = System.Windows.Media.Brushes.Red;
                line.X1 = _hint.X1;
                line.Y1 = _hint.Y1;
                line.X2 = _hint.X2;
                line.Y2 = _hint.Y2;
                if (line.X1 != line.X2 || line.Y1 != line.Y2)
                {
                    _lineList.Add(line);
                    _mapImageView.Children.Add(line);
                }
                _hasLine = true;
            }
            _isRightButtonClickDown = false;
        }

        public void AddTime(double time)
        {
            _timeList.Add(time);
        }

        public double GetLimit(int index)
        {
            _index = index;
            if (!_searching)
            {
                _mapImageView.Children.Add(_nowPosition);
                _searching = true;
            }
            Line line = _lineList[_index];
            Canvas.SetLeft(_nowPosition, line.X1 - 5);
            Canvas.SetTop(_nowPosition, line.Y1 - 5);
            return _timeList[index];
        }

        public void SearchNowPosition(double time)
        {
            Line line = _lineList[_index];
            double totalTime = _timeList[_index];
            double x = (line.X2 - line.X1) / totalTime * time + line.X1;
            double y = (line.Y2 - line.Y1) / totalTime * time + line.Y1;
            Canvas.SetLeft(_nowPosition, x - 5);
            Canvas.SetTop(_nowPosition, y - 5);
        }
    }
}