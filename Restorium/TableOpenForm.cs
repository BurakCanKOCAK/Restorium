using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restorium
{
    public partial class TableOpenForm : Form
    {
        public string MasaNo { get; set; }
        public string Musteri{ get; set; }
        public string Iskonto { get; set; }
        public string PersonelAdi{ get; set; }

        public TableOpenForm()
        {
            InitializeComponent();
        }

        private void TableOpenForm_Load(object sender, EventArgs e)
        {
            tbIskonto.Text = MainForm.iskontoRate.ToString();
            if (MainForm.User != "Admin" && MainForm.User != "KidemliPersonel")
            {
                tbIskonto.Enabled = false;
            }   
        }

        private void bMasaAc_Click(object sender, EventArgs e)
        {
            if (tbMasaNo.Text != "" && tbPersonelAdi.Text != "")
            {
                this.MasaNo = tbMasaNo.Text;
                this.PersonelAdi = tbPersonelAdi.Text;
                this.Iskonto = tbIskonto.Text;
                this.Musteri = tbMusteri.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lutfen Eksik Zorunlu Alanlari Doldurunuz (Masa Numarasi ve Personel Adi)");
            }
        }

        private void bIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bPersonelAdiListe_Click(object sender, EventArgs e)
        {
            using (var showListForm = new ShowListForm())
            {
                showListForm.ListName = "Personel";
                var result = showListForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                     string PersonelAdi = showListForm.SelectedWaiter;
                     tbPersonelAdi.Text = PersonelAdi;
                }
            }
        }

        private void tbPersonelAdi_TextChanged(object sender, EventArgs e)
        {
            TextBox personelADI = sender as TextBox;
            if (personelADI != null)
            {
                personelADI.AutoCompleteMode = AutoCompleteMode.Suggest;
                personelADI.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                for (int i = 0; i < MainForm.personelCount; i++)
                {
                    col.Add(MainForm.personelNames[i]);
                }
                personelADI.AutoCompleteCustomSource = col;
                
            }
        }
    }
}
