using AR.Drone.Client.Command;
using AR.Drone.Data.Navigation.Native;
using AR.Drone.WinApp.CommandToServer;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AR.Drone.WinApp.Forms
{
    public partial class PlaneStateForm : Form
    {
        private bool _isPatroling = false;
        private bool _isShowedWarning = false;
        private bool _isCheckingBle = false;
        private bool _isSendedGCM = false;
        private int _pirTrueTime = 0;
        private int _retryConnectionTimes = 0;
        private List<String> _bleMac = new List<string>();

        Image<Bgr, Byte> _input;
        LineSegment2D[][] _lines;
        private bool _controling = false;
        LineSegment2D _lastLine;
        private bool _haveLastLine = false;
        private float _heading;
        private bool _turning = false;
        private float _targetAngle;

        private DateTime _commandStartTime;
        private DateTime _commandEndTime;
        private DateTime _waitStartTime;
        private DateTime _waitEndTime;
        private TimeSpan _waitTimeLimit = new TimeSpan(0, 0, 0, 1, 0);
        private TimeSpan _totalTime;
        private bool _isForwarding = false;
        private LearnForm.State _nowState = LearnForm.State.Land;

        private bool _isCounting = false;
        private List<LearnForm.State> _commandList;
        private List<TimeSpan> _timeList;
        private List<float> _angleList;
        private int _commandIndex;
        private LearnForm.State _nowCommand = LearnForm.State.Land;
        private bool _needWait = true;
        private bool _waitting = false;

        private bool _isReturn = false;
        private bool _turnBack = false;
        private bool _backHomeWait = false;

        private bool _dependLine = false;

        public PlaneStateForm()
        {
            InitializeComponent();

            _planeStatePanel.ClickPatrolReturnHomeButton += OnClickPlaneStatePanelReturnHomeButton;
            _planeStatePanel.ClickPatrolManualControlButton += OnClickPlaneStatePanelManualControlButton;
            _planeStatePanel.ClickStartLearnButton += OnClickPlaneStatePanelStartLearnButton;
            _planeStatePanel.StartAutoPatrol += OnPlaneStatePanelStartAutoPatrol;
            _planeStatePanel._infoControl.ClickNoButton += ClickInfoControlNoButton;
            _planeStatePanel._infoControl.ClickYesButton += ClickInfoControlYesButton;
            _planeStatePanel._infoControl.ClickSendButton += ClickInfoControlSendButton;
            DroneSingleton.InitializeDrone();
            _planeStateTimer.Enabled = true;
            _videoUpdateTimer.Enabled = true;
        }

        private void ClickInfoControlSendButton()
        {
            Commands.SendGCM("Ar.drone-001", false);
            _isSendedGCM = true;
        }

        private void ClickInfoControlYesButton()
        {
            SwitchForm(new ManualForm(this));
        }

        private void ClickInfoControlNoButton()
        {
            _planeStatePanel.HideInfoPanel();
            _pirTrueTime = 0;
            if (_isSendedGCM)
            {
                Commands.SendGCM("Ar.drone-001", true);
            }
            _isSendedGCM = false;
            _isShowedWarning = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            DroneSingleton._droneClient.Dispose();
            DroneSingleton._videoPacketDecoderWorker.Dispose();
            base.OnClosed(e);
        }

        private void SwitchForm(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = this.Location;
            form.WindowState = this.WindowState;
            form.Width = this.Width;
            form.Height = this.Height;
            form.Show();
            this.Hide();
        }

        public void InitializeChildPanel()
        {
            _planeStatePanel.InitializeChildPanel();
        }

        public void OnClickPlaneStatePanelStartLearnButton()
        {
            LearnForm form = new LearnForm(this);
            form.SetComboBoxSource(_planeStatePanel.ComboBoxItemSource);
            SwitchForm(form);
        }

        public void OnPlaneStatePanelStartAutoPatrol()
        {
            _isPatroling = true;

            //FLY START
            _nowState = LearnForm.State.Land;
            _controling = true;
            _haveLastLine = false;
            _backHomeWait = false;
            _isReturn = false;
            _turnBack = false;
            _commandIndex = 0;
            ReadCommand();
        }

        public void OnClickPlaneStatePanelManualControlButton()
        {
            SwitchForm(new ManualForm(this));
        }

        public void OnClickPlaneStatePanelReturnHomeButton()
        {
            //GO HOME
            _backHomeWait = false;
            _isReturn = !_isReturn;

            Return();

            _backHomeWait = true;
        }

        private void StoreBleMac()
        {
            _bleMac.Clear();
            foreach (var item in DroneSingleton._droneUnity.BeaconMac)
            {
                _bleMac.Add(item);
            }
        }

        private bool CheckBleMacNotUpdate()
        {
            bool ans = true;
            if (_bleMac.Count == DroneSingleton._droneUnity.BeaconMac.Count)
            {
                for (int i = 0; i < _bleMac.Count; i++)
                {
                    if (!_bleMac[i].Equals(DroneSingleton._droneUnity.BeaconMac[i]))
                    {
                        ans = false;
                        break;
                    }
                }
            }
            else
            {
                ans = false;
            }
            return ans;
        }

        private bool CheckCertification()
        {
            List<String> data = DroneSingleton._droneUnity.BeaconMac;

            bool ans = true;
            if (data.Count != 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (!Commands.IsInBeaconTable(data[i]))
                    {
                        Debug.WriteLine(data[i]);
                        ans = false;
                        break;
                    }
                }
            }
            else
            {
                ans = false;
            }
            return ans;
        }

        private void _planeStateTimer_Tick(object sender, EventArgs e)
        {
            if (DroneSingleton._navigationData != null)
            {
                _planeStatePanel.SetBattery(DroneSingleton._navigationData.Battery.Percentage / 100.0);
                _planeStatePanel.AltitudeText = DroneSingleton._navigationData.Altitude.ToString();
                _planeStatePanel.SetPlaneText("Drone-001");
                _planeStatePanel.IsLearningButtonEnable = true;

                if (!_isPatroling)
                {
                    Debug.WriteLine(DroneSingleton._droneUnity.PirState);
                    if (DroneSingleton._droneUnity.PirState && _pirTrueTime < 5)
                    {
                        _pirTrueTime++;
                        if (_pirTrueTime == 5 && (!_isShowedWarning))
                        {
                            _isShowedWarning = true;
                            StoreBleMac();
                            _isCheckingBle = true;
                            _planeStatePanel.ShowInfoPanel();
                        }
                    }
                    else if (!DroneSingleton._droneUnity.PirState)
                    {
                        _pirTrueTime = 0;
                    }

                    if (_isShowedWarning && _isCheckingBle && (!CheckBleMacNotUpdate()))
                    {
                        _planeStatePanel.DetectedPerson();
                        _planeStatePanel.SetInfoState(CheckCertification());
                        _isCheckingBle = false;
                    }
                }
            }
            else
            {
                _retryConnectionTimes++;
                if (_retryConnectionTimes == 3)
                {
                    _retryConnectionTimes = 0;
                    _planeStateTimer.Enabled = false;
                    _planeStatePanel.NotShowChildPanel = true;
                    MessageBox.Show("Please check connection of drone.", "error");
                }
            }

        }

        private void _videoUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (DroneSingleton._frame == null || DroneSingleton._frameNumber == DroneSingleton._frame.Number)
                return;
            DroneSingleton._frameNumber = DroneSingleton._frame.Number;

            if (DroneSingleton._frameBitmap == null)
                DroneSingleton._frameBitmap = VideoHelper.CreateBitmap(ref DroneSingleton._frame);
            else
                VideoHelper.UpdateBitmap(ref DroneSingleton._frameBitmap, ref DroneSingleton._frame);
            _planeStatePanel._infoControl.RefreshMainImageBackground();

            _input = new Image<Bgr, Byte>(DroneSingleton._frameBitmap);
            Image<Gray, Byte> redTemp = _input.Convert<Ycc, Byte>().InRange(new Ycc(0, 140, 0), new Ycc(125, 255, 150)); //ycrcb red

            _lines = redTemp.HoughLines(
                100,  //Canny algorithm low threshold
                255,  //Canny algorithm high threshold
                1,              //rho parameter
                Math.PI / 180.0,  //theta parameter 
                100,            //threshold
                25,             //min length for a line
                999);            //max allowed gap along the line
            //draw lines on image
            Image<Bgr, Byte> redTemp2 = redTemp.Convert<Bgr, Byte>();

            foreach (var line in _lines[0])
            {
                redTemp2.Draw(line, new Bgr(0, 0, 255), 10);
            }
            if (_lines[0].Length > 0)
            {
                _lastLine.P1 = _lines[0][0].P1;
                _lastLine.P2 = _lines[0][0].P2;
                _haveLastLine = true;
                redTemp2.Draw(_lines[0][0], new Bgr(255, 0, 255), 10);
            }

            if (_controling)
                flyControl();

            float nowHeading;
            NavdataBag navdataBag;
            if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
            {
                nowHeading = navdataBag.magneto.heading_fusion_unwrapped;
                nowHeading = CheckAngle(nowHeading);
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

        private void ReadCommand()
        {
            _isForwarding = false;
            _turning = false;
            _needWait = true;
            _nowCommand = _commandList[_commandIndex];
            if (_nowCommand == LearnForm.State.TakeOff)
            {
                if (_isReturn)
                {
                    DroneSingleton._droneClient.Land();

                    _controling = false;
                    _haveLastLine = false;
                    _nowState = LearnForm.State.Land;
                    _isForwarding = false;
                }
                else
                {
                    NavdataBag navdataBag;
                    if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
                    {
                        _heading = navdataBag.magneto.heading_fusion_unwrapped;
                        _heading = CheckAngle(_heading);
                    }
                    _totalTime = _timeList[_commandIndex];
                }
            }
            else if (_nowCommand == LearnForm.State.Land)
            {
                DroneSingleton._droneClient.Land();

                _controling = false;
                _haveLastLine = false;
                _nowState = LearnForm.State.Land;
                _isForwarding = false;
            }
            else if (_nowCommand == LearnForm.State.Hover)
            {
                _totalTime = _timeList[_commandIndex];
            }
            else if (_nowCommand == LearnForm.State.Forward)
            {
                _totalTime = _timeList[_commandIndex];
            }
            else if (_nowCommand == LearnForm.State.Up || _nowCommand == LearnForm.State.Down)
            {
                _totalTime = _timeList[_commandIndex];
            }
            else
            {
                NavdataBag navdataBag;
                if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
                {
                    _heading = navdataBag.magneto.heading_fusion_unwrapped;
                    _heading = CheckAngle(_heading);
                }
                if (_isReturn)
                    _targetAngle = _angleList[_commandIndex] - 180;
                else
                    _targetAngle = _angleList[_commandIndex];
                _turning = true;
            }
        }

        private void flyControl()
        {
            if (_backHomeWait)
            {
                ChangeState(LearnForm.State.Wait);
                return;
            }
            if (_nowCommand == LearnForm.State.TakeOff)
            {
                ChangeState(LearnForm.State.TakeOff);
            }
            else if (_nowCommand == LearnForm.State.Hover)
            {
                ChangeState(LearnForm.State.Hover);
            }
            else if (_nowCommand == LearnForm.State.Up)
            {
                if (_isReturn)
                    ChangeState(LearnForm.State.Down);
                else
                    ChangeState(LearnForm.State.Up);
            }
            else if (_nowCommand == LearnForm.State.Down)
            {
                if (_isReturn)
                    ChangeState(LearnForm.State.Up);
                else
                    ChangeState(LearnForm.State.Down);
            }
            else
            {
                if (_turnBack)
                {
                    TurnBack();
                }
                else
                {
                    if (_turning && _dependLine)
                    {
                        Turn();
                    }
                    else
                    {
                        if (_dependLine)
                        {
                            if (_lines[0].Length > 0)
                            {
                                if (Math.Abs(_lines[0][0].P1.X - _lines[0][0].P2.X) >= 100)
                                    ControlTurnLeftRight();
                                else
                                    ControlLeftRightForward();
                            }
                            else
                                ControlLeftRightForward();
                        }
                        else
                        {
                            ControlWithRecord();
                        }
                    }
                }
            }
        }

        private void ControlWithRecord()
        {
            if (_nowCommand == LearnForm.State.Forward)
            {
                ChangeState(LearnForm.State.Forward);
            }
            else if (_nowCommand == LearnForm.State.TurnLeft || _nowCommand == LearnForm.State.TurnRight)
            {
                TurnWithRecord();
            }
        }

        private void TurnBack()
        {
            if (_dependLine)
            {
                if (_lines[0].Length > 0)
                {
                    ControlTurnBack();
                }
                else
                {
                    LineSegment2D topline = new LineSegment2D(new Point(0, 500), new Point(0, 600));

                    if (!_haveLastLine)
                    {
                        DroneSingleton._droneClient.Land();
                        return;
                    }
                    topline = _lastLine;

                    int lineCenterX = topline.P1.X;
                    int frameCenterX = DroneSingleton._frame.Width / 2;

                    if (lineCenterX - frameCenterX > 0)
                        ChangeState(LearnForm.State.Right);
                    else if (lineCenterX - frameCenterX < 0)
                        ChangeState(LearnForm.State.Left);
                }
            }
            else
            {
                ControlTurnBack();
            }
        }

        private void ControlTurnBack()
        {
            float nowHeading;
            NavdataBag navdataBag;
            if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
            {
                nowHeading = navdataBag.magneto.heading_fusion_unwrapped;
                nowHeading = CheckAngle(nowHeading);

                if (Math.Abs(nowHeading - _targetAngle) < 10)
                {
                    _turnBack = false;
                    if (_nowCommand == LearnForm.State.Hover || _nowCommand == LearnForm.State.TurnLeft || _nowCommand == LearnForm.State.TurnRight)
                    {
                        _commandIndex--;
                        ReadCommand();
                    }
                }
                else
                {
                    ChangeState(LearnForm.State.TurnLeft);
                }
            }
        }

        private void ControlLeftRightForward()
        {
            LineSegment2D topline = new LineSegment2D(new Point(0, 500), new Point(0, 600));
            if (_lines[0].Length == 0)
            {
                if (!_haveLastLine)
                {
                    DroneSingleton._droneClient.Land();
                    return;
                }
                topline = _lastLine;
            }
            else
            {
                foreach (LineSegment2D line in _lines[0])
                {
                    if (line.P1.Y < topline.P1.Y || line.P1.Y < topline.P2.Y || line.P2.Y < topline.P1.Y || line.P2.Y < topline.P2.Y)
                    {
                        topline.P1 = line.P1;
                        topline.P2 = line.P2;
                    }
                }
            }

            int lineCenterX = topline.P1.X;
            int frameCenterX = DroneSingleton._frame.Width / 2;
            int limit = 300;

            if (_lines[0].Length == 0)
            {
                _needWait = true;
                if (lineCenterX - frameCenterX > 0)
                    ChangeState(LearnForm.State.Right);
                else if (lineCenterX - frameCenterX < 0)
                    ChangeState(LearnForm.State.Left);
            }
            else
            {
                if (Math.Abs(lineCenterX - frameCenterX) <= limit)
                {
                    if (_needWait)
                        ChangeState(LearnForm.State.Wait);
                    else
                        ChangeState(LearnForm.State.Forward);
                }
                else if (lineCenterX - frameCenterX > limit)
                    ChangeState(LearnForm.State.Right);
                else if (lineCenterX - frameCenterX > -limit)
                    ChangeState(LearnForm.State.Left);
            }
        }

        private void ControlTurnLeftRight()
        {

            double m = (_lines[0][0].P1.Y - _lines[0][0].P2.Y) / (_lines[0][0].P1.X - _lines[0][0].P2.X);

            if (m >= 0)
                ChangeState(LearnForm.State.TurnLeft);
            else if (m < 0)
                ChangeState(LearnForm.State.TurnRight);

        }

        private void TurnWithRecord()
        {
            float nowHeading;
            NavdataBag navdataBag;
            if (DroneSingleton._navigationPacket.Data != null && NavdataBagParser.TryParse(ref DroneSingleton._navigationPacket, out navdataBag))
            {
                nowHeading = navdataBag.magneto.heading_fusion_unwrapped;
                nowHeading = CheckAngle(nowHeading);

                if (Math.Abs(nowHeading - _targetAngle) < 10)
                {
                    if (_isReturn)
                        _commandIndex--;
                    else
                        _commandIndex++;
                    ReadCommand();
                    _turning = false;
                }
                else
                {
                    if (_nowCommand == LearnForm.State.TurnLeft)
                    {
                        if (_isReturn)
                            ChangeState(LearnForm.State.TurnRight);
                        else
                            ChangeState(LearnForm.State.TurnLeft);
                    }
                    else if (_nowCommand == LearnForm.State.TurnRight)
                    {
                        if (_isReturn)
                            ChangeState(LearnForm.State.TurnLeft);
                        else
                            ChangeState(LearnForm.State.TurnRight);
                    }
                }
            }
        }

        private void Turn()
        {
            if (_lines[0].Length > 0)
            {
                TurnWithRecord();
            }
            else
            {
                LineSegment2D topline = new LineSegment2D(new Point(0, 500), new Point(0, 600));

                if (!_haveLastLine)
                {
                    DroneSingleton._droneClient.Land();
                    return;
                }
                topline = _lastLine;

                int lineCenterX = topline.P1.X;
                int frameCenterX = DroneSingleton._frame.Width / 2;

                if (lineCenterX - frameCenterX > 0)
                    ChangeState(LearnForm.State.Right);
                else if (lineCenterX - frameCenterX < 0)
                    ChangeState(LearnForm.State.Left);
            }
        }

        private void ChangeState(LearnForm.State state)
        {
            if (_nowState != state)
            {
                switch (state)
                {
                    case LearnForm.State.TakeOff:
                        DroneSingleton._droneClient.Takeoff();
                        _commandStartTime = DateTime.Now;
                        _isCounting = true;
                        break;
                    case LearnForm.State.Hover:
                        DroneSingleton._droneClient.Hover();
                        _commandStartTime = DateTime.Now;
                        _isCounting = true;
                        break;
                    case LearnForm.State.Up:
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
                        _commandStartTime = DateTime.Now;
                        _isCounting = true;
                        break;
                    case LearnForm.State.Down:
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
                        _commandStartTime = DateTime.Now;
                        _isCounting = true;
                        break;
                    case LearnForm.State.Forward:
                        _commandStartTime = DateTime.Now;
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
                        _isForwarding = true;
                        _isCounting = true;
                        break;
                    case LearnForm.State.Left:
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
                        CountTime();
                        break;
                    case LearnForm.State.Right:
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
                        CountTime();
                        break;
                    case LearnForm.State.TurnLeft:
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
                        break;
                    case LearnForm.State.TurnRight:
                        DroneSingleton._droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
                        break;
                    case LearnForm.State.Wait:
                        DroneSingleton._droneClient.Hover();
                        _waitStartTime = DateTime.Now;
                        _waitting = true;
                        break;

                }
                _nowState = state;
            }
            else
            {
                if (_isCounting && _nowCommand == _nowState)
                {
                    _commandEndTime = DateTime.Now;
                    TimeSpan costTime = _commandEndTime.Subtract(_commandStartTime);
                    if (_totalTime.CompareTo(costTime) <= 0)
                    {
                        _isCounting = false;
                        if (_isReturn)
                            _commandIndex--;
                        else
                            _commandIndex++;
                        ReadCommand();
                        return;
                    }
                }
                if (_waitting)
                {
                    _waitEndTime = DateTime.Now;
                    TimeSpan costTime = _waitEndTime.Subtract(_waitStartTime);
                    if (_waitTimeLimit.CompareTo(costTime) <= 0)
                    {
                        if (_backHomeWait)
                        {
                            _isForwarding = false;
                            _turning = false;
                            _needWait = true;
                            _backHomeWait = false;
                            _turnBack = true;
                        }
                        _waitting = false;
                        _needWait = false;
                        return;
                    }
                }
            }
        }

        private void CountTime()
        {
            if (_isForwarding)
            {
                _commandEndTime = DateTime.Now;
                TimeSpan costTime = _commandEndTime.Subtract(_commandStartTime);
                _totalTime = _totalTime.Subtract(costTime);
                if (_totalTime.CompareTo(new TimeSpan(0, 0, 0, 0, 0)) <= 0)
                {
                    if (_isReturn)
                        _commandIndex--;
                    else
                        _commandIndex++;
                    ReadCommand();
                }
            }
            _isForwarding = false;
        }

        private void Return()
        {
            if (_nowCommand == LearnForm.State.Forward)
            {
                if (_isForwarding)
                {
                    _commandEndTime = DateTime.Now;
                    _totalTime = _commandEndTime.Subtract(_commandStartTime);
                }
                else
                {
                    _totalTime = _timeList[_commandIndex].Subtract(_totalTime);
                }
                _targetAngle = _heading - 180;
            }
            else if (_nowCommand == LearnForm.State.Up || _nowCommand == LearnForm.State.Down)
            {
                _commandEndTime = DateTime.Now;
                _totalTime = _commandEndTime.Subtract(_commandStartTime);
                _targetAngle = _heading - 180;
            }
            else
            {
                _targetAngle = _heading - 180;
            }
        }
    }
}
