using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace AR.Drone.WinApp.MyUserControl
{
    /// <summary>
    /// Interaction logic for ManualControlPanel.xaml
    /// </summary>
    public partial class ManualControlPanel : UserControl, INotifyPropertyChanged
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
        public event ManualControlPanelEvent ClickBackButton = null;
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _isPressedA = false;
        private bool _isPressedS = false;
        private bool _isPressedW = false;
        private bool _isPressedD = false;
        private bool _isPressedUp = false;
        private bool _isPressedDown = false;
        private bool _isPressedLeft = false;
        private bool _isPressedRight = false;

        private String _electricityText;
        public String EletricityText
        {
            set
            {
                _electricityText = value;
                OnPropertyChanged("EletricityText");
            }
            get
            {
                return _electricityText;
            }
        }

        private String _ultraUpText;
        public String UltraUpText
        {
            set
            {
                String temp = value;
                _ultraUpText = temp + " cm";
                OnPropertyChanged("UltraUpText");
            }
            get
            {
                return _ultraUpText;
            }
        }

        private String _ultraDownText;
        public String UltraDownText
        {
            set
            {
                String temp = value;
                _ultraDownText = temp + " cm";
                OnPropertyChanged("UltraDownText");
            }
            get
            {
                return _ultraDownText;
            }
        }

        private String _ultraLeftText;
        public String UltraLeftText
        {
            set
            {
                String temp = value;
                _ultraLeftText = temp + " cm";
                OnPropertyChanged("UltraLeftText");
            }
            get
            {
                return _ultraLeftText;
            }
        }

        private String _ultraRightText;
        public String UltraRightText
        {
            set
            {
                String temp = value;
                _ultraRightText = temp + " cm";
                OnPropertyChanged("UltraRightText");
            }
            get
            {
                return _ultraRightText;
            }
        }

        private String _ultraForwardText;
        public String UltraForwardText
        {
            set
            {
                String temp = value;
                _ultraForwardText = temp + " cm";
                OnPropertyChanged("UltraForwardText");
            }
            get
            {
                return _ultraForwardText;
            }
        }

        private String _ultraBackText;
        public String UltraBackText
        {
            set
            {
                String temp = value;
                _ultraBackText = temp + " cm";
                OnPropertyChanged("UltraBackText");
            }
            get
            {
                return _ultraBackText;
            }
        }

        public ManualControlPanel()
        {
            InitializeComponent();
            DataContext = this;
            _view.Focus();
        }

        public void SetBattery(double rate)
        {
            int value = (int)(rate * 100.0);
            EletricityText = value.ToString() + "%";
            _electircityShape.Rect = new System.Windows.Rect(0, 0, 100, 40 - 40 * rate);
        }

        private void KeyDownManualControlPanel(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (!_isPressedA) && (!_isPressedD))
            {
                if (_leftControl.SwitchCurrentImageByKey(false))
                {
                    _isPressedA = true;
                    MouseDownPlaneLeftControlButton();
                }
            }
            if (e.Key == Key.S && (!_isPressedS) && (!_isPressedW))
            {
                if (_backControl.SwitchCurrentImageByKey(false))
                {
                    _isPressedS = true;
                    MouseDownPlaneBackControlButton();
                }
            }
            if (e.Key == Key.D && (!_isPressedD) && (!_isPressedA))
            {
                if (_rightControl.SwitchCurrentImageByKey(false))
                {
                    _isPressedD = true;
                    MouseDownPlaneRightControlButton();
                }
            }
            if (e.Key == Key.W && (!_isPressedW) && (!_isPressedS))
            {
                if (_forwardControl.SwitchCurrentImageByKey(false))
                {
                    _isPressedW = true;
                    MouseDownPlaneForwardControlButton();
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
            if (e.Key == Key.Escape)
            {
                DroneSingleton._droneClient.Emergency();
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
                DisableRightPartButtonState();
                MouseDownLeftControlButton();
            }
        }

        private void MouseUpPlaneLeftControlButton()
        {
            if (MouseUpLeftControlButton != null)
            {
                if ((!_isPressedW) && (!_isPressedS))
                {
                    InitializeControlButtonState();
                }
                MouseUpLeftControlButton();
            }
        }

        private void MouseUpPlaneRightControlButton()
        {
            if (MouseUpRightControlButton != null)
            {
                if ((!_isPressedW) && (!_isPressedS))
                {
                    InitializeControlButtonState();
                }
                MouseUpRightControlButton();
            }
        }

        private void MouseDownPlaneRightControlButton()
        {
            if (MouseDownRightControlButton != null)
            {
                DisableRightPartButtonState();
                MouseDownRightControlButton();
            }
        }

        private void MouseDownPlaneBackControlButton()
        {
            if (MouseDownBackControlButton != null)
            {
                DisableRightPartButtonState();
                MouseDownBackControlButton();
            }
        }

        private void MouseUpPlaneBackControlButton()
        {
            if (MouseUpBackControlButton != null)
            {
                if ((!_isPressedA) && (!_isPressedD))
                {
                    InitializeControlButtonState();
                }
                MouseUpBackControlButton();
            }
        }

        private void MouseDownPlaneForwardControlButton()
        {
            if (MouseDownForwardControlButton != null)
            {
                DisableRightPartButtonState();
                MouseDownForwardControlButton();
            }
        }

        private void MouseUpPlaneForwardControlButton()
        {
            if (MouseUpForwardControlButton != null)
            {
                if ((!_isPressedA) && (!_isPressedD))
                {
                    InitializeControlButtonState();
                }
                MouseUpForwardControlButton();
            }
        }

        private void MouseDownPlaneLeftRotateControlButton()
        {
            if (MouseDownLeftRotateControlButton != null)
            {
                DisableControlButtonState();
                _leftRotateControl.IsEnabled = true;
                MouseDownLeftRotateControlButton();
            }
        }

        private void MouseUpPlaneLeftRotateControlButton()
        {
            if (MouseUpLeftRotateControlButton != null)
            {
                InitializeControlButtonState();
                MouseUpLeftRotateControlButton();
            }
        }

        private void MouseDownPlaneRightRotateControlButton()
        {
            if (MouseDownRightRotateControlButton != null)
            {
                DisableControlButtonState();
                _rightRotateControl.IsEnabled = true;
                MouseDownRightRotateControlButton();
            }
        }

        private void MouseUpPlaneRightRotateControlButton()
        {
            if (MouseUpRightRotateControlButton != null)
            {
                InitializeControlButtonState();
                MouseUpRightRotateControlButton();
            }
        }

        private void MouseDownPlaneDownControlButton()
        {
            if (MouseDownDownControlButton != null)
            {
                DisableControlButtonState();
                _downControl.IsEnabled = true;
                MouseDownDownControlButton();
            }
        }

        private void MouseUpPlaneDownControlButton()
        {
            if (MouseUpDownControlButton != null)
            {
                InitializeControlButtonState();
                MouseUpDownControlButton();
            }
        }

        private void MouseDownPlaneUpControlButton()
        {
            if (MouseDownUpControlButton != null)
            {
                DisableControlButtonState();
                _upControl.IsEnabled = true;
                MouseDownUpControlButton();
            }
        }

        private void MouseUpPlaneUpControlButton()
        {
            if (MouseUpUpControlButton != null)
            {
                InitializeControlButtonState();
                MouseUpUpControlButton();
            }
        }

        private void OnClickBackButton()
        {
            if (ClickBackButton != null)
            {
                ClickBackButton();
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
            _mainImage.ImageSource = loadBitmap(DroneSingleton._frameBitmap);
        }

        private void InitializeControlButtonState()
        {
            _forwardControl.IsEnabled = true;
            _backControl.IsEnabled = true;
            _leftControl.IsEnabled = true;
            _rightControl.IsEnabled = true;
            _upControl.IsEnabled = true;
            _downControl.IsEnabled = true;
            _leftRotateControl.IsEnabled = true;
            _rightRotateControl.IsEnabled = true;
        }
        private void DisableControlButtonState()
        {
            _forwardControl.IsEnabled = false;
            _backControl.IsEnabled = false;
            _leftControl.IsEnabled = false;
            _rightControl.IsEnabled = false;
            _upControl.IsEnabled = false;
            _downControl.IsEnabled = false;
            _leftRotateControl.IsEnabled = false;
            _rightRotateControl.IsEnabled = false;
        }
        private void DisableRightPartButtonState()
        {
            _upControl.IsEnabled = false;
            _downControl.IsEnabled = false;
            _leftRotateControl.IsEnabled = false;
            _rightRotateControl.IsEnabled = false;
        }
    }
}
