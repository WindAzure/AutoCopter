using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ManualButton.xaml
    /// </summary>
    public partial class ManualButton : UserControl, INotifyPropertyChanged
    {
        private bool _iskeyUsing = false;
        private bool _isKeyDown = false;
        private bool _isMouseUsing = false;
        private bool _isMouseDown = false;

        public delegate void PlaneControlButtonEvent();
        public event PlaneControlButtonEvent OnButtonMouseDown = null;
        public event PlaneControlButtonEvent OnButtonMouseUp = null;
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private BitmapImage _normalImage;
        private String _normalImagePath;
        public String NormalImagePath
        {
            set
            {
                _normalImagePath = value;
                _normalImage = new BitmapImage(new Uri(_normalImagePath, UriKind.Relative));
            }
            get
            {
                return _normalImagePath;
            }
        }

        private BitmapImage _downImage;
        private String _downImagePath;
        public String DownImagePath
        {
            set
            {
                _downImagePath = value;
                _downImage = new BitmapImage(new Uri(_downImagePath, UriKind.Relative));
            }
            get
            {
                return _downImagePath;
            }
        }

        private BitmapImage _currentSource;
        public BitmapImage CurrentSource
        {
            set
            {
                _currentSource = value;
                OnPropertyChanged("CurrentSource");
            }
            get
            {
                return _currentSource;
            }
        }

        public ManualButton()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += ManualButton_Loaded;
        }

        private void ManualButton_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentSource = _normalImage;
        }

        public bool SwitchCurrentImageByKey(bool state)
        {
            if (_isMouseUsing || (this.IsEnabled==false))
            {
                return false;
            }

            if (state==false)
            {
                _iskeyUsing = true;
                _isKeyDown = true;
                CurrentSource = _downImage;
                return true;
            }
            else
            {
                if (_isKeyDown)
                {
                    CurrentSource = _normalImage;
                    _isKeyDown = false;
                    _iskeyUsing = false;
                    return true;
                }
            }
            return false;
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            if (_iskeyUsing)
            {
                return;
            }

            _isMouseUsing = true;
            _isMouseDown = true;
            CurrentSource = _downImage;
            if (OnButtonMouseDown != null)
            {
                OnButtonMouseDown();
            }
        }

        private void MouseLeftButtonUpImage(object sender, MouseButtonEventArgs e)
        {
            if (_iskeyUsing)
            {
                return;
            }

            if (_isMouseDown)
            {
                CurrentSource = _normalImage;
                if (OnButtonMouseUp != null)
                {
                    OnButtonMouseUp();
                }
                _isMouseDown = false;
                _isMouseUsing = false;
            }
        }

        private void MouseLeaveImage(object sender, MouseEventArgs e)
        {
            if (_iskeyUsing)
            {
                return;
            }

            if (_isMouseDown)
            {
                CurrentSource = _normalImage;
                if (OnButtonMouseUp != null)
                {
                    OnButtonMouseUp();
                }
                _isMouseDown = false;
                _isMouseUsing = false;
            }
        }
    }
}
