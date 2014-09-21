using System;
using System.Collections.Generic;
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
        public LearnForm()
        {
            InitializeComponent();
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
            _learnPanel.ClickUploadButton += ClickLearnPanelUploadButton;
            _learnPanel.ClickBackButton += ClickLearnPanelBackButton;
        }

        void ClickLearnPanelBackButton()
        {
            Debug.WriteLine("OnClickBackButton");
        }

        void ClickLearnPanelUploadButton()
        {
            Debug.WriteLine("OnClickUploadButton");
        }

        void ClickLearnPanelSaveButton()
        {
            Debug.WriteLine("OnClickSaveButton");
        }

        void MouseUpLearnPanelDownControlButton()
        {
            Debug.WriteLine("MouseUpPlaneDownControlButton");
        }

        void MouseLearnPanelDownDownControlButton()
        {
            Debug.WriteLine("MouseDownPlaneDownControlButton");
        }

        void MouseUpLearnPanelUpControlButton()
        {
            Debug.WriteLine("MouseUpPlaneUpControlButton");
        }

        void MouseDownLearnPanelUpControlButton()
        {
            Debug.WriteLine("MouseDownPlaneUpControlButton");
        }

        void MouseUpLearnPanelForwardControlButton()
        {
            Debug.WriteLine("MouseUpPlaneForwardControlButton");
        }

        void MouseDownLearnPanelForwardControlButton()
        {
            Debug.WriteLine("MouseDownPlaneForwardControlButton");
        }

        void MouseUpLearnPanelRightControlButton()
        {
            Debug.WriteLine("MouseUpPlaneRightControlButton");
        }

        void MouseDownLearnPanelRightControlButton()
        {
            Debug.WriteLine("MouseDownPlaneRightControlButton");
        }

        void MouseUpLearnPanelLeftControlButton()
        {
            Debug.WriteLine("MouseUpPlaneLeftControlButton");
        }

        void MouseDownLearnPanelLeftControlButton()
        {
            Debug.WriteLine("MouseDownPlaneLeftControlButton");
        }
    }
}
