using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AR.Drone.WinApp.MyUserControl
{
    /// <summary>
    /// Interaction logic for ManualControlPanel.xaml
    /// </summary>
    public partial class ManualControlPanel : UserControl
    {
        public delegate void ManualControlPanelEvent();
        public event ManualControlPanelEvent MouseDownLeftControlButton = null;
        public event ManualControlPanelEvent MouseUpLeftControlButton = null;
        public event ManualControlPanelEvent MouseDownRightControlButton = null;
        public event ManualControlPanelEvent MouseUpRightControlButton = null;
        public event ManualControlPanelEvent MouseDownForwardControlButton = null;
        public event ManualControlPanelEvent MouseUpForwardControlButton = null;
        public event ManualControlPanelEvent MouseDownBackControlButton = null;
        public event ManualControlPanelEvent MouseUpBackControlButton = null;
        public event ManualControlPanelEvent MouseDownUpControlButton = null;
        public event ManualControlPanelEvent MouseUpUpControlButton = null;
        public event ManualControlPanelEvent MouseDownDownControlButton = null;
        public event ManualControlPanelEvent MouseUpDownControlButton = null;
        public event ManualControlPanelEvent MouseDownLeftRotateControlButton = null;
        public event ManualControlPanelEvent MouseUpLeftRotateControlButton = null;
        public event ManualControlPanelEvent MouseDownRightRotateControlButton = null;
        public event ManualControlPanelEvent MouseUpRightRotateControlButton = null;

        private bool _isPressedA = false;
        private bool _isPressedS = false;
        private bool _isPressedW = false;
        private bool _isPressedD = false;
        private bool _isPressedUp = false;
        private bool _isPressedDown = false;
        private bool _isPressedLeft = false;
        private bool _isPressedRight = false;

        public ManualControlPanel()
        {
            InitializeComponent();
            _view.Focus();
        }

        private void KeyDownManualControlPanel(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (!_isPressedA))
            {
                if (_leftControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneLeftControlButton();
                    _isPressedA = true;
                }
            }
            if (e.Key == Key.S && (!_isPressedS))
            {
                if (_backControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneBackControlButton();
                    _isPressedS = true;
                }
            }
            if (e.Key == Key.D && (!_isPressedD))
            {
                if (_rightControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneRightControlButton();
                    _isPressedD = true;
                }
            }
            if (e.Key == Key.W && (!_isPressedW))
            {
                if (_forwardControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneForwardControlButton();
                    _isPressedW = true;
                }
            }
            if (e.Key == Key.Up && (!_isPressedUp))
            {
                if (_upControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneUpControlButton();
                    _isPressedUp = true;
                }
            }
            if (e.Key == Key.Down && (!_isPressedDown))
            {
                if (_downControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneDownControlButton();
                    _isPressedDown = true;
                }
            }
            if (e.Key == Key.Left && (!_isPressedLeft))
            {
                if (_leftRotateControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneLeftRotateControlButton();
                    _isPressedLeft = true;
                }
            }
            if (e.Key == Key.Right && (!_isPressedRight))
            {
                if (_rightRotateControl.SwitchCurrentImageByKey(false))
                {
                    MouseDownPlaneRightRotateControlButton();
                    _isPressedRight = true;
                }
            }
        }

        private void KeyUpManualControlPanel(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                if (_leftControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneLeftControlButton();
                    _isPressedA = false;
                }
            }
            if (e.Key == Key.S)
            {
                if (_backControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneBackControlButton();
                    _isPressedS = false;
                }
            }
            if (e.Key == Key.W)
            {
                if (_forwardControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneForwardControlButton();
                    _isPressedW = false;
                }
            }
            if (e.Key == Key.D)
            {
                if (_rightControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneRightControlButton();
                    _isPressedD = false;
                }
            }
            if (e.Key == Key.Up)
            {
                if (_upControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneUpControlButton();
                    _isPressedUp = false;
                }
            }
            if (e.Key == Key.Down)
            {
                if (_downControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneDownControlButton();
                    _isPressedDown = false;
                }
            }
            if (e.Key == Key.Left)
            {
                if (_leftRotateControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneLeftRotateControlButton();
                    _isPressedLeft = false;
                }
            }
            if (e.Key == Key.Right)
            {
                if (_rightRotateControl.SwitchCurrentImageByKey(true))
                {
                    MouseUpPlaneRightRotateControlButton();
                    _isPressedRight = false;
                }
            }
        }

        private void MouseDownPlaneLeftControlButton()
        {
            if (MouseDownLeftControlButton != null)
            {
                MouseDownLeftControlButton();
            }
        }

        private void MouseUpPlaneLeftControlButton()
        {
            if (MouseUpLeftControlButton != null)
            {
                MouseUpLeftControlButton();
            }
        }

        private void MouseUpPlaneRightControlButton()
        {
            if (MouseUpRightControlButton != null)
            {
                MouseUpRightControlButton();
            }
        }

        private void MouseDownPlaneRightControlButton()
        {
            if (MouseDownRightControlButton != null)
            {
                MouseDownRightControlButton();
            }
        }

        private void MouseDownPlaneBackControlButton()
        {
            if (MouseDownBackControlButton != null)
            {
                MouseDownBackControlButton();
            }
        }

        private void MouseUpPlaneBackControlButton()
        {
            if (MouseUpBackControlButton != null)
            {
                MouseUpBackControlButton();
            }
        }

        private void MouseDownPlaneForwardControlButton()
        {
            if (MouseDownForwardControlButton != null)
            {
                MouseDownForwardControlButton();
            }
        }

        private void MouseUpPlaneForwardControlButton()
        {
            if (MouseUpForwardControlButton != null)
            {
                MouseUpForwardControlButton();
            }
        }

        private void MouseDownPlaneLeftRotateControlButton()
        {
            if (MouseDownLeftRotateControlButton != null)
            {
                MouseDownLeftRotateControlButton();
            }
        }

        private void MouseUpPlaneLeftRotateControlButton()
        {
            if (MouseUpLeftRotateControlButton != null)
            {
                MouseUpLeftRotateControlButton();
            }
        }

        private void MouseDownPlaneRightRotateControlButton()
        {
            if (MouseDownRightRotateControlButton != null)
            {
                MouseDownRightRotateControlButton();
            }
        }

        private void MouseUpPlaneRightRotateControlButton()
        {
            if (MouseUpRightRotateControlButton != null)
            {
                MouseUpRightRotateControlButton();
            }
        }

        private void MouseDownPlaneDownControlButton()
        {
            if (MouseDownDownControlButton != null)
            {
                MouseDownDownControlButton();
            }
        }

        private void MouseUpPlaneDownControlButton()
        {
            if (MouseUpDownControlButton != null)
            {
                MouseUpDownControlButton();
            }
        }

        private void MouseDownPlaneUpControlButton()
        {
            if (MouseDownUpControlButton != null)
            {
                MouseDownUpControlButton();
            }
        }

        private void MouseUpPlaneUpControlButton()
        {
            if (MouseUpUpControlButton != null)
            {
                MouseUpUpControlButton();
            }
        }
    }
}
