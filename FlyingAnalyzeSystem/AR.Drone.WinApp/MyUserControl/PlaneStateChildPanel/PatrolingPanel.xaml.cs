using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AR.Drone.WinApp.MyUserControl.PlaneStateChildPanel
{
    /// <summary>
    /// Interaction logic for PatrolingPanel.xaml
    /// </summary>
    public partial class PatrolingPanel : UserControl
    {
        public delegate void PatrolingPanelEvent();
        public PatrolingPanelEvent ClickManualButton = null;
        public PatrolingPanelEvent ClickStopButton = null;
        public PatrolingPanelEvent ClickReturnButton = null;
        
        public PatrolingPanel()
        {
            InitializeComponent();
        }

        private void OnClickManualButton()
        {
            if (ClickManualButton != null)
            {
                ClickManualButton();
            }
        }

        private void OnClickStopButton()
        {
            if (ClickStopButton != null)
            {
                ClickStopButton();
            }
        }

        private void OnClickReturnButton()
        {
            if (ClickReturnButton != null)
            {
                ClickReturnButton();
            }
        }
    }
}
