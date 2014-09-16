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
            _comboBoxSource = _comboBox.ImageComboBoxItemSource;
            _comboBoxSource.Add(new ImageComboBoxItemProperty() { ItemText = "科研大樓12F" });
            _comboBoxSource.Add(new ImageComboBoxItemProperty() { ItemText = "科研大樓3F" });
            _comboBoxSource.Add(new ImageComboBoxItemProperty() { ItemText = "科研大樓B2-地下演講廳" });
            _comboBoxSource.Add(new ImageComboBoxItemProperty() { ItemText = "共同科館3F" });
            DataContext = this;
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

        public void SetBattery(double rate)
        {
            _battery.Width = 100 * rate;
            Color endColor=GetRelativeColor(_batterySampleBrush.GradientStops, rate);
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetBattery(bbb.Value/100.0);
        }



        private ObservableCollection<ImageComboBoxItemProperty> _comboBoxSource = new ObservableCollection<ImageComboBoxItemProperty>();

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnComboBoxRemoveImageClick()
        {

        }
    }
}
