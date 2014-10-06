using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace AR.Drone.WinApp.MyUserControl.MapComboBox
{
    public class ImageComboBoxItemProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String _itemText = "";
        public String ItemText
        {
            set
            {
                _itemText = value;
                OnPropertyChanged("ItemText");
            }
            get
            {
                return _itemText;
            }
        }

        private BitmapImage _mapImg = null;
        public BitmapImage MapImage
        {
            set
            {
                _mapImg = value;
                OnPropertyChanged("MapImage");
            }
            get
            {
                return _mapImg;
            }
        }

        private String _mileage = null;
        public String Mileage
        {
            set
            {
                _mileage = value;
                OnPropertyChanged("Mileage");
            }
            get
            {
                return _mileage;
            }
        }

        private String _mileageLine = null;
        public String MileageLine
        {
            set
            {
                _mileageLine = value;
                OnPropertyChanged("MileageLine");
            }
            get
            {
                return _mileageLine;
            }
        }

        private bool _isPlaneUsing = false;
        public bool IsPlaneUsing
        {
            set
            {
                _isPlaneUsing = value;
                OnPropertyChanged("IsPlaneUsing");
            }
            get
            {
                return _isPlaneUsing;
            }
        }

        private String _id;
        public String Id
        {
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
            get
            {
                return _id;
            }
        }
    }
}
