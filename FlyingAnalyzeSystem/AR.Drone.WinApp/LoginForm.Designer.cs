namespace AR.Drone.WinApp
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.button1 = new System.Windows.Forms.Button();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.loginPanel1 = new AR.Drone.WinApp.MyUserControl.LoginPanel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.passwordTexBox1 = new AR.Drone.WinApp.MyUserControl.PasswordTexBox();
            this._accountElementHost = new System.Windows.Forms.Integration.ElementHost();
            this._accountTextBox = new AR.Drone.WinApp.MyUserControl.AccountTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // elementHost2
            // 
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(0, 0);
            this.elementHost2.Margin = new System.Windows.Forms.Padding(0);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(728, 388);
            this.elementHost2.TabIndex = 5;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.loginPanel1;
            // 
            // elementHost1
            // 
            this.elementHost1.BackColor = System.Drawing.Color.Transparent;
            this.elementHost1.Location = new System.Drawing.Point(258, 214);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 30);
            this.elementHost1.TabIndex = 4;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.passwordTexBox1;
            // 
            // _accountElementHost
            // 
            this._accountElementHost.BackColor = System.Drawing.Color.Transparent;
            this._accountElementHost.Location = new System.Drawing.Point(258, 163);
            this._accountElementHost.Name = "_accountElementHost";
            this._accountElementHost.Size = new System.Drawing.Size(200, 30);
            this._accountElementHost.TabIndex = 3;
            this._accountElementHost.Text = "elementHost1";
            this._accountElementHost.Child = this._accountTextBox;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(728, 388);
            this.Controls.Add(this.elementHost2);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this._accountElementHost);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Integration.ElementHost _accountElementHost;
        private MyUserControl.AccountTextBox _accountTextBox;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private MyUserControl.PasswordTexBox passwordTexBox1;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private MyUserControl.LoginPanel loginPanel1;








    }
}