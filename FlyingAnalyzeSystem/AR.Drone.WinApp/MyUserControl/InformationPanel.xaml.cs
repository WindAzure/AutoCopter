using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for InformationPanel.xaml
    /// </summary>
    public partial class InformationPanel : UserControl
    {
        public delegate void InformationPanelEvent();
        public event InformationPanelEvent ClickNoButton = null;
        public event InformationPanelEvent ClickYesButton = null;
        public event InformationPanelEvent ClickSendButton = null;

        public InformationPanel()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            _progressBar.Visibility = Visibility.Visible;
            _okState.Visibility = Visibility.Hidden;
            _noState.Visibility = Visibility.Hidden;
        }

        public void SetCheckResultState(bool state)
        {
            _progressBar.Visibility = Visibility.Hidden;
            if (state)
            {
                _okState.Visibility = Visibility.Visible;
            }
            else
            {
                _noState.Visibility = Visibility.Visible;
            }
        }

        private void OnClickYesButton()
        {
            if (ClickYesButton != null)
            {
                ClickYesButton();
            }
        }

        private void OnClickNoButton()
        {
            if (ClickNoButton != null)
            {
                ClickNoButton();
            }
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        public void RefreshMainImageBackground()
        {
            _mainImage.Source = loadBitmap(DroneSingleton._frameBitmap);
        }

        private void OnClickSendButton()
        {
            if (ClickSendButton != null)
            {
                ClickSendButton();
            }
        }
    }
}
