﻿using AR.Drone.WinApp.MyUserControl.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ImageComboBoxItemProperty> _source = new ObservableCollection<ImageComboBoxItemProperty>();
        public ObservableCollection<ImageComboBoxItemProperty> ImageComboBoxItemSource
        {
            get
            {
                return _source;
            }
        }

        public ImageComboBox()
        {
            InitializeComponent();

            _source.Add(new ImageComboBoxItemProperty() { ItemText = "asdf" });
            _source.Add(new ImageComboBoxItemProperty() { ItemText = "qwer" });

            DataContext = this;
        }
    }
}
