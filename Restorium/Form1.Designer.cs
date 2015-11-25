namespace Restorium
{
    partial class UserLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLogin));
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.bLogin = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUserName
            // 
            this.tbUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbUserName, "tbUserName");
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            this.tbUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.key_press);
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbPassword, "tbPassword");
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.key_press);
            // 
            // bLogin
            // 
            resources.ApplyResources(this.bLogin, "bLogin");
            this.bLogin.Name = "bLogin";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // bExit
            // 
            this.bExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.bExit, "bExit");
            this.bExit.Name = "bExit";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Restorium.Properties.Resources._580861_477789262242512_18934229_n;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // UserLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.BackgroundImage = global::Restorium.Properties.Resources.Untitled_11;
            this.CancelButton = this.bExit;
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.bLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UserLogin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UserLogin_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.key_press);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

