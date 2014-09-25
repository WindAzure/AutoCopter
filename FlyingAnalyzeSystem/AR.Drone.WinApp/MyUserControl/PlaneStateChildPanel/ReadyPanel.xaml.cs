using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ReadyPanel : UserControl, INotifyPropertyChanged
    {
        private int _hh;
        private int _mm;
        private int _ss;

        public delegate void ReadyPanelTimeSpanEvent(TimeSpan span);
        public ReadyPanelTimeSpanEvent ClickOkButton = null;

        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ReadyPanel()
        {
            InitializeComponent();
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour < 12)
            {
                _amButton.IsToggled = true;
                _pmButton.IsToggled = false;
            }
            else
            {
                _amButton.IsToggled = false;
                _pmButton.IsToggled = true;
            }
            _textBox.Text = currentTime.ToString("hh : mm : ss");
        }

        private void OnToggleAmButton()
        {
            _pmButton.IsToggled = !_pmButton.IsToggled;
            _okButton.IsActive = CheckTextLegal(_textBox.Text);
        }

        private void OnTogglePmButton()
        {
            _amButton.IsToggled = !_amButton.IsToggled;
            _okButton.IsActive = CheckTextLegal(_textBox.Text);
        }

        private void OnClickOkButton()
        {
            TimeSpan span;
            if (_amButton.IsToggled)
            {
                span = new TimeSpan(_hh, _mm, _ss);
            }
            else
            {
                if (_hh == 12)
                {
                    span = new TimeSpan(0, _mm, _ss);
                }
                else
                {
                    span = new TimeSpan(_hh + 12, _mm, _ss);
                }
            }
            if (ClickOkButton != null)
            {
                ClickOkButton(span);
            }
        }

        public bool CheckTextLegal(String settingTime)
        {
            try
            {
                String[] data = settingTime.Split(' ', ':');
                int T = 0;
                foreach (String s in data)
                {
                    if (!String.IsNullOrEmpty(s))
                    {
                        int value = Convert.ToInt32(s);
                        if (T == 0)
                        {
                            _hh = value;
                        }
                        else if (T == 1)
                        {
                            _mm = value;
                        }
                        else if (T == 2)
                        {
                            _ss = value;
                        }
                        T++;
                    }
                }

                if (0 <= _mm && _mm <= 59 && 0 <= _ss && _ss <= 59)
                {
                    if ((_amButton.IsToggled && 0<=_hh && _hh<=11) || (_pmButton.IsToggled && 1<=_hh && _hh<=12))
                    {
                        _warningText.Visibility = Visibility.Hidden;
                        return true;
                    }
                }
                _warningText.Visibility = Visibility.Visible;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine("!!");
                _warningText.Visibility = Visibility.Visible;
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
