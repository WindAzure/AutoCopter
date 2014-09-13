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
    /// Interaction logic for PlaneStatePanel.xaml
    /// </summary>
    public partial class PlaneStatePanel : UserControl
    {
        public PlaneStatePanel()
        {
            InitializeComponent();
        }

        public void OnClickForwardButton()
        {
            Debug.WriteLine("OnClickForwardButton");
        }

        public void OnClickStopButton()
        {
            Debug.WriteLine("OnClickStopButton");
        }

        public void OnClickRightButton()
        {
            Debug.WriteLine("OnClickRightButton");
        }

        public void OnClickLeftButton()
        {
            Debug.WriteLine("OnClickLeftButton");
        }

        public void OnClickPlaneItemButton(object sender)
        {
            int T = 0;
            bool isFirst = false;
            UIElementCollection group = _planeItemsStackPanel.Children;
            foreach (PlaneItemButton item in group)
            {
                T++;
                if (item != sender)
                {
                    item.InitializeToNormalImage();
                }
                else
                {
                    if (T == 1)
                    {
                        isFirst = true;
                    }
                }
            }
            Debug.WriteLine(isFirst.ToString());
        }
    }
}
