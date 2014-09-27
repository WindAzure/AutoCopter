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
        }

        public void OnClickPlaneStatePanelStartLearnButton()
        {
            LearnForm form = new LearnForm(this);
            form.StartPosition = FormStartPosition.Manual;
            form.Location = this.Location;
            form.WindowState = this.WindowState;
            form.Width = this.Width;
            form.Height = this.Height;
            form.Show();
            this.Hide();
        }

        public void OnPlaneStatePanelStartAutoPatrol(string mileage)
        {
            Debug.WriteLine(mileage);
        }

        public void OnClickPlaneStatePanelManualControlButton()
        {
            Debug.WriteLine("OnClickPlaneStatePanelManualControlButton");
        }

        public void OnClickPlaneStatePanelReturnHomeButton()
        {
            Debug.WriteLine("OnClickPlaneStatePanelReturnHomeButton");
        }
    }
}
