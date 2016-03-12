namespace Restorium
{
    partial class StokAdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbAciklama = new System.Windows.Forms.TextBox();
            this.tbAdet = new System.Windows.Forms.TextBox();
            this.tbBirimFiyat = new System.Windows.Forms.TextBox();
            this.cbBirim = new System.Windows.Forms.ComboBox();
            this.cbParaBirimi = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bStokAdd = new System.Windows.Forms.Button();
            this.cbMenuUrunu = new System.Windows.Forms.CheckBox();
            this.cbStokUrunu = new System.Windows.Forms.CheckBox();
            this.cbDynamicStokCheckEnabled = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(59, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Aciklama : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(42, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Adet : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(3, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Birim Fiyat : ";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(104, 20);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(100, 20);
            this.tbID.TabIndex = 5;
            this.tbID.TextChanged += new System.EventHandler(this.IDSelectedFromList);
            // 
            // tbAciklama
            // 
            this.tbAciklama.Location = new System.Drawing.Point(104, 62);
            this.tbAciklama.Name = "tbAciklama";
            this.tbAciklama.Size = new System.Drawing.Size(100, 20);
            this.tbAciklama.TabIndex = 6;
            this.tbAciklama.TextChanged += new System.EventHandler(this.AciklamaSelectedFromList);
            // 
            // tbAdet
            // 
            this.tbAdet.Location = new System.Drawing.Point(104, 102);
            this.tbAdet.Name = "tbAdet";
            this.tbAdet.Size = new System.Drawing.Size(100, 20);
            this.tbAdet.TabIndex = 7;
            // 
            // tbBirimFiyat
            // 
            this.tbBirimFiyat.Location = new System.Drawing.Point(104, 138);
            this.tbBirimFiyat.Name = "tbBirimFiyat";
            this.tbBirimFiyat.Size = new System.Drawing.Size(100, 20);
            this.tbBirimFiyat.TabIndex = 8;
            // 
            // cbBirim
            // 
            this.cbBirim.FormattingEnabled = true;
            this.cbBirim.Items.AddRange(new object[] {
            "Birim",
            "Porsiyon",
            "Kutu",
            "kg",
            "gr",
            "Kasa",
            "Buyuk",
            "Kucuk"});
            this.cbBirim.Location = new System.Drawing.Point(211, 100);
            this.cbBirim.Name = "cbBirim";
            this.cbBirim.Size = new System.Drawing.Size(121, 21);
            this.cbBirim.TabIndex = 9;
            // 
            // cbParaBirimi
            // 
            this.cbParaBirimi.FormattingEnabled = true;
            this.cbParaBirimi.Items.AddRange(new object[] {
            "TL",
            "EURO",
            "DOLAR",
            "GBP"});
            this.cbParaBirimi.Location = new System.Drawing.Point(210, 138);
            this.cbParaBirimi.Name = "cbParaBirimi";
            this.cbParaBirimi.Size = new System.Drawing.Size(121, 21);
            this.cbParaBirimi.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Restorium.Properties.Resources.red;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Controls.Add(this.bStokAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 59);
            this.panel1.TabIndex = 11;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(332, 14);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(95, 33);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Iptal";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bStokAdd
            // 
            this.bStokAdd.Location = new System.Drawing.Point(25, 14);
            this.bStokAdd.Name = "bStokAdd";
            this.bStokAdd.Size = new System.Drawing.Size(95, 33);
            this.bStokAdd.TabIndex = 0;
            this.bStokAdd.Text = "Stok Ekle";
            this.bStokAdd.UseVisualStyleBackColor = true;
            this.bStokAdd.Click += new System.EventHandler(this.bStokAdd_Click);
            // 
            // cbMenuUrunu
            // 
            this.cbMenuUrunu.AutoSize = true;
            this.cbMenuUrunu.Location = new System.Drawing.Point(104, 169);
            this.cbMenuUrunu.Name = "cbMenuUrunu";
            this.cbMenuUrunu.Size = new System.Drawing.Size(85, 17);
            this.cbMenuUrunu.TabIndex = 12;
            this.cbMenuUrunu.Text = "Menu Urunu";
            this.cbMenuUrunu.UseVisualStyleBackColor = true;
            this.cbMenuUrunu.CheckedChanged += new System.EventHandler(this.StokCheckLogicMenuCheckbox);
            // 
            // cbStokUrunu
            // 
            this.cbStokUrunu.AutoSize = true;
            this.cbStokUrunu.Location = new System.Drawing.Point(16, 169);
            this.cbStokUrunu.Name = "cbStokUrunu";
            this.cbStokUrunu.Size = new System.Drawing.Size(80, 17);
            this.cbStokUrunu.TabIndex = 13;
            this.cbStokUrunu.Text = "Stok Urunu";
            this.cbStokUrunu.UseVisualStyleBackColor = true;
            this.cbStokUrunu.Visible = false;
            this.cbStokUrunu.CheckedChanged += new System.EventHandler(this.StokCheckLogicStokUrunuCheckbox);
            // 
            // cbDynamicStokCheckEnabled
            // 
            this.cbDynamicStokCheckEnabled.AutoSize = true;
            this.cbDynamicStokCheckEnabled.Enabled = false;
            this.cbDynamicStokCheckEnabled.Location = new System.Drawing.Point(210, 169);
            this.cbDynamicStokCheckEnabled.Name = "cbDynamicStokCheckEnabled";
            this.cbDynamicStokCheckEnabled.Size = new System.Drawing.Size(131, 17);
            this.cbDynamicStokCheckEnabled.TabIndex = 14;
            this.cbDynamicStokCheckEnabled.Text = "Dinamik Stok Kontrolu";
            this.cbDynamicStokCheckEnabled.UseVisualStyleBackColor = true;
            // 
            // StokAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 251);
            this.Controls.Add(this.cbDynamicStokCheckEnabled);
            this.Controls.Add(this.cbStokUrunu);
            this.Controls.Add(this.cbMenuUrunu);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbParaBirimi);
            this.Controls.Add(this.cbBirim);
            this.Controls.Add(this.tbBirimFiyat);
            this.Controls.Add(this.tbAdet);
            this.Controls.Add(this.tbAciklama);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StokAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stok Girisi";
            this.Load += new System.EventHandler(this.StokAdd_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbAciklama;
        private System.Windows.Forms.TextBox tbAdet;
        private System.Windows.Forms.TextBox tbBirimFiyat;
        private System.Windows.Forms.ComboBox cbBirim;
        private System.Windows.Forms.ComboBox cbParaBirimi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bStokAdd;
        private System.Windows.Forms.CheckBox cbMenuUrunu;
        private System.Windows.Forms.CheckBox cbStokUrunu;
        private System.Windows.Forms.CheckBox cbDynamicStokCheckEnabled;
    }
}