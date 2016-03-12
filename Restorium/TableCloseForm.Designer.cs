namespace Restorium
{
    partial class TableCloseForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lTip = new System.Windows.Forms.Label();
            this.cbTip = new System.Windows.Forms.CheckBox();
            this.cbGBP = new System.Windows.Forms.CheckBox();
            this.cbDolar = new System.Windows.Forms.CheckBox();
            this.cbEuro = new System.Windows.Forms.CheckBox();
            this.cbTL = new System.Windows.Forms.CheckBox();
            this.tbCari = new System.Windows.Forms.TextBox();
            this.labelCari = new System.Windows.Forms.Label();
            this.labelNakit = new System.Windows.Forms.Label();
            this.tbNakit = new System.Windows.Forms.TextBox();
            this.labelKredi = new System.Windows.Forms.Label();
            this.tbKredi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lTL = new System.Windows.Forms.Label();
            this.lEuro = new System.Windows.Forms.Label();
            this.lDolar = new System.Windows.Forms.Label();
            this.lGBP = new System.Windows.Forms.Label();
            this.bMasaKapat = new System.Windows.Forms.Button();
            this.bIptal = new System.Windows.Forms.Button();
            this.lIskontoOrani = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.lKalan = new System.Windows.Forms.Label();
            this.labelKalan = new System.Windows.Forms.Label();
            this.lTableName = new System.Windows.Forms.Label();
            this.pbCalculator = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalculator)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lTip);
            this.panel1.Controls.Add(this.cbTip);
            this.panel1.Controls.Add(this.cbGBP);
            this.panel1.Controls.Add(this.cbDolar);
            this.panel1.Controls.Add(this.cbEuro);
            this.panel1.Controls.Add(this.cbTL);
            this.panel1.Controls.Add(this.tbCari);
            this.panel1.Controls.Add(this.labelCari);
            this.panel1.Controls.Add(this.labelNakit);
            this.panel1.Controls.Add(this.tbNakit);
            this.panel1.Controls.Add(this.labelKredi);
            this.panel1.Controls.Add(this.tbKredi);
            this.panel1.Location = new System.Drawing.Point(0, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 102);
            this.panel1.TabIndex = 0;
            // 
            // lTip
            // 
            this.lTip.AutoSize = true;
            this.lTip.Location = new System.Drawing.Point(341, 23);
            this.lTip.Name = "lTip";
            this.lTip.Size = new System.Drawing.Size(31, 13);
            this.lTip.TabIndex = 11;
            this.lTip.Text = "Tip : ";
            this.lTip.Visible = false;
            // 
            // cbTip
            // 
            this.cbTip.AutoSize = true;
            this.cbTip.Location = new System.Drawing.Point(328, 3);
            this.cbTip.Name = "cbTip";
            this.cbTip.Size = new System.Drawing.Size(140, 17);
            this.cbTip.TabIndex = 10;
            this.cbTip.Text = "Para ustunu tip olarak al";
            this.cbTip.UseVisualStyleBackColor = true;
            this.cbTip.CheckStateChanged += new System.EventHandler(this.cbTipCheckStateChanged);
            // 
            // cbGBP
            // 
            this.cbGBP.AutoSize = true;
            this.cbGBP.Location = new System.Drawing.Point(149, 3);
            this.cbGBP.Name = "cbGBP";
            this.cbGBP.Size = new System.Drawing.Size(32, 17);
            this.cbGBP.TabIndex = 9;
            this.cbGBP.Text = "£";
            this.cbGBP.UseVisualStyleBackColor = true;
            this.cbGBP.CheckStateChanged += new System.EventHandler(this.ExchangeCalculateGBP);
            // 
            // cbDolar
            // 
            this.cbDolar.AutoSize = true;
            this.cbDolar.Location = new System.Drawing.Point(104, 3);
            this.cbDolar.Name = "cbDolar";
            this.cbDolar.Size = new System.Drawing.Size(32, 17);
            this.cbDolar.TabIndex = 8;
            this.cbDolar.Text = "$";
            this.cbDolar.UseVisualStyleBackColor = true;
            this.cbDolar.CheckStateChanged += new System.EventHandler(this.ExchangeCalculateDOLAR);
            // 
            // cbEuro
            // 
            this.cbEuro.AutoSize = true;
            this.cbEuro.Location = new System.Drawing.Point(59, 3);
            this.cbEuro.Name = "cbEuro";
            this.cbEuro.Size = new System.Drawing.Size(32, 17);
            this.cbEuro.TabIndex = 7;
            this.cbEuro.Text = "€";
            this.cbEuro.UseVisualStyleBackColor = true;
            this.cbEuro.CheckStateChanged += new System.EventHandler(this.ExchangeCalculateEURO);
            // 
            // cbTL
            // 
            this.cbTL.AutoSize = true;
            this.cbTL.Checked = true;
            this.cbTL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTL.Location = new System.Drawing.Point(12, 3);
            this.cbTL.Name = "cbTL";
            this.cbTL.Size = new System.Drawing.Size(32, 17);
            this.cbTL.TabIndex = 6;
            this.cbTL.Text = "₺";
            this.cbTL.UseVisualStyleBackColor = true;
            this.cbTL.CheckStateChanged += new System.EventHandler(this.ExchangeCalculateTL);
            // 
            // tbCari
            // 
            this.tbCari.Location = new System.Drawing.Point(344, 72);
            this.tbCari.Name = "tbCari";
            this.tbCari.Size = new System.Drawing.Size(124, 20);
            this.tbCari.TabIndex = 4;
            this.tbCari.TextChanged += new System.EventHandler(this.CalculateKalanTutar);
            // 
            // labelCari
            // 
            this.labelCari.AutoSize = true;
            this.labelCari.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCari.Location = new System.Drawing.Point(341, 51);
            this.labelCari.Name = "labelCari";
            this.labelCari.Size = new System.Drawing.Size(53, 18);
            this.labelCari.TabIndex = 5;
            this.labelCari.Text = "Cari (₺)";
            // 
            // labelNakit
            // 
            this.labelNakit.AutoSize = true;
            this.labelNakit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNakit.Location = new System.Drawing.Point(9, 51);
            this.labelNakit.Name = "labelNakit";
            this.labelNakit.Size = new System.Drawing.Size(62, 18);
            this.labelNakit.TabIndex = 3;
            this.labelNakit.Text = "Nakit (₺)";
            // 
            // tbNakit
            // 
            this.tbNakit.Location = new System.Drawing.Point(12, 72);
            this.tbNakit.Name = "tbNakit";
            this.tbNakit.Size = new System.Drawing.Size(124, 20);
            this.tbNakit.TabIndex = 2;
            this.tbNakit.TextChanged += new System.EventHandler(this.CalculateKalanTutar);
            // 
            // labelKredi
            // 
            this.labelKredi.AutoSize = true;
            this.labelKredi.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKredi.Location = new System.Drawing.Point(174, 51);
            this.labelKredi.Name = "labelKredi";
            this.labelKredi.Size = new System.Drawing.Size(94, 18);
            this.labelKredi.TabIndex = 1;
            this.labelKredi.Text = "Kredi Karti (₺)";
            // 
            // tbKredi
            // 
            this.tbKredi.Location = new System.Drawing.Point(177, 72);
            this.tbKredi.Name = "tbKredi";
            this.tbKredi.Size = new System.Drawing.Size(124, 20);
            this.tbKredi.TabIndex = 0;
            this.tbKredi.TextChanged += new System.EventHandler(this.CalculateKalanTutar);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(237, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "₺";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(237, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "€";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(364, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "$";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(365, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "£";
            // 
            // lTL
            // 
            this.lTL.AutoSize = true;
            this.lTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lTL.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lTL.Location = new System.Drawing.Point(272, 12);
            this.lTL.Name = "lTL";
            this.lTL.Size = new System.Drawing.Size(15, 20);
            this.lTL.TabIndex = 12;
            this.lTL.Text = "-";
            // 
            // lEuro
            // 
            this.lEuro.AutoSize = true;
            this.lEuro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lEuro.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lEuro.Location = new System.Drawing.Point(272, 42);
            this.lEuro.Name = "lEuro";
            this.lEuro.Size = new System.Drawing.Size(15, 20);
            this.lEuro.TabIndex = 13;
            this.lEuro.Text = "-";
            // 
            // lDolar
            // 
            this.lDolar.AutoSize = true;
            this.lDolar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lDolar.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lDolar.Location = new System.Drawing.Point(401, 11);
            this.lDolar.Name = "lDolar";
            this.lDolar.Size = new System.Drawing.Size(15, 20);
            this.lDolar.TabIndex = 14;
            this.lDolar.Text = "-";
            // 
            // lGBP
            // 
            this.lGBP.AutoSize = true;
            this.lGBP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lGBP.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lGBP.Location = new System.Drawing.Point(401, 42);
            this.lGBP.Name = "lGBP";
            this.lGBP.Size = new System.Drawing.Size(15, 20);
            this.lGBP.TabIndex = 15;
            this.lGBP.Text = "-";
            // 
            // bMasaKapat
            // 
            this.bMasaKapat.Enabled = false;
            this.bMasaKapat.Location = new System.Drawing.Point(12, 218);
            this.bMasaKapat.Name = "bMasaKapat";
            this.bMasaKapat.Size = new System.Drawing.Size(112, 44);
            this.bMasaKapat.TabIndex = 16;
            this.bMasaKapat.Text = "Masa Kapat";
            this.bMasaKapat.UseVisualStyleBackColor = true;
            this.bMasaKapat.Click += new System.EventHandler(this.bMasaKapat_Click);
            // 
            // bIptal
            // 
            this.bIptal.Location = new System.Drawing.Point(371, 218);
            this.bIptal.Name = "bIptal";
            this.bIptal.Size = new System.Drawing.Size(112, 44);
            this.bIptal.TabIndex = 17;
            this.bIptal.Text = " Iptal";
            this.bIptal.UseVisualStyleBackColor = true;
            this.bIptal.Click += new System.EventHandler(this.bIptal_Click);
            // 
            // lIskontoOrani
            // 
            this.lIskontoOrani.AutoSize = true;
            this.lIskontoOrani.Font = new System.Drawing.Font("Calibri", 13.25F, System.Drawing.FontStyle.Bold);
            this.lIskontoOrani.Location = new System.Drawing.Point(190, 6);
            this.lIskontoOrani.Name = "lIskontoOrani";
            this.lIskontoOrani.Size = new System.Drawing.Size(32, 22);
            this.lIskontoOrani.TabIndex = 6;
            this.lIskontoOrani.Text = "0%";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Restorium.Properties.Resources._2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.lKalan);
            this.panel2.Controls.Add(this.labelKalan);
            this.panel2.Controls.Add(this.lIskontoOrani);
            this.panel2.Location = new System.Drawing.Point(0, 173);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(481, 39);
            this.panel2.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 13.25F);
            this.label15.Location = new System.Drawing.Point(70, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(117, 22);
            this.label15.TabIndex = 19;
            this.label15.Text = "Iskonto Orani :";
            // 
            // lKalan
            // 
            this.lKalan.AutoSize = true;
            this.lKalan.Font = new System.Drawing.Font("Calibri", 13.25F, System.Drawing.FontStyle.Bold);
            this.lKalan.ForeColor = System.Drawing.Color.DarkRed;
            this.lKalan.Location = new System.Drawing.Point(399, 6);
            this.lKalan.Name = "lKalan";
            this.lKalan.Size = new System.Drawing.Size(46, 22);
            this.lKalan.TabIndex = 7;
            this.lKalan.Text = "0.0 ₺";
            // 
            // labelKalan
            // 
            this.labelKalan.AutoSize = true;
            this.labelKalan.Font = new System.Drawing.Font("Calibri", 13.25F);
            this.labelKalan.ForeColor = System.Drawing.Color.DarkRed;
            this.labelKalan.Location = new System.Drawing.Point(334, 6);
            this.labelKalan.Name = "labelKalan";
            this.labelKalan.Size = new System.Drawing.Size(59, 22);
            this.labelKalan.TabIndex = 6;
            this.labelKalan.Text = "Kalan :";
            // 
            // lTableName
            // 
            this.lTableName.AutoSize = true;
            this.lTableName.BackColor = System.Drawing.SystemColors.Control;
            this.lTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 38.25F, System.Drawing.FontStyle.Bold);
            this.lTableName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lTableName.Location = new System.Drawing.Point(17, 6);
            this.lTableName.Name = "lTableName";
            this.lTableName.Size = new System.Drawing.Size(43, 59);
            this.lTableName.TabIndex = 19;
            this.lTableName.Text = "-";
            // 
            // pbCalculator
            // 
            this.pbCalculator.Image = global::Restorium.Properties.Resources.Apps_Calculator_Metro_icon;
            this.pbCalculator.Location = new System.Drawing.Point(320, 222);
            this.pbCalculator.Name = "pbCalculator";
            this.pbCalculator.Size = new System.Drawing.Size(45, 36);
            this.pbCalculator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCalculator.TabIndex = 20;
            this.pbCalculator.TabStop = false;
            this.pbCalculator.Click += new System.EventHandler(this.Calculator);
            this.pbCalculator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseClicked);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            // 
            // TableCloseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(493, 274);
            this.Controls.Add(this.pbCalculator);
            this.Controls.Add(this.lTableName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bIptal);
            this.Controls.Add(this.bMasaKapat);
            this.Controls.Add(this.lGBP);
            this.Controls.Add(this.lDolar);
            this.Controls.Add(this.lEuro);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lTL);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableCloseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Masa Kapama";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TableCloseForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCalculator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lTL;
        private System.Windows.Forms.Label labelCari;
        private System.Windows.Forms.TextBox tbCari;
        private System.Windows.Forms.Label labelNakit;
        private System.Windows.Forms.TextBox tbNakit;
        private System.Windows.Forms.Label labelKredi;
        private System.Windows.Forms.TextBox tbKredi;
        private System.Windows.Forms.Label lEuro;
        private System.Windows.Forms.Label lDolar;
        private System.Windows.Forms.Label lGBP;
        private System.Windows.Forms.Button bMasaKapat;
        private System.Windows.Forms.Button bIptal;
        private System.Windows.Forms.Label lIskontoOrani;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelKalan;
        private System.Windows.Forms.Label lKalan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lTableName;
        private System.Windows.Forms.CheckBox cbGBP;
        private System.Windows.Forms.CheckBox cbDolar;
        private System.Windows.Forms.CheckBox cbEuro;
        private System.Windows.Forms.CheckBox cbTL;
        private System.Windows.Forms.PictureBox pbCalculator;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbTip;
        private System.Windows.Forms.Label lTip;
    }
}