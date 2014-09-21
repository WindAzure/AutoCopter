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
    /// Interaction logic for LearnPanel.xaml
    /// </summary>
    public partial class LearnPanel : UserControl
    {
        public LearnPanel()
        {
            InitializeComponent();
        }

        private void MouseDownPlaneLeftControlButton()
        {
            Debug.WriteLine("MouseDownPlaneLeftControlButton");
        }

        private void MouseUpPlaneLeftControlButton()
        {
            Debug.WriteLine("MouseUpPlaneLeftControlButton");
        }

        private void MouseDownPlaneRightControlButton()
        {
            Debug.WriteLine("MouseDownPlaneRightControlButton");
        }

        private void MouseUpPlaneRightControlButton()
        {

            Debug.WriteLine("MouseUpPlaneRightControlButton");
        }

        private void MouseDownPlaneForwardControlButton()
        {
            Debug.WriteLine("MouseDownPlaneForwardControlButton");
        }

        private void MouseUpPlaneForwardControlButton()
        {
            Debug.WriteLine("MouseUpPlaneForwardControlButton");
        }

        private void MouseDownPlaneUpControlButton()
        {
            Debug.WriteLine("MouseDownPlaneUpControlButton");
        }

        private void MouseUpPlaneUpControlButton()
        {
            Debug.WriteLine("MouseUpPlaneUpControlButton");
        }

        private void MouseDownPlaneDownControlButton()
        {
            Debug.WriteLine("MouseDownPlaneDownControlButton");
        }

        private void MouseUpPlaneDownControlButton()
        {
            Debug.WriteLine("MouseUpPlaneDownControlButton");
        }
    }
}
