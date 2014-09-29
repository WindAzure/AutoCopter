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
    public partial class ManualForm : Form
    {
        private bool _isBack = false;
        private PlaneStateForm _lastForm;

        public ManualForm(PlaneStateForm form)
        {
            InitializeComponent();
            _lastForm = form;
            _elementHost.Parent = _pictureBox;
            _manualControlPanel.MouseDownLeftControlButton += OnMouseDownManualControlPanelLeftControlButton;
            _manualControlPanel.MouseUpLeftControlButton += OnMouseUpManualControlPanelLeftControlButton;
            _manualControlPanel.MouseDownRightControlButton += OnMouseDownManualControlPanelRightControlButton;
            _manualControlPanel.MouseUpRightControlButton += OnMouseUpManualControlPanelRightControlButton;
            _manualControlPanel.MouseDownBackControlButton += OnMouseDownManualControlPanelBackControlButton;
            _manualControlPanel.MouseUpBackControlButton += OnMouseUpManualControlPanelBackControlButton;
            _manualControlPanel.MouseDownForwardControlButton += OnMouseDownManualControlPanelForwardControlButton;
            _manualControlPanel.MouseUpForwardControlButton += OnMouseUpManualControlPanelForwardControlButton;
            _manualControlPanel.MouseDownLeftRotateControlButton += OnMouseDownManualControlPanelLeftRotateControlButton;
            _manualControlPanel.MouseUpLeftRotateControlButton += OnMouseUpManualControlPanelLeftRotateControlButton;
            _manualControlPanel.MouseDownRightRotateControlButton += OnMouseDownManualControlPanelRightRotateControlButton;
            _manualControlPanel.MouseUpRightRotateControlButton += OnMouseUpManualControlPanelRightRotateControlButton;
            _manualControlPanel.MouseDownDownControlButton += OnMouseDownManualControlPanelDownControlButton;
            _manualControlPanel.MouseUpDownControlButton += OnMouseUpManualControlPanelDownControlButton;
            _manualControlPanel.MouseDownUpControlButton += OnMouseDownManualControlPanelUpControlButton;
            _manualControlPanel.MouseUpUpControlButton += OnMouseUpManualControlPanelUpControlButton;
            _manualControlPanel.ClickBackButton += ClickManualControlPanelBackButton;
        }

        public void ClickManualControlPanelBackButton()
        {
            _isBack = true;
            this.Close();
        }

        public void OnMouseUpManualControlPanelUpControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelUpControlButton");
        }

        public void OnMouseDownManualControlPanelUpControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelUpControlButton");
        }

        public void OnMouseUpManualControlPanelDownControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelDownControlButton");
        }

        public void OnMouseDownManualControlPanelDownControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelDownControlButton");
        }

        public void OnMouseUpManualControlPanelRightRotateControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelRightRotateControlButton");
        }

        public void OnMouseDownManualControlPanelRightRotateControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelRightRotateControlButton");
        }

        public void OnMouseUpManualControlPanelLeftRotateControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelLeftRotateControlButton");
        }

        public void OnMouseDownManualControlPanelLeftRotateControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelLeftRotateControlButton");
        }

        public void OnMouseUpManualControlPanelForwardControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelForwardControlButton");
        }

        public void OnMouseDownManualControlPanelForwardControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelForwardControlButton");
        }

        public void OnMouseUpManualControlPanelBackControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelBackControlButton");
        }

        public void OnMouseDownManualControlPanelBackControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelBackControlButton");
        }

        public void OnMouseDownManualControlPanelLeftControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelLeftControlButton");
        }

        public void OnMouseUpManualControlPanelLeftControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelLeftControlButton");
        }

        public void OnMouseDownManualControlPanelRightControlButton()
        {
            Debug.WriteLine("OnMouseDownManualControlPanelRightControlButton");
        }

        public void OnMouseUpManualControlPanelRightControlButton()
        {
            Debug.WriteLine("OnMouseUpManualControlPanelRightControlButton");
        }

        private void ManualForm_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
