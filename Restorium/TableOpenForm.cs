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
            //----------------------//
            UserLog.WConsole("TableOpenForm Started!!");
            UserLog.WConsole("Personel Okundu  (" + MainForm.personelCount.ToString() + ")");
            tbPersonelAdi.AutoCompleteMode = AutoCompleteMode.Suggest;
            tbPersonelAdi.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            for (int i = 0; i < MainForm.personelCount; i++)
            {
                col.Add(MainForm.personelNames[i]);
            }
            tbPersonelAdi.AutoCompleteCustomSource = col;
            //----------------------//
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
                bool tableCheck = true;
                for (int i = 0; i < MainForm.tableCounter; i++)
                {
                    UserLog.WConsole(MainForm.tableCounter.ToString());
                    UserLog.WConsole(MainForm.tableNumbers[i].ToString());
                    if (MainForm.tableNumbers[i].ToString() == tbMasaNo.Text)
                    {
                        MessageBox.Show("Bu masa zaten acik durumda !");
                        tableCheck = false;
                        UserLog.WConsole("IF");
                        break;
                    }
                }
                if(tableCheck==true)
                { 
                    UserLog.WConsole("FOR BITTI");
                    this.MasaNo = tbMasaNo.Text;
                    this.PersonelAdi = tbPersonelAdi.Text;
                    this.Iskonto = tbIskonto.Text;
                    this.Musteri = tbMusteri.Text;
                    MainForm.tableNumbers[MainForm.tableCounter] =Convert.ToInt16(tbMasaNo.Text);
                    MainForm.tableCounter++;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
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
                    try
                    { 
                     string PersonelAdi = showListForm.SelectedWaiter;
                     tbPersonelAdi.Text = PersonelAdi;
                    }catch
                    {
                        MessageBox.Show("Personel secimi yapilmadi !");
                    }
                }
            }
        }

        private void tbPersonelAdi_TextChanged(object sender, EventArgs e)
        {
        ////
        }

        private void key_press(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                UserLog.WConsole("Table opening/reserving cancelled !");
                this.Close();
            }
        }
    }
}
