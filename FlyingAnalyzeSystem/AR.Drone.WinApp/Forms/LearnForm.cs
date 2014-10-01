using AR.Drone.WinApp.MyUserControl.MapComboBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AR.Drone.WinApp.Forms
{
    public partial class LearnForm : Form
    {
        private bool _isBack = false;
        private PlaneStateForm _lastForm=null;

        public LearnForm(PlaneStateForm form)
        {
            InitializeComponent();
            _lastForm = form;
            _learnPanel.MouseDownLeftControlButton += MouseDownLearnPanelLeftControlButton;
            _learnPanel.MouseUpLeftControlButton += MouseUpLearnPanelLeftControlButton;
            _learnPanel.MouseDownRightControlButton += MouseDownLearnPanelRightControlButton;
            _learnPanel.MouseUpRightControlButton += MouseUpLearnPanelRightControlButton;
            _learnPanel.MouseDownForwardControlButton += MouseDownLearnPanelForwardControlButton;
            _learnPanel.MouseUpForwardControlButton += MouseUpLearnPanelForwardControlButton;
            _learnPanel.MouseDownUpControlButton += MouseDownLearnPanelUpControlButton;
            _learnPanel.MouseUpUpControlButton += MouseUpLearnPanelUpControlButton;
            _learnPanel.MouseDownDownControlButton += MouseLearnPanelDownDownControlButton;
            _learnPanel.MouseUpDownControlButton += MouseUpLearnPanelDownControlButton;
            _learnPanel.ClickSaveButton += ClickLearnPanelSaveButton;
            _learnPanel.ClickBackButton += ClickLearnPanelBackButton;
            _learnPanel.ClickTakeOffButton += ClickLearnPanelTakeOffButton;
            _learnPanel.ClickLandButton += ClickLearnPanelLandButton;
        }

        public void ClickLearnPanelLandButton()
        {
            Debug.WriteLine("ClickLearnPanelLandButton");
        }

        public void ClickLearnPanelTakeOffButton()
        {
            Debug.WriteLine("ClickLearnPanelTakeOffButton");
        }

        public void ClickLearnPanelBackButton()
        {
            _isBack = true;
            this.Close();
        }

        public void ClickLearnPanelSaveButton()
        {
            Debug.WriteLine("OnClickSaveButton");
        }

        public void MouseUpLearnPanelDownControlButton()
        {
            Debug.WriteLine("MouseUpPlaneDownControlButton");
        }

        public void MouseLearnPanelDownDownControlButton()
        {
            Debug.WriteLine("MouseDownPlaneDownControlButton");
        }

        public void MouseUpLearnPanelUpControlButton()
        {
            Debug.WriteLine("MouseUpPlaneUpControlButton");
        }

        public void MouseDownLearnPanelUpControlButton()
        {
            Debug.WriteLine("MouseDownPlaneUpControlButton");
        }

        public void MouseUpLearnPanelForwardControlButton()
        {
            Debug.WriteLine("MouseUpPlaneForwardControlButton");
        }

        public void MouseDownLearnPanelForwardControlButton()
        {
            Debug.WriteLine("MouseDownPlaneForwardControlButton");
        }

        public void MouseUpLearnPanelRightControlButton()
        {
            Debug.WriteLine("MouseUpPlaneRightControlButton");
        }

        public void MouseDownLearnPanelRightControlButton()
        {
            Debug.WriteLine("MouseDownPlaneRightControlButton");
        }

        public void MouseUpLearnPanelLeftControlButton()
        {
            Debug.WriteLine("MouseUpPlaneLeftControlButton");
        }

        public void MouseDownLearnPanelLeftControlButton()
        {
            Debug.WriteLine("MouseDownPlaneLeftControlButton");
        }

        private void LearnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason==CloseReason.UserClosing)
            {
                if (_isBack)
                {
                    _lastForm.Location = this.Location;
                    _lastForm.WindowState = this.WindowState;
                    _lastForm.Width = this.Width;
                    _lastForm.Height = this.Height;
                    _lastForm.Show();
                }
                else
                {
                    _lastForm.Close();
                }
            }
        }
    }
}
