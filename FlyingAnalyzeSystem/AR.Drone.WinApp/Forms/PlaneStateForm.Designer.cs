﻿namespace AR.Drone.WinApp.Forms
{
    partial class PlaneStateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._planeStateTimer = new System.Windows.Forms.Timer(this.components);
            this._videoUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this._elementHost = new System.Windows.Forms.Integration.ElementHost();
            this._planeStatePanel = new AR.Drone.WinApp.MyUserControl.PlaneStatePanel();
            this.SuspendLayout();
            // 
            // _planeStateTimer
            // 
            this._planeStateTimer.Interval = 500;
            this._planeStateTimer.Tick += new System.EventHandler(this._planeStateTimer_Tick);
            // 
            // _videoUpdateTimer
            // 
            this._videoUpdateTimer.Interval = 20;
            this._videoUpdateTimer.Tick += new System.EventHandler(this._videoUpdateTimer_Tick);
            // 
            // _elementHost
            // 
            this._elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._elementHost.Location = new System.Drawing.Point(0, 0);
            this._elementHost.Name = "_elementHost";
            this._elementHost.Size = new System.Drawing.Size(984, 562);
            this._elementHost.TabIndex = 0;
            this._elementHost.Text = "elementHost1";
            this._elementHost.Child = this._planeStatePanel;
            // 
            // PlaneStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this._elementHost);
            this.Name = "PlaneStateForm";
            this.Text = "PlaneStateForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost _elementHost;
        private MyUserControl.PlaneStatePanel _planeStatePanel;
        private System.Windows.Forms.Timer _planeStateTimer;
        private System.Windows.Forms.Timer _videoUpdateTimer;
    }
}