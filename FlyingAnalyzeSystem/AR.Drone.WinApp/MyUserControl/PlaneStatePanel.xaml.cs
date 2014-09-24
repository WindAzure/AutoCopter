using AR.Drone.WinApp.CommandToServer;
using AR.Drone.WinApp.MyUserControl.MapComboBox;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AR.Drone.WinApp.MyUserControl
{
    /// <summary>
    /// Interaction logic for PlaneStatePanel.xaml
    /// </summary>
    public partial class PlaneStatePanel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private TimeSpan _span;
        private Timer _timer = new Timer();
        private delegate void TimerDispatcherDelegate();

        private ObservableCollection<ImageComboBoxItemProperty> _itemSource = new ObservableCollection<ImageComboBoxItemProperty>();
        public ObservableCollection<ImageComboBoxItemProperty> ComboBoxItemSource
        {
            set
            {
                _itemSource = value;
                OnPropertyChanged("ComboBoxItemSource");
            }
            get
            {
                return _itemSource;
            }
        }

        private String _electricQuantityText = "";
        public String ElectricQuantityText
        {
            set
            {
                _electricQuantityText = value;
                OnPropertyChanged("ElectricQuantityText");
            }
            get
            {
                return _electricQuantityText;
            }
        }

        private String _altitudeText = "";
        public String AltitudeText
        {
            set
            {
                _altitudeText = value;
                OnPropertyChanged("AltitudeText");
            }
            get
            {
                return _altitudeText;
            }
        }

        private String _selectedMapItemMileage = null;
        public String SelectedMapItemMileage
        {
            set
            {
                _selectedMapItemMileage = value;
                OnPropertyChanged("SelectedMapItemMileage");
            }
            get
            {
                return _selectedMapItemMileage;
            }
        }

        private bool _isFirstPlaneItemSelected = false;
        public bool IsFirstPlaneItemSelected
        {
            set
            {
                _isFirstPlaneItemSelected = value;
                OnPropertyChanged("IsFirstPlaneItemSelected");
            }
            get
            {
                return _isFirstPlaneItemSelected;
            }
        }

        public PlaneStatePanel()
        {
            InitializeComponent();
            DataContext = this;
            _patrolPanel.ClickManualButton += OnClickPatrolManualButton;
            _patrolPanel.ClickReturnButton += OnClickPatrolReturnButton;
            _patrolPanel.ClickStopButton += OnClickPatrolStopButton;
            _readyPanel.ClickOkButton += OnClickOkButton;
            _standbyPanel.ClickCancelButton += OnClickCancelButton;
            _comboBox.ImageComboBoxItemSource = ComboBoxItemSource;
            _timer.Interval = 1000;
            _timer.Elapsed += ElapsedTimer;
            LoadImageFromServer();
        }

        private void UpdatePanel()
        {
            _standbyPanel.Visibility = Visibility.Hidden;
            _patrolPanel.Visibility = Visibility.Visible;
        }

        private void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (_span.TotalSeconds == 0)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, new TimerDispatcherDelegate(UpdatePanel));
                _timer.Stop();
                return;
            }
            _span = _span.Subtract(new TimeSpan(0, 0, 1));
            _standbyPanel.TimeText = _span.ToString();
        }

        private void OnClickCancelButton()
        {
            _timer.Stop();
            _standbyPanel.Visibility = Visibility.Hidden;
            _readyPanel.Visibility = Visibility.Visible;
        }

        private void OnClickPatrolStopButton()
        {
            _patrolPanel.Visibility = Visibility.Hidden;
            _readyPanel.Visibility = Visibility.Visible;
            Debug.WriteLine("OnClickPatrolStopButton");
        }

        private void OnClickPatrolReturnButton()
        {
            Debug.WriteLine("OnClickPatrolReturnButton");
        }

        private void OnClickPatrolManualButton()
        {
            Debug.WriteLine("OnClickManualButton");
        }

        private void OnClickOkButton(TimeSpan span)
        {
            if(_mapImage.ImagePath==null)
            {
                MessageBox.Show("Please select map,first.", "Error");
            }
            else if (String.IsNullOrEmpty(SelectedMapItemMileage))
            {
                MessageBox.Show("The map not been learned.","Error");
            }
            else if (!IsFirstPlaneItemSelected)
            {
                MessageBox.Show("Please select plane,first.", "Error");
            }
            else
            {
                DateTime current = DateTime.Now;
                TimeSpan currentSpan = new TimeSpan(current.Hour, current.Minute, current.Second);
                Debug.WriteLine(currentSpan.ToString());
                Debug.WriteLine(span.ToString());
                _span = currentSpan - span;
                _standbyPanel.TimeText = _span.ToString();

                _timer.Start();
                _readyPanel.Visibility = Visibility.Hidden;
                _standbyPanel.Visibility = Visibility.Visible;
            }
        }

        private void LoadImageFromServer()
        {
            DataSet data = Commands.GetFloorInformation();
            int rows = data.Tables[0].Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                byte[] imageBytes = (byte[])data.Tables[0].Rows[i][1];
                MemoryStream stream = new MemoryStream();
                stream.Write(imageBytes, 0, imageBytes.Length);
                stream.Position = 0;
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = stream;
                img.EndInit();
                ComboBoxItemSource.Add(new ImageComboBoxItemProperty() { ItemText = data.Tables[0].Rows[i][0].ToString(), MapImage = img ,Mileage=data.Tables[0].Rows[i][2].ToString()});
            }
        }

        public void OnClickPlaneItemButton(object sender)
        {
            int T = 0;
            IsFirstPlaneItemSelected = false;
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
                        IsFirstPlaneItemSelected = true;
                    }
                }
            }
        }

        public void SetBattery(double rate)
        {
            ElectricQuantityText = ((int)(rate * 100.0)).ToString();
            _battery.Width = _baterySample.Width * rate;
            Color endColor = GetRelativeColor(_batterySampleBrush.GradientStops, rate);
            Color middleColor = GetRelativeColor(_batterySampleBrush.GradientStops, rate / 2.0);

            LinearGradientBrush gradientBrushGroup = new LinearGradientBrush();
            GradientStop start = new GradientStop();
            start.Color = Colors.Red;
            start.Offset = 0;
            gradientBrushGroup.GradientStops.Add(start);

            GradientStop middle = new GradientStop();
            middle.Color = middleColor;
            middle.Offset = 0.5;
            gradientBrushGroup.GradientStops.Add(middle);

            GradientStop end = new GradientStop();
            end.Color = endColor;
            end.Offset = 1;
            gradientBrushGroup.GradientStops.Add(end);

            _battery.Fill = gradientBrushGroup;
        }

        private Color GetRelativeColor(GradientStopCollection gsc, double offset)
        {
            GradientStop before = gsc.Where(w => w.Offset == gsc.Min(m => m.Offset)).First();
            GradientStop after = gsc.Where(w => w.Offset == gsc.Max(m => m.Offset)).First();

            foreach (var gs in gsc)
            {
                if (gs.Offset < offset && gs.Offset > before.Offset)
                {
                    before = gs;
                }
                if (gs.Offset > offset && gs.Offset < after.Offset)
                {
                    after = gs;
                }
            }

            var color = new Color();
            color.ScA = (float)((offset - before.Offset) * (after.Color.ScA - before.Color.ScA) / (after.Offset - before.Offset) + before.Color.ScA);
            color.ScR = (float)((offset - before.Offset) * (after.Color.ScR - before.Color.ScR) / (after.Offset - before.Offset) + before.Color.ScR);
            color.ScG = (float)((offset - before.Offset) * (after.Color.ScG - before.Color.ScG) / (after.Offset - before.Offset) + before.Color.ScG);
            color.ScB = (float)((offset - before.Offset) * (after.Color.ScB - before.Color.ScB) / (after.Offset - before.Offset) + before.Color.ScB);
            return color;
        }

        private void OnClickUploadButton()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "PNG File(.png)|*.png|JPEG File(.jpeg)|*jpeg|JPG File(.jpg)|*jpg";
            Nullable<bool> isFileReaded = dialog.ShowDialog();
            if (isFileReaded == true)
            {
                _descriptionPanel.Visibility = Visibility.Hidden;
                Commands.RegistFloor(dialog.FileName);
                ComboBoxItemSource.Add(new ImageComboBoxItemProperty() { ItemText = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName), MapImage = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute)) });
            }
            /*    OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".png";
                dialog.Filter = "PNG File(.png)|*.png|JPEG File(.jpeg)|*jpeg|JPG File(.jpg)|*jpg";
                Nullable<bool> isFileReaded = dialog.ShowDialog();
                if (isFileReaded==true)
                {
                    _mapImage.Initialize();
                    _mapImage.ImagePath=new BitmapImage(new Uri(dialog.FileName,UriKind.Absolute));
                    Commands.RegistFloor(dialog.FileName);
                }*/
        }

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (0 <= comboBox.SelectedIndex && comboBox.SelectedIndex < ComboBoxItemSource.Count)
            {
                _descriptionPanel.Visibility = Visibility.Hidden;
                _mapImage.ImagePath = ComboBoxItemSource[comboBox.SelectedIndex].MapImage;
                SelectedMapItemMileage = ComboBoxItemSource[comboBox.SelectedIndex].Mileage;
                if (String.IsNullOrEmpty(SelectedMapItemMileage))
                {
                    _mapImageViewConstraint.Visibility = Visibility.Visible;
                }
                else
                {
                    _mapImageViewConstraint.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                _mapImage.ImagePath = null;
                SelectedMapItemMileage = null;
            }
        }

        private void OnClickLearnModeDescription()
        {
            _descriptionPanel.Visibility = Visibility.Hidden;
        }
    }
}
