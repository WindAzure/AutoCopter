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

        public LearnPanel()
        {
            InitializeComponent();
            DataContext = this;
            _timer.Interval = 1000;
            _timer.Elapsed += ElapsedTimer;
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
            Debug.WriteLine("MouseDownPlaneLeftControlButton");
            InitializeTimerAndValue("Degs");
        }

        private void MouseUpPlaneLeftControlButton()
        {
            Debug.WriteLine("MouseUpPlaneLeftControlButton");
            _timer.Stop();
        }

        private void MouseDownPlaneRightControlButton()
        {
            Debug.WriteLine("MouseDownPlaneRightControlButton");
            InitializeTimerAndValue("Degs");
        }

        private void MouseUpPlaneRightControlButton()
        {
            Debug.WriteLine("MouseUpPlaneRightControlButton");
            _timer.Stop();
        }

        private void MouseDownPlaneForwardControlButton()
        {
            Debug.WriteLine("MouseDownPlaneForwardControlButton");
            InitializeTimerAndValue("Secs");
        }

        private void MouseUpPlaneForwardControlButton()
        {
            Debug.WriteLine("MouseUpPlaneForwardControlButton");
            _timer.Stop();
        }

        private void MouseDownPlaneUpControlButton()
        {
            Debug.WriteLine("MouseDownPlaneUpControlButton");
            InitializeTimerAndValue("Secs");
        }

        private void MouseUpPlaneUpControlButton()
        {
            Debug.WriteLine("MouseUpPlaneUpControlButton");
            _timer.Stop();
        }

        private void MouseDownPlaneDownControlButton()
        {
            Debug.WriteLine("MouseDownPlaneDownControlButton");
            InitializeTimerAndValue("Secs");
        }

        private void MouseUpPlaneDownControlButton()
        {
            Debug.WriteLine("MouseUpPlaneDownControlButton");
            _timer.Stop();
        }
    }
}
