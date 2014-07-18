namespace DetectSystem
{
    partial class AnalysisForm
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
            this._startButton = new System.Windows.Forms.Button();
            this._inputGroupBox = new System.Windows.Forms.GroupBox();
            this._inputPictureBox = new System.Windows.Forms.PictureBox();
            this._outputGroupBox = new System.Windows.Forms.GroupBox();
            this._outputPictureBox = new System.Windows.Forms.PictureBox();
            this._qrTextLabel = new System.Windows.Forms.Label();
            this._tableGroupBox = new System.Windows.Forms.GroupBox();
            this._tableAreaLabel = new System.Windows.Forms.Label();
            this._table1AreaLabel = new System.Windows.Forms.Label();
            this._tableNameLabel = new System.Windows.Forms.Label();
            this._table1Label = new System.Windows.Forms.Label();
            this._takeOffButton = new System.Windows.Forms.Button();
            this._autoPilotButton = new System.Windows.Forms.Button();
            this._emergencyLandButton = new System.Windows.Forms.Button();
            this._inputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._inputPictureBox)).BeginInit();
            this._outputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._outputPictureBox)).BeginInit();
            this._tableGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _startButton
            // 
            this._startButton.Location = new System.Drawing.Point(838, 126);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(86, 34);
            this._startButton.TabIndex = 1;
            this._startButton.Text = "Open Camera";
            this._startButton.UseVisualStyleBackColor = true;
            this._startButton.Click += new System.EventHandler(this.ClickStartButton);
            // 
            // _inputGroupBox
            // 
            this._inputGroupBox.Controls.Add(this._inputPictureBox);
            this._inputGroupBox.Location = new System.Drawing.Point(22, 30);
            this._inputGroupBox.Name = "_inputGroupBox";
            this._inputGroupBox.Size = new System.Drawing.Size(370, 380);
            this._inputGroupBox.TabIndex = 2;
            this._inputGroupBox.TabStop = false;
            this._inputGroupBox.Text = "Input Video";
            // 
            // _inputPictureBox
            // 
            this._inputPictureBox.Location = new System.Drawing.Point(10, 20);
            this._inputPictureBox.Name = "_inputPictureBox";
            this._inputPictureBox.Size = new System.Drawing.Size(350, 350);
            this._inputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._inputPictureBox.TabIndex = 1;
            this._inputPictureBox.TabStop = false;
            this._inputPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownInputBox);
            this._inputPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveInputBox);
            this._inputPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpInputBox);
            // 
            // _outputGroupBox
            // 
            this._outputGroupBox.Controls.Add(this._outputPictureBox);
            this._outputGroupBox.Location = new System.Drawing.Point(443, 30);
            this._outputGroupBox.Name = "_outputGroupBox";
            this._outputGroupBox.Size = new System.Drawing.Size(370, 380);
            this._outputGroupBox.TabIndex = 3;
            this._outputGroupBox.TabStop = false;
            this._outputGroupBox.Text = "Output Video";
            // 
            // _outputPictureBox
            // 
            this._outputPictureBox.Location = new System.Drawing.Point(10, 20);
            this._outputPictureBox.Name = "_outputPictureBox";
            this._outputPictureBox.Size = new System.Drawing.Size(350, 350);
            this._outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._outputPictureBox.TabIndex = 1;
            this._outputPictureBox.TabStop = false;
            // 
            // _qrTextLabel
            // 
            this._qrTextLabel.AutoSize = true;
            this._qrTextLabel.Location = new System.Drawing.Point(620, 414);
            this._qrTextLabel.Name = "_qrTextLabel";
            this._qrTextLabel.Size = new System.Drawing.Size(0, 13);
            this._qrTextLabel.TabIndex = 6;
            // 
            // _tableGroupBox
            // 
            this._tableGroupBox.Controls.Add(this._tableAreaLabel);
            this._tableGroupBox.Controls.Add(this._table1AreaLabel);
            this._tableGroupBox.Controls.Add(this._tableNameLabel);
            this._tableGroupBox.Controls.Add(this._table1Label);
            this._tableGroupBox.Location = new System.Drawing.Point(839, 30);
            this._tableGroupBox.Name = "_tableGroupBox";
            this._tableGroupBox.Size = new System.Drawing.Size(193, 80);
            this._tableGroupBox.TabIndex = 7;
            this._tableGroupBox.TabStop = false;
            this._tableGroupBox.Text = "Table setting";
            // 
            // _tableAreaLabel
            // 
            this._tableAreaLabel.AutoSize = true;
            this._tableAreaLabel.Location = new System.Drawing.Point(115, 24);
            this._tableAreaLabel.Name = "_tableAreaLabel";
            this._tableAreaLabel.Size = new System.Drawing.Size(63, 13);
            this._tableAreaLabel.TabIndex = 33;
            this._tableAreaLabel.Text = "Target Area";
            // 
            // _table1AreaLabel
            // 
            this._table1AreaLabel.AutoSize = true;
            this._table1AreaLabel.Location = new System.Drawing.Point(126, 52);
            this._table1AreaLabel.Name = "_table1AreaLabel";
            this._table1AreaLabel.Size = new System.Drawing.Size(13, 13);
            this._table1AreaLabel.TabIndex = 29;
            this._table1AreaLabel.Text = "0";
            // 
            // _tableNameLabel
            // 
            this._tableNameLabel.AutoSize = true;
            this._tableNameLabel.Location = new System.Drawing.Point(19, 24);
            this._tableNameLabel.Name = "_tableNameLabel";
            this._tableNameLabel.Size = new System.Drawing.Size(69, 13);
            this._tableNameLabel.TabIndex = 5;
            this._tableNameLabel.Text = "Target Name";
            // 
            // _table1Label
            // 
            this._table1Label.AutoSize = true;
            this._table1Label.Location = new System.Drawing.Point(21, 52);
            this._table1Label.Name = "_table1Label";
            this._table1Label.Size = new System.Drawing.Size(64, 13);
            this._table1Label.TabIndex = 1;
            this._table1Label.Text = "Target No.1";
            // 
            // _takeOffButton
            // 
            this._takeOffButton.Location = new System.Drawing.Point(957, 126);
            this._takeOffButton.Name = "_takeOffButton";
            this._takeOffButton.Size = new System.Drawing.Size(86, 34);
            this._takeOffButton.TabIndex = 23;
            this._takeOffButton.Text = "Take Off";
            this._takeOffButton.UseVisualStyleBackColor = true;
            this._takeOffButton.Click += new System.EventHandler(this.ClickTakeOffButton);
            // 
            // _autoPilotButton
            // 
            this._autoPilotButton.Location = new System.Drawing.Point(841, 177);
            this._autoPilotButton.Name = "_autoPilotButton";
            this._autoPilotButton.Size = new System.Drawing.Size(202, 34);
            this._autoPilotButton.TabIndex = 24;
            this._autoPilotButton.Text = "Auto Pilot";
            this._autoPilotButton.UseVisualStyleBackColor = true;
            this._autoPilotButton.Click += new System.EventHandler(this.ClickAutoPilotButton);
            // 
            // _emergencyLandButton
            // 
            this._emergencyLandButton.Location = new System.Drawing.Point(841, 227);
            this._emergencyLandButton.Name = "_emergencyLandButton";
            this._emergencyLandButton.Size = new System.Drawing.Size(202, 34);
            this._emergencyLandButton.TabIndex = 25;
            this._emergencyLandButton.Text = "Emergency Land";
            this._emergencyLandButton.UseVisualStyleBackColor = true;
            this._emergencyLandButton.Click += new System.EventHandler(this.ClickEmergencyLandButton);
            // 
            // AnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 442);
            this.Controls.Add(this._emergencyLandButton);
            this.Controls.Add(this._autoPilotButton);
            this.Controls.Add(this._takeOffButton);
            this.Controls.Add(this._tableGroupBox);
            this.Controls.Add(this._qrTextLabel);
            this.Controls.Add(this._outputGroupBox);
            this.Controls.Add(this._inputGroupBox);
            this.Controls.Add(this._startButton);
            this.Name = "AnalysisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DetectForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingAnalysisForm);
            this._inputGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._inputPictureBox)).EndInit();
            this._outputGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._outputPictureBox)).EndInit();
            this._tableGroupBox.ResumeLayout(false);
            this._tableGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.GroupBox _inputGroupBox;
        private System.Windows.Forms.PictureBox _inputPictureBox;
        private System.Windows.Forms.GroupBox _outputGroupBox;
        private System.Windows.Forms.PictureBox _outputPictureBox;
        private System.Windows.Forms.Label _qrTextLabel;
        private System.Windows.Forms.GroupBox _tableGroupBox;
        private System.Windows.Forms.Label _tableNameLabel;
        private System.Windows.Forms.Label _table1Label;
        private System.Windows.Forms.Label _tableAreaLabel;
        private System.Windows.Forms.Label _table1AreaLabel;
        private System.Windows.Forms.Button _takeOffButton;
        private System.Windows.Forms.Button _autoPilotButton;
        private System.Windows.Forms.Button _emergencyLandButton;
    }
}

