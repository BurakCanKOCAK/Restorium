namespace Restorium
{
    partial class Kasa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kasa));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbIslemAdi = new System.Windows.Forms.TextBox();
            this.tbAciklama = new System.Windows.Forms.TextBox();
            this.tbTutar = new System.Windows.Forms.TextBox();
            this.cbPersonel = new System.Windows.Forms.ComboBox();
            this.bKasaCancel = new System.Windows.Forms.Button();
            this.bKasaOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Personel :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Islem Adi :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(13, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tutar       :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Aciklama :";
            // 
            // tbIslemAdi
            // 
            this.tbIslemAdi.Location = new System.Drawing.Point(104, 66);
            this.tbIslemAdi.Name = "tbIslemAdi";
            this.tbIslemAdi.Size = new System.Drawing.Size(291, 20);
            this.tbIslemAdi.TabIndex = 7;
            // 
            // tbAciklama
            // 
            this.tbAciklama.Location = new System.Drawing.Point(104, 102);
            this.tbAciklama.Name = "tbAciklama";
            this.tbAciklama.Size = new System.Drawing.Size(291, 20);
            this.tbAciklama.TabIndex = 8;
            // 
            // tbTutar
            // 
            this.tbTutar.Location = new System.Drawing.Point(104, 138);
            this.tbTutar.Name = "tbTutar";
            this.tbTutar.Size = new System.Drawing.Size(291, 20);
            this.tbTutar.TabIndex = 9;
            // 
            // cbPersonel
            // 
            this.cbPersonel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPersonel.FormattingEnabled = true;
            this.cbPersonel.Location = new System.Drawing.Point(104, 30);
            this.cbPersonel.Name = "cbPersonel";
            this.cbPersonel.Size = new System.Drawing.Size(290, 21);
            this.cbPersonel.TabIndex = 10;
            // 
            // bKasaCancel
            // 
            this.bKasaCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bKasaCancel.Image = global::Restorium.Properties.Resources.circle__7_;
            this.bKasaCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bKasaCancel.Location = new System.Drawing.Point(283, 193);
            this.bKasaCancel.Name = "bKasaCancel";
            this.bKasaCancel.Size = new System.Drawing.Size(112, 27);
            this.bKasaCancel.TabIndex = 5;
            this.bKasaCancel.Text = "Iptal";
            this.bKasaCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bKasaCancel.UseVisualStyleBackColor = true;
            this.bKasaCancel.Click += new System.EventHandler(this.bKasaCancel_Click);
            // 
            // bKasaOK
            // 
            this.bKasaOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bKasaOK.Image = ((System.Drawing.Image)(resources.GetObject("bKasaOK.Image")));
            this.bKasaOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bKasaOK.Location = new System.Drawing.Point(17, 193);
            this.bKasaOK.Name = "bKasaOK";
            this.bKasaOK.Size = new System.Drawing.Size(113, 27);
            this.bKasaOK.TabIndex = 3;
            this.bKasaOK.Text = "Tamam";
            this.bKasaOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bKasaOK.UseVisualStyleBackColor = true;
            this.bKasaOK.Click += new System.EventHandler(this.bKasaOK_Click);
            // 
            // Kasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 230);
            this.Controls.Add(this.cbPersonel);
            this.Controls.Add(this.tbTutar);
            this.Controls.Add(this.tbAciklama);
            this.Controls.Add(this.tbIslemAdi);
            this.Controls.Add(this.bKasaCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bKasaOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Kasa";
            this.Text = "Kasa";
            this.Load += new System.EventHandler(this.Kasa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bKasaOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bKasaCancel;
        private System.Windows.Forms.TextBox tbIslemAdi;
        private System.Windows.Forms.TextBox tbAciklama;
        private System.Windows.Forms.TextBox tbTutar;
        private System.Windows.Forms.ComboBox cbPersonel;
    }
}