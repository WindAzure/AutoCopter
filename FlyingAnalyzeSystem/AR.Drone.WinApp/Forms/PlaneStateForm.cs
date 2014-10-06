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
        private int _retryConnectionTimes = 0;

        public PlaneStateForm()
        {
            InitializeComponent();

            _planeStatePanel.ClickPatrolReturnHomeButton += OnClickPlaneStatePanelReturnHomeButton;
            _planeStatePanel.ClickPatrolManualControlButton += OnClickPlaneStatePanelManualControlButton;
            _planeStatePanel.ClickStartLearnButton += OnClickPlaneStatePanelStartLearnButton;
            _planeStatePanel.StartAutoPatrol += OnPlaneStatePanelStartAutoPatrol;
            DroneSingleton.InitializeDrone();
            _planeStateTimer.Enabled = true;
            _videoUpdateTimer.Enabled = true;
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
            Debug.WriteLine("!!");
        }

        public void OnClickPlaneStatePanelManualControlButton()
        {
            SwitchForm(new ManualForm(this));
        }

        public void OnClickPlaneStatePanelReturnHomeButton()
        {
            Debug.WriteLine("OnClickPlaneStatePanelReturnHomeButton");
        }

        private void _planeStateTimer_Tick(object sender, EventArgs e)
        {
            if (DroneSingleton._navigationData != null)
            {
                _planeStatePanel.SetBattery(DroneSingleton._navigationData.Battery.Percentage / 100.0);
                _planeStatePanel.AltitudeText = DroneSingleton._navigationData.Altitude.ToString();
                _planeStatePanel.SetPlaneText("Drone-001");
                Debug.WriteLine(DroneSingleton._droneUnity.PirState);
                List<String> data = DroneSingleton._droneUnity.BeaconMac;
                foreach (var item in data)
                {
                    Debug.WriteLine(item);
                }
                int[] dis = DroneSingleton._droneUnity.UltraSonicDistance;
                for (int i = 0; i < 5; i++)
                    Debug.WriteLine(dis[i]);
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
