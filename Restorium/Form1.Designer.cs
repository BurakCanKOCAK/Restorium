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
            this.bLogin = new System.Windows.Forms.PictureBox();
            this.bExit = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bExit)).BeginInit();
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
            this.bLogin.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.bLogin, "bLogin");
            this.bLogin.Name = "bLogin";
            this.bLogin.TabStop = false;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // bExit
            // 
            this.bExit.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.bExit, "bExit");
            this.bExit.Name = "bExit";
            this.bExit.TabStop = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // UserLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.bLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserLogin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.key_press);
            ((System.ComponentModel.ISupportInitialize)(this.bLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.PictureBox bLogin;
        private System.Windows.Forms.PictureBox bExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

