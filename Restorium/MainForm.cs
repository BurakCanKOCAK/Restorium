﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Drawing;


namespace Restorium
{
    /*
    //tanımlama
    // Creates or loads an INI file in the same directory as your executable
    // named EXE.ini (where EXE is the name of your executable)
    var MyIni = new IniFile();

    // Or specify a specific name in the current dir
    var MyIni = new IniFile("Settings.ini");

    // Or specify a specific name in a specific dir
    var MyIni = new IniFile(@"C:\Settings.ini");
     * 
     * INI.Read(key,section);
     * INI.Write(key,value,section);
     * INI.DeleteKey(key,section);
     * INI.DeleteSection(section);
     * INI.KeyExists(key,section)
     * 
    */
    public partial class MainForm : Form
    {
        #region Variables 
        IniFile INI = new IniFile();
        public static string[] personelNames = new string[100];
        public static string[] menuItems = new string[500];
        public static string[] tableNumbers = new string[200];
        public static int tableCounter=0;
        public static int dailyTableCounter=0;
        public static string[,] tableDetails = new string[6,300];
        public static bool[] emptyTableList = new bool[200];
        //       0       1          2      3         4                5        6             7  8  9  10
        //    0  id      kacinci    tutar  personel  kac siparis(3)   iskonto  Musteri adi
        //    1  1.id    2.id       3.id  
        //    2  x tane  y tane     z tane
        //    3 
        //    4
        //    5
        //    6
        public static int stokCount;
        public static int personelCount;
        private bool duzenlemeModu = false;
        private bool personelDuzenlemeModu = false;
        int countOfTables = 1;
        bool  tableFlag = false;
        private Bitmap bitmap;
        public static int iskontoRate;
        public static string User;
        #endregion
        public MainForm()
        {
            InitializeComponent();
            User = INI.Read("LoggedUser", "Login");
            SetExchangeValues();
            StokDataSet();
            SettingsDataSet();
        }

        private void SetExchangeValues()
        {
            try
            {
                decimal Dolar = Convert.ToDecimal(INI.Read("Dolar", "Exchange"));
                decimal Euro = Convert.ToDecimal(INI.Read("Euro", "Exchange"));
                decimal GBP = Convert.ToDecimal(INI.Read("GBP", "Exchange"));
                tbDolar.Text = Dolar.ToString();
                tbEuro.Text = Euro.ToString();
                tbGBP.Text = GBP.ToString();
                lExchange.Text = "1 ₺ = " + tbDolar.Text.ToString() + " $ = " + tbEuro.Text.ToString() + " € = " + tbGBP.Text.ToString() + " £";
                UserLog.WConsole("Doviz kurlari basariyla okundu !");
            }
            catch
            {
                UserLog.WConsole("Doviz kurlari okumada hata ! (Deger girilmemis!)");
            }
        }
        private void SettingsDataSet()
        {

            this.dgViewWaiter.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            getPersonelFromFile();
            dgViewWaiter.AllowUserToAddRows = false;
            dgViewWaiter.AllowUserToDeleteRows = false;
            dgViewWaiter.AllowUserToOrderColumns = false;
            dgViewWaiter.AllowDrop = false;
            dgViewWaiter.ReadOnly = true;
            dgViewWaiter.Refresh();
            try
            {
                iskontoRate = Convert.ToInt16(INI.Read("Iskonto", "Settings"));
                tbDefaultIskontoValue.Text = iskontoRate.ToString();
            }catch
            {
                UserLog.WConsole("Atanmis bir iskonto degeri bulunamadi !");
            }
            if (User == "Admin")
            {
                dgViewWaiter.ForeColor = Color.Black;
                bPersonelDuzenle.ForeColor = Color.Red;
                tbDefaultIskontoValue.Enabled = true;
            }
            else
            {
                
                dgViewWaiter.ForeColor = Color.Gray;
                bPersonelDuzenle.ForeColor = Color.DarkGray;
                bPersonelDuzenle.Enabled = false;
                tbDefaultIskontoValue.Enabled = false;
            }
        }
        private void bYeniMasa_Click(object sender, EventArgs e)
        {//Yeni masa Acmak icin
            using (var tableOpenForm = new TableOpenForm())
            {
                var result = tableOpenForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dailyTableCounter++; // dailyTableCounter -> Bugun acilan kacinci masa oldugunu gosteriyor
                    tableCounter++;  // countOfTables -> Su anda  mevcut kac tane acik masa oldugunu gosteriyor
                    string PersonelAdi = tableOpenForm.PersonelAdi;            
                    string MasaNo = tableOpenForm.MasaNo;

                        //MessageBox.Show("Masa No :" + MasaNo+"\nPersonel Adi :" +PersonelAdi);
                        //Button Left
                        Button bLeft = new Button();
                        bLeft.Name = "bLeft" + MasaNo.ToString();
                        bLeft.Text = MasaNo.ToString() + "\n(" + dailyTableCounter.ToString() + ")";
                        bLeft.AutoSize = true;
                        bLeft.ForeColor = Color.Black;
                        bLeft.BackColor = Color.Red;
                        bLeft.Size = new Size(80, 80);
                        bLeft.Click += new EventHandler(masa_click);
                        //Button Right
                        /*Button bRight = new Button();
                        bRight.Font = new Font(bRight.Font.FontFamily, 8);
                        bRight.Size = new Size(450, 40);
                        bRight.Text = "Bugun acilan " + dailyTableCounter.ToString()+". Masa" + "\nGarson : "+PersonelAdi + " | Tarih : " + DateTime.UtcNow.ToLocalTime().ToString(); 
                        bRight.Name = "bRight" + MasaNo.ToString();
                        bRight.AutoSize = true;
                        bRight.BackColor = Color.LightGray;
                        bRight.ForeColor = Color.DarkBlue;*/
                        //Masa Soldami Sagdami Olacak (Logic Function)
                        if (tableFlag == false)
                        {
                            UserLog.WConsole(TableOpenForm.lastTablePlace.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace-1)/5));
                           // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                            tableFlag = true;
                        }
                        else
                        {
                            UserLog.WConsole(countOfTables.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace%5), (TableOpenForm.lastTablePlace-1)/5);
                           // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                            tableFlag = false;
                        }
                    //                 
                }
            }

        }
        private void bMasaTasi_Click(object sender, EventArgs e)
        {
            // Masa TASI
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            lDate.Text = DateTime.UtcNow.ToLocalTime().ToString();
        }
        private void Data_Update(object sender, EventArgs e)
        {
            lDate.Text = DateTime.UtcNow.ToLocalTime().ToString();
            if (isConnectedToInternet())
            {
                pbWifi.Image = Properties.Resources.Network_Wifi_icon;
                pbWifi.Refresh();
            }
            else 
            {
                pbWifi.Image = Properties.Resources.no_conection_256;
                pbWifi.Refresh();
            }
            GC.Collect();  //Duruma gore daha sonra kaldirilabilir ya da method degistirilebilir
        }
        #region checking Systems
        private bool isConnectedToInternet()
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion    
        #region STOK PROCESSES
        private void StokDataSet()
        {
            dgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            getStokFromFile();
            if (User != "Admin" && User != "KidemliPersonel")
            {
                bDuzenle.ForeColor = Color.DarkGray;
                bDuzenle.Enabled = false;
            }
               
                DataTable dataTable = new DataTable();
            this.dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void bDuzenle_Click(object sender, EventArgs e)
        {
            // SEKME : STOK  - GOREV : bDuzenle butonuna basildiginda tabloyu editable yapmak
             
                duzenlemeModu = !duzenlemeModu;
                if (duzenlemeModu == true)
                {
                    //dgView.AllowUserToAddRows = true;
                    dgView.AllowUserToDeleteRows = true;
                    dgView.AllowUserToOrderColumns = true;
                    dgView.AllowDrop = true;
                    dgView.ReadOnly = false;
                    dgView.Refresh();
                    bDuzenle.BackColor = Color.Green;
                    bDuzenle.Text = "Duzenle(Acik)";
                    dgView.SelectionMode = DataGridViewSelectionMode.CellSelect;

            }
                else
                {
                    //dgView.AllowUserToAddRows = false;
                    dgView.AllowUserToDeleteRows = false;
                    dgView.AllowUserToOrderColumns = false;
                    dgView.AllowDrop = false;
                    dgView.ReadOnly = true;
                    dgView.Refresh();
                    bDuzenle.BackColor = Color.Silver;
                    bDuzenle.Text = "Duzenle(Kapali)";
                    dgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    saveStokToFile();
                
            }
        }
        #endregion
        #region STOK DATA SAVE / LOAD / SEARCH FROM FILE
        private void saveStokToFile()
        {
            int stokCount = dgView.RowCount;
            INI.DeleteSection("Stok");
            INI.Write("stokCount", stokCount.ToString(), "Stok");
            for (int i = 0; i < stokCount; i++)
            {
                INI.Write("id" + i.ToString(), dgView.Rows[i].Cells[0].Value.ToString(), "Stok");
                INI.Write("aciklama" + i.ToString(), dgView.Rows[i].Cells[1].Value.ToString(), "Stok");
                INI.Write("adet" + i.ToString(), dgView.Rows[i].Cells[2].Value.ToString(), "Stok");
                INI.Write("birim" + i.ToString(), dgView.Rows[i].Cells[3].Value.ToString(), "Stok");
                INI.Write("birimFiyat" + i.ToString(), dgView.Rows[i].Cells[4].Value.ToString(), "Stok");
                INI.Write("paraBirimi" + i.ToString(), dgView.Rows[i].Cells[5].Value.ToString(), "Stok");
                // UserLog.WConsole(dgView.Rows[i].Cells[6].Value.ToString());
                bool checkBoxStatus = Convert.ToBoolean(dgView.Rows[i].Cells[6].Value);
                if (checkBoxStatus == false)
                {
                    INI.Write("dinamikStokKontrolu" + i.ToString(), "false", "Stok");
                }
                else
                {
                    INI.Write("dinamikStokKontrolu" + i.ToString(), "true", "Stok");
                }

            }
            UserLog.WConsole("Stok Listesi Kaydetme Basarili");

        }
        private void getStokFromFile()
        {//Dosyadan Stok Datasini alik gdView'a set eder
            try
            {
                stokCount = Convert.ToInt32(INI.Read("stokCount", "Stok"));
                UserLog.WConsole("Toplam Stok Urunu Sayisi : " + stokCount.ToString());
                for (int i = 0; i < stokCount; i++)
                {
                    string id = INI.Read("id" + i.ToString(), "Stok");
                    string aciklama = INI.Read("aciklama" + i.ToString(), "Stok");
                    string adet = INI.Read("adet" + i.ToString(), "Stok");
                    string birim = INI.Read("birim" + i.ToString(), "Stok");
                    string birimFiyat = INI.Read("birimFiyat" + i.ToString(), "Stok");
                    string paraBirimi = INI.Read("paraBirimi" + i.ToString(), "Stok");
                    string dinamikStokKontrolu = INI.Read("dinamikStokKontrolu" + i.ToString(), "Stok");
                    menuItems[i] = aciklama;
                    if (dinamikStokKontrolu == "true")
                    {
                        dgView.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, CheckState.Checked);
                    }
                    else
                    {
                        dgView.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, CheckState.Unchecked);
                    }
                    dgView.Refresh();
                }
                UserLog.WConsole("Dosyadan Stok Okuma Basarili !");

            }
            catch
            {
                UserLog.WConsole(" (HATA) Dosyadan Stok Okuma Hatasi !");
            }

        }
        private void bStokAra_Click(object sender, EventArgs e)
        {
            //tbSearchKey'de yazani al ona gore DGView'de arama yap.
        }       
        #endregion
        #region  Adisyon
        private void bRezervasyon_Click(object sender, EventArgs e)
        {
            UserLog.WConsole("Reserving Table ");
            using (var tableOpenForm = new TableOpenForm())
            {
                var result = tableOpenForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dailyTableCounter++; // dailyTableCounter -> Bugun acilan kacinci masa oldugunu gosteriyor
                    tableCounter++;  // countOfTables -> Su anda  mevcut kac tane acik masa oldugunu gosteriyor
                    string PersonelAdi = tableOpenForm.PersonelAdi;
                    string MasaNo = tableOpenForm.MasaNo;

                    //MessageBox.Show("Masa No :" + MasaNo+"\nPersonel Adi :" +PersonelAdi);
                    //Button Left
                    Button bLeft = new Button();
                    bLeft.Name = "bLeft" + MasaNo.ToString();
                    bLeft.Text = MasaNo.ToString() + "\n(" + dailyTableCounter.ToString() + ")";
                    bLeft.AutoSize = true;
                    bLeft.ForeColor = Color.Black;
                    bLeft.BackColor = Color.Red;
                    bLeft.Size = new Size(80, 80);
                    bLeft.Click += new EventHandler(masa_click);
                    //Button Right
                    /*Button bRight = new Button();
                    bRight.Font = new Font(bRight.Font.FontFamily, 8);
                    bRight.Size = new Size(450, 40);
                    bRight.Text = "Bugun acilan " + dailyTableCounter.ToString()+". Masa" + "\nGarson : "+PersonelAdi + " | Tarih : " + DateTime.UtcNow.ToLocalTime().ToString(); 
                    bRight.Name = "bRight" + MasaNo.ToString();
                    bRight.AutoSize = true;
                    bRight.BackColor = Color.LightGray;
                    bRight.ForeColor = Color.DarkBlue;*/
                    //Masa Soldami Sagdami Olacak (Logic Function)
                    if (tableFlag == false)
                    {
                        UserLog.WConsole(TableOpenForm.lastTablePlace.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace - 1) / 5));
                        // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                        tableFlag = true;
                    }
                    else
                    {
                        UserLog.WConsole(countOfTables.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace - 1) / 5);
                        // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                        tableFlag = false;
                    }
                }
            }
          }
        private void masa_click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            // Buton fonksiyonu
            UserLog.WConsole(b.Name);
            string masaNoLocal = b.Name;
            masaNoLocal = masaNoLocal.Replace("bLeft", "");
            lMasaNo.Text = masaNoLocal; 

        }
        #endregion
        #region PERSONEL PROCESSES
        private void bPersonelDuzenle_Click(object sender, EventArgs e)
        {

            personelDuzenlemeModu = !personelDuzenlemeModu;
            if (personelDuzenlemeModu ==true)
            {
                dgViewWaiter.AllowUserToAddRows = true;
                dgViewWaiter.AllowUserToDeleteRows = true;
                dgViewWaiter.AllowUserToOrderColumns = true;
                dgViewWaiter.AllowDrop = false;
                dgViewWaiter.ReadOnly = false;
                dgViewWaiter.Refresh();
                dgViewWaiter.SelectionMode = DataGridViewSelectionMode.CellSelect;
                //dgViewWaiter.ForeColor = Color.Black;
                bPersonelDuzenle.ForeColor = Color.Green;
            }
            else
            {
                dgViewWaiter.AllowUserToAddRows = false;
                dgViewWaiter.AllowUserToDeleteRows = false;
                dgViewWaiter.AllowUserToOrderColumns = false;
                dgViewWaiter.AllowDrop = false;
                dgViewWaiter.ReadOnly = true;
                dgViewWaiter.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgViewWaiter.Refresh();
                //dgViewWaiter.ForeColor = Color.Gray;
                bPersonelDuzenle.ForeColor = Color.Red;
                savePersonnelToFile();
            }

        }
        private void savePersonnelToFile()
        {//Personel Listesini duzenleme bitince dosyaya kaydeder.
            personelCount = dgViewWaiter.RowCount;
            INI.DeleteSection("Personel");
            INI.Write("personelCount", personelCount.ToString(), "Personel");
            for (int i = 0; i < personelCount; i++)
            {
                INI.Write("id" + i.ToString(), dgViewWaiter.Rows[i].Cells[0].Value.ToString(), "Personel");
                INI.Write("name" + i.ToString(), dgViewWaiter.Rows[i].Cells[1].Value.ToString(), "Personel");
                INI.Write("work" + i.ToString(), dgViewWaiter.Rows[i].Cells[2].Value.ToString(), "Personel");
                personelNames[i] = dgViewWaiter.Rows[i].Cells[1].Value.ToString();
            }
            UserLog.WConsole("Personel Listesi Kaydetme Basarili ("+ personelCount.ToString()+")");
        }
        private void getPersonelFromFile()
        {//Personel listesini dosyadan ceker
            dgViewWaiter.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                personelCount = Convert.ToInt32(INI.Read("personelCount", "Personel"));
                UserLog.WConsole("Toplam Personel Sayisi : "+personelCount.ToString());
                for (int i = 0; i < personelCount; i++)
                {
                  string id = INI.Read("id" + i.ToString(), "Personel");
                  string name = INI.Read("name" + i.ToString(), "Personel");
                  string work = INI.Read("work" + i.ToString(), "Personel");
                  personelNames[i] = name;
                  dgViewWaiter.Rows.Add(id, name, work);
                  dgViewWaiter.Refresh();
                }
                UserLog.WConsole("Dosyadan Personel Okuma Basarili !");
               
            }
            catch
            {
                UserLog.WConsole(" (HATA) Dosyadan Personel Okuma Hatasi !");
            }
        }
        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Print Function
            
        }
        private void MainForm_Minimize(object sender, FormClosingEventArgs e)
        {
            this.MinimizeBox = true;
        }
        private void IskontoValueChanged(object sender, EventArgs e)
        {
          INI.Write("Iskonto",tbDefaultIskontoValue.Text.ToString(), "Settings");
          iskontoRate = Convert.ToInt16(tbDefaultIskontoValue.Text);
        }
        private void key_press(object sender, KeyEventArgs e) //Key Press Handle Function
        {
            if (e.KeyCode == Keys.F4) //F4 -> NEW TABLE
            {
                UserLog.WConsole("<<F4 Pressed>> Opening Table ");
                using (var tableOpenForm = new TableOpenForm())
                {
                    var result = tableOpenForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        dailyTableCounter++; // dailyTableCounter -> Bugun acilan kacinci masa oldugunu gosteriyor
                        tableCounter++;  // countOfTables -> Su anda  mevcut kac tane acik masa oldugunu gosteriyor
                        string PersonelAdi = tableOpenForm.PersonelAdi;
                        string MasaNo = tableOpenForm.MasaNo;

                        //MessageBox.Show("Masa No :" + MasaNo+"\nPersonel Adi :" +PersonelAdi);
                        //Button Left
                        Button bLeft = new Button();
                        bLeft.Name = "bLeft" + MasaNo.ToString();
                        bLeft.Text = MasaNo.ToString() + "\n(" + dailyTableCounter.ToString() + ")";
                        bLeft.AutoSize = true;
                        bLeft.ForeColor = Color.Black;
                        bLeft.BackColor = Color.Red;
                        bLeft.Size = new Size(80, 80);
                        bLeft.Click += new EventHandler(masa_click);
                        //Button Right
                        /*Button bRight = new Button();
                        bRight.Font = new Font(bRight.Font.FontFamily, 8);
                        bRight.Size = new Size(450, 40);
                        bRight.Text = "Bugun acilan " + dailyTableCounter.ToString()+". Masa" + "\nGarson : "+PersonelAdi + " | Tarih : " + DateTime.UtcNow.ToLocalTime().ToString(); 
                        bRight.Name = "bRight" + MasaNo.ToString();
                        bRight.AutoSize = true;
                        bRight.BackColor = Color.LightGray;
                        bRight.ForeColor = Color.DarkBlue;*/
                        //Masa Soldami Sagdami Olacak (Logic Function)
                        if (tableFlag == false)
                        {
                            UserLog.WConsole(TableOpenForm.lastTablePlace.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace - 1) / 5));
                            // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                            tableFlag = true;
                        }
                        else
                        {
                            UserLog.WConsole(countOfTables.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace - 1) / 5);
                            // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                            tableFlag = false;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.F5) //F5 -> REZERVATION
            {
                UserLog.WConsole("<<F5 Pressed>> Reserving Table ");
                using (var tableOpenForm = new TableOpenForm())
                {
                    var result = tableOpenForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        dailyTableCounter++; // dailyTableCounter -> Bugun acilan kacinci masa oldugunu gosteriyor
                        tableCounter++;  // countOfTables -> Su anda  mevcut kac tane acik masa oldugunu gosteriyor
                        string PersonelAdi = tableOpenForm.PersonelAdi;
                        string MasaNo = tableOpenForm.MasaNo;

                        //MessageBox.Show("Masa No :" + MasaNo+"\nPersonel Adi :" +PersonelAdi);
                        //Button Left
                        Button bLeft = new Button();
                        bLeft.Name = "bLeft" + MasaNo.ToString();
                        bLeft.Text = MasaNo.ToString() + "\n(" + dailyTableCounter.ToString() + ")";
                        bLeft.AutoSize = true;
                        bLeft.ForeColor = Color.Black;
                        bLeft.BackColor = Color.Red;
                        bLeft.Size = new Size(80, 80);
                        bLeft.Click += new EventHandler(masa_click);
                        //Button Right
                        /*Button bRight = new Button();
                        bRight.Font = new Font(bRight.Font.FontFamily, 8);
                        bRight.Size = new Size(450, 40);
                        bRight.Text = "Bugun acilan " + dailyTableCounter.ToString()+". Masa" + "\nGarson : "+PersonelAdi + " | Tarih : " + DateTime.UtcNow.ToLocalTime().ToString(); 
                        bRight.Name = "bRight" + MasaNo.ToString();
                        bRight.AutoSize = true;
                        bRight.BackColor = Color.LightGray;
                        bRight.ForeColor = Color.DarkBlue;*/
                        //Masa Soldami Sagdami Olacak (Logic Function)
                        if (tableFlag == false)
                        {
                            UserLog.WConsole(TableOpenForm.lastTablePlace.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace - 1) / 5));
                            // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                            tableFlag = true;
                        }
                        else
                        {
                            UserLog.WConsole(countOfTables.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace - 1) / 5);
                            // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                            tableFlag = false;
                        }
                    }
                }
            }
        }
        private void ExchangeValuesChanged(object sender, EventArgs e)
        {
            INI.Write("Dolar" , tbDolar.Text.ToString(), "Exchange");
            INI.Write("Euro" , tbEuro.Text.ToString(), "Exchange");
            INI.Write("GBP" ,tbGBP.Text.ToString(), "Exchange");
            lExchange.Text = "1 ₺ = " + tbDolar.Text.ToString() + " $ = " + tbEuro.Text.ToString() + " € = " + tbGBP.Text.ToString() + " £";
            UserLog.WConsole("Doviz kurlari basariyla kaydedildi !");

        }
        private void Shutdown(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void bCalculator_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
        } 
        private void StokAdd_Click(object sender, EventArgs e)
        {
            using (var stokAdd = new StokAdd())
            {
                //stokAdd.ListName = "Personel";
                var result = stokAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {

                    }
                    catch
                    {
                        MessageBox.Show("Personel secimi yapilmadi !");
                    }
                }
            }
        }
        private void bTableClose_Click(object sender, EventArgs e)
        {
            string tableName = lMasaNo.Text;
            for (int i = 0; i < tableCounter; i++)
            {
                if (tableNumbers[i] == tableName)
                {
                    tableNumbers[i] = "";
                    emptyTableList[i] = false;
                    tableName = "bLeft" + tableName;
                    tableLayoutPanel1.Controls.RemoveByKey(tableName);
                    tableCounter--;
                }
            }
        }
    }
}
