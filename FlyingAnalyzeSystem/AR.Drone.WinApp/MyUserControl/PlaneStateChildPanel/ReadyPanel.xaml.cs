using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AR.Drone.WinApp.MyUserControl.PlaneStateChildPanel
{
    /// <summary>
    /// Interaction logic for ReadyPanel.xaml
    /// </summary>
    public partial class ReadyPanel : UserControl
    {
        public ReadyPanel()
        {
            InitializeComponent();
            _textBox.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void OnClickAmButton()
        {
        }

        private void OnClickPmButton()
        {
            Debug.WriteLine("OnClickPmButton");
        }

        private void OnClickOkButton()
        {
        }

        public bool CheckTextLegal(String settingTime)
        {
            try
            {
                int timeValue = Convert.ToInt32(Regex.Replace(settingTime, "[^0-9]", ""));
                int hh = timeValue / 10000;
                int mm = (timeValue - hh * 10000) / 100;
                int ss = timeValue - hh * 10000 - mm * 100;
                if (0 <= hh && hh <= 11 && 0 <= mm && mm <= 59 && 0 <= ss && ss <= 59)
                {
                    _warningText.Visibility = Visibility.Hidden;
                    return true;
                }
                _warningText.Visibility = Visibility.Visible;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_okButton != null)
            {
                _okButton.IsActive = CheckTextLegal(_textBox.Text);
            }
        }
    }
}
