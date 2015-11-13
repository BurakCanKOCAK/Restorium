namespace Restorium
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_Rehber = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tp_Stok = new System.Windows.Forms.TabPage();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tp_Adisyon = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bMasaTasi = new System.Windows.Forms.Button();
            this.pbWifi = new System.Windows.Forms.PictureBox();
            this.lDate = new System.Windows.Forms.Label();
            this.bYeniMasa = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.bRezervasyon = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bStokAra = new System.Windows.Forms.Button();
            this.bDuzenle = new System.Windows.Forms.Button();
            this.tbSearchKey = new System.Windows.Forms.TextBox();
            this.tp_Kasa = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tp_Rapor = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tp_Ayarlar = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tp_Rehber.SuspendLayout();
            this.tp_Stok.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.tp_Adisyon.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWifi)).BeginInit();
            this.panel3.SuspendLayout();
            this.tp_Kasa.SuspendLayout();
            this.tp_Rapor.SuspendLayout();
            this.tp_Ayarlar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tp_Adisyon);
            this.tabControl1.Controls.Add(this.tp_Stok);
            this.tabControl1.Controls.Add(this.tp_Kasa);
            this.tabControl1.Controls.Add(this.tp_Rapor);
            this.tabControl1.Controls.Add(this.tp_Rehber);
            this.tabControl1.Controls.Add(this.tp_Ayarlar);
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(-3, 0);
            this.tabControl1.MinimumSize = new System.Drawing.Size(1014, 733);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1014, 733);
            this.tabControl1.TabIndex = 0;
            // 
            // tp_Rehber
            // 
            this.tp_Rehber.BackgroundImage = global::Restorium.Properties.Resources.back_aliminium;
            this.tp_Rehber.Controls.Add(this.panel5);
            this.tp_Rehber.Location = new System.Drawing.Point(4, 28);
            this.tp_Rehber.Name = "tp_Rehber";
            this.tp_Rehber.Size = new System.Drawing.Size(1006, 701);
            this.tp_Rehber.TabIndex = 5;
            this.tp_Rehber.Text = "Rehber";
            this.tp_Rehber.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Data_Update);
            // 
            // tp_Stok
            // 
            this.tp_Stok.BackColor = System.Drawing.Color.Transparent;
            this.tp_Stok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tp_Stok.Controls.Add(this.panel3);
            this.tp_Stok.Controls.Add(this.dgView);
            this.tp_Stok.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tp_Stok.Location = new System.Drawing.Point(4, 28);
            this.tp_Stok.Name = "tp_Stok";
            this.tp_Stok.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Stok.Size = new System.Drawing.Size(1006, 701);
            this.tp_Stok.TabIndex = 1;
            this.tp_Stok.Text = "Stok";
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeColumns = false;
            this.dgView.AllowUserToResizeRows = false;
            this.dgView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgView.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.dgView.Location = new System.Drawing.Point(0, 69);
            this.dgView.Name = "dgView";
            this.dgView.Size = new System.Drawing.Size(1006, 632);
            this.dgView.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Column1";
            this.Column1.Items.AddRange(new object[] {
            "a",
            "b",
            "c",
            "d"});
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tp_Adisyon
            // 
            this.tp_Adisyon.BackgroundImage = global::Restorium.Properties.Resources.back_aliminium;
            this.tp_Adisyon.Controls.Add(this.tableLayoutPanel1);
            this.tp_Adisyon.Controls.Add(this.panel1);
            this.tp_Adisyon.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tp_Adisyon.Location = new System.Drawing.Point(4, 28);
            this.tp_Adisyon.Name = "tp_Adisyon";
            this.tp_Adisyon.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Adisyon.Size = new System.Drawing.Size(1006, 701);
            this.tp_Adisyon.TabIndex = 0;
            this.tp_Adisyon.Text = "Adisyon";
            this.tp_Adisyon.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Restorium.Properties.Resources._22;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.bMasaTasi);
            this.panel1.Controls.Add(this.pbWifi);
            this.panel1.Controls.Add(this.lDate);
            this.panel1.Controls.Add(this.bYeniMasa);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.bRezervasyon);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 72);
            this.panel1.TabIndex = 7;
            // 
            // bMasaTasi
            // 
            this.bMasaTasi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMasaTasi.BackColor = System.Drawing.Color.Yellow;
            this.bMasaTasi.Location = new System.Drawing.Point(612, 14);
            this.bMasaTasi.Name = "bMasaTasi";
            this.bMasaTasi.Size = new System.Drawing.Size(115, 36);
            this.bMasaTasi.TabIndex = 2;
            this.bMasaTasi.Text = "Masa Taşı";
            this.bMasaTasi.UseVisualStyleBackColor = false;
            this.bMasaTasi.Click += new System.EventHandler(this.bMasaTasi_Click);
            // 
            // pbWifi
            // 
            this.pbWifi.BackColor = System.Drawing.Color.Transparent;
            this.pbWifi.Image = global::Restorium.Properties.Resources.no_conection_256;
            this.pbWifi.Location = new System.Drawing.Point(111, 10);
            this.pbWifi.Name = "pbWifi";
            this.pbWifi.Size = new System.Drawing.Size(22, 19);
            this.pbWifi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWifi.TabIndex = 0;
            this.pbWifi.TabStop = false;
            // 
            // lDate
            // 
            this.lDate.AutoSize = true;
            this.lDate.BackColor = System.Drawing.Color.Transparent;
            this.lDate.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lDate.Location = new System.Drawing.Point(106, 32);
            this.lDate.Name = "lDate";
            this.lDate.Size = new System.Drawing.Size(179, 26);
            this.lDate.TabIndex = 6;
            this.lDate.Text = "00/00/00 11:35:47";
            // 
            // bYeniMasa
            // 
            this.bYeniMasa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bYeniMasa.BackColor = System.Drawing.Color.SlateBlue;
            this.bYeniMasa.Location = new System.Drawing.Point(474, 14);
            this.bYeniMasa.Name = "bYeniMasa";
            this.bYeniMasa.Size = new System.Drawing.Size(115, 36);
            this.bYeniMasa.TabIndex = 0;
            this.bYeniMasa.Text = "Yeni Masa";
            this.bYeniMasa.UseVisualStyleBackColor = false;
            this.bYeniMasa.Click += new System.EventHandler(this.bYeniMasa_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button3.Location = new System.Drawing.Point(749, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 36);
            this.button3.TabIndex = 1;
            this.button3.Text = "Hesap";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // bRezervasyon
            // 
            this.bRezervasyon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRezervasyon.BackColor = System.Drawing.Color.Tomato;
            this.bRezervasyon.Location = new System.Drawing.Point(885, 14);
            this.bRezervasyon.Name = "bRezervasyon";
            this.bRezervasyon.Size = new System.Drawing.Size(115, 36);
            this.bRezervasyon.TabIndex = 3;
            this.bRezervasyon.Text = "Rezervasyon";
            this.bRezervasyon.UseVisualStyleBackColor = false;
            this.bRezervasyon.Click += new System.EventHandler(this.bRezervasyon_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Restorium.Properties.Resources.stok;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel3.Controls.Add(this.bStokAra);
            this.panel3.Controls.Add(this.bDuzenle);
            this.panel3.Controls.Add(this.tbSearchKey);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1010, 72);
            this.panel3.TabIndex = 8;
            // 
            // bStokAra
            // 
            this.bStokAra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bStokAra.Location = new System.Drawing.Point(956, 24);
            this.bStokAra.Name = "bStokAra";
            this.bStokAra.Size = new System.Drawing.Size(47, 27);
            this.bStokAra.TabIndex = 3;
            this.bStokAra.Text = "Ara";
            this.bStokAra.UseVisualStyleBackColor = true;
            this.bStokAra.Click += new System.EventHandler(this.bStokAra_Click);
            // 
            // bDuzenle
            // 
            this.bDuzenle.BackColor = System.Drawing.Color.Silver;
            this.bDuzenle.Location = new System.Drawing.Point(125, 25);
            this.bDuzenle.Name = "bDuzenle";
            this.bDuzenle.Size = new System.Drawing.Size(144, 28);
            this.bDuzenle.TabIndex = 1;
            this.bDuzenle.Text = "Duzenle(Kapali)";
            this.bDuzenle.UseVisualStyleBackColor = false;
            this.bDuzenle.Click += new System.EventHandler(this.bDuzenle_Click);
            // 
            // tbSearchKey
            // 
            this.tbSearchKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchKey.Location = new System.Drawing.Point(712, 24);
            this.tbSearchKey.Name = "tbSearchKey";
            this.tbSearchKey.Size = new System.Drawing.Size(227, 27);
            this.tbSearchKey.TabIndex = 2;
            // 
            // tp_Kasa
            // 
            this.tp_Kasa.BackgroundImage = global::Restorium.Properties.Resources.back_aliminium;
            this.tp_Kasa.Controls.Add(this.panel2);
            this.tp_Kasa.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tp_Kasa.Location = new System.Drawing.Point(4, 28);
            this.tp_Kasa.Name = "tp_Kasa";
            this.tp_Kasa.Size = new System.Drawing.Size(1006, 701);
            this.tp_Kasa.TabIndex = 2;
            this.tp_Kasa.Text = "Kasa";
            this.tp_Kasa.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Restorium.Properties.Resources.kasa1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 72);
            this.panel2.TabIndex = 9;
            // 
            // tp_Rapor
            // 
            this.tp_Rapor.BackgroundImage = global::Restorium.Properties.Resources.back_aliminium;
            this.tp_Rapor.Controls.Add(this.panel4);
            this.tp_Rapor.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tp_Rapor.Location = new System.Drawing.Point(4, 28);
            this.tp_Rapor.Name = "tp_Rapor";
            this.tp_Rapor.Size = new System.Drawing.Size(1006, 701);
            this.tp_Rapor.TabIndex = 4;
            this.tp_Rapor.Text = "Rapor";
            this.tp_Rapor.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::Restorium.Properties.Resources.rapor2;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1010, 72);
            this.panel4.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BackgroundImage = global::Restorium.Properties.Resources.rehber;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1010, 72);
            this.panel5.TabIndex = 11;
            // 
            // tp_Ayarlar
            // 
            this.tp_Ayarlar.BackgroundImage = global::Restorium.Properties.Resources.back_aliminium;
            this.tp_Ayarlar.Controls.Add(this.panel6);
            this.tp_Ayarlar.Location = new System.Drawing.Point(4, 28);
            this.tp_Ayarlar.Name = "tp_Ayarlar";
            this.tp_Ayarlar.Size = new System.Drawing.Size(1006, 701);
            this.tp_Ayarlar.TabIndex = 3;
            this.tp_Ayarlar.Text = "Ayarlar";
            this.tp_Ayarlar.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::Restorium.Properties.Resources.settings2;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1010, 72);
            this.panel6.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 92);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 375);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(1024, 769);
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_Rehber.ResumeLayout(false);
            this.tp_Stok.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.tp_Adisyon.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWifi)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tp_Kasa.ResumeLayout(false);
            this.tp_Rapor.ResumeLayout(false);
            this.tp_Ayarlar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_Adisyon;
        private System.Windows.Forms.TabPage tp_Stok;
        private System.Windows.Forms.TabPage tp_Kasa;
        private System.Windows.Forms.Button bYeniMasa;
        private System.Windows.Forms.Button bRezervasyon;
        private System.Windows.Forms.Button bMasaTasi;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tp_Ayarlar;
        private System.Windows.Forms.Label lDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tp_Rapor;
        private System.Windows.Forms.TabPage tp_Rehber;
        private System.Windows.Forms.PictureBox pbWifi;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bStokAra;
        private System.Windows.Forms.Button bDuzenle;
        private System.Windows.Forms.TextBox tbSearchKey;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}