using System;
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
        public static string[] menuAciklama = new string[500];
        public static string[] menuID = new string[500];
        public static decimal[] menuPrice = new decimal[500];
        public static string[] tableNumbers = new string[200];
        public static int tableCounter = 0;
        public static int dailyTableCounter = 0;
        public static bool[] emptyTableList = new bool[200];
        public static string[,] tableDetails = new string[300, 10];
        //       0       1          2       3          4                5         6             7            8         9        10
        //    0  id      kacinci    tutar   personel   kac siparis(3)   iskonto   Musteri adi   "R" or "N"   R. date   R.Clock  Time of CloseTable
        //    1  1.id    2.id       3.id  
        //    2  x tane  y tane     z tane
        //    3 
        //    4
        //    5
        //    6
        public static int reservedTableCount = 0;
        public static string[,] reservationList = new string[100, 2];
        public static int stokCount;
        public static int personelCount;
        private bool duzenlemeModu = false;
        private bool personelDuzenlemeModu = false;
        int countOfTables = 1;
        bool tableFlag = false;
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
            SiparisScreenInitialSet();
        }

        private void SiparisScreenInitialSet()
        {
            dgViewSiparis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgViewSiparis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgViewSiparis.ReadOnly = true;
            clearSiparisTable();
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
            }
            catch
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
                LastChoosenTable.reservation = false;
                var result = tableOpenForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    clearSiparisTable();
                    UserLog.WConsole("Opening Table");
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
                    lMasaNo.Text = MasaNo.ToString();
                    lMasaNo.ForeColor = Color.Red;
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
                    tableDetails[TableOpenForm.lastTablePlace * 3, 0] = MasaNo;             //0,0 -> ID (Orn: A1 ,A2 ..)
                    tableDetails[TableOpenForm.lastTablePlace * 3, 1] = dailyTableCounter.ToString();     //0,1 -> Bugun kacinci masa
                    tableDetails[TableOpenForm.lastTablePlace * 3, 2] = "0";                              //0,2 -> Tutar
                    tableDetails[TableOpenForm.lastTablePlace * 3, 3] = PersonelAdi;        //0,3 -> Personel Adi
                    tableDetails[TableOpenForm.lastTablePlace * 3, 4] = "0";                              //0,4 -> Siparis Cesidi
                    tableDetails[TableOpenForm.lastTablePlace * 3, 5] = tableOpenForm.Iskonto.ToString(); //0,5 -> Iskonto
                    tableDetails[TableOpenForm.lastTablePlace * 3, 6] = tableOpenForm.Musteri.ToString(); //0,6 -> Musteri Adi
                    tableDetails[TableOpenForm.lastTablePlace * 3, 7] = "N";                              //0,7 -> R:Rezervasyon - N:Normal
                    tableDetails[TableOpenForm.lastTablePlace * 3, 8] = "";                               //0,8 -> Rezervasyon Tarihi  
                    //Detaylari masa acilirken anlik olarak ekrana yazdirma
                    lTableCounter.Text = "Bugun acilan " + tableDetails[TableOpenForm.lastTablePlace * 3, 1] + ". masa";
                    if (tableDetails[TableOpenForm.lastTablePlace * 3, 7] == "R")
                    {
                        lMasaNo.ForeColor = Color.Turquoise;
                    }
                    else
                    {
                        lMasaNo.ForeColor = Color.Red;
                    }
                    lPersonel.Text = "Personel : " + tableDetails[TableOpenForm.lastTablePlace * 3, 3];
                    lIskonto.Text = "Iskonto Orani : " + tableDetails[TableOpenForm.lastTablePlace * 3, 5] + "%";
                    lMusteriAdi.Text = "Müşteri : " + tableDetails[TableOpenForm.lastTablePlace * 3, 6];
                    //
                    if (tableFlag == false)
                    {
                        UserLog.WConsole("LastTablePlace : " + TableOpenForm.lastTablePlace.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace) / 5));
                        // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                        tableFlag = true;
                    }
                    else
                    {
                        UserLog.WConsole(countOfTables.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace) / 5);
                        // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                        tableFlag = false;
                    }
                    LastChoosenTable.TableNumber = MasaNo.ToString();
                    bSiparisEkle.Enabled = true;
                    bTableClose.Enabled = true;
                    //                 
                }
            }
            if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
            {
                bActiveEt.Visible = true;
                bActiveEt.Enabled = true;
                bSiparisEkle.Enabled = false;
                bTableClose.Text = "R. Iptal";
            }
            else
            {
                bActiveEt.Visible = false;
                bActiveEt.Enabled = false;
                bSiparisEkle.Enabled = true;
                bTableClose.Text = "Masa Kapat";
            }
            bTableClose.Enabled = true;

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
                    menuAciklama[i] = aciklama;
                    menuID[i] = id;
                    menuPrice[i] = Convert.ToDecimal(birimFiyat);

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

            using (var tableOpenForm = new TableOpenForm())
            {
                LastChoosenTable.reservation = true;
                var result = tableOpenForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    reservedTableCount++; // Reserve edilmis masa sayisini bir artir
                    UserLog.WConsole("Rezerve edilmis toplam masa sayisi : " + reservedTableCount.ToString());
                    clearSiparisTable();
                    UserLog.WConsole("Reserving Table");
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
                    bLeft.BackColor = Color.Turquoise;
                    bLeft.Size = new Size(80, 80);
                    bLeft.Click += new EventHandler(masa_click);
                    lMasaNo.Text = MasaNo.ToString();
                    lMasaNo.ForeColor = Color.Turquoise;
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
                    tableDetails[TableOpenForm.lastTablePlace * 3, 0] = tableOpenForm.MasaNo;             //0,0 -> ID (Orn: A1 ,A2 ..)
                    tableDetails[TableOpenForm.lastTablePlace * 3, 1] = dailyTableCounter.ToString();     //0,1 -> Bugun kacinci masa
                    tableDetails[TableOpenForm.lastTablePlace * 3, 2] = "0";                              //0,2 -> Tutar
                    tableDetails[TableOpenForm.lastTablePlace * 3, 3] = tableOpenForm.PersonelAdi;        //0,3 -> Personel Adi
                    tableDetails[TableOpenForm.lastTablePlace * 3, 4] = "0";                              //0,4 -> Siparis Cesidi
                    tableDetails[TableOpenForm.lastTablePlace * 3, 5] = tableOpenForm.Iskonto.ToString(); //0,5 -> Iskonto
                    tableDetails[TableOpenForm.lastTablePlace * 3, 6] = tableOpenForm.Musteri.ToString(); //0,6 -> Musteri Adi
                    tableDetails[TableOpenForm.lastTablePlace * 3, 7] = "R";                              //0,7 -> R:Rezervasyon - N:Normal
                    tableDetails[TableOpenForm.lastTablePlace * 3, 8] = "";                               //0,8 -> Rezervasyon Tarihi  
                    //Detaylari masa acilirken anlik olarak ekrana yazdirma
                    lTableCounter.Text = "Bugun acilan " + tableDetails[TableOpenForm.lastTablePlace * 3, 1] + ". masa";
                    if (tableDetails[TableOpenForm.lastTablePlace * 3, 7] == "R")
                    {
                        lMasaNo.ForeColor = Color.Turquoise;
                    }
                    else
                    {
                        lMasaNo.ForeColor = Color.Red;
                    }
                    lPersonel.Text = "Personel : " + tableDetails[TableOpenForm.lastTablePlace * 3, 3];
                    lIskonto.Text = "Iskonto Orani : " + tableDetails[TableOpenForm.lastTablePlace * 3, 5] + "%";
                    lMusteriAdi.Text = "Musteri : " + tableDetails[TableOpenForm.lastTablePlace * 3, 6];
                    //
                    if (tableFlag == false)
                    {
                        UserLog.WConsole("LastTablePlace : "+TableOpenForm.lastTablePlace.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace) / 5));
                        // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                        tableFlag = true;
                    }
                    else
                    {
                        UserLog.WConsole(countOfTables.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace) / 5);
                        // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                        tableFlag = false;
                    }
                    LastChoosenTable.TableNumber = MasaNo.ToString();
                    bSiparisEkle.Enabled = true;
                    bTableClose.Enabled = true;
                }
            }
            if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
            {
                bActiveEt.Visible = true;
                bActiveEt.Enabled = true;
                bSiparisEkle.Enabled = false;
                bTableClose.Text = "R. Iptal";
            }
            else
            {
                bActiveEt.Visible = false;
                bActiveEt.Enabled = false;
                bSiparisEkle.Enabled = true;
                bTableClose.Text = "Masa Kapat";
            }
            bTableClose.Enabled = true;
        }

        private void masa_click(object sender, EventArgs e)
        {
            UserLog.WConsole("<<< masa_click >>>");
            Button b = sender as Button;
            // Buton fonksiyonu
            UserLog.WConsole("Masanin ID si : " + b.Name);
            string masaNoLocal = b.Name;
            masaNoLocal = masaNoLocal.Replace("bLeft", "");
            lMasaNo.Text = masaNoLocal;
            for (int i = 0; i <= tableCounter; i++)
            {
                if (tableDetails[i * 3, 0] == masaNoLocal)
                {
                    LastChoosenTable.TableNumber = masaNoLocal;
                    UserLog.WConsole("Masanin Adi : \"" + LastChoosenTable.TableNumber.ToString() + "\"");
                    lTableCounter.Text = "Bugun acilan " + tableDetails[i * 3, 1] + ". masa";
                    if (tableDetails[i * 3, 7] == "R")
                    {
                        lMasaNo.ForeColor = Color.Turquoise;
                    }
                    else
                    {
                        lMasaNo.ForeColor = Color.Red;
                    }
                    lPersonel.Text = "Personel : " + tableDetails[i * 3, 3];
                    lIskonto.Text = "Iskonto Orani : " + tableDetails[i * 3, 5] + "%";
                    lMusteriAdi.Text = "Musteri : " + tableDetails[i * 3, 6];
                    UserLog.WConsole("Masa Detayi Ekrana Yazdirildi...");
                }
            }
            if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
            {
                bActiveEt.Visible = true;
                bActiveEt.Enabled = true;
                bSiparisEkle.Enabled = false;
                bTableClose.Text = "R. Iptal";
            }
            else
            {
                bActiveEt.Visible = false;
                bActiveEt.Enabled = false;
                bSiparisEkle.Enabled = true;
                bTableClose.Text = "Masa Kapat";
            }
            bTableClose.Enabled = true;
            clearSiparisTable();
            setSiparisListToTable();
        }
        #endregion
        
        #region PERSONEL PROCESSES
        private void bPersonelDuzenle_Click(object sender, EventArgs e)
        {

            personelDuzenlemeModu = !personelDuzenlemeModu;
            if (personelDuzenlemeModu == true)
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
            UserLog.WConsole("Personel Listesi Kaydetme Basarili (" + personelCount.ToString() + ")");
        }

        private void getPersonelFromFile()
        {//Personel listesini dosyadan ceker
            dgViewWaiter.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                personelCount = Convert.ToInt32(INI.Read("personelCount", "Personel"));
                UserLog.WConsole("Toplam Personel Sayisi : " + personelCount.ToString());
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
            INI.Write("Iskonto", tbDefaultIskontoValue.Text.ToString(), "Settings");
            iskontoRate = Convert.ToInt16(tbDefaultIskontoValue.Text);
        }

        private void key_press(object sender, KeyEventArgs e) //Key Press Handle Function
        {
            UserLog.WConsole("<<< key_press >>>");
            if (e.KeyCode == Keys.F4) //F4 -> NEW TABLE
            {

                using (var tableOpenForm = new TableOpenForm())
                {
                    LastChoosenTable.reservation = false;
                    var result = tableOpenForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        clearSiparisTable();
                        UserLog.WConsole("<<F4 Pressed>> Opening Table ");
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
                        lMasaNo.Text = MasaNo.ToString();
                        lMasaNo.ForeColor = Color.Red;
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
                        tableDetails[TableOpenForm.lastTablePlace * 3, 0] = MasaNo;             //0,0 -> ID (Orn: A1 ,A2 ..)
                        tableDetails[TableOpenForm.lastTablePlace * 3, 1] = dailyTableCounter.ToString();     //0,1 -> Bugun kacinci masa
                        tableDetails[TableOpenForm.lastTablePlace * 3, 2] = "0";                              //0,2 -> Tutar
                        tableDetails[TableOpenForm.lastTablePlace * 3, 3] = PersonelAdi;        //0,3 -> Personel Adi
                        tableDetails[TableOpenForm.lastTablePlace * 3, 4] = "0";                              //0,4 -> Siparis Cesidi
                        tableDetails[TableOpenForm.lastTablePlace * 3, 5] = tableOpenForm.Iskonto.ToString(); //0,5 -> Iskonto
                        tableDetails[TableOpenForm.lastTablePlace * 3, 6] = tableOpenForm.Musteri.ToString(); //0,6 -> Musteri Adi
                        tableDetails[TableOpenForm.lastTablePlace * 3, 7] = "N";                              //0,7 -> R:Rezervasyon - N:Normal
                        tableDetails[TableOpenForm.lastTablePlace * 3, 8] = "";                               //0,8 -> Rezervasyon Tarihi
                        //Detaylari masa acilirken anlik olarak ekrana yazdirma
                        lTableCounter.Text = "Bugun acilan " + tableDetails[TableOpenForm.lastTablePlace * 3, 1] + ". masa";
                        if (tableDetails[TableOpenForm.lastTablePlace * 3, 7] == "R")
                        {
                            lMasaNo.ForeColor = Color.Turquoise;
                        }
                        else
                        {
                            lMasaNo.ForeColor = Color.Red;
                        }
                        lPersonel.Text = "Personel : " + tableDetails[TableOpenForm.lastTablePlace * 3, 3];
                        lIskonto.Text = "Iskonto Orani : " + tableDetails[TableOpenForm.lastTablePlace * 3, 5] + "%";
                        lMusteriAdi.Text = "Musteri : " + tableDetails[TableOpenForm.lastTablePlace * 3, 6];
                        //
                        if (tableFlag == false)
                        {
                            UserLog.WConsole("LastTablePlace : " + TableOpenForm.lastTablePlace.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace) / 5));
                            // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                            tableFlag = true;
                        }
                        else
                        {
                            UserLog.WConsole(countOfTables.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace) / 5);
                            // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                            tableFlag = false;
                        }
                        LastChoosenTable.TableNumber = MasaNo.ToString();
                        if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
                        {
                            bActiveEt.Visible = true;
                            bActiveEt.Enabled = true;
                            bSiparisEkle.Enabled = false;
                            bTableClose.Text = "R. Iptal";
                        }
                        else
                        {
                            bActiveEt.Visible = false;
                            bActiveEt.Enabled = false;
                            bSiparisEkle.Enabled = true;
                            bTableClose.Text = "Masa Kapat";
                        }
                        bTableClose.Enabled = true;
                    }
                }
            }
            if (e.KeyCode == Keys.F5) //F5 -> REZERVATION
            {
                using (var tableOpenForm = new TableOpenForm())
                {
                    LastChoosenTable.reservation = true;
                    var result = tableOpenForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        reservedTableCount++; // reserve edilmis masa sayisini bir artirir
                        UserLog.WConsole("Rezerve edilmis toplam masa sayisi : " + reservedTableCount.ToString());
                        clearSiparisTable();
                        UserLog.WConsole("<<F5 Pressed>> Reserving Table ");
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
                        bLeft.BackColor = Color.Turquoise;
                        bLeft.Size = new Size(80, 80);
                        bLeft.Click += new EventHandler(masa_click);
                        lMasaNo.Text = MasaNo.ToString();
                        lMasaNo.ForeColor = Color.Turquoise;
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
                        tableDetails[TableOpenForm.lastTablePlace * 3, 0] = tableOpenForm.MasaNo;             //0,0 -> ID (Orn: A1 ,A2 ..)
                        tableDetails[TableOpenForm.lastTablePlace * 3, 1] = dailyTableCounter.ToString();     //0,1 -> Bugun kacinci masa
                        tableDetails[TableOpenForm.lastTablePlace * 3, 2] = "0";                              //0,2 -> Tutar
                        tableDetails[TableOpenForm.lastTablePlace * 3, 3] = tableOpenForm.PersonelAdi;        //0,3 -> Personel Adi
                        tableDetails[TableOpenForm.lastTablePlace * 3, 4] = "0";                              //0,4 -> Siparis Cesidi
                        tableDetails[TableOpenForm.lastTablePlace * 3, 5] = tableOpenForm.Iskonto.ToString(); //0,5 -> Iskonto
                        tableDetails[TableOpenForm.lastTablePlace * 3, 6] = tableOpenForm.Musteri.ToString(); //0,6 -> Musteri Adi
                        tableDetails[TableOpenForm.lastTablePlace * 3, 7] = "R";                              //0,7 -> R:Rezervasyon - N:Normal
                        tableDetails[TableOpenForm.lastTablePlace * 3, 8] = "";                               //0,8 -> Rezervasyon Tarihi
                        //Detaylari masa acilirken anlik olarak ekrana yazdirma
                        lTableCounter.Text = "Bugun acilan " + tableDetails[TableOpenForm.lastTablePlace * 3, 1] + ". masa";
                        if (tableDetails[TableOpenForm.lastTablePlace * 3, 7] == "R")
                        {
                            lMasaNo.ForeColor = Color.Turquoise;
                        }
                        else
                        {
                            lMasaNo.ForeColor = Color.Red;
                        }
                        lPersonel.Text = "Personel : " + tableDetails[TableOpenForm.lastTablePlace * 3, 3];
                        lIskonto.Text = "Iskonto Orani : " + tableDetails[TableOpenForm.lastTablePlace * 3, 5] + "%";
                        lMusteriAdi.Text = "Musteri : " + tableDetails[TableOpenForm.lastTablePlace * 3, 6];
                        //  
                        if (tableFlag == false)
                        {
                            UserLog.WConsole("LastTablePlace : " + TableOpenForm.lastTablePlace.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), ((TableOpenForm.lastTablePlace) / 5));
                            // tableLayoutPanel1.Controls.Add(bRight, 1, countOfTables / 2);
                            tableFlag = true;
                        }
                        else
                        {
                            UserLog.WConsole(countOfTables.ToString());
                            tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace) / 5);
                            // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                            tableFlag = false;
                        }
                        LastChoosenTable.TableNumber = MasaNo.ToString();
                        if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
                        {
                            bActiveEt.Visible = true;
                            bActiveEt.Enabled = true;
                            bSiparisEkle.Enabled = false;
                            bTableClose.Text = "R. Iptal";
                        }
                        else
                        {
                            bActiveEt.Visible = false;
                            bActiveEt.Enabled = false;
                            bSiparisEkle.Enabled = true;
                            bTableClose.Text = "Masa Kapat";
                        }
                        bTableClose.Enabled = true;
                    }
                }
            }
        }

        private void ExchangeValuesChanged(object sender, EventArgs e)
        {
            INI.Write("Dolar", tbDolar.Text.ToString(), "Exchange");
            INI.Write("Euro", tbEuro.Text.ToString(), "Exchange");
            INI.Write("GBP", tbGBP.Text.ToString(), "Exchange");
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
                    bool stokFound = false;
                    UserLog.WConsole("Adet Turu : " + StokAdd.AdetTuru.ToString());
                    UserLog.WConsole("Para Birimi : " + StokAdd.ParaBirimi.ToString());
                    //if there is same stock , update it .
                    stokCount = Convert.ToInt32(INI.Read("stokCount", "Stok"));
                    for (int i = 0; i < stokCount; i++)
                    {
                        if (StokAdd.ID == dgView.Rows[i].Cells[0].Value.ToString())
                        {
                            int countItem = Convert.ToInt16(dgView.Rows[i].Cells[2].Value.ToString());
                            countItem += StokAdd.Adet;
                            dgView.Rows[i].Cells[2].Value = countItem.ToString();
                            //*******************************************************
                            //Listedeki urunun sayisinin artirildigini log olarak yaz burada 
                            //*******************************************************
                            saveStokToFile();
                            dgView.Refresh();
                            MessageBox.Show("Listedeki stok guncellendi ( "+StokAdd.Aciklama.ToString()+" )\n"+"Eklenen urun adedi = "+ StokAdd.Adet+"\n"+"Toplam urun adedi = "+ countItem.ToString());
                            stokFound = true;
                            break;
                        }
                    }
                    if (stokFound == false)
                    {
                        try
                        {
                            dgView.Rows.Add(StokAdd.ID, StokAdd.Aciklama, StokAdd.Adet.ToString(), StokAdd.AdetTuru.ToString(), StokAdd.BirimFiyat.ToString(), StokAdd.ParaBirimi.ToString(), CheckState.Unchecked);
                            // dgView.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, CheckState.Checked);
                            saveStokToFile();
                            dgView.Refresh();
                            MessageBox.Show("Yeni stok eklendi ! ( " + StokAdd.Aciklama.ToString() + " )\n"+"Eklenen Miktar = "+StokAdd.Adet.ToString()+"\nSatis Fiyati = "+ StokAdd.BirimFiyat.ToString());
                        }
                        catch
                        {
                            MessageBox.Show("Stok secimi yapilmadi !");
                        }
                    }
                }
            }
        }

        private void bTableClose_Click(object sender, EventArgs e)
        {
            string title = "";
            string text = "";
            if (bTableClose.Text == "Masa Kapat")
            {
                text = "Kapatmak istediginize emin misiniz?";
                title = "Masa Kapatma Uyarisi";
                
            } else
            {
                text = "Rezervasyonu iptal etmek istediginize emin misiniz?";
                title = "Rezervasyon Iptal Uyarisi";
            }

            DialogResult dialogResult = MessageBox.Show("Masa Adi : " + lMasaNo.Text + "\n"+text, title, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                clearSiparisTable();
                string tableName = lMasaNo.Text;
                int i = 0;
                foreach (string tablenames in tableNumbers)
                {
                    if (tableNumbers[i] == tableName)
                    {

                        ////////////////////////////////////////////////////////////////////////////////
                        ////Tutar
                        decimal kasaToplam = Convert.ToDecimal(lKasaToplam.Text);
                        kasaToplam += Convert.ToDecimal(lToplamTutar.Text);
                        lKasaToplam.Text = kasaToplam.ToString();
                        ////Kasa Hareketleri
                        this.lbKasaHareketleri.Text += " :: Masa Kapama :: " + System.DateTime.UtcNow + " :: " + "Masa : " + tableName.ToString() + " :: " +"Tutar : "+lToplamTutar.ToString()+" TL ::"+ " Personel : " + lPersonel.Text.ToString(); 
                        this.lbKasaHareketleri.Text += Environment.NewLine;
                        //this.lbKasaHareketleri.Text += ;
                        ////////////////////////////////////////////////////////////////////////////////


                        tableNumbers[i] = "";
                        emptyTableList[i] = false;
                        tableName = "bLeft" + tableName;
                        tableLayoutPanel1.Controls.RemoveByKey(tableName);
                        lIskonto.Text = "-";
                        lPersonel.Text = "-";
                        lMasaNo.Text = "-";
                        lTableCounter.Text = "-";
                        lMusteriAdi.Text = "-";
                        lMasaNo.ForeColor = Color.Black;
                        LastChoosenTable.TableNumber = "";
                        tableCounter--;
                        bSiparisEkle.Enabled = false;
                        bTableClose.Enabled = false;
                        UserLog.WConsole("Masa : " + tableName + " kapatildi...");
                        UserLog.WConsole("Acik masa sayisi ; " + tableCounter.ToString());
                        lToplamTutar.Text = "-";
                        // tableDetails dizisinden de silinmeli
                    }
                    i++;
                }
                bActiveEt.Visible = false;
                bActiveEt.Enabled = false;
            }
        }

        private void bSiparisEkle_Click(object sender, EventArgs e)
        {
            UserLog.WConsole("<<< bSiparisEkle_Click >>>");
            using (var showListForm = new ShowListForm())
            {
                showListForm.ListName = "Stok";
                var result = showListForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    int countOFRows = dgViewSiparis.Rows.Count;
                    int existedRow = dgViewSiparis.Rows.Count;
                    ///// Searching for the existance of selected meal id
                    for (int i = 0; i < countOFRows; i++)
                    {
                        string id = "";
                        try
                        {
                            id = dgViewSiparis.Rows[i].Cells[0].Value.ToString();
                            id = id.Replace(" ", "");
                        }
                        catch
                        {
                            //throw exception
                        }
                        if (id == showListForm.Selected_Meal_ID)
                        {
                            existedRow = i;
                            break;
                        }
                    }
                    ///// IF Meal id exist on the list +1 or add new row
                    if (existedRow != dgViewSiparis.Rows.Count)
                    {
                        UserLog.WConsole(showListForm.Selected_Meal_ID + " (" + showListForm.Selected_Meal + ")" + " nolu siparis guncelleniyor..");
                        int countOfElement = Convert.ToInt16(dgViewSiparis.Rows[existedRow].Cells[2].Value);
                        UserLog.WConsole(countOfElement.ToString());
                        //Adet
                        dgViewSiparis.Rows[existedRow].Cells[2].Value = (countOfElement + 1).ToString();
                        //Tutar
                        string tutar = dgViewSiparis.Rows[existedRow].Cells[5].Value.ToString();  // 5 TL
                        tutar = tutar.Replace(" TL", "");  // 5
                        tutar = (Convert.ToDecimal(tutar) + Convert.ToDecimal(showListForm.Selected_Meal_Price)).ToString();  // 10
                        dgViewSiparis.Rows[existedRow].Cells[5].Value = tutar + " TL";
                        dgViewSiparis.Refresh();
                        saveAdisyonToTable();
                    }
                    else
                    {
                        UserLog.WConsole(showListForm.Selected_Meal_ID + " (" + showListForm.Selected_Meal + ")" + " nolu siparis listeye ekleniyor..");
                        string mealID = showListForm.Selected_Meal_ID;
                        string meal = showListForm.Selected_Meal;
                        decimal mealPrice = showListForm.Selected_Meal_Price;
                        dgViewSiparis.AllowUserToAddRows = true;
                        //Sutuna ilk degeri yazdirma
                        dgViewSiparis.Rows.Add(mealID, meal, "1", null, null, mealPrice.ToString() + " TL", mealPrice.ToString() + " TL");
                        //UserLog.WConsole("Yemek fiyati : " + mealPrice.ToString());
                        dgViewSiparis.AllowUserToAddRows = false;
                        dgViewSiparis.Refresh();
                        saveAdisyonToTable();
                    }
                    //// End
                }
            }
        }

        private void dgViewSiparis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3) // Siparis miktarini 1 artir
            {
                //UserLog.WConsole(e.RowIndex.ToString());

                int oldCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                dgViewSiparis.Rows[e.RowIndex].Cells[2].Value = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value) + 1).ToString();
                int newCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                //tutar hesabi
                string tutar = dgViewSiparis.Rows[e.RowIndex].Cells[5].Value.ToString(); // 5 TL
                tutar = tutar.Replace(" TL", "");
                tutar = ((Convert.ToDecimal(tutar) / oldCount) * newCount).ToString();  // 10
                dgViewSiparis.Rows[e.RowIndex].Cells[5].Value = tutar + " TL";
            }
            if (e.ColumnIndex == 4) // Siparis miktarini 1 azalt
            {
                UserLog.WConsole((e.RowIndex + 1).ToString() + ". siradaki urune tiklandi"); // Silinmeli
                int adet = Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value);
                if (adet != 1)
                {
                    int oldCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                    dgViewSiparis.Rows[e.RowIndex].Cells[2].Value = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value) - 1).ToString();
                    int newCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                    //tutar hesabi
                    string tutar = dgViewSiparis.Rows[e.RowIndex].Cells[5].Value.ToString(); // 5 TL
                    tutar = tutar.Replace(" TL", "");
                    tutar = (((Convert.ToDecimal(tutar)) / oldCount) * newCount).ToString();
                    dgViewSiparis.Rows[e.RowIndex].Cells[5].Value = tutar + " TL";
                }
                else if (adet == 0)
                {
                    dgViewSiparis.Rows.Remove(dgViewSiparis.Rows[e.RowIndex]);
                    dgViewSiparis.Refresh();
                }
                else
                {
                    //dgViewSiparis.AllowUserToDeleteRows = true;
                    //dgViewSiparis.Refresh();
                    try
                    {
                        dgViewSiparis.Rows.Remove(dgViewSiparis.Rows[e.RowIndex]);
                        dgViewSiparis.Refresh();
                    }
                    catch
                    {
                        UserLog.WConsole("Siparis listendeki " + e.RowIndex + 1 + ". eleman silinirken sorun olustu !");
                    }
                }
            }
            saveAdisyonToTable();
        }

        private void saveAdisyonToTable()
        {
            decimal toplamTutar = 0;
            for (int i = 0; i < dgViewSiparis.RowCount; i++)
            {
                string tutar = dgViewSiparis.Rows[i].Cells[5].Value.ToString();
                tutar = tutar.Replace(" TL", "");
                toplamTutar = toplamTutar + Convert.ToDecimal(tutar);

            }
            lToplamTutar.Text = toplamTutar.ToString() + " TL";
            //Masaya kaydet
            tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 2] = toplamTutar.ToString(); // Tutar
            tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 4] = dgViewSiparis.RowCount.ToString(); // Toplam urun siparis cesidi
            for (int i = 0; i < dgViewSiparis.RowCount; i++)
            {
                tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 1, i] = dgViewSiparis.Rows[i].Cells[0].Value.ToString(); // Urun id si
                tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 2, i] = dgViewSiparis.Rows[i].Cells[2].Value.ToString(); // Urun adedi
            }
            UserLog.WConsole(LastChoosenTable.TableNumber + " nolu masa siparisleri basariyla kaydedildi");
        }

        private int findTableOrder(string tableNameInput) // tableNameInput : masanin adi . BU fonksiyon masanin tableNumbers dizinsindeki sirasini doner
        {
            int i = 0;
            foreach (string names in tableNumbers)
            {
                if (names == tableNameInput)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void setSiparisListToTable() // LastChoosenTable.TableNumber degiskenine set edilmis masa detaylarini dgviewSiparis'e set eder
        {
            UserLog.WConsole("Masanin listedeki sirasi : " + findTableOrder(LastChoosenTable.TableNumber));
            UserLog.WConsole("Masadaki siparis cesidi : " + tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 4]);
            int siparisCount = Convert.ToInt16(tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 4]);
            for (int i = 0; i < siparisCount; i++)
            {
                string mealID = tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 1, i].ToString();
                string meal = findMeal(mealID);
                decimal mealPrice = findMealPrice(mealID);
                int countOfMeal = Convert.ToInt16(tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 2, i]);
                dgViewSiparis.Rows.Add(mealID, meal, countOfMeal, null, null, (mealPrice * countOfMeal).ToString() + " TL", findMealPrice(mealID).ToString() + " TL");
            }
            lToplamTutar.Text = tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 2] + " TL";
            dgViewSiparis.Refresh();
        }

        private void clearSiparisTable() //dgviewSiparis listesini temizler
        {
            dgViewSiparis.Rows.Clear();
            dgViewSiparis.Refresh();
        }

        private string findMeal(string mealID)  // mealID : Yemegin stokda kayitli oldugu id . ID ile request edilen yemegin aciklama kisminda yazani doner.
        {
            for (int i = 0; i < dgView.RowCount; i++)
            {
                if (dgView.Rows[i].Cells[0].Value.ToString() == mealID.ToString())
                {
                    return dgView.Rows[i].Cells[1].Value.ToString();
                }
            }
            return null;
        }

        private decimal findMealPrice(string mealID) // mealID : Yemegin stokda kayitli oldugu id . ID ile request edilen yemegin fiyat kisminda yazani doner.
        {
            for (int i = 0; i < dgView.RowCount; i++)
            {
                if (dgView.Rows[i].Cells[0].Value.ToString() == mealID.ToString())
                {
                    return Convert.ToDecimal(dgView.Rows[i].Cells[4].Value.ToString());
                }
            }
            return 0;
        }

        private void bActiveEt_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Masa Adi : " + lMasaNo.Text + "\n" + "Masayi acmak istediginize emin misiniz?", "Masa Acma Onayi", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                reservedTableCount--;
                UserLog.WConsole("Kalan reserve edilmis masa sayisi : " + reservedTableCount.ToString());
                tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] = "N";
                int i = 0;
                foreach (string tablenames in tableNumbers)
                {
                    string tableName = lMasaNo.Text;
                    if (tableNumbers[i] == tableName)
                    {
                        tableName = "bLeft" + tableName;
                        foreach (Control c in tableLayoutPanel1.Controls)
                        {

                            if (c is Button)
                            {
                                Button ButtonControl = (Button)c;
                                if (ButtonControl.Name.Equals(tableName))
                                {
                                    ButtonControl.ForeColor = Color.Black;
                                    ButtonControl.BackColor = Color.Red;
                                    lMasaNo.ForeColor = Color.Red;
                                    UserLog.WConsole("Masa basariyla acildi");

                                }
                            }
                        }
                        bSiparisEkle.Enabled = true;
                        bActiveEt.Enabled = false;
                    }
                    i++;
                }
                if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
                {
                    bActiveEt.Visible = true;
                    bActiveEt.Enabled = true;
                    bSiparisEkle.Enabled = false;
                    bTableClose.Text = "R. Iptal";
                }
                else
                {
                    bActiveEt.Visible = false;
                    bActiveEt.Enabled = false;
                    bSiparisEkle.Enabled = true;
                    bTableClose.Text = "Masa Kapat";
                }
                bTableClose.Enabled = true;
            }
        }

        private void reservedTableStateChecker(object sender, EventArgs e)
        {
            for (int i = 0; i < reservedTableCount; i++)
            {
                string dateTimeNow = DateTime.UtcNow.ToLocalTime().ToShortTimeString();
                string[] separatedHourAndMinute = dateTimeNow.Split(':');
                // Hour Now : separatedHourAndMinute[0] 
                // Minute Now : separatedHourAndMinute[1]
                // rHour
                // 8  -> Reservation Date
                // 9  -> Reservation Time
                // 10 -> Reservation Limit Time
                //tableDetails dizisinden ilgili masanin reservasyon tarihini ve saatini cek
                ////////////////////////////////////////////////////////////////////////////

                string dateReservation = tableDetails[findTableOrder(reservationList[i, 0])*3, 8]; 
                //string timeReservation = tableDetails[findTableOrder(reservationList[i, 0])*3, 9]; 
                //string[] dateReservationHourandMinute = timeReservation.Split(':');  // Seperated reservation hour and minute

                UserLog.WConsole(DateTime.UtcNow.ToLocalTime().ToShortDateString());
                //if(DateTime.UtcNow.ToLocalTime().ToShortDateString()==)
            }


        }
    }
}
