using AR.Drone.WinApp.MyUserControl.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ImageComboBox : UserControl
    {
        public delegate void ImageComboBoxRemoveImageOnClickEvent();
        public delegate void ImageComboBoxSelectionChangedEvent(object sender, SelectionChangedEventArgs e);
        public event ImageComboBoxRemoveImageOnClickEvent OnRemoveImageClick=null;
        public event ImageComboBoxSelectionChangedEvent SelectionChanged = null;

        private Boolean _isRemovable = false;
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
            }
        }

        public ImageComboBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ClickRemoveImageButton()
        {
            if (OnRemoveImageClick != null)
            {
                OnRemoveImageClick();
            }
            _isRemovable = true;
        }

        private void SelectionChangedComboBox(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender,e);
            }
            if (_isRemovable)
            {
                int pos = _imageComboBox.SelectedIndex;
                _imageComboBox.ClearValue(ItemsControl.ItemsSourceProperty);
                if (0 <= pos && pos < _source.Count)
                {
                    _source.RemoveAt(pos);
                }
                _imageComboBox.ItemsSource = _source;
            }
            _isRemovable = false;
        }
    }
}
