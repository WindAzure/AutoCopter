using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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
            _planeStatePanel.StartAutoPatrol += OnPlaneStatePanelStartAutoPatrol;
        }

        public void OnPlaneStatePanelStartAutoPatrol(string mileage)
        {
            Debug.WriteLine(mileage);
        }

        void OnClickPlaneStatePanelManualControlButton()
        {
            Debug.WriteLine(_planeStatePanel._comboBox._imageComboBox.SelectedValue);
        }

        public void OnClickPlaneStatePanelReturnHomeButton()
        {
            Debug.WriteLine("OnClickPlaneStatePanelReturnHomeButton");
        }
    }
}
