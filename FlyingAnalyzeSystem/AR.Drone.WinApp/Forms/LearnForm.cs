using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using AR.Drone.Data.Navigation.Native;
using AR.Drone.WinApp.CommandToServer;
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

        private float _heading;

        private DateTime _commandStartTime;
        private DateTime _commandEndTime;

        public enum State { TakeOff, Hover, Up, Down, Forward, Right, Left, TurnRight, TurnLeft, Land, Wait };
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

            _learnPanel.PlaneID = "Drone-001";
            _planeStateTimer.Enabled = true;
        }

        public void ClickLearnPanelLandButton()
        {
            Record();
            _nowState = State.Land;
            DroneSingleton._droneClient.Land();
            _commandList.Add(State.Land);
        }

        public void ClickLearnPanelTakeOffButton()
        {
            DroneSingleton._droneClient.Takeoff();
            _commandStartTime = DateTime.Now;
            NavdataBag navdataBag;
            if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
            {
                _heading = navdataBag.magneto.heading_fusion_unwrapped;
                _heading = CheckAngle(_heading);
            }
            _commandList.Add(State.TakeOff);
        }

        public void ClickLearnPanelBackButton()
        {
            _isBack = true;
            this.Close();
        }

        public void ClickLearnPanelSaveButton(String id)
        {
            string data = "";
            for (int index = 0; index < _commandList.Count; index++)
            {
                data = data + _commandList[index].ToString() + " " + _timeList[index].ToString() + " " + _angleList[index].ToString();
                if (index != _commandList.Count - 1)
                    data = data + " ";
            }
            Commands.UpdateMileage(id, data);
            string lineData = _learnPanel._mapImage.LineString;
            Commands.UpdateMileageLine(id, lineData);
        }

        public void SetComboBoxSource(ObservableCollection<ImageComboBoxItemProperty> source)
        {
            _learnPanel.SetComboBoxSource(source);
        }

        public void MouseUpLearnPanelDownControlButton()
        {
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseLearnPanelDownDownControlButton()
        {
            Record();
            _nowState = State.Down;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
            _commandList.Add(State.Down);
            _commandStartTime = DateTime.Now;
        }

        public void MouseUpLearnPanelUpControlButton()
        {
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelUpControlButton()
        {
            Record();
            _nowState = State.Up;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
            _commandList.Add(State.Up);
            _commandStartTime = DateTime.Now;
        }

        public void MouseUpLearnPanelForwardControlButton()
        {
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelForwardControlButton()
        {
            Record();
            _nowState = State.Forward;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
            _commandList.Add(State.Forward);
            _commandStartTime = DateTime.Now;
        }

        public void MouseUpLearnPanelRightControlButton()
        {
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelRightControlButton()
        {
            Record();
            NavdataBag navdataBag;
            if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
            {
                _heading = navdataBag.magneto.heading_fusion_unwrapped;
                _heading = CheckAngle(_heading);
            }
            _nowState = State.TurnRight;
            DroneSingleton._droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
            _commandList.Add(State.TurnRight);
        }

        public void MouseUpLearnPanelLeftControlButton()
        {
            Record();
            _nowState = State.Hover;
            DroneSingleton._droneClient.Hover();
            _commandList.Add(State.Hover);
            _commandStartTime = DateTime.Now;
        }

        public void MouseDownLearnPanelLeftControlButton()
        {
            Record();
            NavdataBag navdataBag;
            if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
            {
                _heading = navdataBag.magneto.heading_fusion_unwrapped;
                _heading = CheckAngle(_heading);
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
                if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
                {
                    nowHeading = navdataBag.magneto.heading_fusion_unwrapped;
                    nowHeading = CheckAngle(nowHeading);
                    _timeList.Add(new TimeSpan(0, 0, 0, 0));
                    _angleList.Add(nowHeading);
                    _heading = nowHeading;
                }
            }
            else
            {
                _commandEndTime = DateTime.Now;
                TimeSpan costTime = _commandEndTime.Subtract(_commandStartTime);
                _timeList.Add(costTime);
                _angleList.Add(_heading);
            }
        }

        private float CheckAngle(float angle)
        {
            if (angle >= 360)
                angle -= 360;
            else if (angle < 0)
                angle += 360;
            return angle;
        }

        private void _planeStateTimer_Tick(object sender, EventArgs e)
        {
            _learnPanel.SetBattery(DroneSingleton._navigationData.Battery.Percentage / 100.0);
            _learnPanel.AltitudeText = DroneSingleton._navigationData.Altitude.ToString();
        }
    }
}
