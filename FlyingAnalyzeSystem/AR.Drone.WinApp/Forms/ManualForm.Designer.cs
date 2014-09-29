namespace AR.Drone.WinApp.Forms
{
    partial class ManualForm
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
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._elementHost = new System.Windows.Forms.Integration.ElementHost();
            this._manualControlPanel = new AR.Drone.WinApp.MyUserControl.ManualControlPanel();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _pictureBox
            // 
            this._pictureBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(984, 562);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 1;
            this._pictureBox.TabStop = false;
            // 
            // _elementHost
            // 
            this._elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._elementHost.Location = new System.Drawing.Point(0, 0);
            this._elementHost.Name = "_elementHost";
            this._elementHost.Size = new System.Drawing.Size(984, 562);
            this._elementHost.TabIndex = 2;
            this._elementHost.Text = "elementHost1";
            this._elementHost.Child = this._manualControlPanel;
            // 
            // ManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this._elementHost);
            this.Controls.Add(this._pictureBox);
            this.Name = "ManualForm";
            this.Text = "ManualForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManualForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.Integration.ElementHost _elementHost;
        private MyUserControl.ManualControlPanel _manualControlPanel;
    }
}