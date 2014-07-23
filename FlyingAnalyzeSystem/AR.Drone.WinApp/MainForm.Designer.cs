namespace AR.Drone.WinApp
{
    partial class MainForm
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
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this._tvInfo = new System.Windows.Forms.TreeView();
            this.tmrVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this._btnSwitchCam = new System.Windows.Forms.Button();
            this._btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._pbVideo = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrStateUpdate
            // 
            this.tmrStateUpdate.Interval = 500;
            this.tmrStateUpdate.Tick += new System.EventHandler(this.tmrStateUpdate_Tick);
            // 
            // _tvInfo
            // 
            this._tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tvInfo.Location = new System.Drawing.Point(603, 23);
            this._tvInfo.Name = "_tvInfo";
            this._tvInfo.Size = new System.Drawing.Size(214, 316);
            this._tvInfo.TabIndex = 18;
            // 
            // tmrVideoUpdate
            // 
            this.tmrVideoUpdate.Interval = 20;
            this.tmrVideoUpdate.Tick += new System.EventHandler(this.tmrVideoUpdate_Tick);
            // 
            // _btnSwitchCam
            // 
            this._btnSwitchCam.Location = new System.Drawing.Point(714, 360);
            this._btnSwitchCam.Name = "_btnSwitchCam";
            this._btnSwitchCam.Size = new System.Drawing.Size(89, 23);
            this._btnSwitchCam.TabIndex = 23;
            this._btnSwitchCam.Text = "Video Channel";
            this._btnSwitchCam.UseVisualStyleBackColor = true;
            this._btnSwitchCam.Click += new System.EventHandler(this.btnSwitchCam_Click);
            // 
            // _btnStart
            // 
            this._btnStart.Location = new System.Drawing.Point(618, 360);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(75, 23);
            this._btnStart.TabIndex = 22;
            this._btnStart.Text = "Activate";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "label1";
            // 
            // _pbVideo
            // 
            this._pbVideo.BackColor = System.Drawing.SystemColors.ControlDark;
            this._pbVideo.Location = new System.Drawing.Point(15, 23);
            this._pbVideo.Name = "_pbVideo";
            this._pbVideo.Size = new System.Drawing.Size(570, 360);
            this._pbVideo.TabIndex = 2;
            this._pbVideo.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(668, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(332, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 409);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btnSwitchCam);
            this.Controls.Add(this._btnStart);
            this.Controls.Add(this._tvInfo);
            this.Controls.Add(this._pbVideo);
            this.Name = "MainForm";
            this.Text = "AR.Drone Control";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._pbVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrStateUpdate;
        private System.Windows.Forms.TreeView _tvInfo;
        private System.Windows.Forms.Timer tmrVideoUpdate;
        private System.Windows.Forms.Button _btnSwitchCam;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox _pbVideo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

