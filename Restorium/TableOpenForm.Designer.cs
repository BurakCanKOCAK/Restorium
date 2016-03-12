namespace Restorium
{
    partial class TableOpenForm
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
            this.bMasaAc = new System.Windows.Forms.Button();
            this.bIptal = new System.Windows.Forms.Button();
            this.tbMasaNo = new System.Windows.Forms.TextBox();
            this.tbMusteri = new System.Windows.Forms.TextBox();
            this.tbIskonto = new System.Windows.Forms.TextBox();
            this.tbPersonelAdi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bPersonelAdiListe = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.gbRezervasyonSettings = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rLimitDakika = new System.Windows.Forms.NumericUpDown();
            this.rLimitSaat = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rTarih = new System.Windows.Forms.DateTimePicker();
            this.rSaat = new System.Windows.Forms.DateTimePicker();
            this.rDakika = new System.Windows.Forms.DateTimePicker();
            this.gbRezervasyonSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rLimitDakika)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rLimitSaat)).BeginInit();
            this.SuspendLayout();
            // 
            // bMasaAc
            // 
            this.bMasaAc.Location = new System.Drawing.Point(13, 300);
            this.bMasaAc.Name = "bMasaAc";
            this.bMasaAc.Size = new System.Drawing.Size(112, 23);
            this.bMasaAc.TabIndex = 0;
            this.bMasaAc.Text = "Masa Ac";
            this.bMasaAc.UseVisualStyleBackColor = true;
            this.bMasaAc.Click += new System.EventHandler(this.bMasaAc_Click);
            this.bMasaAc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // bIptal
            // 
            this.bIptal.Location = new System.Drawing.Point(329, 300);
            this.bIptal.Name = "bIptal";
            this.bIptal.Size = new System.Drawing.Size(113, 23);
            this.bIptal.TabIndex = 1;
            this.bIptal.Text = "Iptal";
            this.bIptal.UseVisualStyleBackColor = true;
            this.bIptal.Click += new System.EventHandler(this.bIptal_Click);
            this.bIptal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // tbMasaNo
            // 
            this.tbMasaNo.Location = new System.Drawing.Point(173, 24);
            this.tbMasaNo.Name = "tbMasaNo";
            this.tbMasaNo.Size = new System.Drawing.Size(224, 20);
            this.tbMasaNo.TabIndex = 2;
            this.tbMasaNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // tbMusteri
            // 
            this.tbMusteri.Location = new System.Drawing.Point(173, 50);
            this.tbMusteri.Name = "tbMusteri";
            this.tbMusteri.Size = new System.Drawing.Size(224, 20);
            this.tbMusteri.TabIndex = 3;
            this.tbMusteri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // tbIskonto
            // 
            this.tbIskonto.Location = new System.Drawing.Point(173, 76);
            this.tbIskonto.Name = "tbIskonto";
            this.tbIskonto.Size = new System.Drawing.Size(60, 20);
            this.tbIskonto.TabIndex = 4;
            this.tbIskonto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // tbPersonelAdi
            // 
            this.tbPersonelAdi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbPersonelAdi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbPersonelAdi.Location = new System.Drawing.Point(173, 102);
            this.tbPersonelAdi.Name = "tbPersonelAdi";
            this.tbPersonelAdi.Size = new System.Drawing.Size(224, 20);
            this.tbPersonelAdi.TabIndex = 5;
            this.tbPersonelAdi.TextChanged += new System.EventHandler(this.tbPersonelAdi_TextChanged);
            this.tbPersonelAdi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Masa Numarasi : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Musteri :  ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Iskonto : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Personel Adi : ";
            // 
            // bPersonelAdiListe
            // 
            this.bPersonelAdiListe.Location = new System.Drawing.Point(403, 99);
            this.bPersonelAdiListe.Name = "bPersonelAdiListe";
            this.bPersonelAdiListe.Size = new System.Drawing.Size(38, 23);
            this.bPersonelAdiListe.TabIndex = 10;
            this.bPersonelAdiListe.Text = "Liste";
            this.bPersonelAdiListe.UseVisualStyleBackColor = true;
            this.bPersonelAdiListe.Click += new System.EventHandler(this.bPersonelAdiListe_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(239, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "%";
            // 
            // gbRezervasyonSettings
            // 
            this.gbRezervasyonSettings.BackColor = System.Drawing.Color.Turquoise;
            this.gbRezervasyonSettings.Controls.Add(this.rDakika);
            this.gbRezervasyonSettings.Controls.Add(this.rSaat);
            this.gbRezervasyonSettings.Controls.Add(this.label11);
            this.gbRezervasyonSettings.Controls.Add(this.label10);
            this.gbRezervasyonSettings.Controls.Add(this.rLimitDakika);
            this.gbRezervasyonSettings.Controls.Add(this.rLimitSaat);
            this.gbRezervasyonSettings.Controls.Add(this.label9);
            this.gbRezervasyonSettings.Controls.Add(this.label8);
            this.gbRezervasyonSettings.Controls.Add(this.label7);
            this.gbRezervasyonSettings.Controls.Add(this.label6);
            this.gbRezervasyonSettings.Controls.Add(this.rTarih);
            this.gbRezervasyonSettings.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRezervasyonSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbRezervasyonSettings.Location = new System.Drawing.Point(13, 157);
            this.gbRezervasyonSettings.Name = "gbRezervasyonSettings";
            this.gbRezervasyonSettings.Size = new System.Drawing.Size(428, 137);
            this.gbRezervasyonSettings.TabIndex = 12;
            this.gbRezervasyonSettings.TabStop = false;
            this.gbRezervasyonSettings.Text = "Rezervasyon Ayarlari";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(175, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 18);
            this.label11.TabIndex = 11;
            this.label11.Text = "Saat ve";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.Location = new System.Drawing.Point(280, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 18);
            this.label10.TabIndex = 10;
            this.label10.Text = "Dakika ";
            // 
            // rLimitDakika
            // 
            this.rLimitDakika.Location = new System.Drawing.Point(234, 100);
            this.rLimitDakika.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.rLimitDakika.Name = "rLimitDakika";
            this.rLimitDakika.Size = new System.Drawing.Size(45, 26);
            this.rLimitDakika.TabIndex = 9;
            // 
            // rLimitSaat
            // 
            this.rLimitSaat.Location = new System.Drawing.Point(124, 100);
            this.rLimitSaat.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.rLimitSaat.Name = "rLimitSaat";
            this.rLimitSaat.Size = new System.Drawing.Size(45, 26);
            this.rLimitSaat.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 10.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label9.Location = new System.Drawing.Point(18, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(391, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "Rezervasyon saatinden once kullanima kapatma suresi belirleyiniz :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(118, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = ":";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(233, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 18);
            this.label7.TabIndex = 5;
            this.label7.Text = "Tarih :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Saat :";
            // 
            // rTarih
            // 
            this.rTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rTarih.Location = new System.Drawing.Point(292, 29);
            this.rTarih.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.rTarih.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.rTarih.Name = "rTarih";
            this.rTarih.Size = new System.Drawing.Size(117, 26);
            this.rTarih.TabIndex = 1;
            this.rTarih.Value = new System.DateTime(2015, 12, 13, 22, 58, 2, 0);
            // 
            // rSaat
            // 
            this.rSaat.CustomFormat = "HH";
            this.rSaat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rSaat.Location = new System.Drawing.Point(64, 29);
            this.rSaat.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.rSaat.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.rSaat.Name = "rSaat";
            this.rSaat.ShowUpDown = true;
            this.rSaat.Size = new System.Drawing.Size(48, 26);
            this.rSaat.TabIndex = 12;
            this.rSaat.Value = new System.DateTime(2015, 12, 13, 22, 58, 2, 0);
            // 
            // rDakika
            // 
            this.rDakika.CustomFormat = "HH";
            this.rDakika.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rDakika.Location = new System.Drawing.Point(136, 29);
            this.rDakika.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.rDakika.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.rDakika.Name = "rDakika";
            this.rDakika.ShowUpDown = true;
            this.rDakika.Size = new System.Drawing.Size(48, 26);
            this.rDakika.TabIndex = 13;
            this.rDakika.Value = new System.DateTime(2015, 12, 13, 22, 58, 2, 0);
            // 
            // TableOpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 335);
            this.Controls.Add(this.gbRezervasyonSettings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bPersonelAdiListe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPersonelAdi);
            this.Controls.Add(this.tbIskonto);
            this.Controls.Add(this.tbMusteri);
            this.Controls.Add(this.tbMasaNo);
            this.Controls.Add(this.bIptal);
            this.Controls.Add(this.bMasaAc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TableOpenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni Masa";
            this.Load += new System.EventHandler(this.TableOpenForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            this.gbRezervasyonSettings.ResumeLayout(false);
            this.gbRezervasyonSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rLimitDakika)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rLimitSaat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bMasaAc;
        private System.Windows.Forms.Button bIptal;
        private System.Windows.Forms.TextBox tbMasaNo;
        private System.Windows.Forms.TextBox tbMusteri;
        private System.Windows.Forms.TextBox tbIskonto;
        private System.Windows.Forms.TextBox tbPersonelAdi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bPersonelAdiListe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbRezervasyonSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker rTarih;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown rLimitDakika;
        private System.Windows.Forms.NumericUpDown rLimitSaat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker rSaat;
        private System.Windows.Forms.DateTimePicker rDakika;
    }
}