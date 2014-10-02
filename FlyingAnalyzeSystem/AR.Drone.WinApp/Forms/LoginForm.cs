using AR.Drone.WinApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;

namespace AR.Drone.WinApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            _loginPanel.SignInSuccess += OnSignInSuccess;
        }

        public void OnSignInSuccess()
        {
            PlaneStateForm form = new PlaneStateForm();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = this.Location;
            form.WindowState = this.WindowState;
            form.Width = this.Width;
            form.Height = this.Height;
            form.FormClosing += delegate { Close(); };
            form.Show();
            this.Hide();
        }
    }
}
