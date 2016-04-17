namespace Restorium
{
    partial class SoftwareActivation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftwareActivation));
            this.lInternalIpAddress = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pbStatusBar = new System.Windows.Forms.ProgressBar();
            this.pbStage3 = new System.Windows.Forms.PictureBox();
            this.pbStage2 = new System.Windows.Forms.PictureBox();
            this.pbStage1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pbNoRemoteConnection = new System.Windows.Forms.PictureBox();
            this.pbRemoteConnection = new System.Windows.Forms.PictureBox();
            this.pbNoWifi = new System.Windows.Forms.PictureBox();
            this.pbWifiStatus = new System.Windows.Forms.PictureBox();
            this.lExternalIpAddress = new System.Windows.Forms.Label();
            this.timerPublicIp = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNoRemoteConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemoteConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNoWifi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWifiStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // lInternalIpAddress
            // 
            this.lInternalIpAddress.AutoSize = true;
            this.lInternalIpAddress.Font = new System.Drawing.Font("NeueHaasGroteskText Pro", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lInternalIpAddress.Location = new System.Drawing.Point(12, 204);
            this.lInternalIpAddress.Name = "lInternalIpAddress";
            this.lInternalIpAddress.Size = new System.Drawing.Size(93, 16);
            this.lInternalIpAddress.TabIndex = 2;
            this.lInternalIpAddress.Text = "Internal IP   : -";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timerTick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("NeueHaasGroteskText Pro", 9.749999F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(308, 200);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(147, 20);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Manuel Aktivasyon";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pbNoWifi);
            this.panel1.Controls.Add(this.pbWifiStatus);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(131, 116);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NeueHaasGroteskText Pro", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-7, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(853, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pbNoRemoteConnection);
            this.panel2.Controls.Add(this.pbRemoteConnection);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(168, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 116);
            this.panel2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NeueHaasGroteskText Pro", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "2";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.pictureBox5);
            this.panel3.Location = new System.Drawing.Point(324, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(131, 116);
            this.panel3.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NeueHaasGroteskText Pro", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "3";
            // 
            // pbStatusBar
            // 
            this.pbStatusBar.ForeColor = System.Drawing.Color.YellowGreen;
            this.pbStatusBar.Location = new System.Drawing.Point(-4, 134);
            this.pbStatusBar.Name = "pbStatusBar";
            this.pbStatusBar.Size = new System.Drawing.Size(469, 12);
            this.pbStatusBar.TabIndex = 10;
            // 
            // pbStage3
            // 
            this.pbStage3.BackgroundImage = global::Restorium.Properties.Resources.check__3_;
            this.pbStage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbStage3.Location = new System.Drawing.Point(380, 152);
            this.pbStage3.Name = "pbStage3";
            this.pbStage3.Size = new System.Drawing.Size(27, 29);
            this.pbStage3.TabIndex = 12;
            this.pbStage3.TabStop = false;
            this.pbStage3.Visible = false;
            // 
            // pbStage2
            // 
            this.pbStage2.BackgroundImage = global::Restorium.Properties.Resources.check__3_;
            this.pbStage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbStage2.Location = new System.Drawing.Point(221, 152);
            this.pbStage2.Name = "pbStage2";
            this.pbStage2.Size = new System.Drawing.Size(27, 29);
            this.pbStage2.TabIndex = 11;
            this.pbStage2.TabStop = false;
            this.pbStage2.Visible = false;
            // 
            // pbStage1
            // 
            this.pbStage1.BackgroundImage = global::Restorium.Properties.Resources.check__3_;
            this.pbStage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbStage1.Location = new System.Drawing.Point(65, 152);
            this.pbStage1.Name = "pbStage1";
            this.pbStage1.Size = new System.Drawing.Size(27, 29);
            this.pbStage1.TabIndex = 4;
            this.pbStage1.TabStop = false;
            this.pbStage1.Visible = false;
            this.pbStage1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(23, 19);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 34);
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox5.Location = new System.Drawing.Point(33, 33);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(72, 55);
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // pbNoRemoteConnection
            // 
            this.pbNoRemoteConnection.BackColor = System.Drawing.Color.Transparent;
            this.pbNoRemoteConnection.BackgroundImage = global::Restorium.Properties.Resources.exclamation__4_;
            this.pbNoRemoteConnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbNoRemoteConnection.Location = new System.Drawing.Point(21, 19);
            this.pbNoRemoteConnection.Name = "pbNoRemoteConnection";
            this.pbNoRemoteConnection.Size = new System.Drawing.Size(35, 34);
            this.pbNoRemoteConnection.TabIndex = 1;
            this.pbNoRemoteConnection.TabStop = false;
            // 
            // pbRemoteConnection
            // 
            this.pbRemoteConnection.BackgroundImage = global::Restorium.Properties.Resources.REMOTE_BLACK_CLOUD;
            this.pbRemoteConnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbRemoteConnection.Location = new System.Drawing.Point(31, 33);
            this.pbRemoteConnection.Name = "pbRemoteConnection";
            this.pbRemoteConnection.Size = new System.Drawing.Size(72, 55);
            this.pbRemoteConnection.TabIndex = 0;
            this.pbRemoteConnection.TabStop = false;
            // 
            // pbNoWifi
            // 
            this.pbNoWifi.BackColor = System.Drawing.Color.Transparent;
            this.pbNoWifi.BackgroundImage = global::Restorium.Properties.Resources.exclamation__4_;
            this.pbNoWifi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbNoWifi.Location = new System.Drawing.Point(21, 19);
            this.pbNoWifi.Name = "pbNoWifi";
            this.pbNoWifi.Size = new System.Drawing.Size(38, 34);
            this.pbNoWifi.TabIndex = 1;
            this.pbNoWifi.TabStop = false;
            // 
            // pbWifiStatus
            // 
            this.pbWifiStatus.BackgroundImage = global::Restorium.Properties.Resources.WIFI_BLACK;
            this.pbWifiStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbWifiStatus.Location = new System.Drawing.Point(31, 33);
            this.pbWifiStatus.Name = "pbWifiStatus";
            this.pbWifiStatus.Size = new System.Drawing.Size(72, 55);
            this.pbWifiStatus.TabIndex = 0;
            this.pbWifiStatus.TabStop = false;
            // 
            // lExternalIpAddress
            // 
            this.lExternalIpAddress.AutoSize = true;
            this.lExternalIpAddress.Font = new System.Drawing.Font("NeueHaasGroteskText Pro", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lExternalIpAddress.Location = new System.Drawing.Point(12, 223);
            this.lExternalIpAddress.Name = "lExternalIpAddress";
            this.lExternalIpAddress.Size = new System.Drawing.Size(92, 16);
            this.lExternalIpAddress.TabIndex = 13;
            this.lExternalIpAddress.Text = "External IP : -";
            // 
            // timerPublicIp
            // 
            this.timerPublicIp.Enabled = true;
            this.timerPublicIp.Interval = 10000;
            this.timerPublicIp.Tick += new System.EventHandler(this.timerTickPublic);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-7, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(853, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // SoftwareActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 329);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lExternalIpAddress);
            this.Controls.Add(this.pbStage3);
            this.Controls.Add(this.pbStage2);
            this.Controls.Add(this.pbStatusBar);
            this.Controls.Add(this.pbStage1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lInternalIpAddress);
            this.Name = "SoftwareActivation";
            this.Text = "Software Activation";
            this.Load += new System.EventHandler(this.SoftwareActivation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNoRemoteConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemoteConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNoWifi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWifiStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWifiStatus;
        private System.Windows.Forms.PictureBox pbNoWifi;
        private System.Windows.Forms.Label lInternalIpAddress;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pbStage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbNoRemoteConnection;
        private System.Windows.Forms.PictureBox pbRemoteConnection;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.ProgressBar pbStatusBar;
        private System.Windows.Forms.PictureBox pbStage2;
        private System.Windows.Forms.PictureBox pbStage3;
        private System.Windows.Forms.Label lExternalIpAddress;
        private System.Windows.Forms.Timer timerPublicIp;
        private System.Windows.Forms.Label label5;
    }
}