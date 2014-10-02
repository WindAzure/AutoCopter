using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using AR.Drone.Data.Navigation.Native;
using AR.Drone.WinApp.MyUserControl.MapComboBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AR.Drone.WinApp.Forms
{
    public partial class LearnForm : Form
    {
        private bool _isBack = false;
        private PlaneStateForm _lastForm = null;

        private const string ARDroneTrackFileExt = ".ardrone";
        private const string ARDroneTrackFilesFilter = "AR.Drone track files (*.ardrone)|*.ardrone";

        private NavigationData _navigationData;
        private NavigationPacket _navigationPacket;

        private float _heading;

        private DateTime _commandStartTime;
        private DateTime _commandEndTime;
        private enum State { TakeOff, Hover, Up, Down, Forward, Right, Left, TurnRight, TurnLeft, Land, Wait };
        private State _nowState = State.Land;

        private List<State> _commandList = new List<State>();
        private List<TimeSpan> _timeList = new List<TimeSpan>();
        private List<float> _angleList = new List<float>();

        public LearnForm(PlaneStateForm form)
        {
            InitializeComponent();
            _lastForm = form;
            _learnPanel.MouseDownLeftControlButton += MouseDownLearnPanelLeftControlButton;
            _learnPanel.MouseUpLeftControlButton += MouseUpLearnPanelLeftControlButton;
            _learnPanel.MouseDownRightControlButton += MouseDownLearnPanelRightControlButton;
            _learnPanel.MouseUpRightControlButton += MouseUpLearnPanelRightControlButton;
            _learnPanel.MouseDownForwardControlButton += MouseDownLearnPanelForwardControlButton;
            _learnPanel.MouseUpForwardControlButton += MouseUpLearnPanelForwardControlButton;
            _learnPanel.MouseDownUpControlButton += MouseDownLearnPanelUpControlButton;
            _learnPanel.MouseUpUpControlButton += MouseUpLearnPanelUpControlButton;
            _learnPanel.MouseDownDownControlButton += MouseLearnPanelDownDownControlButton;
            _learnPanel.MouseUpDownControlButton += MouseUpLearnPanelDownControlButton;
            _learnPanel.ClickSaveButton += ClickLearnPanelSaveButton;
            _learnPanel.ClickBackButton += ClickLearnPanelBackButton;
            _learnPanel.ClickTakeOffButton += ClickLearnPanelTakeOffButton;
            _learnPanel.ClickLandButton += ClickLearnPanelLandButton;

            DroneSingleton._droneClient = new DroneClient("192.168.1.1");
            DroneSingleton._droneClient.NavigationPacketAcquired += OnNavigationPacketAcquired;
            DroneSingleton._droneClient.NavigationDataAcquired += data => _navigationData = data;

            DroneSingleton._droneClient.Start();
            DroneSingleton._droneClient.ResetEmergency();
            DroneSingleton._droneClient.FlatTrim();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text += Environment.Is64BitProcess ? " [64-bit]" : " [32-bit]";
        }

        protected override void OnClosed(EventArgs e)
        {
            DroneSingleton._droneClient.Dispose();

            base.OnClosed(e);
        }

        private void OnNavigationPacketAcquired(NavigationPacket packet)
        {
            _navigationPacket = packet;
        }

        public void ClickLearnPanelLandButton()
        {
            Debug.WriteLine("ClickLearnPanelLandButton");
            Record();
            _nowState = State.Land;
            DroneSingleton._droneClient.Land();
            _commandList.Add(State.Land);
        }

        public void ClickLearnPanelTakeOffButton()
        {
            Debug.WriteLine("ClickLearnPanelTakeOffButton");
            DroneSingleton._droneClient.Takeoff();
            _commandStartTime = DateTime.Now;
            _commandList.Add(State.TakeOff);
        }

        public void ClickLearnPanelBackButton()
        {
            _isBack = true;
            this.Close();
        }

        public void ClickLearnPanelSaveButton()
        {
            Debug.WriteLine("OnClickSaveButton");
        }

        public void MouseUpLearnPanelDownControlButton()
        {
            Debug.WriteLine("MouseUpPlaneDownControlButton");
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseLearnPanelDownDownControlButton()
        {
            Debug.WriteLine("MouseDownPlaneDownControlButton");
            Record();
            _nowState = State.Down;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
            _commandList.Add(State.Down);
            _commandStartTime = DateTime.Now;
        }

        public void MouseUpLearnPanelUpControlButton()
        {
            Debug.WriteLine("MouseUpPlaneUpControlButton");
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelUpControlButton()
        {
            Debug.WriteLine("MouseDownPlaneUpControlButton");
            Record();
            _nowState = State.Up;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
            _commandList.Add(State.Up);
            _commandStartTime = DateTime.Now;
        }

        public void MouseUpLearnPanelForwardControlButton()
        {
            Debug.WriteLine("MouseUpPlaneForwardControlButton");
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelForwardControlButton()
        {
            Debug.WriteLine("MouseDownPlaneForwardControlButton");
            Record();
            _nowState = State.Forward;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
            _commandList.Add(State.Forward);
            _commandStartTime = DateTime.Now;
        }

        public void MouseUpLearnPanelRightControlButton()
        {
            Debug.WriteLine("MouseUpPlaneRightControlButton");
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelRightControlButton()
        {
            Debug.WriteLine("MouseDownPlaneRightControlButton");
            Record();
            NavdataBag navdataBag;
            if (_navigationPacket.Data != null && NavdataBagParser.TryParse(ref _navigationPacket, out navdataBag))
            {
                _heading = navdataBag.magneto.heading_fusion_unwrapped;
            }
            _nowState = State.TurnRight;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
            _commandList.Add(State.TurnRight);
        }

        public void MouseUpLearnPanelLeftControlButton()
        {
            Debug.WriteLine("MouseUpPlaneLeftControlButton");
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelLeftControlButton()
        {
            Debug.WriteLine("MouseDownPlaneLeftControlButton");
            Record();
            NavdataBag navdataBag;
            if (_navigationPacket.Data != null && NavdataBagParser.TryParse(ref _navigationPacket, out navdataBag))
            {
                _heading = navdataBag.magneto.heading_fusion_unwrapped;
            }
            _nowState = State.TurnLeft;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
            _commandList.Add(State.TurnLeft);
        }

        private void LearnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (_isBack)
                {
                    _lastForm.Location = this.Location;
                    _lastForm.WindowState = this.WindowState;
                    _lastForm.Width = this.Width;
                    _lastForm.Height = this.Height;
                    _lastForm.Show();
                }
                else
                {
                    _lastForm.Close();
                }
            }
        }

        private void Record()
        {
            if (_nowState == State.TurnLeft || _nowState == State.TurnRight)
            {
                float nowHeading;
                NavdataBag navdataBag;
                if (_navigationPacket.Data != null && NavdataBagParser.TryParse(ref _navigationPacket, out navdataBag))
                {
                    nowHeading = navdataBag.magneto.heading_fusion_unwrapped;
                    _timeList.Add(new TimeSpan(0, 0, 0, 0));
                    _angleList.Add(nowHeading);
                }
            }
            else
            {
                _commandEndTime = DateTime.Now;
                TimeSpan costTime = _commandEndTime.Subtract(_commandStartTime);
                _timeList.Add(costTime);
                _angleList.Add(0);
            }
        }
    }
}
