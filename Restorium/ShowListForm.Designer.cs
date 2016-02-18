namespace Restorium
{
    partial class ShowListForm
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
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.bAra = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.dgViewStok = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACIKLAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIRIM = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BIRIM_FIYAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARA_BIRIMI = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DINAMIK_STOK_KONTROLU = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgViewWaiter = new System.Windows.Forms.DataGridView();
            this.ID_WAITER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAITER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GOREVI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewStok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewWaiter)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(445, 414);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(193, 20);
            this.tbSearch.TabIndex = 1;
            this.tbSearch.TextChanged += new System.EventHandler(this.tSearchTextChanged);
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // bAra
            // 
            this.bAra.Location = new System.Drawing.Point(644, 411);
            this.bAra.Name = "bAra";
            this.bAra.Size = new System.Drawing.Size(75, 23);
            this.bAra.TabIndex = 2;
            this.bAra.Text = "Ara";
            this.bAra.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(1, 414);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(79, 31);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "Tamam";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(112, 414);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(79, 31);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Iptal";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // dgViewStok
            // 
            this.dgViewStok.AllowUserToAddRows = false;
            this.dgViewStok.AllowUserToDeleteRows = false;
            this.dgViewStok.AllowUserToResizeColumns = false;
            this.dgViewStok.AllowUserToResizeRows = false;
            this.dgViewStok.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgViewStok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewStok.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ACIKLAMA,
            this.ADET,
            this.BIRIM,
            this.BIRIM_FIYAT,
            this.PARA_BIRIMI,
            this.DINAMIK_STOK_KONTROLU});
            this.dgViewStok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgViewStok.Location = new System.Drawing.Point(1, 2);
            this.dgViewStok.Name = "dgViewStok";
            this.dgViewStok.ReadOnly = true;
            this.dgViewStok.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewStok.Size = new System.Drawing.Size(718, 403);
            this.dgViewStok.TabIndex = 5;
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
            this.ACIKLAMA.Width = 200;
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
            this.BIRIM_FIYAT.Width = 60;
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
            this.PARA_BIRIMI.Width = 60;
            // 
            // DINAMIK_STOK_KONTROLU
            // 
            this.DINAMIK_STOK_KONTROLU.HeaderText = "DINAMIK STOK KONTROLU";
            this.DINAMIK_STOK_KONTROLU.Name = "DINAMIK_STOK_KONTROLU";
            this.DINAMIK_STOK_KONTROLU.ReadOnly = true;
            this.DINAMIK_STOK_KONTROLU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DINAMIK_STOK_KONTROLU.Width = 55;
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
            this.dgViewWaiter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgViewWaiter.Location = new System.Drawing.Point(1, 2);
            this.dgViewWaiter.Name = "dgViewWaiter";
            this.dgViewWaiter.ReadOnly = true;
            this.dgViewWaiter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewWaiter.Size = new System.Drawing.Size(718, 403);
            this.dgViewWaiter.TabIndex = 14;
            this.dgViewWaiter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            // 
            // ID_WAITER
            // 
            this.ID_WAITER.HeaderText = "ID";
            this.ID_WAITER.Name = "ID_WAITER";
            this.ID_WAITER.ReadOnly = true;
            this.ID_WAITER.Width = 110;
            // 
            // WAITER
            // 
            this.WAITER.HeaderText = "PERSONEL ADI";
            this.WAITER.Name = "WAITER";
            this.WAITER.ReadOnly = true;
            this.WAITER.Width = 295;
            // 
            // GOREVI
            // 
            this.GOREVI.HeaderText = "GOREVI";
            this.GOREVI.Name = "GOREVI";
            this.GOREVI.ReadOnly = true;
            this.GOREVI.Width = 270;
            // 
            // ShowListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 451);
            this.Controls.Add(this.dgViewWaiter);
            this.Controls.Add(this.dgViewStok);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bAra);
            this.Controls.Add(this.tbSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database";
            this.Load += new System.EventHandler(this.ShowListForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_press);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewStok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewWaiter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button bAra;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.DataGridView dgViewStok;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACIKLAMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADET;
        private System.Windows.Forms.DataGridViewComboBoxColumn BIRIM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIRIM_FIYAT;
        private System.Windows.Forms.DataGridViewComboBoxColumn PARA_BIRIMI;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DINAMIK_STOK_KONTROLU;
        private System.Windows.Forms.DataGridView dgViewWaiter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_WAITER;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAITER;
        private System.Windows.Forms.DataGridViewTextBoxColumn GOREVI;
    }
}