namespace Restorium
{
    partial class MainDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDisplay));
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tutar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kacSiparis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iskono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.musteri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RorN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rESERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_Clock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RCTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.kac,
            this.tutar,
            this.personel,
            this.kacSiparis,
            this.iskono,
            this.musteri,
            this.RorN,
            this.rESERDATE,
            this.R_Clock,
            this.RCTime});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(1141, 562);
            this.dgvData.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "id";
            this.name.Name = "name";
            // 
            // kac
            // 
            this.kac.HeaderText = "kacinci masa";
            this.kac.Name = "kac";
            // 
            // tutar
            // 
            this.tutar.HeaderText = "tutar";
            this.tutar.Name = "tutar";
            // 
            // personel
            // 
            this.personel.HeaderText = "personel";
            this.personel.Name = "personel";
            // 
            // kacSiparis
            // 
            this.kacSiparis.HeaderText = "kac siparis";
            this.kacSiparis.Name = "kacSiparis";
            // 
            // iskono
            // 
            this.iskono.HeaderText = "iskonto";
            this.iskono.Name = "iskono";
            // 
            // musteri
            // 
            this.musteri.HeaderText = "musteri";
            this.musteri.Name = "musteri";
            // 
            // RorN
            // 
            this.RorN.HeaderText = "RorN";
            this.RorN.Name = "RorN";
            // 
            // rESERDATE
            // 
            this.rESERDATE.HeaderText = "R_Date";
            this.rESERDATE.Name = "rESERDATE";
            // 
            // R_Clock
            // 
            this.R_Clock.HeaderText = "R_Clock";
            this.R_Clock.Name = "R_Clock";
            // 
            // RCTime
            // 
            this.RCTime.HeaderText = "RCTime";
            this.RCTime.Name = "RCTime";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.Second);
            // 
            // MainDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 562);
            this.Controls.Add(this.dgvData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainDisplay";
            this.Text = "MainDisplay";
            this.Load += new System.EventHandler(this.MainDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn kac;
        private System.Windows.Forms.DataGridViewTextBoxColumn tutar;
        private System.Windows.Forms.DataGridViewTextBoxColumn personel;
        private System.Windows.Forms.DataGridViewTextBoxColumn kacSiparis;
        private System.Windows.Forms.DataGridViewTextBoxColumn iskono;
        private System.Windows.Forms.DataGridViewTextBoxColumn musteri;
        private System.Windows.Forms.DataGridViewTextBoxColumn RorN;
        private System.Windows.Forms.DataGridViewTextBoxColumn rESERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_Clock;
        private System.Windows.Forms.DataGridViewTextBoxColumn RCTime;
        private System.Windows.Forms.Timer timer1;
    }
}