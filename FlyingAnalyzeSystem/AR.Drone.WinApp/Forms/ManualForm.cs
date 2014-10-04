using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace AR.Drone.WinApp.Forms
{
    public partial class ManualForm : Form
    {
        private bool _isLeft = false;
        private bool _isRight = false;
        private bool _isForward = false;
        private bool _isBackward = false;
        private bool _isBack = false;
        private PlaneStateForm _lastForm;

        public ManualForm(PlaneStateForm form)
        {
            InitializeComponent();
            _lastForm = form;
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

            _videoUpdateTimer.Enabled = true;
            _stateUpdateTimer.Enabled = true;
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
            _isForward = false;
            if (_isLeft)
            {
                OnMouseDownManualControlPanelLeftControlButton();
            }
            else if (_isRight)
            {
                OnMouseDownManualControlPanelRightControlButton();
            }
            else
            {
                Debug.WriteLine("OnMouseUpManualControlPanelForwardControlButton");
            }
        }

        public void OnMouseDownManualControlPanelForwardControlButton()
        {
            _isForward = true;
            if (_isLeft)
            {
                OnLeftForward();
            }
            else if (_isRight)
            {
                OnRightForward();
            }
            else
            {
                Debug.WriteLine("OnMouseDownManualControlPanelForwardControlButton");
            }
        }

        public void OnMouseUpManualControlPanelBackControlButton()
        {
            _isBackward = false;
            if (_isLeft)
            {
                OnMouseDownManualControlPanelLeftControlButton();
            }
            else if (_isRight)
            {
                OnMouseDownManualControlPanelRightControlButton();
            }
            else
            {
                Debug.WriteLine("OnMouseUpManualControlPanelBackControlButton");
            }
        }

        public void OnMouseDownManualControlPanelBackControlButton()
        {
            _isBackward = true;
            if (_isLeft)
            {
                OnLeftBackward();
            }
            else if (_isRight)
            {
                OnRightBackward();
            }
            else
            {
                Debug.WriteLine("OnMouseDownManualControlPanelBackControlButton");
            }
        }

        public void OnMouseDownManualControlPanelLeftControlButton()
        {
            _isLeft = true;
            if (_isBackward)
            {
                OnLeftBackward();
            }
            else if (_isForward)
            {
                OnLeftForward();
            }
            else
            {
                Debug.WriteLine("OnMouseDownManualControlPanelLeftControlButton");
            }
        }

        public void OnMouseUpManualControlPanelLeftControlButton()
        {
            _isLeft = false;
            if (_isForward)
            {
                OnMouseDownManualControlPanelForwardControlButton();
            }
            else if (_isBackward)
            {
                OnMouseDownManualControlPanelBackControlButton();
            }
            else
            {
                Debug.WriteLine("OnMouseUpManualControlPanelLeftControlButton");
            }
        }

        public void OnMouseDownManualControlPanelRightControlButton()
        {
            _isRight = true;
            if (_isBackward)
            {
                OnRightBackward();
            }
            else if (_isForward)
            {
                OnRightForward();
            }
            else
            {
                Debug.WriteLine("OnMouseDownManualControlPanelRightControlButton");
            }
        }

        public void OnMouseUpManualControlPanelRightControlButton()
        {
            _isRight = false;
            if (_isForward)
            {
                OnMouseDownManualControlPanelForwardControlButton();
            }
            else if (_isBackward)
            {
                OnMouseDownManualControlPanelBackControlButton();
            }
            else
            {
                Debug.WriteLine("OnMouseUpManualControlPanelRightControlButton");
            }
        }

        public void OnLeftForward()
        {
            Debug.WriteLine("OnLeftForward");
        }

        public void OnRightForward()
        {
            Debug.WriteLine("OnRightForward");
        }

        public void OnLeftBackward()
        {
            Debug.WriteLine("OnLeftBackward");
        }

        public void OnRightBackward()
        {
            Debug.WriteLine("OnRightBackward");
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

        private void _videoUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (DroneSingleton._frame == null || DroneSingleton._frameNumber == DroneSingleton._frame.Number)
                return;
            DroneSingleton._frameNumber = DroneSingleton._frame.Number;

            if (DroneSingleton._frameBitmap == null)
                DroneSingleton._frameBitmap = VideoHelper.CreateBitmap(ref DroneSingleton._frame);
            else
                VideoHelper.UpdateBitmap(ref DroneSingleton._frameBitmap, ref DroneSingleton._frame);

            _manualControlPanel.RefreshMainImageBackground();
        }

        private void _stateUpdateTimer_Tick(object sender, EventArgs e)
        {
            _manualControlPanel.SetBattery(DroneSingleton._navigationData.Battery.Percentage / 100.0);
        }
    }
}
