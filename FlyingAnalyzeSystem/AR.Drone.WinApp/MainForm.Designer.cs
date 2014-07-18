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
            this._btnStart = new System.Windows.Forms.Button();
            this._pbVideo = new System.Windows.Forms.PictureBox();
            this.tmrStateUpdate = new System.Windows.Forms.Timer(this.components);
            this._btnSwitchCam = new System.Windows.Forms.Button();
            this._tvInfo = new System.Windows.Forms.TreeView();
            this.tmrVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this._ultraSoundGroupBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pbVideo)).BeginInit();
            this._ultraSoundGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnStart
            // 
            this._btnStart.Location = new System.Drawing.Point(808, 23);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(75, 23);
            this._btnStart.TabIndex = 0;
            this._btnStart.Text = "Activate";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // _pbVideo
            // 
            this._pbVideo.BackColor = System.Drawing.SystemColors.ControlDark;
            this._pbVideo.Location = new System.Drawing.Point(15, 23);
            this._pbVideo.Name = "_pbVideo";
            this._pbVideo.Size = new System.Drawing.Size(640, 360);
            this._pbVideo.TabIndex = 2;
            this._pbVideo.TabStop = false;
            // 
            // tmrStateUpdate
            // 
            this.tmrStateUpdate.Interval = 500;
            this.tmrStateUpdate.Tick += new System.EventHandler(this.tmrStateUpdate_Tick);
            // 
            // _btnSwitchCam
            // 
            this._btnSwitchCam.Location = new System.Drawing.Point(808, 52);
            this._btnSwitchCam.Name = "_btnSwitchCam";
            this._btnSwitchCam.Size = new System.Drawing.Size(89, 23);
            this._btnSwitchCam.TabIndex = 8;
            this._btnSwitchCam.Text = "Video Channel";
            this._btnSwitchCam.UseVisualStyleBackColor = true;
            this._btnSwitchCam.Click += new System.EventHandler(this.btnSwitchCam_Click);
            // 
            // _tvInfo
            // 
            this._tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tvInfo.Location = new System.Drawing.Point(665, 166);
            this._tvInfo.Name = "_tvInfo";
            this._tvInfo.Size = new System.Drawing.Size(319, 253);
            this._tvInfo.TabIndex = 18;
            // 
            // tmrVideoUpdate
            // 
            this.tmrVideoUpdate.Interval = 20;
            this.tmrVideoUpdate.Tick += new System.EventHandler(this.tmrVideoUpdate_Tick);
            // 
            // _ultraSoundGroupBox
            // 
            this._ultraSoundGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ultraSoundGroupBox.Controls.Add(this.label7);
            this._ultraSoundGroupBox.Controls.Add(this.label8);
            this._ultraSoundGroupBox.Controls.Add(this.label5);
            this._ultraSoundGroupBox.Controls.Add(this.label6);
            this._ultraSoundGroupBox.Controls.Add(this.label3);
            this._ultraSoundGroupBox.Controls.Add(this.label4);
            this._ultraSoundGroupBox.Controls.Add(this.label2);
            this._ultraSoundGroupBox.Controls.Add(this.label1);
            this._ultraSoundGroupBox.Location = new System.Drawing.Point(674, 23);
            this._ultraSoundGroupBox.Name = "_ultraSoundGroupBox";
            this._ultraSoundGroupBox.Size = new System.Drawing.Size(131, 136);
            this._ultraSoundGroupBox.TabIndex = 20;
            this._ultraSoundGroupBox.TabStop = false;
            this._ultraSoundGroupBox.Text = "超音波";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "右 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "左 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "後 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "前 :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(844, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 533);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._ultraSoundGroupBox);
            this.Controls.Add(this._tvInfo);
            this.Controls.Add(this._btnSwitchCam);
            this.Controls.Add(this._pbVideo);
            this.Controls.Add(this._btnStart);
            this.Name = "MainForm";
            this.Text = "AR.Drone Control";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._pbVideo)).EndInit();
            this._ultraSoundGroupBox.ResumeLayout(false);
            this._ultraSoundGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.PictureBox _pbVideo;
        private System.Windows.Forms.Timer tmrStateUpdate;
        private System.Windows.Forms.Button _btnSwitchCam;
        private System.Windows.Forms.TreeView _tvInfo;
        private System.Windows.Forms.Timer tmrVideoUpdate;
        private System.Windows.Forms.GroupBox _ultraSoundGroupBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

