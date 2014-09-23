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

namespace AR.Drone.WinApp.MyUserControl.PlaneStateChildPanel
{
    /// <summary>
    /// Interaction logic for StandbyPanel.xaml
    /// </summary>
    public partial class StandbyPanel : UserControl
    {
        public delegate void StanbyPanelEvent();
        public StanbyPanelEvent ClickCancelButton = null;

        public StandbyPanel()
        {
            InitializeComponent();
        }

        private void OnClickCancelButton()
        {
            if (ClickCancelButton != null)
            {
                ClickCancelButton();
            }
        }
    }
}
