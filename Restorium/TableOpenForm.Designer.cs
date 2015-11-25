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
            this.SuspendLayout();
            // 
            // bMasaAc
            // 
            this.bMasaAc.Location = new System.Drawing.Point(13, 275);
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
            this.bIptal.Location = new System.Drawing.Point(329, 275);
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
            this.tbMusteri.Location = new System.Drawing.Point(173, 71);
            this.tbMusteri.Name = "tbMusteri";
            this.tbMusteri.Size = new System.Drawing.Size(224, 20);
            this.tbMusteri.TabIndex = 3;
            this.tbMusteri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // tbIskonto
            // 
            this.tbIskonto.Location = new System.Drawing.Point(173, 124);
            this.tbIskonto.Name = "tbIskonto";
            this.tbIskonto.Size = new System.Drawing.Size(60, 20);
            this.tbIskonto.TabIndex = 4;
            this.tbIskonto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // tbPersonelAdi
            // 
            this.tbPersonelAdi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbPersonelAdi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbPersonelAdi.Location = new System.Drawing.Point(173, 181);
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
            this.label2.Location = new System.Drawing.Point(21, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Musteri :  ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Iskonto : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Personel Adi : ";
            // 
            // bPersonelAdiListe
            // 
            this.bPersonelAdiListe.Location = new System.Drawing.Point(403, 178);
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
            this.label5.Location = new System.Drawing.Point(239, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "%";
            // 
            // TableOpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 310);
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
    }
}