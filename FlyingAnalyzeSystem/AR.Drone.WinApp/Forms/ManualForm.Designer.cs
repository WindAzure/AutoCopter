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
            this._elementHost = new System.Windows.Forms.Integration.ElementHost();
            this._manualControlPanel = new AR.Drone.WinApp.MyUserControl.ManualControlPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _elementHost
            // 
            this._elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._elementHost.Location = new System.Drawing.Point(0, 0);
            this._elementHost.Name = "_elementHost";
            this._elementHost.Size = new System.Drawing.Size(984, 562);
            this._elementHost.TabIndex = 0;
            this._elementHost.Text = "elementHost1";
            this._elementHost.Child = this._manualControlPanel;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(984, 562);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this._elementHost);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ManualForm";
            this.Text = "ManualForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost _elementHost;
        private MyUserControl.ManualControlPanel _manualControlPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}