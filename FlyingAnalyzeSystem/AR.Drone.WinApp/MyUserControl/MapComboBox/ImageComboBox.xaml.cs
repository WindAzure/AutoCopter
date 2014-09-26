using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AR.Drone.WinApp.MyUserControl.MapComboBox
{
    /// <summary>
    /// Interaction logic for ImageComboBox.xaml
    /// </summary>
    public partial class ImageComboBox : UserControl, INotifyPropertyChanged
    {
        public delegate void ImageComboBoxRemoveImageOnClickEvent();
        public delegate void ImageComboBoxSelectionChangedEvent(object sender, SelectionChangedEventArgs e);
        public event ImageComboBoxRemoveImageOnClickEvent OnRemoveImageClick = null;
        public event ImageComboBoxSelectionChangedEvent SelectionChanged = null;
        public event PropertyChangedEventHandler PropertyChanged = null;

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ComboBoxItem _highLightedItem = null;
        private ObservableCollection<ImageComboBoxItemProperty> _source = new ObservableCollection<ImageComboBoxItemProperty>();
        public ObservableCollection<ImageComboBoxItemProperty> ImageComboBoxItemSource
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged("ImageComboBoxItemSource");
            }
        }

        public ImageComboBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ClickRemoveImageButton()
        {
            int pos = -1;
            foreach (var item in _imageComboBox.Items)
            {
                pos++;
                if (_highLightedItem.Content.Equals(item))
                {
                    break;
                }
            }

            if ((0 <= pos && pos < _source.Count))
            {
                if (!ImageComboBoxItemSource[pos].IsPlaneUsing)
                {
                    _imageComboBox.ClearValue(ItemsControl.ItemsSourceProperty);
                    _source.RemoveAt(pos);
                    _imageComboBox.ItemsSource = _source;
                }
                else
                {
                    MessageBox.Show("Plane is patroling on this map.", "Error");
                }
            }

            if (OnRemoveImageClick != null)
            {
                OnRemoveImageClick();
            }
        }

        private void OnComboBoxItemMouseMove(object sender, MouseEventArgs e)
        {
            _highLightedItem = sender as ComboBoxItem;
        }

        private void SelectionChangedComboBox(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            }
        }
    }
}
