﻿namespace DetectSystem
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
            this.button2 = new System.Windows.Forms.Button();
            this._qrTextLabel = new System.Windows.Forms.Label();
            this._tableGroupBox = new System.Windows.Forms.GroupBox();
            this._table1Label = new System.Windows.Forms.Label();
            this._table2Label = new System.Windows.Forms.Label();
            this._table3Label = new System.Windows.Forms.Label();
            this._table4Label = new System.Windows.Forms.Label();
            this._tableNameLabel = new System.Windows.Forms.Label();
            this._tableStateLabel = new System.Windows.Forms.Label();
            this._table4StateLabel = new System.Windows.Forms.Label();
            this._table3StateLabel = new System.Windows.Forms.Label();
            this._table2StateLabel = new System.Windows.Forms.Label();
            this._table1StateLabel = new System.Windows.Forms.Label();
            this._table1StateChangeButton = new System.Windows.Forms.Button();
            this._stateChangeLabel = new System.Windows.Forms.Label();
            this._table2StateChangeButton = new System.Windows.Forms.Button();
            this._table3StateChangeButton = new System.Windows.Forms.Button();
            this._table4StateChangeButton = new System.Windows.Forms.Button();
            this._tableAreaLabel = new System.Windows.Forms.Label();
            this._table4AreaLabel = new System.Windows.Forms.Label();
            this._table3AreaLabel = new System.Windows.Forms.Label();
            this._table2AreaLabel = new System.Windows.Forms.Label();
            this._table1AreaLabel = new System.Windows.Forms.Label();
            this._inputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._inputPictureBox)).BeginInit();
            this._outputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._outputPictureBox)).BeginInit();
            this._tableGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _startButton
            // 
            this._startButton.Location = new System.Drawing.Point(887, 294);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(86, 34);
            this._startButton.TabIndex = 1;
            this._startButton.Text = "Start";
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1124, 294);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 34);
            this.button2.TabIndex = 5;
            this.button2.Text = "Camera Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this._tableGroupBox.Controls.Add(this._table4AreaLabel);
            this._tableGroupBox.Controls.Add(this._table3AreaLabel);
            this._tableGroupBox.Controls.Add(this._table2AreaLabel);
            this._tableGroupBox.Controls.Add(this._table1AreaLabel);
            this._tableGroupBox.Controls.Add(this._table4StateChangeButton);
            this._tableGroupBox.Controls.Add(this._table3StateChangeButton);
            this._tableGroupBox.Controls.Add(this._table2StateChangeButton);
            this._tableGroupBox.Controls.Add(this._stateChangeLabel);
            this._tableGroupBox.Controls.Add(this._table1StateChangeButton);
            this._tableGroupBox.Controls.Add(this._tableStateLabel);
            this._tableGroupBox.Controls.Add(this._table4StateLabel);
            this._tableGroupBox.Controls.Add(this._table3StateLabel);
            this._tableGroupBox.Controls.Add(this._table2StateLabel);
            this._tableGroupBox.Controls.Add(this._table1StateLabel);
            this._tableGroupBox.Controls.Add(this._tableNameLabel);
            this._tableGroupBox.Controls.Add(this._table4Label);
            this._tableGroupBox.Controls.Add(this._table3Label);
            this._tableGroupBox.Controls.Add(this._table2Label);
            this._tableGroupBox.Controls.Add(this._table1Label);
            this._tableGroupBox.Location = new System.Drawing.Point(839, 30);
            this._tableGroupBox.Name = "_tableGroupBox";
            this._tableGroupBox.Size = new System.Drawing.Size(371, 189);
            this._tableGroupBox.TabIndex = 7;
            this._tableGroupBox.TabStop = false;
            this._tableGroupBox.Text = "Table setting";
            // 
            // _table1Label
            // 
            this._table1Label.AutoSize = true;
            this._table1Label.Location = new System.Drawing.Point(21, 65);
            this._table1Label.Name = "_table1Label";
            this._table1Label.Size = new System.Drawing.Size(60, 13);
            this._table1Label.TabIndex = 1;
            this._table1Label.Text = "Table No.1";
            // 
            // _table2Label
            // 
            this._table2Label.AutoSize = true;
            this._table2Label.Location = new System.Drawing.Point(21, 94);
            this._table2Label.Name = "_table2Label";
            this._table2Label.Size = new System.Drawing.Size(60, 13);
            this._table2Label.TabIndex = 2;
            this._table2Label.Text = "Table No.2";
            // 
            // _table3Label
            // 
            this._table3Label.AutoSize = true;
            this._table3Label.Location = new System.Drawing.Point(21, 120);
            this._table3Label.Name = "_table3Label";
            this._table3Label.Size = new System.Drawing.Size(60, 13);
            this._table3Label.TabIndex = 3;
            this._table3Label.Text = "Table No.3";
            // 
            // _table4Label
            // 
            this._table4Label.AutoSize = true;
            this._table4Label.Location = new System.Drawing.Point(21, 148);
            this._table4Label.Name = "_table4Label";
            this._table4Label.Size = new System.Drawing.Size(60, 13);
            this._table4Label.TabIndex = 4;
            this._table4Label.Text = "Table No.4";
            // 
            // _tableNameLabel
            // 
            this._tableNameLabel.AutoSize = true;
            this._tableNameLabel.Location = new System.Drawing.Point(19, 37);
            this._tableNameLabel.Name = "_tableNameLabel";
            this._tableNameLabel.Size = new System.Drawing.Size(65, 13);
            this._tableNameLabel.TabIndex = 5;
            this._tableNameLabel.Text = "Table Name";
            // 
            // _tableStateLabel
            // 
            this._tableStateLabel.AutoSize = true;
            this._tableStateLabel.Location = new System.Drawing.Point(107, 37);
            this._tableStateLabel.Name = "_tableStateLabel";
            this._tableStateLabel.Size = new System.Drawing.Size(62, 13);
            this._tableStateLabel.TabIndex = 10;
            this._tableStateLabel.Text = "Table State";
            // 
            // _table4StateLabel
            // 
            this._table4StateLabel.AutoSize = true;
            this._table4StateLabel.Location = new System.Drawing.Point(121, 148);
            this._table4StateLabel.Name = "_table4StateLabel";
            this._table4StateLabel.Size = new System.Drawing.Size(33, 13);
            this._table4StateLabel.TabIndex = 9;
            this._table4StateLabel.Text = "None";
            // 
            // _table3StateLabel
            // 
            this._table3StateLabel.AutoSize = true;
            this._table3StateLabel.Location = new System.Drawing.Point(121, 120);
            this._table3StateLabel.Name = "_table3StateLabel";
            this._table3StateLabel.Size = new System.Drawing.Size(33, 13);
            this._table3StateLabel.TabIndex = 8;
            this._table3StateLabel.Text = "None";
            // 
            // _table2StateLabel
            // 
            this._table2StateLabel.AutoSize = true;
            this._table2StateLabel.Location = new System.Drawing.Point(121, 94);
            this._table2StateLabel.Name = "_table2StateLabel";
            this._table2StateLabel.Size = new System.Drawing.Size(33, 13);
            this._table2StateLabel.TabIndex = 7;
            this._table2StateLabel.Text = "None";
            // 
            // _table1StateLabel
            // 
            this._table1StateLabel.AutoSize = true;
            this._table1StateLabel.Location = new System.Drawing.Point(121, 65);
            this._table1StateLabel.Name = "_table1StateLabel";
            this._table1StateLabel.Size = new System.Drawing.Size(33, 13);
            this._table1StateLabel.TabIndex = 6;
            this._table1StateLabel.Text = "None";
            // 
            // _table1StateChangeButton
            // 
            this._table1StateChangeButton.Enabled = false;
            this._table1StateChangeButton.Location = new System.Drawing.Point(279, 60);
            this._table1StateChangeButton.Name = "_table1StateChangeButton";
            this._table1StateChangeButton.Size = new System.Drawing.Size(75, 23);
            this._table1StateChangeButton.TabIndex = 11;
            this._table1StateChangeButton.Text = "Change";
            this._table1StateChangeButton.UseVisualStyleBackColor = true;
            this._table1StateChangeButton.Click += new System.EventHandler(this.ClickTable1StateChangeButton);
            // 
            // _stateChangeLabel
            // 
            this._stateChangeLabel.AutoSize = true;
            this._stateChangeLabel.Location = new System.Drawing.Point(282, 37);
            this._stateChangeLabel.Name = "_stateChangeLabel";
            this._stateChangeLabel.Size = new System.Drawing.Size(72, 13);
            this._stateChangeLabel.TabIndex = 15;
            this._stateChangeLabel.Text = "State Change";
            // 
            // _table2StateChangeButton
            // 
            this._table2StateChangeButton.Enabled = false;
            this._table2StateChangeButton.Location = new System.Drawing.Point(279, 89);
            this._table2StateChangeButton.Name = "_table2StateChangeButton";
            this._table2StateChangeButton.Size = new System.Drawing.Size(75, 23);
            this._table2StateChangeButton.TabIndex = 16;
            this._table2StateChangeButton.Text = "Change";
            this._table2StateChangeButton.UseVisualStyleBackColor = true;
            this._table2StateChangeButton.Click += new System.EventHandler(this.ClickTable2StateChangeButton);
            // 
            // _table3StateChangeButton
            // 
            this._table3StateChangeButton.Enabled = false;
            this._table3StateChangeButton.Location = new System.Drawing.Point(279, 115);
            this._table3StateChangeButton.Name = "_table3StateChangeButton";
            this._table3StateChangeButton.Size = new System.Drawing.Size(75, 23);
            this._table3StateChangeButton.TabIndex = 17;
            this._table3StateChangeButton.Text = "Change";
            this._table3StateChangeButton.UseVisualStyleBackColor = true;
            this._table3StateChangeButton.Click += new System.EventHandler(this.ClickTable3StateChangeButton);
            // 
            // _table4StateChangeButton
            // 
            this._table4StateChangeButton.Enabled = false;
            this._table4StateChangeButton.Location = new System.Drawing.Point(279, 144);
            this._table4StateChangeButton.Name = "_table4StateChangeButton";
            this._table4StateChangeButton.Size = new System.Drawing.Size(75, 23);
            this._table4StateChangeButton.TabIndex = 18;
            this._table4StateChangeButton.Text = "Change";
            this._table4StateChangeButton.UseVisualStyleBackColor = true;
            this._table4StateChangeButton.Click += new System.EventHandler(this.ClickTable4StateChangeButton);
            // 
            // _tableAreaLabel
            // 
            this._tableAreaLabel.AutoSize = true;
            this._tableAreaLabel.Location = new System.Drawing.Point(189, 37);
            this._tableAreaLabel.Name = "_tableAreaLabel";
            this._tableAreaLabel.Size = new System.Drawing.Size(59, 13);
            this._tableAreaLabel.TabIndex = 33;
            this._tableAreaLabel.Text = "Table Area";
            // 
            // _table4AreaLabel
            // 
            this._table4AreaLabel.AutoSize = true;
            this._table4AreaLabel.Location = new System.Drawing.Point(191, 148);
            this._table4AreaLabel.Name = "_table4AreaLabel";
            this._table4AreaLabel.Size = new System.Drawing.Size(13, 13);
            this._table4AreaLabel.TabIndex = 32;
            this._table4AreaLabel.Text = "0";
            // 
            // _table3AreaLabel
            // 
            this._table3AreaLabel.AutoSize = true;
            this._table3AreaLabel.Location = new System.Drawing.Point(191, 120);
            this._table3AreaLabel.Name = "_table3AreaLabel";
            this._table3AreaLabel.Size = new System.Drawing.Size(13, 13);
            this._table3AreaLabel.TabIndex = 31;
            this._table3AreaLabel.Text = "0";
            // 
            // _table2AreaLabel
            // 
            this._table2AreaLabel.AutoSize = true;
            this._table2AreaLabel.Location = new System.Drawing.Point(191, 94);
            this._table2AreaLabel.Name = "_table2AreaLabel";
            this._table2AreaLabel.Size = new System.Drawing.Size(13, 13);
            this._table2AreaLabel.TabIndex = 30;
            this._table2AreaLabel.Text = "0";
            // 
            // _table1AreaLabel
            // 
            this._table1AreaLabel.AutoSize = true;
            this._table1AreaLabel.Location = new System.Drawing.Point(191, 65);
            this._table1AreaLabel.Name = "_table1AreaLabel";
            this._table1AreaLabel.Size = new System.Drawing.Size(13, 13);
            this._table1AreaLabel.TabIndex = 29;
            this._table1AreaLabel.Text = "0";
            // 
            // AnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 459);
            this.Controls.Add(this._tableGroupBox);
            this.Controls.Add(this._qrTextLabel);
            this.Controls.Add(this.button2);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label _qrTextLabel;
        private System.Windows.Forms.GroupBox _tableGroupBox;
        private System.Windows.Forms.Button _table4StateChangeButton;
        private System.Windows.Forms.Button _table3StateChangeButton;
        private System.Windows.Forms.Button _table2StateChangeButton;
        private System.Windows.Forms.Label _stateChangeLabel;
        private System.Windows.Forms.Button _table1StateChangeButton;
        private System.Windows.Forms.Label _tableStateLabel;
        private System.Windows.Forms.Label _table4StateLabel;
        private System.Windows.Forms.Label _table3StateLabel;
        private System.Windows.Forms.Label _table2StateLabel;
        private System.Windows.Forms.Label _table1StateLabel;
        private System.Windows.Forms.Label _tableNameLabel;
        private System.Windows.Forms.Label _table4Label;
        private System.Windows.Forms.Label _table3Label;
        private System.Windows.Forms.Label _table2Label;
        private System.Windows.Forms.Label _table1Label;
        private System.Windows.Forms.Label _tableAreaLabel;
        private System.Windows.Forms.Label _table4AreaLabel;
        private System.Windows.Forms.Label _table3AreaLabel;
        private System.Windows.Forms.Label _table2AreaLabel;
        private System.Windows.Forms.Label _table1AreaLabel;
    }
}

