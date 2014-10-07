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
using AR.Drone.WinApp.Forms;

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
        //private BitmapImage _imagePath = new BitmapImage();
        private Line _hint = new Line();
        private Boolean _hasLine = false;
        private List<Line> _lineList = new List<Line>();
        private Image _nowPosition = new Image();
        private RotateTransform _rotate = new RotateTransform();
        private float _initAngle = 0;
        private bool _isDrawPlane = false;

        private List<LearnForm.State> _commandList = new List<LearnForm.State>();
        private List<TimeSpan> _timeList = new List<TimeSpan>();
        private List<float> _angleList = new List<float>();

        private Point _nowPoint = new Point();

        private string _lineString;

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

        public MapImageView()
        {
            InitializeComponent();
            DataContext = this;

            _hint.Stroke = System.Windows.Media.Brushes.Red;
            _mapImageView.Children.Add(_hint);
            _nowPosition.Source = new BitmapImage(new Uri("../Image/PlaneState/PlanePosition.png", UriKind.Relative));
            _nowPosition.RenderTransformOrigin = new Point(0.5, 0.5);

            _rotate.Angle = 0;
            _nowPosition.RenderTransform = _rotate;
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

        public void SetIsDrawPlane(bool state)
        {
            _isDrawPlane = state;
            if (_isDrawPlane)
                _mapImageView.Children.Add(_nowPosition);
            else
                _mapImageView.Children.Remove(_nowPosition);
        }

        public void GetPosition(int index, double time, float angle)
        {
            Point point;
            int lineIndex = -1;
            for (int i = 0; i <= index; i++)
            {
                if (_commandList[i] == LearnForm.State.Forward)
                {
                    lineIndex++;
                }
            }

            if (_commandList[index] == LearnForm.State.Forward)
            {
                Line line = _lineList[lineIndex];
                double costTime = _timeList[index].Seconds + (double)_timeList[index].Milliseconds / 1000;

                double x = (line.X2 - line.X1) / costTime * time + line.X1;
                double y = (line.Y2 - line.Y1) / costTime * time + line.Y1;
                _nowPoint.X = x;
                _nowPoint.Y = y;

                Canvas.SetLeft(_nowPosition, x - 5);
                Canvas.SetTop(_nowPosition, y - 4);
            }
            else if (_commandList[index] == LearnForm.State.TurnLeft || _commandList[index] == LearnForm.State.TurnRight)
            {
                Line line = _lineList[lineIndex + 1];
                float originalAngle = _angleList[0];

                _nowPoint.X = line.X1;
                _nowPoint.Y = line.Y1;

                Canvas.SetLeft(_nowPosition, line.X1 - 5);
                Canvas.SetTop(_nowPosition, line.Y1 - 4);

                float turnAngle = angle - originalAngle;
                if (_commandList[index] == LearnForm.State.TurnLeft)
                    turnAngle = -turnAngle;
                _rotate.Angle = _initAngle + turnAngle;
                _nowPosition.RenderTransform = _rotate;
            }
            else
            {
                Line line = _lineList[lineIndex + 1];

                _nowPoint.X = line.X1;
                _nowPoint.Y = line.Y1;

                Canvas.SetLeft(_nowPosition, line.X1 - 5);
                Canvas.SetTop(_nowPosition, line.Y1 - 4);
            }
        }

        public void Decode(String data)
        {
            _commandList.Clear();
            _timeList.Clear();
            _angleList.Clear();
            char split = ' ';
            string[] dataString = data.Split(split);
            for (int i = 0; i < dataString.Length; i += 3)
            {
                _commandList.Add(Convert(dataString[i]));
                _timeList.Add(TimeSpan.Parse(dataString[i + 1]));
                _angleList.Add(float.Parse(dataString[i + 2]));
            }
        }

        private LearnForm.State Convert(string conmandString)
        {
            switch (conmandString)
            {
                case "TakeOff":
                    return LearnForm.State.TakeOff;
                case "Hover":
                    return LearnForm.State.Hover;
                case "Up":
                    return LearnForm.State.Up;
                case "Down":
                    return LearnForm.State.Down;
                case "Forward":
                    return LearnForm.State.Forward;
                case "Left":
                    return LearnForm.State.Left;
                case "Right":
                    return LearnForm.State.Right;
                case "TurnLeft":
                    return LearnForm.State.TurnLeft;
                case "TurnRight":
                    return LearnForm.State.TurnRight;
                default:
                    return LearnForm.State.Land;
            }
        }

        private void InitAngle()
        {
            Line line = _lineList[0];
            double m = (line.Y2 - line.Y1) / (line.X2 - line.X1);
            float angle = (float)(Math.Atan(m) * 180 / Math.PI);
            if (m == 0)
            {
                if (line.X2 > line.X1)
                    _initAngle = 0;
                else
                    _initAngle = 180;
            }
            else if (line.X2 > line.X1)
            {
                _initAngle = angle;
            }
            else if (line.X2 == line.X1)
            {
                if (line.Y2 > line.Y1)
                    _initAngle = -90;
                else
                    _initAngle = 90;
            }
            else if (line.X2 < line.X1)
            {
                _initAngle = -180 + angle;
            }
            _rotate.Angle = _initAngle;
            _nowPosition.RenderTransform = _rotate;
        }

        public string LineString
        {
            get
            {
                _lineString = "";
                for (int index = 0; index < _lineList.Count; index++)
                {
                    _lineString = _lineString + _lineList[index].X1 + " " + _lineList[index].Y1 + " " + _lineList[index].X2 + " " + _lineList[index].Y2;
                    if (index != _lineList.Count - 1)
                        _lineString = _lineString + " ";
                }
                return _lineString;
            }
        }

        public void SetLineString(string lineString)
        {
            _lineString = lineString;
            DecodeLineString();
        }

        private void DecodeLineString()
        {
            _lineList.Clear();
            char split = ' ';
            string[] dataString = _lineString.Split(split);
            for (int index = 0; index < dataString.Length; index += 4)
            {
                Line line = new Line();
                line.X1 = double.Parse(dataString[index]);
                line.Y1 = double.Parse(dataString[index + 1]);
                line.X2 = double.Parse(dataString[index + 2]);
                line.Y2 = double.Parse(dataString[index + 3]);
                _lineList.Add(line);
            }
        }

        public List<LearnForm.State> CommandList
        {
            get
            {
                return _commandList;
            }
        }

        public List<TimeSpan> TimeList
        {
            get
            {
                return _timeList;
            }
        }

        public List<float> AngleList
        {
            get
            {
                return _angleList;
            }
        }

        public double X
        {
            get
            {
                return _nowPoint.X;
            }
        }

        public double Y
        {
            get
            {
                return _nowPoint.Y;
            }
        }
    }
}