using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
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
    /// Interaction logic for LearnPanel.xaml
    /// </summary>
    public partial class LearnPanel : UserControl,INotifyPropertyChanged
    {
        private int _timeValue = 1;
        private Timer _timer=new Timer();

        public delegate void LearnPanelEvent();
        public event LearnPanelEvent MouseDownLeftControlButton = null;
        public event LearnPanelEvent MouseUpLeftControlButton = null;
        public event LearnPanelEvent MouseDownRightControlButton = null;
        public event LearnPanelEvent MouseUpRightControlButton = null;
        public event LearnPanelEvent MouseDownForwardControlButton = null;
        public event LearnPanelEvent MouseUpForwardControlButton = null;
        public event LearnPanelEvent MouseDownUpControlButton = null;
        public event LearnPanelEvent MouseUpUpControlButton = null;
        public event LearnPanelEvent MouseDownDownControlButton = null;
        public event LearnPanelEvent MouseUpDownControlButton = null;
        public event LearnPanelEvent ClickSaveButton = null;
        public event LearnPanelEvent ClickUploadButton = null;
        public event LearnPanelEvent ClickBackButton = null;
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String _mainCircleText = "";
        public String MainCircleText
        {
            set
            {
                _mainCircleText = value;
                OnPropertyChanged("MainCircleText");
            }
            get
            {
                return _mainCircleText;
            }
        }

        private String _mainCircleValue="";
        public String MainCircleValue
        {
            set
            {
                _mainCircleValue = value;
                OnPropertyChanged("MainCircleValue");
            }
            get
            {
                return _mainCircleValue;
            }
        }

        private String _planeID = "";
        public String PlaneID
        {
            set
            {
                _planeID = value;
                OnPropertyChanged("PlaneID");
            }
            get
            {
                return _planeID;
            }
        }

        private String _electricQuantityText = "";
        public String ElectricQuantityText
        {
            set
            {
                _electricQuantityText = value;
                OnPropertyChanged("ElectricQuantityText");
            }
            get
            {
                return _electricQuantityText;
            }
        }

        private String _altitudeText="";
        public String AltitudeText
        {
            set
            {
                _altitudeText = value;
                OnPropertyChanged("AltitudeText");
            }
            get
            {
                return _altitudeText;
            }
        }

        public LearnPanel()
        {
            InitializeComponent();
            DataContext = this;
            _timer.Interval = 1000;
            _timer.Elapsed += ElapsedTimer;
        }

        private Color GetRelativeColor(GradientStopCollection gsc, double offset)
        {
            GradientStop before = gsc.Where(w => w.Offset == gsc.Min(m => m.Offset)).First();
            GradientStop after = gsc.Where(w => w.Offset == gsc.Max(m => m.Offset)).First();

            foreach (var gs in gsc)
            {
                if (gs.Offset < offset && gs.Offset > before.Offset)
                {
                    before = gs;
                }
                if (gs.Offset > offset && gs.Offset < after.Offset)
                {
                    after = gs;
                }
            }

            var color = new Color();
            color.ScA = (float)((offset - before.Offset) * (after.Color.ScA - before.Color.ScA) / (after.Offset - before.Offset) + before.Color.ScA);
            color.ScR = (float)((offset - before.Offset) * (after.Color.ScR - before.Color.ScR) / (after.Offset - before.Offset) + before.Color.ScR);
            color.ScG = (float)((offset - before.Offset) * (after.Color.ScG - before.Color.ScG) / (after.Offset - before.Offset) + before.Color.ScG);
            color.ScB = (float)((offset - before.Offset) * (after.Color.ScB - before.Color.ScB) / (after.Offset - before.Offset) + before.Color.ScB);

            return color;
        }

        void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (_timeValue < 1000)
            {
                MainCircleValue = _timeValue.ToString();
                _timeValue++;
            }
            else
            {
                MainCircleValue = "N/A";
            }
        }

        private void InitializeTimerAndValue(String text)
        {
            MainCircleText = text;
            MainCircleValue = "0";
            _timeValue = 1;
            _timer.Start();
        }

        private void MouseDownPlaneLeftControlButton()
        {
            InitializeTimerAndValue("Degs");
            if (MouseDownLeftControlButton != null)
            {
                MouseDownLeftControlButton();
            }
        }

        private void MouseUpPlaneLeftControlButton()
        {
            _timer.Stop();
            if (MouseUpLeftControlButton != null)
            {
                MouseUpLeftControlButton();
            }
        }

        private void MouseDownPlaneRightControlButton()
        {
            InitializeTimerAndValue("Degs");
            if (MouseDownRightControlButton != null)
            {
                MouseDownRightControlButton();
            }
        }

        private void MouseUpPlaneRightControlButton()
        {
            _timer.Stop();
            if (MouseUpRightControlButton != null)
            {
                MouseUpRightControlButton();
            }
        }

        private void MouseDownPlaneForwardControlButton()
        {
            InitializeTimerAndValue("Secs");
            if (MouseDownForwardControlButton != null)
            {
                MouseDownForwardControlButton();
            }
        }

        private void MouseUpPlaneForwardControlButton()
        {
            _timer.Stop();
            if (MouseUpForwardControlButton != null)
            {
                MouseUpForwardControlButton();
            }
        }

        private void MouseDownPlaneUpControlButton()
        {
            InitializeTimerAndValue("Secs");
            if (MouseDownUpControlButton != null)
            {
                MouseDownUpControlButton();
            }
        }

        private void MouseUpPlaneUpControlButton()
        {
            _timer.Stop();
            if (MouseUpUpControlButton != null)
            {
                MouseUpUpControlButton();
            }
        }

        private void MouseDownPlaneDownControlButton()
        {
            InitializeTimerAndValue("Secs");
            if (MouseDownDownControlButton != null)
            {
                MouseDownDownControlButton();
            }
        }

        private void MouseUpPlaneDownControlButton()
        {
            _timer.Stop();
            if (MouseUpDownControlButton != null)
            {
                MouseUpDownControlButton();
            }
        }

        private void OnClickSaveButton()
        {
            if (ClickSaveButton != null)
            {
                ClickSaveButton();
            }
        }

        private void OnClickUploadButton()
        {
            if (ClickUploadButton != null)
            {
                ClickUploadButton();
            }
        }

        private void OnClickBackButton()
        {
            if (ClickBackButton != null)
            {
                ClickBackButton();
            }
        }

        public void SetBattery(double rate)
        {
            ElectricQuantityText = ((int)(rate * 100.0)).ToString();
            _battery.Width = _baterySample.Width * rate;
            Color endColor = GetRelativeColor(_batterySampleBrush.GradientStops, rate);
            Color middleColor = GetRelativeColor(_batterySampleBrush.GradientStops, rate / 2.0);

            LinearGradientBrush gradientBrushGroup = new LinearGradientBrush();
            GradientStop start = new GradientStop();
            start.Color = Colors.Red;
            start.Offset = 0;
            gradientBrushGroup.GradientStops.Add(start);

            GradientStop middle = new GradientStop();
            middle.Color = middleColor;
            middle.Offset = 0.5;
            gradientBrushGroup.GradientStops.Add(middle);

            GradientStop end = new GradientStop();
            end.Color = endColor;
            end.Offset = 1;
            gradientBrushGroup.GradientStops.Add(end);

            _battery.Fill = gradientBrushGroup;
        }
    }
}
