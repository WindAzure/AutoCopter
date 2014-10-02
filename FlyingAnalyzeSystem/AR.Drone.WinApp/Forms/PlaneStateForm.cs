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
        public PlaneStateForm()
        {
            InitializeComponent();

            _planeStatePanel.ClickPatrolReturnHomeButton += OnClickPlaneStatePanelReturnHomeButton;
            _planeStatePanel.ClickPatrolManualControlButton += OnClickPlaneStatePanelManualControlButton;
            _planeStatePanel.ClickStartLearnButton += OnClickPlaneStatePanelStartLearnButton;
            _planeStatePanel.StartAutoPatrol += OnPlaneStatePanelStartAutoPatrol;
            DroneSingleton.InitializeDrone();
            _planeStateTimer.Enabled = true;
            
        }
        protected override void OnClosed(EventArgs e)
        {
            DroneSingleton._droneClient.Dispose();
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

        public void OnClickPlaneStatePanelStartLearnButton()
        {
            SwitchForm(new LearnForm(this));
        }

        public void OnPlaneStatePanelStartAutoPatrol(string mileage)
        {
            Debug.WriteLine(mileage);
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
            _planeStatePanel.SetBattery(DroneSingleton._navigationData.Battery.Percentage/100.0);
            _planeStatePanel.AltitudeText = DroneSingleton._navigationData.Altitude.ToString();
        }
    }
}
