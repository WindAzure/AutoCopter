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
        private List<State> _commandList = new List<State>();
        private List<TimeSpan> _timeList2 = new List<TimeSpan>();
        private List<float> _angleList = new List<float>();
        private bool _isDrawPlane = false;

        private enum State
        {
            TakeOff, Hover, Up, Down, Forward, Right, Left, TurnRight, TurnLeft, Land, Wait
        };

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

        private bool _isDrawMileageLine = false;
        public bool IsDrawMileageLine
        {
            set
            {
                _isDrawMileageLine = value;
                OnPropertyChanged("IsDrawMileageLine");
            }
            get
            {
                return _isDrawMileageLine;
            }
        }

        public void DrawMileageLine(int index, double currentTimePoint, float angle)
        {

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
            if (!IsDrawMileageLine)
            {
                return;
            }

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
            if (!IsDrawMileageLine)
            {
                return;
            }

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

        public void SetIsDrawPlane(bool state)
        {
            _isDrawPlane = state;
        }

        private void DrawPlane(double x, double y,double angle)
        {
            //Canvas.SetLeft(_nowPosition, line.X1 - 5);
            //Canvas.SetTop(_nowPosition, line.Y1 - 5);
        }

        public void GetPosition(int index, double time, float angle)
        {
            if (_isDrawPlane == false)
            {
                return;
            }

            int lineIndex = -1;
            for (int i = 0; i <= index; i++)
            {
                if (_commandList[i] == State.Forward)
                {
                    lineIndex++;
                }
            }

            if (_commandList[index] == State.Forward)
            {
                Line line = _lineList[lineIndex];
                double costTime = _timeList2[index].Seconds + (double)_timeList2[index].Milliseconds / 1000;

                double x = (line.X2 - line.X1) / costTime * time + line.X1;
                double y = (line.Y2 - line.Y1) / costTime * time + line.Y1;
                DrawPlane(x, y,0);
            }
            else if (_commandList[index] == State.TurnLeft || _commandList[index] == State.TurnRight)
            {
                Line line = _lineList[lineIndex + 1];
                float originalAngle = _angleList[index - 1];

                Canvas.SetLeft(_nowPosition, line.X1 - 5);
                Canvas.SetTop(_nowPosition, line.Y1 - 5);

                float turnAngle = Math.Abs(angle - originalAngle);
                if (_commandList[index] == State.TurnLeft)
                    turnAngle = -turnAngle;
                Debug.WriteLine(_angleList[index - 1]);
                Debug.WriteLine(turnAngle);
            }
            else
            {
                Line line = _lineList[lineIndex + 1];

                Canvas.SetLeft(_nowPosition, line.X1 - 5);
                Canvas.SetTop(_nowPosition, line.Y1 - 5);
            }
        }

        public void Decode(String data)
        {
        /*   for (int index = 0; index < data.Count; index++)
            {
                char split = ' ';
                string[] dataString = data[index].Split(split);
                _commandList.Add(Convert(dataString[0]));
                _timeList.Add(TimeSpan.Parse(dataString[1]));
                _angleList.Add(float.Parse(dataString[2]));
            }*/
        }

        private State Convert(string conmandString)
        {
            switch (conmandString)
            {
                case "TakeOff":
                    return State.TakeOff;
                case "Hover":
                    return State.Hover;
                case "Up":
                    return State.Up;
                case "Down":
                    return State.Down;
                case "Forward":
                    return State.Forward;
                case "Left":
                    return State.Left;
                case "Right":
                    return State.Right;
                case "TurnLeft":
                    return State.TurnLeft;
                case "TurnRight":
                    return State.TurnRight;
                default:
                    return State.Land;
            }
        }
    }
}