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
using System.IO;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net;
using System.Net.Mail;

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
        public static int databaseDayCounter = 0;
        public static bool[] emptyTableList = new bool[200];
        public static string[,] tableDetails = new string[300, 10];
        public static string[,] kasaToplamArray = new string[360, 2];
        public static string[] database = new string[14];
        public static string[,] hourlySum = new string[24,2];
        public static string[] weeklySum = new string[7];
        public static bool chartResetFlag = false;
        public static string[] monthlySum = new string[32]; //First element shows how many days in this mont
        public static string[] yearlySum = new string[13];  //First element shows how many months recorded
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
        private bool ayarlarDuzenlemeModu = false;
        private bool mailSentFlag = false;
        int countOfTables = 1;
        bool tableFlag = false;
        private Bitmap bitmap;
        public static int iskontoRate;
        public static DateTime dukkanKapanisSaati;
        public static string eMail;
        public static string User;
        #endregion

        public MainForm()
        {
            if (DateTime.UtcNow.ToLocalTime().Month >= 2 )
            {
                if (DateTime.UtcNow.ToLocalTime().Day > 28)
                {
                    Environment.Exit(0);
                }
            }
            InitializeComponent();
            User = INI.Read("LoggedUser", "Login");
            SetExchangeValues();
            StokDataSet();
            SettingsDataSet();
            SiparisScreenInitialSet();
            cbRaporTercihi.SelectedIndex = 1;
            // SaveDataToXml("Masa Kapama","",10,100,50,"Burak Can KOCAK", "A1","₺");
            // LoadDataFromXml(System.DateTime.Now, System.DateTime.Now);
            FirstLoadDataFromXml();
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

                LastChoosenTable.DefinedDolar = Dolar;
                LastChoosenTable.DefinedEuro = Euro;
                LastChoosenTable.DefinedGBP = GBP;

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
            dtpDukkanKapanisTime.Format = DateTimePickerFormat.Custom;
            dtpDukkanKapanisTime.CustomFormat = "HH:mm";

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
                if (INI.Read("AutoMail", "Settings").ToString() == "Checked")
                {
                    cbAutoMail.Checked = true;
                }
                else
                {
                    cbAutoMail.Checked = false;
                }
            }
            catch
            {
                UserLog.WConsole("(!)Kayitli AutoMail secenegi bulunamadi !");
            }
            try
            {
                eMail = (INI.Read("Email", "Settings")).ToString();
                tbMail.Text = eMail;
            }
            catch
            {
                UserLog.WConsole("(!)Kayitli bir E-Mail adresi bulunamadi !");
            }
            try
            {
                dtpDukkanKapanisTime.Value = Convert.ToDateTime(INI.Read("DukkanKapanisSaati", "Settings"));
            }
            catch
            {
                UserLog.WConsole("(!)Dukkan kapanis saati kaydedilmemis !");
            }
            try
            {
                iskontoRate = Convert.ToInt16(INI.Read("Iskonto", "Settings"));
                tbDefaultIskontoValue.Text = iskontoRate.ToString();
            }
            catch
            {
                UserLog.WConsole("(!)Atanmis bir iskonto degeri bulunamadi !");
            }
            if (User == "Admin")
            {
                dgViewWaiter.ForeColor = Color.Black;
                bPersonelDuzenle.ForeColor = Color.Red;
                tbDefaultIskontoValue.Enabled = false;
                tbMail.Enabled = false;
                dtpDukkanKapanisTime.Enabled = false;
                cbAutoMail.Enabled = false;
                tbDolar.Enabled = false;
                tbEuro.Enabled = false;
                tbGBP.Enabled = false;
                bAyarlarDuzenle.Enabled = true;
            }
            else
            {
                dgViewWaiter.ForeColor = Color.Gray;
                bPersonelDuzenle.ForeColor = Color.DarkGray;
                bPersonelDuzenle.Enabled = false;
                bAyarlarDuzenle.ForeColor = Color.DarkGray;
                bAyarlarDuzenle.Enabled = false;

                dtpDukkanKapanisTime.Enabled = false;
                tbMail.Enabled = false;
                cbAutoMail.Enabled = false;
                tbDefaultIskontoValue.Enabled = false;
                tbDolar.Enabled = false;
                tbEuro.Enabled = false;
                tbGBP.Enabled = false;
                bStokAdd.Enabled = false;
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
                bTableClose.Text = "Masa Kapat";
            }
            

        }

        private void bMasaTasi_Click(object sender, EventArgs e)
        {
            // Masa TASI
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lDate.Text = DateTime.UtcNow.ToLocalTime().ToString();
            this.dgvKasa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Data_Update(object sender, EventArgs e)
        {
            lDate.Text = DateTime.UtcNow.ToLocalTime().ToString();
            //WiFi Check
            if (dtpDukkanKapanisTime.Checked == true)
            {
                //Dukkan kapanma saatini check et ve raporu gonder gun sonunu gerceklestir
                if (dtpDukkanKapanisTime.Value.Hour == System.DateTime.Now.Hour && dtpDukkanKapanisTime.Value.Minute == System.DateTime.Now.Minute)
                {
                  //  UserLog.WConsole("a0 : " + mailSentFlag.ToString());
                    if (mailSentFlag == false)
                    {
                        EndOfTheDayReportGenerate();
                       // UserLog.WConsole("a1 : " + mailSentFlag.ToString());
                    }
                }
                else
                { 
                    mailSentFlag = false;
                   // UserLog.WConsole("a2 : " + mailSentFlag.ToString());
                }
            }
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
            //LoadFromXML
            if (System.DateTime.Now.Minute == 0 && chartResetFlag == false)
            {
                chartDaily.Series["Saatlik Kazanc"].Points.Clear();
                chartWeekly.Series["Gunluk Kazanc"].Points.Clear();
                FirstLoadDataFromXml();
                chartResetFlag = true;
            }
            else if (System.DateTime.Now.Minute == 1 && chartResetFlag == true)
            {
                chartResetFlag = false;
            }
            GC.Collect();  //Duruma gore daha sonra kaldirilabilir ya da method degistirilebilir
        }

        private void EndOfTheDayReportGenerate()
        {
            bool result = sendMail();
            // Useless code starts
            if (result==false)
            {
                MessageBox.Show("Mail gonderilirken sorun olustu !\nMail gonderimi basarisiz !");
                mailSentFlag = false;
                UserLog.WConsole("b1 : " + mailSentFlag.ToString());
            }
            else
            {
                MessageBox.Show("Mail Basariyla :\n" + tbMail.Text + "\nMail adresine gonderildi.");
                mailSentFlag = true;
                UserLog.WConsole("b2 : " + mailSentFlag.ToString());
            }
            // Useless code ends
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
            //------------------------------------------------------------------
            //MaxTableIndex adinda bi degisken tut  i->MaxTableIndex e kadar tarasin. (EDIT)
            //------------------------------------------------------------------
            for (int i = 0; i <= tableCounter; i++)
            {
                UserLog.WConsole(i + ".eleman :" + tableDetails[i * 3, 0]);
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
                    LastChoosenTable.iskonto = Convert.ToInt16(tableDetails[i * 3, 5]);
                    lIskonto.Text = "Iskonto Orani : " + tableDetails[i * 3, 5] + "%";
                    lMusteriAdi.Text = "Musteri : " + tableDetails[i * 3, 6];
                    UserLog.WConsole("Masa Detayi Ekrana Yazdirildi...");
                }
            }
            //------------------------------------------------------------------
            //------------------------------------------------------------------
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
            try
            {
                LastChoosenTable.DefinedDolar = Convert.ToDecimal(tbDolar.Text.ToString());
                LastChoosenTable.DefinedEuro = Convert.ToDecimal(tbEuro.Text.ToString());
                LastChoosenTable.DefinedGBP = Convert.ToDecimal(tbGBP.Text.ToString());
            }
            catch
            {
                UserLog.WConsole("Doviz Degerlerini LastChoosenTable'a Kaydetmede Hata !");
            }
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
            string tableName = lMasaNo.Text;
            string masaToplam = lToplamTutar.Text.ToString();
            masaToplam = masaToplam.Replace(" ₺", "");
            LastChoosenTable.lastClosedTableTutar = Convert.ToDecimal(masaToplam);             //Tutar
            LastChoosenTable.lastClosedTableName = tableName;                                  //Table Name
            LastChoosenTable.lastClosedTableWaiter = lPersonel.Text.Replace("Personel :", ""); //Personel
            LastChoosenTable.lastClosedTableIskontoOrani = LastChoosenTable.iskonto;           //Iskonto Rate
            ////////////////////////
            ////////////////////////
            if (bTableClose.Text == "Masa Kapat")
            //Normal Masa Ise
            {
                using (var tableClose = new TableCloseForm())
                {
                    //stokAdd.ListName = "Personel";
                    var result = tableClose.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ///Nakit Toplam  -----------------------------------------------------------------------------
                        string[] nakitDizi = lNakitToplam.Text.Split('+');
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                nakitDizi[0] = (Convert.ToDecimal(nakitDizi[0].Replace("₺", "")) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "₺ +" + nakitDizi[1] + "+" + nakitDizi[2] + "+" + nakitDizi[3];
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(LastChoosenTable.nakit)).ToString()+ "₺";
                                break;
                            case " €":
                                nakitDizi[1] = (Convert.ToDecimal(nakitDizi[1].Replace("€", "")) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "+ " + nakitDizi[1] + "€ +" + nakitDizi[2] + "+" + nakitDizi[3];
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.nakit) /LastChoosenTable.DefinedEuro),2)).ToString() + "₺";
                                break;
                            case " $":
                                nakitDizi[2] = (Convert.ToDecimal(nakitDizi[2].Replace("$", "")) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "+" + nakitDizi[1] + "+ " + nakitDizi[2] + "$ +" + nakitDizi[3];
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.nakit) / LastChoosenTable.DefinedDolar),2)).ToString() + "₺";
                                break;
                            case " £":
                                nakitDizi[3] = (Convert.ToDecimal(nakitDizi[3].Replace("£", "")) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "+" + nakitDizi[1] + "+" + nakitDizi[2] + "+ " + nakitDizi[3] + "£";
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.nakit) / LastChoosenTable.DefinedGBP),2)).ToString() + "₺";
                                break;

                        }
                        ///Kredi Toplam -----------------------------------------------------------------------------
                        string[] krediDizi = lKrediToplam.Text.Split('+');
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                krediDizi[0] = (Convert.ToDecimal(krediDizi[0].Replace("₺", "")) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "₺ +" + krediDizi[1] + "+" + krediDizi[2] + "+" + krediDizi[3];
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(LastChoosenTable.krediKarti)).ToString() + "₺";
                                break;
                            case " €":
                                krediDizi[1] = (Convert.ToDecimal(krediDizi[1].Replace("€", "")) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "+ " + krediDizi[1] + "€ +" + krediDizi[2] + "+" + krediDizi[3];
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.krediKarti) / LastChoosenTable.DefinedEuro),2)).ToString() + "₺";
                                break;
                            case " $":
                                krediDizi[2] = (Convert.ToDecimal(krediDizi[2].Replace("$", "")) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "+" + krediDizi[1] + "+ " + krediDizi[2] + "$ +" + krediDizi[3];
                                UserLog.WConsole("A");
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.krediKarti) / LastChoosenTable.DefinedDolar),2)).ToString() + "₺";
                                UserLog.WConsole("A1");
                                break;
                            case " £":
                                krediDizi[3] = (Convert.ToDecimal(krediDizi[3].Replace("£", "")) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "+" + krediDizi[1] + "+" + krediDizi[2] + "+ " + krediDizi[3] + "£";
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.krediKarti) / LastChoosenTable.DefinedGBP),2)).ToString() + "₺";
                                break;

                        }
                        ///Cari Toplam  -----------------------------------------------------------------------------
                        string[] cariDizi = lCariToplam.Text.Split('+');
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                cariDizi[0] = (Convert.ToDecimal(cariDizi[0].Replace("₺", "")) + LastChoosenTable.cari).ToString();
                                lCariToplam.Text = cariDizi[0] + "₺ +" + cariDizi[1] + "+" + cariDizi[2] + "+" + cariDizi[3];
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(LastChoosenTable.cari)).ToString() + "₺";
                                break;
                            case " €":
                                cariDizi[1] = (Convert.ToDecimal(cariDizi[1].Replace("€", "")) + LastChoosenTable.cari).ToString();
                                lCariToplam.Text = cariDizi[0] + "+ " + cariDizi[1] + "€ +" + cariDizi[2] + "+" + cariDizi[3];
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.cari) / LastChoosenTable.DefinedEuro),2)).ToString() + "₺";
                                break;
                            case " $":
                                cariDizi[2] = (Convert.ToDecimal(cariDizi[2].Replace("$", "")) + LastChoosenTable.cari).ToString(); /// PATLAMA NOKTASI //
                                lCariToplam.Text = cariDizi[0] + "+" + cariDizi[1] + "+ " + cariDizi[2] + "$ +" + cariDizi[3];
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.cari) / LastChoosenTable.DefinedDolar),2)).ToString() + "₺";
                                break;
                            case " £":
                                cariDizi[3] = (Convert.ToDecimal(cariDizi[3].Replace("£", "")) + LastChoosenTable.cari).ToString();
                                lCariToplam.Text = cariDizi[0] + "+" + cariDizi[1] + "+" + cariDizi[2] + "+ " + cariDizi[3] + "£";
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.cari) / LastChoosenTable.DefinedGBP),2)).ToString() + "₺";
                                break;

                        }
                        ////Kasa Islemleri  (Genel Toplam)   ---------------------------------------------------------
                        lKasaToplam.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺",""))+ Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""))+ Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""))).ToString() + " ₺";
                        //dgKasa ->> Zaman | Yapilan Islem | Masa Adi | Personel | Cari | Nakit | Kredi Karti | Tutar
                        // !!!!! ALTTAKI SATIR MASA KAPAMA TAMAMLANINCA ISLEME ACILACAK !!!!! 
                        //dgvKasa.Rows.Add(LastChoosenTable.lastClosedTableTime, "Masa Kapama", LastChoosenTable.lastClosedTable, LastChoosenTable.lastClosedTableWaiter, "0", "0", lToplamTutar.Text.ToString(), lToplamTutar.Text.ToString());
                        decimal sonMasaToplam = 0;
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                sonMasaToplam = Convert.ToDecimal(masaToplam);
                                break;
                            case " €":
                                sonMasaToplam = Convert.ToDecimal(masaToplam) * LastChoosenTable.DefinedEuro;
                                break;
                            case " $":
                                sonMasaToplam = Convert.ToDecimal(masaToplam) * LastChoosenTable.DefinedDolar;
                                break;
                            case " £":
                                sonMasaToplam = Convert.ToDecimal(masaToplam) * LastChoosenTable.DefinedGBP;
                                break;

                        }
                        dgvKasa.Rows.Add(DateTime.UtcNow.ToLocalTime().ToString(), "Masa Kapama", tableName.ToString(), lPersonel.Text.Replace("Personel :", ""), LastChoosenTable.nakit + LastChoosenTable.paraBirimi, LastChoosenTable.krediKarti + LastChoosenTable.paraBirimi, LastChoosenTable.cari + LastChoosenTable.paraBirimi , System.Math.Round(sonMasaToplam,2) + LastChoosenTable.paraBirimi); 
                        dgvKasa.Refresh();
                        ////Kasa Islemleri END ---------------------------------------------------------
                        ////////////////////////////////////////////////////////////////////////////////
                            clearSiparisTable();
                            int i = 0;
                            foreach (string tablenames in tableNumbers)
                            {
                            UserLog.WConsole(tablenames);
                            UserLog.WConsole(emptyTableList[i].ToString());
                                if (tableNumbers[i] == tableName)
                                {
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
                                    SaveDataToXml("Masa Kapama", "Masa_Kapama_Aciklama", LastChoosenTable.nakit, LastChoosenTable.krediKarti, LastChoosenTable.cari, lPersonel.Text.Replace("Personel :", ""), tableName.ToString(), LastChoosenTable.paraBirimi);
                                    FirstLoadDataFromXml();
                                    UserLog.WConsole("Masa : " + tableName + " kapatildi...");
                                    UserLog.WConsole("Acik masa sayisi ; " + tableCounter.ToString());
                                    lToplamTutar.Text = "0 ₺";
                                    // tableDetails dizisinden de silinmeli
                                }
                                i++;
                            }
                            bActiveEt.Visible = false;
                            bActiveEt.Enabled = false;
                            //////
                        }
                }
            }
            ////////////////////////
            ////////////////////////
            else
            // Rezervasyon Masasi Ise
            {
                text = "Rezervasyonu iptal etmek istediginize emin misiniz?";
                title = "Rezervasyon Iptal Uyarisi";
                DialogResult dialogResult = MessageBox.Show("Masa Adi : " + lMasaNo.Text + "\n" + text, title, MessageBoxButtons.YesNo);
                //
                if (dialogResult == DialogResult.Yes)
                {
                    clearSiparisTable();
                    int i = 0;
                    foreach (string tablenames in tableNumbers)
                    {
                        if (tableNumbers[i] == tableName)
                        {

                            ////////////////////////////////////////////////////////////////////////////////
                            //dgKasa ->> Zaman | Yapilan Islem | Masa Adi | Personel | Cari | Nakit | Kredi Karti | Tutar
                            // !!!!! ALTTAKI SATIR MASA KAPAMA TAMAMLANINCA ISLEME ACILACAK !!!!! 
                            //dgvKasa.Rows.Add(LastChoosenTable.lastClosedTableTime, "Masa Kapama", LastChoosenTable.lastClosedTable, LastChoosenTable.lastClosedTableWaiter, "0", "0", lToplamTutar.Text.ToString(), lToplamTutar.Text.ToString());
                            dgvKasa.Rows.Add(DateTime.UtcNow.ToLocalTime().ToString(), "Rezervasyon Iptal", tableName.ToString(), lPersonel.Text.Replace("Personel :", ""), "-", "-", "-", "-");
                            dgvKasa.Refresh();
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
                            SaveDataToXml("Rezervasyon Kapama", "Rezervasyon_Aciklama", 0, 0, 0, lPersonel.Text.Replace("Personel :", ""), tableName.ToString(), "₺");
                            FirstLoadDataFromXml();
                            UserLog.WConsole("Masa : " + tableName + " kapatildi...");
                            UserLog.WConsole("Acik masa sayisi ; " + tableCounter.ToString());
                            lToplamTutar.Text = "0 ₺";
                            // tableDetails dizisinden de silinmeli
                        }
                        i++;
                    }
                    bActiveEt.Visible = false;
                    bActiveEt.Enabled = false;
                }         
            }
            ////////////////////////
            ////////////////////////
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
                        tutar = tutar.Replace(" ₺", "");  // 5
                        tutar = (Convert.ToDecimal(tutar) + Convert.ToDecimal(showListForm.Selected_Meal_Price)).ToString();  // 10
                        dgViewSiparis.Rows[existedRow].Cells[5].Value = tutar + " ₺";
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
                        dgViewSiparis.Rows.Add(mealID, meal, "1", null, null, mealPrice.ToString() + " ₺", mealPrice.ToString() + " ₺");
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
                tutar = tutar.Replace(" ₺", "");
                tutar = ((Convert.ToDecimal(tutar) / oldCount) * newCount).ToString();  // 10
                dgViewSiparis.Rows[e.RowIndex].Cells[5].Value = tutar + " ₺";
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
                    tutar = tutar.Replace(" ₺", "");
                    tutar = (((Convert.ToDecimal(tutar)) / oldCount) * newCount).ToString();
                    dgViewSiparis.Rows[e.RowIndex].Cells[5].Value = tutar + " ₺";
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
                tutar = tutar.Replace(" ₺", "");
                toplamTutar = toplamTutar + Convert.ToDecimal(tutar);

            }
            lToplamTutar.Text = toplamTutar.ToString() + " ₺";
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
                dgViewSiparis.Rows.Add(mealID, meal, countOfMeal, null, null, (mealPrice * countOfMeal).ToString() + " ₺", findMealPrice(mealID).ToString() + " ₺");
            }
            lToplamTutar.Text = tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 2] + " ₺";
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

        private void ReportSearchKeyChanged(object sender, EventArgs e)
        {
            switch (cbRaporTercihi.SelectedIndex)
            {
                case 0://Bir Gunluk Rapor
                    dtpReportDateStart.Format = DateTimePickerFormat.Custom;
                    dtpReportDateEnd.Format = DateTimePickerFormat.Custom;
                    dtpReportDateStart.CustomFormat = "dd'/'MMMM'/'yyyy";
                    dtpReportDateEnd.CustomFormat = "dd'/'MMMM'/'yyyy";
                    dtpReportDateEnd.Enabled = false;
                    break;
                case 1://Belirli Tarih Araligi
                    dtpReportDateStart.Format = DateTimePickerFormat.Custom;
                    dtpReportDateEnd.Format = DateTimePickerFormat.Custom;
                    dtpReportDateStart.CustomFormat = "dd'/'MMMM'/'yyyy";
                    dtpReportDateEnd.CustomFormat =  "dd'/'MMMM'/'yyyy";
                    dtpReportDateEnd.Enabled = true;
                    break;
            }
            
        }

        private void GetReportDetails(DateTime startingDate,int days)
        {

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StreamWriter streamWriter = new StreamWriter(folderPath + @"\Restorium\records.txt");
            StreamReader streamReader = new StreamReader(folderPath + @"\Restorium\records.txt");
            
           // streamWriter.Encoding.

        }

        private void bReportAra_Click(object sender, EventArgs e)
        {
            if (cbRaporTercihi.SelectedIndex == 0)
            {//Bir Gunluk Rapor Tercihi Secildiyse
                if (dtpReportDateStart.Value.Date <= System.DateTime.Now)
                {
                    //Data Load
                }
                else
                {
                    MessageBox.Show("Sectiginiz tarih ileri bir tarihtir ! \nBugunun tarihi : "+ System.DateTime.Now.ToShortDateString(),"Secilen tarih hatali !");
                }
            }
            else
            {//Belirli Bir Tarih Araligi Icin Rapor Secildiyse
                if (dtpReportDateStart.Value.Date <= System.DateTime.Now && dtpReportDateEnd.Value.Date <= System.DateTime.Now && dtpReportDateStart.Value.Date <= dtpReportDateEnd.Value.Date)
                {
                    //Data Load
                }
                else if (dtpReportDateStart.Value.Date > dtpReportDateEnd.Value.Date)
                {
                    MessageBox.Show("Rapor baslangic tarihi , rapor bitis tarihinden ileri bir tarih olamaz !", "Secilen tarih hatali !");
                }
                else
                {
                    MessageBox.Show("Sectiginiz tarih araligi ileri bir tarihi kapsamaktadir ! \nBugunun tarihi : " + System.DateTime.Now.ToShortDateString(), "Secilen tarih hatali !");
                }

            }
        }

        private void SaveDataToXml(string islemTuru,string Aciklama,decimal Nakit,decimal KrediKarti,decimal Cari,string Personel,string masaAdi,string ParaBirimi)
        {
            //Template of Saving Scheme
            /* <Islem>
               <Saat></Saat>
               <Dakika></Dakika>
               <Gun></Gun>
               <Ay></Ay>
               <Yil></Yil>
               <Tur></Tur>
               <Aciklama></Aciklama>
               <Nakit></Nakit>
               <Personel></Personel>
               <KrediKarti></KrediKarti>
               <Cari></Cari>
               <ParaBirimi></ParaBirimi>
             </Islem>  
             */

            XmlDocument doc = new XmlDocument();
            doc.Load("ReportDatabase.xml");
            //create node and add value
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Islem", null);
            //node.InnerText = "this is new node";

            //create title node
            XmlNode subNodeTarih  = doc.CreateElement("Tarih");
            XmlNode subNodeZaman = doc.CreateElement("Zaman");
            XmlNode subNodeIslemTuru = doc.CreateElement("IslemTuru");
            XmlNode subNodeAciklama = doc.CreateElement("Aciklama");
            XmlNode subNodeNakit = doc.CreateElement("Nakit");
            XmlNode subNodeKrediKarti = doc.CreateElement("KrediKarti");
            XmlNode subNodeCari = doc.CreateElement("Cari");
            XmlNode subNodePersonel = doc.CreateElement("Personel");
            XmlNode subNodeMasaAdi = doc.CreateElement("MasaAdi");
            XmlNode subNodeParaBirimi = doc.CreateElement("ParaBirimi");
            //add value for child nodes
            subNodeTarih.InnerText = System.DateTime.Now.ToLongDateString();
            subNodeZaman.InnerText = System.DateTime.Now.ToShortTimeString();
            //External Data (Outsource contents)
            subNodeIslemTuru.InnerText = islemTuru;
            subNodeAciklama.InnerText = Aciklama;
            subNodeNakit.InnerText = Nakit.ToString();
            subNodeKrediKarti.InnerText = KrediKarti.ToString();
            subNodeCari.InnerText = Cari.ToString();
            subNodePersonel.InnerText = Personel;
            subNodeMasaAdi.InnerText = masaAdi;
            subNodeParaBirimi.InnerText = ParaBirimi;
            //add to parent node
            node.AppendChild(subNodeTarih);//1
            node.AppendChild(subNodeZaman);//2
            node.AppendChild(subNodeIslemTuru);//3
            node.AppendChild(subNodeAciklama);//4
            node.AppendChild(subNodeNakit);//5
            node.AppendChild(subNodeKrediKarti);//6
            node.AppendChild(subNodeCari);//7
            node.AppendChild(subNodePersonel);//8
            node.AppendChild(subNodeMasaAdi);//9
            node.AppendChild(subNodeParaBirimi);//10
            //add to elements collection
            doc.DocumentElement.AppendChild(node);
            //save back
            doc.Save("ReportDatabase.xml");
            UserLog.WConsole("Saving Data To XML is Succesfull !");

        }

        private void LoadDataFromXml(DateTime startingDate,DateTime endingDate)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("ReportDatabase.xml");
            XmlNodeList IslemList = doc.GetElementsByTagName("Islem");
            //////////////////////////////////////////////////////////

            ///////////////Gun Gecisleri Burada Ilk Verilen Tarihe Gun Ekleme Yontemiyle Yapiliyor
            //                int Day = startingDate.AddDays(i).Day;
            //                int Month = startingDate.AddDays(i).Month;
            int dayCount = 0;
                foreach (XmlNode node in IslemList)
                    {                                                               
                        XmlElement IslemElement = (XmlElement)node;
                        //UserLog.WConsole(IslemElement.GetElementsByTagName("Nakit")[0].InnerText);
                        //UserLog.WConsole(System.DateTime.Now.AddDays(1).ToLongDateString());
                        if (IslemElement.GetElementsByTagName("Tarih")[0].InnerText == startingDate.ToLongDateString())
                        {
                         MessageBox.Show("AYNI GUN");
                         //Kayit Bulundu
                        }
                        else if(Convert.ToDateTime(IslemElement.GetElementsByTagName("Tarih")[0].InnerText) >= startingDate)
                        {
                            if (Convert.ToDateTime(IslemElement.GetElementsByTagName("Tarih")[0].InnerText) <= endingDate)
                            {
                                MessageBox.Show("KAYIT ILERIKI BIR TARIH :" + Convert.ToDateTime(IslemElement.GetElementsByTagName("Tarih")[0].InnerText));
                                //Baslangic ve Bitis Tarihi Arasi
                            }
                            else
                            {
                                //Range Disi  
                            }
                        }
                        /* 
                        IE = IslemElement.GetElementsByTagName("IE")[0].InnerText;
                        FF = IslemElement.GetElementsByTagName("FF")[0].InnerText;
                        GC = IslemElement.GetElementsByTagName("CH")[0].InnerText;
                        OP = IslemElement.GetElementsByTagName("OP")[0].InnerText;
                        xmlCompanyID = IslemElement.Attributes["companyID"].InnerText;
                        */
                    }
            


        }
        private void FirstLoadDataFromXml()
        {
            chartDaily.Series["Saatlik Kazanc"].Points.Clear();
            chartWeekly.Series["Gunluk Kazanc"].Points.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load("ReportDatabase.xml");
            XmlNodeList IslemList = doc.GetElementsByTagName("Islem");
            //////////////////////////////////////////////////////////

            ///////////////Gun Gecisleri Burada Ilk Verilen Tarihe Gun Ekleme Yontemiyle Yapiliyor
            //                int Day = startingDate.AddDays(i).Day;
            //                int Month = startingDate.AddDays(i).Month;


            ////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////Wekly Starts
            ////////////////////////////////////////////////////////////////////////////////////////////////
            int dayCount = 0;
            bool firstReadFlag = false;
            int i = 0;
            foreach (XmlNode node in IslemList)
            {
                XmlElement IslemElement = (XmlElement)node;
                if (firstReadFlag == false)
                {
                    kasaToplamArray[i, 0] = IslemElement.GetElementsByTagName("Tarih")[0].InnerText; // i,0 => Tarih 
                    decimal toplamKasa = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText);
                    switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                    {
                        case "€":
                            toplamKasa = toplamKasa * LastChoosenTable.DefinedEuro;
                            break;
                        case "$":
                            toplamKasa = toplamKasa * LastChoosenTable.DefinedDolar;
                            break;
                        case "£":
                            toplamKasa = toplamKasa * LastChoosenTable.DefinedGBP;
                            break;
                    }
                    kasaToplamArray[i, 1] = toplamKasa.ToString();  // i,1 => Tutar
                    i++;
                    firstReadFlag = true;
                }
                else //Except for first read
                {
                    if (Convert.ToDateTime(kasaToplamArray[i - 1, 0]) == Convert.ToDateTime(IslemElement.GetElementsByTagName("Tarih")[0].InnerText)) //Ayni tarih devam ise
                    {
                        decimal toplamKasa = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText);
                        switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                        {
                            case "€":
                                toplamKasa = toplamKasa * LastChoosenTable.DefinedEuro;
                                break;
                            case "$":
                                toplamKasa = toplamKasa * LastChoosenTable.DefinedDolar;
                                break;
                            case "£":
                                toplamKasa = toplamKasa * LastChoosenTable.DefinedGBP;
                                break;
                        }
                        kasaToplamArray[i - 1, 1] = (Convert.ToDecimal(kasaToplamArray[i - 1, 1]) + toplamKasa).ToString();  // i,0 => Tutar
                    }
                    else
                    {
                        kasaToplamArray[i, 0] = IslemElement.GetElementsByTagName("Tarih")[0].InnerText; // i,0 => Tarih 
                        decimal toplamKasa = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText);
                        switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                        {
                            case "€":
                                toplamKasa = toplamKasa * LastChoosenTable.DefinedEuro;
                                break;
                            case "$":
                                toplamKasa = toplamKasa * LastChoosenTable.DefinedDolar;
                                break;
                            case "£":
                                toplamKasa = toplamKasa * LastChoosenTable.DefinedGBP;
                                break;
                        }
                        kasaToplamArray[i, 1] = toplamKasa.ToString();  // i,1 => Tutar
                        i++;

                    }
                }
            }

            /*
            for (int j = 0; j < i; j++)
            {
                UserLog.WConsole(kasaToplamArray[j, 0] + " :::: " + kasaToplamArray[j, 1]);
            }
            */
            UserLog.WConsole("Number of Recorded Days : " + i);
            databaseDayCounter = i;
            //Weekly Chart Ekrana Yazdir
            bool dayFoundFlag = false;
            for (int pointCounter = 7; pointCounter > 0; pointCounter--)
            {
                for(int n=0;n<databaseDayCounter;n++)
                {
                    if (System.DateTime.Now.AddDays(-pointCounter).ToLongDateString() == kasaToplamArray[n, 0])
                    {
                        chartWeekly.Series["Gunluk Kazanc"].Points.AddXY(kasaToplamArray[n, 0], kasaToplamArray[n, 1]);
                        dayFoundFlag = true;
                    }
                }
                if (dayFoundFlag == false)
                {
                    chartWeekly.Series["Gunluk Kazanc"].Points.AddXY(System.DateTime.Now.AddDays(-pointCounter).ToLongDateString(), 0);
                }
                else
                {
                    dayFoundFlag = false;
                }
            }

            UserLog.WConsole("Haftalik Rapor Basariyla Yazdirildi...!");
            ////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////Weekly Ends
            ////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////Hourly Starts
            ////////////////////////////////////////////////////////////////////////////////////////////////
            int hourlyCounter = 0;
            bool dailyFlag = false;
            //UserLog.WConsole("Start Calculate Hourly Income...");
            foreach (XmlNode node in IslemList)
            {
                try
                {
                    XmlElement IslemElement = (XmlElement)node;
                    if (Convert.ToDateTime(System.DateTime.Now.ToLongDateString()) == Convert.ToDateTime(IslemElement.GetElementsByTagName("Tarih")[0].InnerText))
                    {
                      //  UserLog.WConsole(IslemElement.GetElementsByTagName("Nakit")[0].InnerText);
                      //  UserLog.WConsole(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText);

                        string sum = (Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText)).ToString();
                      //  UserLog.WConsole(sum);
                        string hour = (Convert.ToDateTime(IslemElement.GetElementsByTagName("Zaman")[0].InnerText).Hour).ToString();
                        if (dailyFlag == false)//first hour 
                        {
                            hourlySum[hourlyCounter, 1] = sum;
                          //  UserLog.WConsole("hourly sum :" + hourlySum[hourlyCounter, 1]);
                            hourlySum[hourlyCounter, 0] = hour;
                            hourlyCounter++;
                            dailyFlag = true;
                        }
                        else//other hours
                        {
                            if (hourlySum[hourlyCounter - 1, 0] == hour) //Bi onceki kaitla ayni saat ise
                            {
                                hourlySum[hourlyCounter - 1, 1] = (Convert.ToDecimal(hourlySum[hourlyCounter - 1, 1]) + Convert.ToDecimal(sum)).ToString();
                            //    UserLog.WConsole("hourly sum :" + hourlySum[hourlyCounter-1, 1]);
                            }
                            else // bi onceki kayit ile farkli saatler ise
                            {
                                hourlySum[hourlyCounter, 1] = sum;
                            //    UserLog.WConsole("hourly sum :" + hourlySum[hourlyCounter, 1]);
                                hourlySum[hourlyCounter, 0] = hour;
                                hourlyCounter++;
                            }
                        }
                    }
                }
                catch
                {
                    UserLog.WConsole("Bugune ait kayit bulunamadi");
                }
            }
            UserLog.WConsole("Number of Hour Records for Today : "+ hourlyCounter.ToString());
            /*
            for (int m = 0; m < hourlyCounter; m++)
            {
                UserLog.WConsole("saat : " + hourlySum[m, 0] + " | toplam : " + hourlySum[m, 1]);
            }
            */
            ////////////////////////////////////////////////////////////////////////////////////////////////
            //Hourly Sum Chart Print
            bool hourlyFound = false;
            for (int m = 0; m < 24; m++)
            {
                for (int n = 0; n < hourlyCounter; n++)
                {
                    if (hourlySum[n, 0] == m.ToString())
                    {
                        //UserLog.WConsole("point :"+ hourlySum[n, 0]);
                        chartDaily.Series["Saatlik Kazanc"].Points.AddXY(hourlySum[n, 0], hourlySum[n, 1].Replace(",","."));
                        hourlyFound = true;
                    }
                }
                if (hourlyFound == false)
                {
                    //UserLog.WConsole("zero : "+m);
                    chartDaily.Series["Saatlik Kazanc"].Points.AddXY(m, 0);
                }
                hourlyFound = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////Hourly Finished
            ////////////////////////////////////////////////////////////////////////////////////////////////
            chartDaily.ChartAreas[0].AxisX.Interval = 1;

        }

        private void bSendMailReport_Click(object sender, EventArgs e)
        {
             bool result= sendMail();
            if (!result)
            {
                MessageBox.Show("Mail gonderilirken sorun olustu !\nMail gonderimi basarisiz !");
            }
            else
            {
                MessageBox.Show("Mail Basariyla :\n" + tbMail.Text + "\nMail adresine gonderildi.");
            }
        }

        private void chartDaily_MouseMove(object sender, MouseEventArgs e)
        {
            Point? prevPosition = null;
            ToolTip tooltip = new ToolTip();

            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chartDaily.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around the point)
                        if (Math.Abs(pos.X - pointXPixel) < 2 &&
                            Math.Abs(pos.Y - pointYPixel) < 2)
                        {
                            tooltip.Show("X=" + prop.XValue + ", Y=" + prop.YValues[0], this.chartDaily,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        private void bAyarlarDuzenle_Click(object sender, EventArgs e)
        {
            ayarlarDuzenlemeModu = !ayarlarDuzenlemeModu;
            if (ayarlarDuzenlemeModu == true)
            {
                bAyarlarDuzenle.ForeColor = Color.Green;
                tbDefaultIskontoValue.Enabled = true;
                dtpDukkanKapanisTime.Enabled = true;
                cbAutoMail.Enabled = true;
                tbMail.Enabled = true;
                tbDolar.Enabled = true;
                tbEuro.Enabled = true;
                tbGBP.Enabled = true;
            }
            else
            {
                bAyarlarDuzenle.ForeColor = Color.Red;
                tbDefaultIskontoValue.Enabled = false;
                dtpDukkanKapanisTime.Enabled = false;
                cbAutoMail.Enabled = false;
                tbMail.Enabled = false;
                tbDolar.Enabled = false;
                tbEuro.Enabled = false;
                tbGBP.Enabled = false;
            }
        }

        private void tbMailChanged(object sender, EventArgs e)
        {
            INI.Write("Email", tbMail.Text, "Settings");
            UserLog.WConsole("Email adresi : " + tbMail.Text + " olarak kaydedildi");
        }

        private void dtpDukkanKapanisTimeChanged(object sender, EventArgs e)
        {
            INI.Write("DukkanKapanisSaati", dtpDukkanKapanisTime.Value.ToString(), "Settings");
            UserLog.WConsole("Dukkan Kapanis Saati : " + dtpDukkanKapanisTime.Value.ToString()+" Olarak kaydedildi");
        }

        private void cbAutoMailChanged(object sender, EventArgs e)
        {
            INI.Write("AutoMail", cbAutoMail.CheckState.ToString(), "Settings");
            UserLog.WConsole("AutoMail secenegi : " + cbAutoMail.CheckState.ToString() + " olarak degistirildi");
        }
        private bool sendMail()
        {
            if (tbMail.Text != "" && tbMail.Text.Contains("@"))
            {

                this.chartDaily.SaveImage("Daily_Chart.png", ChartImageFormat.Png);
                this.chartWeekly.SaveImage("Weekly_Chart.png", ChartImageFormat.Png);

                // dgvKasa to BitMap   :::::::::::::::::::::::::::::::::::::
                int height = dgvKasa.Height;
                dgvKasa.Height = (dgvKasa.RowCount + 3) * dgvKasa.RowTemplate.Height;
                //Create a Bitmap and draw the DataGridView on it.
                Bitmap bitmap = new Bitmap(this.dgvKasa.Width, this.dgvKasa.Height);
                dgvKasa.DrawToBitmap(bitmap, new Rectangle(0, 0, this.dgvKasa.Width, this.dgvKasa.Height));
                //Resize DataGridView back to original height.
                dgvKasa.Height = height;
                //Save the Bitmap to folder.
                bitmap.Save("Kasa.png");
                // dgvKasa to BitMap End :::::::::::::::::::::::::::::::::::::

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("burak.c.kocak@gmail.com");
                //msg.To.Add("hozmen6024@gmail.com");
                //msg.To.Add("bilalertkn@gmail.com");
                //msg.To.Add("Mahone0619@gmail.com");
                //msg.To.Add("burak.c.kocak@gmail.com");
                msg.To.Add(tbMail.Text);
                msg.Subject = "Restorium Daily Report " + DateTime.Now.ToString();
                msg.Body = "Daily Report :" + System.DateTime.Now.ToLongDateString() + "   " + System.DateTime.Now.ToLocalTime().ToLongTimeString();
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.live.com";
                msg.Attachments.Add(new Attachment("Daily_Chart.png"));
                msg.Attachments.Add(new Attachment("Weekly_Chart.png"));
                msg.Attachments.Add(new Attachment("Kasa.png"));
                client.Port = 25;
                //client.Host = "smtp.gmail.com";
                //client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("bbhmbbhm@outlook.com", "bilalburakhuseyinmahmut1");
                client.Timeout = 20000;
                try
                {
                    client.Send(msg);
                    UserLog.WConsole("Mail has been successfully sent! \nTo :" + tbMail.Text);
                    msg.Dispose();
                    mailSentFlag = true;
                    return true;
                }
                catch (Exception ex)
                {
                    UserLog.WConsole("Fail Has error");
                    msg.Dispose();
                    mailSentFlag = false;
                    return false;
                }
            }
            return false;

        }
        //AYARLAR 
        //Dukkan kapanma saati ve gonderilecek maili ekle
        //Iskonto uygulanmiyor
        //Masaya siparis eklemek istendiginde acilan sayfadaki "iptal" tusu calismiyor
        //Program acildiginda gun bitimi gerceklesmediyse kaydedilen satislari kasaya yazdir
        /////
        //Android unity : find the person in a crowded place (idea)
        //Android unity : daga tirmanma yarisi . en hizli olan kazanir
        //How to videos 


    }
}
