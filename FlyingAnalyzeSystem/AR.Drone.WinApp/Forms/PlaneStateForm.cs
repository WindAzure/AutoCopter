using AR.Drone.WinApp.CommandToServer;
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
        private List<String> _bleMac=new List<string>();

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
        }

        public void OnClickPlaneStatePanelManualControlButton()
        {
            SwitchForm(new ManualForm(this));
        }

        public void OnClickPlaneStatePanelReturnHomeButton()
        {
            Debug.WriteLine("OnClickPlaneStatePanelReturnHomeButton");
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
        }
    }
}
