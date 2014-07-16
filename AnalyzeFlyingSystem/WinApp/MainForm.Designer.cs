namespace WinApp
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
            this._videoGroupBox = new System.Windows.Forms.GroupBox();
            this._planeBottomPictureBox = new System.Windows.Forms.PictureBox();
            this._planeHeadPictureBox = new System.Windows.Forms.PictureBox();
            this._parameterGroupBox = new System.Windows.Forms.GroupBox();
            this._magNeticZContentLabel = new System.Windows.Forms.Label();
            this._magNeticZLabel = new System.Windows.Forms.Label();
            this._magNeticYContentLabel = new System.Windows.Forms.Label();
            this._magNeticYLabel = new System.Windows.Forms.Label();
            this._magNeticXContentLabel = new System.Windows.Forms.Label();
            this._magNeticXLabel = new System.Windows.Forms.Label();
            this._ultraSoundRightContentLabel = new System.Windows.Forms.Label();
            this._ultraSoundRightLabel = new System.Windows.Forms.Label();
            this._ultraSoundLeftContentLabel = new System.Windows.Forms.Label();
            this._ultraSoundLeftLabel = new System.Windows.Forms.Label();
            this._ultraSoundBackContentLabel = new System.Windows.Forms.Label();
            this._ultraSoundBackLabel = new System.Windows.Forms.Label();
            this._ultraSoundForntContentLabel = new System.Windows.Forms.Label();
            this._ultraSoundFrontLabel = new System.Windows.Forms.Label();
            this._activeButton = new System.Windows.Forms.Button();
            this._modeContentLabel = new System.Windows.Forms.Label();
            this._modeLabel = new System.Windows.Forms.Label();
            this._videoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._planeBottomPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._planeHeadPictureBox)).BeginInit();
            this._parameterGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _videoGroupBox
            // 
            this._videoGroupBox.Controls.Add(this._planeBottomPictureBox);
            this._videoGroupBox.Controls.Add(this._planeHeadPictureBox);
            this._videoGroupBox.Location = new System.Drawing.Point(30, 31);
            this._videoGroupBox.Name = "_videoGroupBox";
            this._videoGroupBox.Size = new System.Drawing.Size(700, 350);
            this._videoGroupBox.TabIndex = 1;
            this._videoGroupBox.TabStop = false;
            this._videoGroupBox.Text = "影像";
            // 
            // _planeBottomPictureBox
            // 
            this._planeBottomPictureBox.Location = new System.Drawing.Point(363, 25);
            this._planeBottomPictureBox.Name = "_planeBottomPictureBox";
            this._planeBottomPictureBox.Size = new System.Drawing.Size(300, 300);
            this._planeBottomPictureBox.TabIndex = 4;
            this._planeBottomPictureBox.TabStop = false;
            // 
            // _planeHeadPictureBox
            // 
            this._planeHeadPictureBox.Location = new System.Drawing.Point(35, 25);
            this._planeHeadPictureBox.Name = "_planeHeadPictureBox";
            this._planeHeadPictureBox.Size = new System.Drawing.Size(300, 300);
            this._planeHeadPictureBox.TabIndex = 3;
            this._planeHeadPictureBox.TabStop = false;
            // 
            // _parameterGroupBox
            // 
            this._parameterGroupBox.Controls.Add(this._modeContentLabel);
            this._parameterGroupBox.Controls.Add(this._modeLabel);
            this._parameterGroupBox.Controls.Add(this._magNeticZContentLabel);
            this._parameterGroupBox.Controls.Add(this._magNeticZLabel);
            this._parameterGroupBox.Controls.Add(this._magNeticYContentLabel);
            this._parameterGroupBox.Controls.Add(this._magNeticYLabel);
            this._parameterGroupBox.Controls.Add(this._magNeticXContentLabel);
            this._parameterGroupBox.Controls.Add(this._magNeticXLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundRightContentLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundRightLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundLeftContentLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundLeftLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundBackContentLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundBackLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundForntContentLabel);
            this._parameterGroupBox.Controls.Add(this._ultraSoundFrontLabel);
            this._parameterGroupBox.Location = new System.Drawing.Point(753, 31);
            this._parameterGroupBox.Name = "_parameterGroupBox";
            this._parameterGroupBox.Size = new System.Drawing.Size(200, 283);
            this._parameterGroupBox.TabIndex = 2;
            this._parameterGroupBox.TabStop = false;
            this._parameterGroupBox.Text = "機體參數";
            // 
            // _magNeticZContentLabel
            // 
            this._magNeticZContentLabel.AutoSize = true;
            this._magNeticZContentLabel.Location = new System.Drawing.Point(94, 248);
            this._magNeticZContentLabel.Name = "_magNeticZContentLabel";
            this._magNeticZContentLabel.Size = new System.Drawing.Size(0, 13);
            this._magNeticZContentLabel.TabIndex = 13;
            // 
            // _magNeticZLabel
            // 
            this._magNeticZLabel.AutoSize = true;
            this._magNeticZLabel.Location = new System.Drawing.Point(24, 248);
            this._magNeticZLabel.Name = "_magNeticZLabel";
            this._magNeticZLabel.Size = new System.Drawing.Size(53, 13);
            this._magNeticZLabel.TabIndex = 12;
            this._magNeticZLabel.Text = "磁力 (Z) :";
            // 
            // _magNeticYContentLabel
            // 
            this._magNeticYContentLabel.AutoSize = true;
            this._magNeticYContentLabel.Location = new System.Drawing.Point(94, 215);
            this._magNeticYContentLabel.Name = "_magNeticYContentLabel";
            this._magNeticYContentLabel.Size = new System.Drawing.Size(0, 13);
            this._magNeticYContentLabel.TabIndex = 11;
            // 
            // _magNeticYLabel
            // 
            this._magNeticYLabel.AutoSize = true;
            this._magNeticYLabel.Location = new System.Drawing.Point(24, 215);
            this._magNeticYLabel.Name = "_magNeticYLabel";
            this._magNeticYLabel.Size = new System.Drawing.Size(53, 13);
            this._magNeticYLabel.TabIndex = 10;
            this._magNeticYLabel.Text = "磁力 (Y) :";
            // 
            // _magNeticXContentLabel
            // 
            this._magNeticXContentLabel.AutoSize = true;
            this._magNeticXContentLabel.Location = new System.Drawing.Point(94, 188);
            this._magNeticXContentLabel.Name = "_magNeticXContentLabel";
            this._magNeticXContentLabel.Size = new System.Drawing.Size(0, 13);
            this._magNeticXContentLabel.TabIndex = 9;
            // 
            // _magNeticXLabel
            // 
            this._magNeticXLabel.AutoSize = true;
            this._magNeticXLabel.Location = new System.Drawing.Point(24, 188);
            this._magNeticXLabel.Name = "_magNeticXLabel";
            this._magNeticXLabel.Size = new System.Drawing.Size(53, 13);
            this._magNeticXLabel.TabIndex = 8;
            this._magNeticXLabel.Text = "磁力 (X) :";
            // 
            // _ultraSoundRightContentLabel
            // 
            this._ultraSoundRightContentLabel.AutoSize = true;
            this._ultraSoundRightContentLabel.Location = new System.Drawing.Point(94, 153);
            this._ultraSoundRightContentLabel.Name = "_ultraSoundRightContentLabel";
            this._ultraSoundRightContentLabel.Size = new System.Drawing.Size(0, 13);
            this._ultraSoundRightContentLabel.TabIndex = 7;
            // 
            // _ultraSoundRightLabel
            // 
            this._ultraSoundRightLabel.AutoSize = true;
            this._ultraSoundRightLabel.Location = new System.Drawing.Point(24, 153);
            this._ultraSoundRightLabel.Name = "_ultraSoundRightLabel";
            this._ultraSoundRightLabel.Size = new System.Drawing.Size(70, 13);
            this._ultraSoundRightLabel.TabIndex = 6;
            this._ultraSoundRightLabel.Text = "超音波 (右) :";
            // 
            // _ultraSoundLeftContentLabel
            // 
            this._ultraSoundLeftContentLabel.AutoSize = true;
            this._ultraSoundLeftContentLabel.Location = new System.Drawing.Point(94, 121);
            this._ultraSoundLeftContentLabel.Name = "_ultraSoundLeftContentLabel";
            this._ultraSoundLeftContentLabel.Size = new System.Drawing.Size(0, 13);
            this._ultraSoundLeftContentLabel.TabIndex = 5;
            // 
            // _ultraSoundLeftLabel
            // 
            this._ultraSoundLeftLabel.AutoSize = true;
            this._ultraSoundLeftLabel.Location = new System.Drawing.Point(24, 121);
            this._ultraSoundLeftLabel.Name = "_ultraSoundLeftLabel";
            this._ultraSoundLeftLabel.Size = new System.Drawing.Size(70, 13);
            this._ultraSoundLeftLabel.TabIndex = 4;
            this._ultraSoundLeftLabel.Text = "超音波 (左) :";
            // 
            // _ultraSoundBackContentLabel
            // 
            this._ultraSoundBackContentLabel.AutoSize = true;
            this._ultraSoundBackContentLabel.Location = new System.Drawing.Point(94, 88);
            this._ultraSoundBackContentLabel.Name = "_ultraSoundBackContentLabel";
            this._ultraSoundBackContentLabel.Size = new System.Drawing.Size(0, 13);
            this._ultraSoundBackContentLabel.TabIndex = 3;
            // 
            // _ultraSoundBackLabel
            // 
            this._ultraSoundBackLabel.AutoSize = true;
            this._ultraSoundBackLabel.Location = new System.Drawing.Point(24, 88);
            this._ultraSoundBackLabel.Name = "_ultraSoundBackLabel";
            this._ultraSoundBackLabel.Size = new System.Drawing.Size(70, 13);
            this._ultraSoundBackLabel.TabIndex = 2;
            this._ultraSoundBackLabel.Text = "超音波 (後) :";
            // 
            // _ultraSoundForntContentLabel
            // 
            this._ultraSoundForntContentLabel.AutoSize = true;
            this._ultraSoundForntContentLabel.Location = new System.Drawing.Point(94, 61);
            this._ultraSoundForntContentLabel.Name = "_ultraSoundForntContentLabel";
            this._ultraSoundForntContentLabel.Size = new System.Drawing.Size(0, 13);
            this._ultraSoundForntContentLabel.TabIndex = 1;
            // 
            // _ultraSoundFrontLabel
            // 
            this._ultraSoundFrontLabel.AutoSize = true;
            this._ultraSoundFrontLabel.Location = new System.Drawing.Point(24, 61);
            this._ultraSoundFrontLabel.Name = "_ultraSoundFrontLabel";
            this._ultraSoundFrontLabel.Size = new System.Drawing.Size(70, 13);
            this._ultraSoundFrontLabel.TabIndex = 0;
            this._ultraSoundFrontLabel.Text = "超音波 (前) :";
            // 
            // _activeButton
            // 
            this._activeButton.Location = new System.Drawing.Point(825, 342);
            this._activeButton.Name = "_activeButton";
            this._activeButton.Size = new System.Drawing.Size(75, 23);
            this._activeButton.TabIndex = 3;
            this._activeButton.Text = "啟動";
            this._activeButton.UseVisualStyleBackColor = true;
            // 
            // _modeContentLabel
            // 
            this._modeContentLabel.AutoSize = true;
            this._modeContentLabel.Location = new System.Drawing.Point(94, 30);
            this._modeContentLabel.Name = "_modeContentLabel";
            this._modeContentLabel.Size = new System.Drawing.Size(0, 13);
            this._modeContentLabel.TabIndex = 15;
            // 
            // _modeLabel
            // 
            this._modeLabel.AutoSize = true;
            this._modeLabel.Location = new System.Drawing.Point(24, 30);
            this._modeLabel.Name = "_modeLabel";
            this._modeLabel.Size = new System.Drawing.Size(37, 13);
            this._modeLabel.TabIndex = 14;
            this._modeLabel.Text = "模式 :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 417);
            this.Controls.Add(this._activeButton);
            this.Controls.Add(this._parameterGroupBox);
            this.Controls.Add(this._videoGroupBox);
            this.Name = "MainForm";
            this.Text = "Floor Guiding System";
            this._videoGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._planeBottomPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._planeHeadPictureBox)).EndInit();
            this._parameterGroupBox.ResumeLayout(false);
            this._parameterGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _videoGroupBox;
        private System.Windows.Forms.PictureBox _planeBottomPictureBox;
        private System.Windows.Forms.PictureBox _planeHeadPictureBox;
        private System.Windows.Forms.GroupBox _parameterGroupBox;
        private System.Windows.Forms.Label _magNeticZContentLabel;
        private System.Windows.Forms.Label _magNeticZLabel;
        private System.Windows.Forms.Label _magNeticYContentLabel;
        private System.Windows.Forms.Label _magNeticYLabel;
        private System.Windows.Forms.Label _magNeticXContentLabel;
        private System.Windows.Forms.Label _magNeticXLabel;
        private System.Windows.Forms.Label _ultraSoundRightContentLabel;
        private System.Windows.Forms.Label _ultraSoundRightLabel;
        private System.Windows.Forms.Label _ultraSoundLeftContentLabel;
        private System.Windows.Forms.Label _ultraSoundLeftLabel;
        private System.Windows.Forms.Label _ultraSoundBackContentLabel;
        private System.Windows.Forms.Label _ultraSoundBackLabel;
        private System.Windows.Forms.Label _ultraSoundForntContentLabel;
        private System.Windows.Forms.Label _ultraSoundFrontLabel;
        private System.Windows.Forms.Label _modeContentLabel;
        private System.Windows.Forms.Label _modeLabel;
        private System.Windows.Forms.Button _activeButton;

    }
}

