namespace Restorium
{
    partial class UserSettings
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
            this.lKullaniciTuru = new System.Windows.Forms.Label();
            this.bKasaCancel = new System.Windows.Forms.Button();
            this.bKasaOK = new System.Windows.Forms.Button();
            this.tbKullaniciAdi = new System.Windows.Forms.TextBox();
            this.tbEskiSifre = new System.Windows.Forms.TextBox();
            this.tbYeniSifre = new System.Windows.Forms.TextBox();
            this.tbYeniSifreTekrar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bExit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbYeniKullaniciAdi = new System.Windows.Forms.TextBox();
            this.pbCheckPass = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckPass)).BeginInit();
            this.SuspendLayout();
            // 
            // lKullaniciTuru
            // 
            this.lKullaniciTuru.AutoSize = true;
            this.lKullaniciTuru.BackColor = System.Drawing.Color.Transparent;
            this.lKullaniciTuru.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.lKullaniciTuru.Location = new System.Drawing.Point(12, 9);
            this.lKullaniciTuru.Name = "lKullaniciTuru";
            this.lKullaniciTuru.Size = new System.Drawing.Size(139, 24);
            this.lKullaniciTuru.TabIndex = 0;
            this.lKullaniciTuru.Text = "Kullanici Turu";
            // 
            // bKasaCancel
            // 
            this.bKasaCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bKasaCancel.Image = global::Restorium.Properties.Resources.circle__7_;
            this.bKasaCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bKasaCancel.Location = new System.Drawing.Point(288, 216);
            this.bKasaCancel.Name = "bKasaCancel";
            this.bKasaCancel.Size = new System.Drawing.Size(100, 27);
            this.bKasaCancel.TabIndex = 7;
            this.bKasaCancel.Text = "Iptal";
            this.bKasaCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bKasaCancel.UseVisualStyleBackColor = true;
            this.bKasaCancel.Click += new System.EventHandler(this.bKasaCancel_Click);
            // 
            // bKasaOK
            // 
            this.bKasaOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bKasaOK.Image = global::Restorium.Properties.Resources.check__2_1;
            this.bKasaOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bKasaOK.Location = new System.Drawing.Point(16, 216);
            this.bKasaOK.Name = "bKasaOK";
            this.bKasaOK.Size = new System.Drawing.Size(100, 27);
            this.bKasaOK.TabIndex = 6;
            this.bKasaOK.Text = "Tamam";
            this.bKasaOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bKasaOK.UseVisualStyleBackColor = true;
            this.bKasaOK.Click += new System.EventHandler(this.bKasaOK_Click);
            // 
            // tbKullaniciAdi
            // 
            this.tbKullaniciAdi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbKullaniciAdi.Location = new System.Drawing.Point(160, 53);
            this.tbKullaniciAdi.Name = "tbKullaniciAdi";
            this.tbKullaniciAdi.Size = new System.Drawing.Size(228, 13);
            this.tbKullaniciAdi.TabIndex = 8;
            this.tbKullaniciAdi.TextChanged += new System.EventHandler(this.tbKullaniciAdi_TextChanged);
            // 
            // tbEskiSifre
            // 
            this.tbEskiSifre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbEskiSifre.Location = new System.Drawing.Point(160, 82);
            this.tbEskiSifre.Name = "tbEskiSifre";
            this.tbEskiSifre.PasswordChar = '*';
            this.tbEskiSifre.Size = new System.Drawing.Size(228, 13);
            this.tbEskiSifre.TabIndex = 9;
            this.tbEskiSifre.UseSystemPasswordChar = true;
            // 
            // tbYeniSifre
            // 
            this.tbYeniSifre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbYeniSifre.Location = new System.Drawing.Point(160, 139);
            this.tbYeniSifre.Name = "tbYeniSifre";
            this.tbYeniSifre.PasswordChar = '*';
            this.tbYeniSifre.Size = new System.Drawing.Size(228, 13);
            this.tbYeniSifre.TabIndex = 10;
            this.tbYeniSifre.UseSystemPasswordChar = true;
            this.tbYeniSifre.TextChanged += new System.EventHandler(this.PassMatch);
            // 
            // tbYeniSifreTekrar
            // 
            this.tbYeniSifreTekrar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbYeniSifreTekrar.Location = new System.Drawing.Point(160, 165);
            this.tbYeniSifreTekrar.Name = "tbYeniSifreTekrar";
            this.tbYeniSifreTekrar.PasswordChar = '*';
            this.tbYeniSifreTekrar.Size = new System.Drawing.Size(228, 13);
            this.tbYeniSifreTekrar.TabIndex = 11;
            this.tbYeniSifreTekrar.UseSystemPasswordChar = true;
            this.tbYeniSifreTekrar.TextChanged += new System.EventHandler(this.PassMatch);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Kullanici Adi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Eski Sifre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(13, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Yeni Sifre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Yeni Sifre Tekrar";
            // 
            // bExit
            // 
            this.bExit.BackColor = System.Drawing.Color.Red;
            this.bExit.BackgroundImage = global::Restorium.Properties.Resources.sign_error_icon1;
            this.bExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bExit.Location = new System.Drawing.Point(368, 9);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(29, 24);
            this.bExit.TabIndex = 16;
            this.bExit.UseVisualStyleBackColor = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(13, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Yeni Kullanici Adi";
            // 
            // tbYeniKullaniciAdi
            // 
            this.tbYeniKullaniciAdi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbYeniKullaniciAdi.Location = new System.Drawing.Point(160, 112);
            this.tbYeniKullaniciAdi.Name = "tbYeniKullaniciAdi";
            this.tbYeniKullaniciAdi.Size = new System.Drawing.Size(228, 13);
            this.tbYeniKullaniciAdi.TabIndex = 18;
            // 
            // pbCheckPass
            // 
            this.pbCheckPass.BackColor = System.Drawing.Color.Transparent;
            this.pbCheckPass.Image = global::Restorium.Properties.Resources.check__2_1;
            this.pbCheckPass.Location = new System.Drawing.Point(359, 184);
            this.pbCheckPass.Name = "pbCheckPass";
            this.pbCheckPass.Size = new System.Drawing.Size(29, 16);
            this.pbCheckPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCheckPass.TabIndex = 19;
            this.pbCheckPass.TabStop = false;
            this.pbCheckPass.Visible = false;
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Restorium.Properties.Resources._30_display;
            this.ClientSize = new System.Drawing.Size(409, 255);
            this.Controls.Add(this.pbCheckPass);
            this.Controls.Add(this.tbYeniKullaniciAdi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbYeniSifreTekrar);
            this.Controls.Add(this.tbYeniSifre);
            this.Controls.Add(this.tbEskiSifre);
            this.Controls.Add(this.tbKullaniciAdi);
            this.Controls.Add(this.bKasaCancel);
            this.Controls.Add(this.bKasaOK);
            this.Controls.Add(this.lKullaniciTuru);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserSettings";
            this.Load += new System.EventHandler(this.UserSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckPass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lKullaniciTuru;
        private System.Windows.Forms.Button bKasaCancel;
        private System.Windows.Forms.Button bKasaOK;
        private System.Windows.Forms.TextBox tbKullaniciAdi;
        private System.Windows.Forms.TextBox tbEskiSifre;
        private System.Windows.Forms.TextBox tbYeniSifre;
        private System.Windows.Forms.TextBox tbYeniSifreTekrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbYeniKullaniciAdi;
        private System.Windows.Forms.PictureBox pbCheckPass;
    }
}