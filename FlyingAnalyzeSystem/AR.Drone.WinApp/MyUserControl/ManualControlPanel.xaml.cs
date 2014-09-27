using System;
using System.Collections.Generic;
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

        public ManualControlPanel()
        {
            InitializeComponent();
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
