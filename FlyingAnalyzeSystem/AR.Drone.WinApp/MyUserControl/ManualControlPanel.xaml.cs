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
        public event ManualControlPanelEvent MouseDownUpControlButton = null;
        public event ManualControlPanelEvent MouseUpUpControlButton = null;
        public event ManualControlPanelEvent MouseDownDownControlButton = null;
        public event ManualControlPanelEvent MouseUpDownControlButton = null;

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
    }
}
