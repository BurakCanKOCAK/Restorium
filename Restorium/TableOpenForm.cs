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
        public int Iskonto { get; set; }
        public string PersonelAdi{ get; set; }
        public static int lastTablePlace = 0;

        public static string changeMasaAdi;
        public static string changePersonel;
        public static string changeIskonto;
        public static string changeMusteriAdi;
        public static string changeRezervationDate;
        public static bool tableSettingsChange = false;
        public static DateTime Saat, Dakika, RezervationDate, TimeLeft;


        public TableOpenForm()
        {
            InitializeComponent();
        }

        private void TableOpenForm_Load(object sender, EventArgs e)
        {
            //----------------------//
            this.ActiveControl = tbMasaNo;
            UserLog.WConsole("<<< TableOpenForm (Form)>>>");
            UserLog.WConsole("Personel Okundu  (" + MainForm.personelCount.ToString() + ")");
            rSaat.Format = DateTimePickerFormat.Custom;
            rSaat.CustomFormat = "HH";
            rDakika.Format = DateTimePickerFormat.Custom;
            rDakika.CustomFormat = "mm";
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
            if (LastChoosenTable.reservation == true) //Rezervasyon masasi aciliyorsa
            {
                gbRezervasyonSettings.Enabled = true;
                gbRezervasyonSettings.Visible = true;
            }
            else
            {
                gbRezervasyonSettings.Enabled = false;
                gbRezervasyonSettings.Visible = false;
            }

            // Izinler - Permissions
            if (MainForm.User != "Admin" && MainForm.User != "KidemliPersonel")
            {
                tbIskonto.Enabled = false;
            }
            tbMusteri.Text = "Müşteri";
            if (tableSettingsChange)
            {
                tbIskonto.Text = LastChoosenTable.iskonto.ToString();
                tbMasaNo.Text = LastChoosenTable.TableNumber;
                tbMusteri.Text = LastChoosenTable.musteriAdi;
                tbPersonelAdi.Text = LastChoosenTable.Waiter;
            }
        }

        private void bMasaAc_Click(object sender, EventArgs e)
        {
            //**************************************************************************
            //Rezervasyon olup olmamasina gore datayi MainForm daki TableDetails dizisine aktar
            //***************************************************************************
            //rDakika;
            //rSaat;
            //rLimitSaat;
            //rLimitDakika;
            //                  NowDateTime.UtcNow.ToLocalTime().ToShortTimeString().ToString();
            //
            if (tbMasaNo.Text != "" && tbPersonelAdi.Text != "")
            {
                bool tableCheck = true;
                for (int i = 0; i < MainForm.tableCounter; i++)
                {
                    if (MainForm.tableNumbers[i].ToUpper().ToString() == tbMasaNo.Text.ToUpper())
                    {
                        if (tableSettingsChange != true || MainForm.tableNumbers[i].ToUpper().ToString() != LastChoosenTable.TableNumber.ToUpper())
                        {
                            //Rezervasyon masasi mi check et 
                            MessageBox.Show("Bu masa zaten acik durumda !");
                            tableCheck = false;
                            break;

                        }
                    }
                }
                 
                if (tableCheck==true)
                {
                    
                    try
                    {
                        Iskonto = Convert.ToInt16(tbIskonto.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Lutfen Iskonto Alanina Gecerli Bir Deger Giriniz ! (0-100)");
                    }
                    if (Iskonto >= 0 && Iskonto <= 100)
                    {
                        //Masa Acma basarili
                        //MainForm.tableDetails[MainForm.tableCounter, 1] = 
                        this.MasaNo = tbMasaNo.Text;
                        LastChoosenTable.TableNumber = MasaNo;
                        this.PersonelAdi = tbPersonelAdi.Text;
                        LastChoosenTable.Waiter = PersonelAdi;
                        this.Iskonto = Convert.ToInt16(tbIskonto.Text);
                        LastChoosenTable.iskonto = Convert.ToInt16(Iskonto);
                        this.Musteri = tbMusteri.Text;
                        LastChoosenTable.musteriAdi = Musteri;
                        UserLog.WConsole("Masa Acildi : " + tbMasaNo.Text);
                        MainForm.tableNumbers[getEmptyQueue()] = tbMasaNo.Text;
                        //MainForm.dailyTableCounter++;
                        //MainForm.tableCounter++;
                        Saat = rSaat.Value;
                        Dakika = rDakika.Value;
                        RezervationDate = rTarih.Value;
                        TimeLeft = Convert.ToDateTime(rLimitSaat);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lutfen Gecerli Bir Iskonto Orani Giriniz !");
                    }
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
            else if (e.KeyCode == Keys.Enter)
            {
                if (tbMasaNo.Focused)
                {
                    tbMusteri.Focus();
                }
                else if (tbMusteri.Focused)
                {
                    tbIskonto.Focus();
                } else if (tbIskonto.Focused)
                {
                    tbPersonelAdi.Focus();
                }
                else if (tbPersonelAdi.Focused)
                {
                    bMasaAc.Focus();
                }
            }
        }
        public int getEmptyQueue()
        {
            for (int i = 0; i < MainForm.tableCounter; i++)
            {
                if (MainForm.emptyTableList[i] != true)
                {
                    UserLog.WConsole("Bos olan masa indexi : " + i.ToString());
                    MainForm.emptyTableList[i] = true;
                    lastTablePlace = i;
                    return i;
                }
            }
            MainForm.emptyTableList[MainForm.tableCounter] = true;
            lastTablePlace = MainForm.tableCounter;
            UserLog.WConsole("Bos olan masa indexi : " + MainForm.tableCounter.ToString());
            return MainForm.tableCounter;
        }

        private void Rezervasyon_Check(object sender, EventArgs e)
        {

        }
    }
}
