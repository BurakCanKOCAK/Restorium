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
            this.tp_Adisyon = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bMasaTasi = new System.Windows.Forms.Button();
            this.pbWifi = new System.Windows.Forms.PictureBox();
            this.lDate = new System.Windows.Forms.Label();
            this.bYeniMasa = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.bRezervasyon = new System.Windows.Forms.Button();
            this.tp_Stok = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bStokAra = new System.Windows.Forms.Button();
            this.bDuzenle = new System.Windows.Forms.Button();
            this.tbSearchKey = new System.Windows.Forms.TextBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACIKLAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIRIM = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BIRIM_FIYAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARA_BIRIMI = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DINAMIK_STOK_KONTROLU = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tp_Kasa = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tp_Rapor = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tp_Rehber = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tp_Ayarlar = new System.Windows.Forms.TabPage();
            this.bPersonelDuzenle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgViewWaiter = new System.Windows.Forms.DataGridView();
            this.ID_WAITER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAITER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GOREVI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDefaultIskontoValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEuro = new System.Windows.Forms.TextBox();
            this.tbDolar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbGBP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tp_Adisyon.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWifi)).BeginInit();
            this.tp_Stok.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.tp_Kasa.SuspendLayout();
            this.tp_Rapor.SuspendLayout();
            this.tp_Rehber.SuspendLayout();
            this.tp_Ayarlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewWaiter)).BeginInit();
            this.panel7.SuspendLayout();
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 92);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 598);
            this.tableLayoutPanel1.TabIndex = 8;
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
            // tp_Stok
            // 
            this.tp_Stok.BackColor = System.Drawing.Color.Transparent;
            this.tp_Stok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tp_Stok.Controls.Add(this.pictureBox1);
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Restorium.Properties.Resources.printer;
            this.pictureBox1.Location = new System.Drawing.Point(6, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
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
            this.ID,
            this.ACIKLAMA,
            this.ADET,
            this.BIRIM,
            this.BIRIM_FIYAT,
            this.PARA_BIRIMI,
            this.DINAMIK_STOK_KONTROLU});
            this.dgView.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.dgView.Location = new System.Drawing.Point(0, 69);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(1006, 632);
            this.dgView.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // ACIKLAMA
            // 
            this.ACIKLAMA.HeaderText = "ACIKLAMA";
            this.ACIKLAMA.Name = "ACIKLAMA";
            this.ACIKLAMA.ReadOnly = true;
            // 
            // ADET
            // 
            this.ADET.HeaderText = "ADET";
            this.ADET.Name = "ADET";
            this.ADET.ReadOnly = true;
            // 
            // BIRIM
            // 
            this.BIRIM.HeaderText = "BIRIM";
            this.BIRIM.Items.AddRange(new object[] {
            "Birim",
            "Porsiyon",
            "Kutu",
            "kg",
            "gr",
            "Kasa",
            "Buyuk",
            "Kucuk"});
            this.BIRIM.Name = "BIRIM";
            this.BIRIM.ReadOnly = true;
            this.BIRIM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // BIRIM_FIYAT
            // 
            this.BIRIM_FIYAT.HeaderText = "BIRIM FIYAT";
            this.BIRIM_FIYAT.Name = "BIRIM_FIYAT";
            this.BIRIM_FIYAT.ReadOnly = true;
            // 
            // PARA_BIRIMI
            // 
            this.PARA_BIRIMI.HeaderText = "PARA BIRIMI";
            this.PARA_BIRIMI.Items.AddRange(new object[] {
            "TL",
            "EURO",
            "DOLAR",
            "GBP"});
            this.PARA_BIRIMI.Name = "PARA_BIRIMI";
            this.PARA_BIRIMI.ReadOnly = true;
            this.PARA_BIRIMI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DINAMIK_STOK_KONTROLU
            // 
            this.DINAMIK_STOK_KONTROLU.HeaderText = "DINAMIK STOK KONTROLU";
            this.DINAMIK_STOK_KONTROLU.Name = "DINAMIK_STOK_KONTROLU";
            this.DINAMIK_STOK_KONTROLU.ReadOnly = true;
            this.DINAMIK_STOK_KONTROLU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            this.tp_Ayarlar.Controls.Add(this.panel7);
            this.tp_Ayarlar.Controls.Add(this.bPersonelDuzenle);
            this.tp_Ayarlar.Controls.Add(this.label1);
            this.tp_Ayarlar.Controls.Add(this.dgViewWaiter);
            this.tp_Ayarlar.Controls.Add(this.panel6);
            this.tp_Ayarlar.Location = new System.Drawing.Point(4, 28);
            this.tp_Ayarlar.Name = "tp_Ayarlar";
            this.tp_Ayarlar.Size = new System.Drawing.Size(1006, 701);
            this.tp_Ayarlar.TabIndex = 3;
            this.tp_Ayarlar.Text = "Ayarlar";
            this.tp_Ayarlar.UseVisualStyleBackColor = true;
            // 
            // bPersonelDuzenle
            // 
            this.bPersonelDuzenle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bPersonelDuzenle.Location = new System.Drawing.Point(920, 78);
            this.bPersonelDuzenle.Name = "bPersonelDuzenle";
            this.bPersonelDuzenle.Size = new System.Drawing.Size(75, 28);
            this.bPersonelDuzenle.TabIndex = 15;
            this.bPersonelDuzenle.Text = "Duzenle";
            this.bPersonelDuzenle.UseVisualStyleBackColor = true;
            this.bPersonelDuzenle.Click += new System.EventHandler(this.bPersonelDuzenle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "Personel Listesi";
            // 
            // dgViewWaiter
            // 
            this.dgViewWaiter.AllowUserToAddRows = false;
            this.dgViewWaiter.AllowUserToDeleteRows = false;
            this.dgViewWaiter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgViewWaiter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewWaiter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_WAITER,
            this.WAITER,
            this.GOREVI});
            this.dgViewWaiter.Location = new System.Drawing.Point(11, 112);
            this.dgViewWaiter.Name = "dgViewWaiter";
            this.dgViewWaiter.ReadOnly = true;
            this.dgViewWaiter.Size = new System.Drawing.Size(984, 339);
            this.dgViewWaiter.TabIndex = 13;
            // 
            // ID_WAITER
            // 
            this.ID_WAITER.HeaderText = "ID";
            this.ID_WAITER.Name = "ID_WAITER";
            this.ID_WAITER.ReadOnly = true;
            this.ID_WAITER.Width = 50;
            // 
            // WAITER
            // 
            this.WAITER.HeaderText = "PERSONEL ADI";
            this.WAITER.Name = "WAITER";
            this.WAITER.ReadOnly = true;
            // 
            // GOREVI
            // 
            this.GOREVI.HeaderText = "GOREVI";
            this.GOREVI.Name = "GOREVI";
            this.GOREVI.ReadOnly = true;
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
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Data_Update);
            // 
            // printDocument1
            // 
            this.printDocument1.DocumentName = "Stok Raporu";
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.tbGBP);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.tbDolar);
            this.panel7.Controls.Add(this.tbEuro);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.tbDefaultIskontoValue);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Italic);
            this.panel7.Location = new System.Drawing.Point(11, 474);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(983, 214);
            this.panel7.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ayarlar";
            // 
            // tbDefaultIskontoValue
            // 
            this.tbDefaultIskontoValue.Location = new System.Drawing.Point(140, 53);
            this.tbDefaultIskontoValue.Name = "tbDefaultIskontoValue";
            this.tbDefaultIskontoValue.Size = new System.Drawing.Size(53, 30);
            this.tbDefaultIskontoValue.TabIndex = 1;
            this.tbDefaultIskontoValue.TextChanged += new System.EventHandler(this.IskontoValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Iskonto Orani :  ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "1 TL  =  ";
            // 
            // tbEuro
            // 
            this.tbEuro.Location = new System.Drawing.Point(75, 103);
            this.tbEuro.Name = "tbEuro";
            this.tbEuro.Size = new System.Drawing.Size(53, 30);
            this.tbEuro.TabIndex = 5;
            // 
            // tbDolar
            // 
            this.tbDolar.Location = new System.Drawing.Point(199, 103);
            this.tbDolar.Name = "tbDolar";
            this.tbDolar.Size = new System.Drawing.Size(53, 30);
            this.tbDolar.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "Euro =";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 23);
            this.label6.TabIndex = 9;
            this.label6.Text = "Dolar = ";
            // 
            // tbGBP
            // 
            this.tbGBP.Location = new System.Drawing.Point(327, 103);
            this.tbGBP.Name = "tbGBP";
            this.tbGBP.Size = new System.Drawing.Size(53, 30);
            this.tbGBP.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(386, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 23);
            this.label8.TabIndex = 11;
            this.label8.Text = "GBP";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(1024, 726);
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_Adisyon.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWifi)).EndInit();
            this.tp_Stok.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.tp_Kasa.ResumeLayout(false);
            this.tp_Rapor.ResumeLayout(false);
            this.tp_Rehber.ResumeLayout(false);
            this.tp_Ayarlar.ResumeLayout(false);
            this.tp_Ayarlar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewWaiter)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bStokAra;
        private System.Windows.Forms.Button bDuzenle;
        private System.Windows.Forms.TextBox tbSearchKey;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACIKLAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADET;
        private System.Windows.Forms.DataGridViewComboBoxColumn BIRIM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIRIM_FIYAT;
        private System.Windows.Forms.DataGridViewComboBoxColumn PARA_BIRIMI;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DINAMIK_STOK_KONTROLU;
        private System.Windows.Forms.DataGridView dgViewWaiter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bPersonelDuzenle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_WAITER;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAITER;
        private System.Windows.Forms.DataGridViewTextBoxColumn GOREVI;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDefaultIskontoValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDolar;
        private System.Windows.Forms.TextBox tbEuro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbGBP;
    }
}