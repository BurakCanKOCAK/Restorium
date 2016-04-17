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
using System.Globalization;
using System.Threading;

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
        CultureInfo culture = new CultureInfo("tr-TR", true);
        public static string[] personelNames = new string[100];
        public static string[] menuAciklama = new string[500];
        public static string[] menuID = new string[500];
        public static decimal[] menuPrice = new decimal[500];
        public static decimal OldKasaToplam,oldKasaTipToplam,oldKasaCariToplam;
        public static string[] tableNumbers = new string[200];
        int[] matchedStokIndexList = new int[5000];
        public static int tableCounter = 0;
        public static int dailyTableCounter = 0;
        public static int databaseDayCounter = 0;
        public static int NoOfRecordToday = 0;
        public static bool[] emptyTableList = new bool[200];
        public static string[,] tableDetails = new string[300, 20];
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
        public static int maxCountOfTables;
        public static int countSearchIndexList = 0;
        private bool duzenlemeModu = false;
        private bool personelDuzenlemeModu = false;
        private bool ayarlarDuzenlemeModu = false;
        private bool mailSentFlag = false;
        private bool rehberDuzenleFlag = false;
        private bool searchModeOnStok = false;
        private bool kasaReset = false;
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
            if (DateTime.UtcNow.ToLocalTime().Month >= 3 )
            {
                if (DateTime.UtcNow.ToLocalTime().Day > 20)
                {
                    Environment.Exit(0);
                }
            }
            InitializeComponent();
            User = INI.Read("LoggedUser", "Login");
            SetExchangeValues();
            StokDataSet();
            //GetStokFromDB();
            RehberDataSet();
            SettingsDataSet();
            SiparisScreenInitialSet();
            cbRaporTercihi.SelectedIndex = 1;
            // SaveDataToXml("Masa Kapama","",10,100,50,"Burak Can KOCAK", "A1","₺");
            // LoadDataFromXml(System.DateTime.Now, System.DateTime.Now);
            FirstLoadDataFromXml();
            GetKasaFromXML();
            MainDisplay md = new MainDisplay();
            md.Show();
            //KasaLoad();
        }

/// ////////////////////////////////////////
        private void GetStokFromDB()
        {
            SqlConnection con;
            SqlDataAdapter da;
            SqlCommand cmd;
            DataSet ds;
            con = new SqlConnection("server=.; Initial Catalog=Database/Database_Stok;Integrated Security=SSPI");
            da = new SqlDataAdapter("Select *From TableStok", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "TableStok");
            dgView.DataSource = ds.Tables["TableStok"];
            con.Close();
        }
/// ////////////////////////////////////////

        private void RehberDataSet()
        {
            dgvRehber.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRehber.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRehber.ReadOnly = true;
            try
            {
                string column0, column1, column2, column3, column4, column5,column6,column7,column8;
                int rowCount =Convert.ToInt16(INI.Read("NoOfRecords", "ContactList"));
                for (int i = 0; i < rowCount; i++)
                {
                    //Each Row
                    column0 = INI.Read("Sat" + i + "Sut0", "ContactList");
                    column1 = INI.Read("Sat" + i + "Sut1", "ContactList");
                    column2 = INI.Read("Sat" + i + "Sut2", "ContactList");
                    column3 = INI.Read("Sat" + i + "Sut3", "ContactList");
                    column4 = INI.Read("Sat" + i + "Sut4", "ContactList");
                    column5 = INI.Read("Sat" + i + "Sut5", "ContactList");
                    column6 = INI.Read("Sat" + i + "Sut6", "ContactList");
                    column7 = INI.Read("Sat" + i + "Sut7", "ContactList");
                    column8 = INI.Read("Sat" + i + "Sut8", "ContactList");
                    dgvRehber.Rows.Add(column0, column1, column2, column3, column4, column5,column6,column7,column8);
                    dgvRehber.Refresh();
                }
            }
            catch
            {
                UserLog.WConsole("(!)Rehber Okunurken Hata Olustu !");
            }
            
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
            decimal Dolar, Euro, GBP;
            try
            {
                Euro = Convert.ToDecimal(INI.Read("Euro", "Exchange"), culture);
                LastChoosenTable.DefinedEuro = Euro;
                tbEuro.Text = Euro.ToString();
                Settings.Euro = true;
            }
            catch
            {
                UserLog.WConsole("(!) Euro Degeri Okumada Hata ! (Deger girilmemis!)");
            }
            try
            {
                Dolar = Convert.ToDecimal(INI.Read("Dolar", "Exchange"), culture);
                LastChoosenTable.DefinedDolar = Dolar;
                tbDolar.Text = Dolar.ToString();
                Settings.Dolar = true;
            }
            catch
            {
                UserLog.WConsole("(!) Dolar Degeri Okumada Hata  (Deger girilmemis!)");
            }
            try
            {
                GBP = Convert.ToDecimal(INI.Read("GBP", "Exchange"), culture);
                LastChoosenTable.DefinedGBP = GBP;
                tbGBP.Text = GBP.ToString();
                Settings.GBP = true;
            }
            catch
            {
                UserLog.WConsole("(!) GBP Degeri Okumada Hata  (Deger girilmemis!)");
            }

                lExchange.Text = "1 ₺ = " + tbDolar.Text.ToString() + " $ = " + tbEuro.Text.ToString() + " € = " + tbGBP.Text.ToString() + " £";
                UserLog.WConsole("Doviz kurlari okuma tamamlandi !");


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
                if (eMail.Contains("@") == true && eMail.Contains(".") == true)
                {
                    Settings.useMail = true;
                }
                else
                {
                    Settings.useMail = false;
                }
            }
            catch
            {
                UserLog.WConsole("(!)Kayitli bir E-Mail adresi bulunamadi !");
                Settings.useMail = false;
            }
            try
            {
                dtpDukkanKapanisTime.Value = Convert.ToDateTime(INI.Read("DukkanKapanisSaati", "Settings"));
            }
            catch
            {
                UserLog.WConsole("(!)Dukkan kapanis saati kaydedilmemis !");
                dtpDukkanKapanisTime.Value = DateTime.Now.ToLocalTime().AddHours(-1);
            }
            try
            {
                iskontoRate = Convert.ToInt16(INI.Read("Iskonto", "Settings"));
                tbDefaultIskontoValue.Text = iskontoRate.ToString();
            }
            catch
            {
                UserLog.WConsole("(!)Atanmis bir iskonto degeri bulunamadi !");
                tbDefaultIskontoValue.Text = "0";
            }
            if (User == "Admin")
            {
                dgViewWaiter.ForeColor = Color.Black;
                bPersonelDuzenle.ForeColor = Color.Black;
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
                bPersonelEkle.Enabled = false;
                bDeletePersonel.Enabled = false;
                bStokAdd.Enabled = false;
                bStokSil.Enabled = false;
                bKayitEkle.Enabled = false;
                bKayitSil.Enabled = false;
                bDuzenleRehber.Enabled = false;
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
                    if (tableCounter >= maxCountOfTables)
                    {
                        maxCountOfTables = tableCounter;
                    }
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
                    bLeft.MouseDown += new MouseEventHandler(masa_click);
                    bLeft.MouseDoubleClick += new MouseEventHandler(masa_MouseDoubleClick);
                    lMasaNo.Text = MasaNo.ToString();
                    string text = lMasaNo.Text;
                    switch (text.Length)
                    {
                        case 1:
                        case 2:
                        case 3:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 42f, lMasaNo.Font.Style);
                            break;
                        case 4:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 37f, lMasaNo.Font.Style);
                            break;
                        case 5:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 35f, lMasaNo.Font.Style);
                            break;
                        case 6:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 30f, lMasaNo.Font.Style);
                            break;
                        case 7:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 25f, lMasaNo.Font.Style);
                            break;
                        case 8:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 22f, lMasaNo.Font.Style);
                            break;
                        case 9:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 20f, lMasaNo.Font.Style);
                            break;
                        case 10:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 18f, lMasaNo.Font.Style);
                            break;
                        case 11:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 16f, lMasaNo.Font.Style);
                            break;
                        case 12:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 15f, lMasaNo.Font.Style);
                            break;
                        default:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 8f, lMasaNo.Font.Style);
                            break;
                    }
                    lMasaNo.ForeColor = Color.Red;
                    /////////////
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
                        UserLog.WConsole("LastTablePlace : " + TableOpenForm.lastTablePlace.ToString());
                        UserLog.WConsole(countOfTables.ToString());
                        tableLayoutPanel1.Controls.Add(bLeft, (TableOpenForm.lastTablePlace % 5), (TableOpenForm.lastTablePlace) / 5);
                        // tableLayoutPanel1.Controls.Add(bRight, 3, countOfTables / 2 - 1);
                        tableFlag = false;
                    }
                    LastChoosenTable.TableNumber = MasaNo.ToString();
                    bSiparisEkle.Enabled = true;
                    bPrint.Enabled = true;
                    bTableDetailChange.Enabled = true;
                    bTableClose.Enabled = true;
                    //                 
                    lToplamTutar.Text = "0 ₺";
                }
            }
            if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
            {
                bActiveEt.Visible = true;
                bActiveEt.Enabled = true;
                bSiparisEkle.Enabled = false;
                bPrint.Enabled = true;
                bTableDetailChange.Enabled = true;
                bTableClose.Text = "R. Iptal";
            }
            else
            {
                bActiveEt.Visible = false;
                bPrint.Enabled = true;
                bActiveEt.Enabled = false;
                bTableClose.Text = "Masa Kapat";
            }
            

        }

        private void masa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            bSiparisEkle_Click(sender, e);
        }

        private void masa_doubleClick(object sender, EventArgs e)
        {
            UserLog.WConsole("ASDASDFSADSADAS");
            bSiparisEkle_Click(sender, e);
        }

        private void bMasaTasi_Click(object sender, EventArgs e)
        {
            // Masa TASI
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database_StokDataSet1.TableStok' table. You can move, or remove it, as needed.
            //  this.tableStokTableAdapter.Fill(this.database_StokDataSet1.TableStok);

            lDate.Text = DateTime.UtcNow.ToLocalTime().ToString();
            this.dgvKasa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Data_Update(object sender, EventArgs e)
        {
            //Console.WriteLine("Debug : " + DebugMonitor.text);
            lDate.Text = DateTime.UtcNow.ToLocalTime().ToString();
            if (cbAutoMail.Checked == true)
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
            ////////////////////////////KASA RESET   
           /* DateTime DT = new DateTime(2015, 10, 12, 23, 59, 00);
            if (System.DateTime.Now.AddHours(-1) >= dtpDukkanKapanisTime.Value)
            {
                if (System.DateTime.Now.AddHours(-1).Hour <= DT.Hour)
                {  
                    if (kasaReset == false)
                    {
                        for (int i = dgvKasa.Rows.Count - 1; i >= 0; i--)
                        {
                            try
                            {
                                dgvKasa.Rows.RemoveAt(i);
                                dgvKasa.Refresh();
                                kasaReset = true;
                            }
                            catch
                            {
                                UserLog.WConsole("(!)Gun Sonu Kasasi Silinirken Hata Olustu !");
                            }
                        }
                        lBugunToplam.Text = "0.0 ₺";
                        lNakitToplamTL.Text = "0.0 ₺";
                        lKrediToplamTL.Text = "0.0 ₺";
                        lCariToplamTL.Text = "0.0 ₺";
                    }
                }
            }
            else
            {
                kasaReset = false;
            }*/
            ////////////////////////////KASA RESET END
            //WiFi Check
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
                bStokSil.Enabled = false;
                //dgView.AllowUserToAddRows = true;
                dgView.AllowUserToDeleteRows = true;
                dgView.AllowUserToOrderColumns = true;
                dgView.AllowDrop = true;
                dgView.ReadOnly = false;
                dgView.Refresh();
                //bDuzenle.BackColor = Color.Green;
                bDuzenle.Image = Properties.Resources.switch_on;
                bDuzenle.Text = "Duzenle";
                bDuzenle.ForeColor = Color.Green;
                bStokSil.Visible = false;
                dgView.SelectionMode = DataGridViewSelectionMode.CellSelect;

            }
            else
            {
                bStokSil.Enabled = true;
                //dgView.AllowUserToAddRows = false;
                dgView.AllowUserToDeleteRows = false;
                dgView.AllowUserToOrderColumns = false;
                dgView.AllowDrop = false;
                dgView.ReadOnly = true;
                dgView.Refresh();
                //bDuzenle.BackColor = Color.Silver;
                bDuzenle.Image = Properties.Resources.switch_off;
                bDuzenle.Text = "Duzenle";
                bDuzenle.ForeColor = Color.Red;
                bStokSil.Visible = true;
                dgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                saveStokToFile();

            }
        }
        #endregion
       
        #region STOK DATA SAVE / LOAD / SEARCH FROM FILE
        private void saveStokToFile()
        {
            if(searchModeOnStok== false)
            {
            stokCount = dgView.Rows.Count;
            INI.DeleteSection("Stok");
            INI.Write("stokCount", stokCount.ToString(), "Stok");
            }
            /////
            
                if (searchModeOnStok==true)
                {
                    for (int j = 0; j < countSearchIndexList; j++)
                    {
                            if (dgView.Rows[j].Cells[4].Value.ToString().LastIndexOf(',') != -1)
                            culture.NumberFormat.NumberDecimalSeparator = ",";
                            ////
                            INI.Write("id" + matchedStokIndexList[j], dgView.Rows[j].Cells[0].Value.ToString(), "Stok");
                            INI.Write("aciklama" + matchedStokIndexList[j], dgView.Rows[j].Cells[1].Value.ToString(), "Stok");
                            INI.Write("adet" + matchedStokIndexList[j], dgView.Rows[j].Cells[2].Value.ToString(), "Stok");
                            INI.Write("birim" + matchedStokIndexList[j], dgView.Rows[j].Cells[3].Value.ToString(), "Stok");
                            INI.Write("birimFiyat" + matchedStokIndexList[j],(Convert.ToDecimal(dgView.Rows[j].Cells[4].Value,culture)).ToString(), "Stok");
                            INI.Write("paraBirimi" + matchedStokIndexList[j], dgView.Rows[j].Cells[5].Value.ToString(), "Stok");
                            bool checkBoxStatusMenuUrunu = Convert.ToBoolean(dgView.Rows[j].Cells[7].Value);

                           dgView.Rows[j].Cells[4].Value = (Convert.ToDecimal(dgView.Rows[j].Cells[4].Value, culture)).ToString();
                            
                            if (checkBoxStatusMenuUrunu == false)
                            {
                                INI.Write("menuUrunu" + matchedStokIndexList[j], "false", "Stok");
                                INI.Write("dinamikStokKontrolu" + matchedStokIndexList[j], "false", "Stok");
                                dgView.Rows[j].Cells[7].Value = CheckState.Unchecked;
                                dgView.Refresh();
                            }
                            else
                            {
                                INI.Write("menuUrunu" + matchedStokIndexList[j], "true", "Stok");
                                bool checkBoxStatus = Convert.ToBoolean(dgView.Rows[j].Cells[6].Value);
                                if (checkBoxStatus == false)
                                {
                                    INI.Write("dinamikStokKontrolu" + matchedStokIndexList[j], "false", "Stok");
                                }
                                else
                                {
                                    INI.Write("dinamikStokKontrolu" + matchedStokIndexList[j], "true", "Stok");
                                }
                            }
                            ////
                        if(Convert.ToBoolean(dgView.Rows[j].Cells[6].Value) == true)
                        {
                            if (Convert.ToInt32(dgView.Rows[j].Cells[2].Value) < 1)
                            {
                                dgView.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {
                                dgView.Rows[j].DefaultCellStyle.BackColor = Color.White;
                            }
                        }else
                        {
                        dgView.Rows[j].DefaultCellStyle.BackColor = Color.White;
                        
                        }
                    
                }     
                }
                else
                {
                    for (int i = 0; i < stokCount; i++)
                    {
                        if (dgView.Rows[i].Cells[4].Value.ToString().LastIndexOf(',') != -1)
                        culture.NumberFormat.NumberDecimalSeparator = ",";

                        INI.Write("id" + i.ToString(), dgView.Rows[i].Cells[0].Value.ToString(), "Stok");
                        INI.Write("aciklama" + i.ToString(), dgView.Rows[i].Cells[1].Value.ToString(), "Stok");
                        INI.Write("adet" + i.ToString(), dgView.Rows[i].Cells[2].Value.ToString(), "Stok");
                        INI.Write("birim" + i.ToString(), dgView.Rows[i].Cells[3].Value.ToString(), "Stok");
                        INI.Write("birimFiyat" + i.ToString(),(Convert.ToDecimal(dgView.Rows[i].Cells[4].Value,culture)).ToString(), "Stok");
                        INI.Write("paraBirimi" + i.ToString(), dgView.Rows[i].Cells[5].Value.ToString(), "Stok");
                    // UserLog.WConsole(dgView.Rows[i].Cells[6].Value.ToString());
                    //dinamikStokKontrolu
                        dgView.Rows[i].Cells[4].Value=(Convert.ToDecimal(dgView.Rows[i].Cells[4].Value, culture)).ToString();

                        bool checkBoxStatusMenuUrunu = Convert.ToBoolean(dgView.Rows[i].Cells[7].Value);
                        if (checkBoxStatusMenuUrunu == false)
                        {
                            INI.Write("menuUrunu" + i.ToString(), "false", "Stok");
                            INI.Write("dinamikStokKontrolu" + i.ToString(), "false", "Stok");
                            dgView.Rows[i].Cells[7].Value = CheckState.Unchecked;
                            dgView.Rows[i].Cells[6].Value = CheckState.Unchecked;
                            dgView.Refresh();
                        }
                        else
                        {
                            INI.Write("menuUrunu" + i.ToString(), "true", "Stok");
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
                    if (Convert.ToBoolean(dgView.Rows[i].Cells[6].Value) == true)
                    {
                        if (dgView.Rows[i].Cells[2].Value.ToString()=="0" || dgView.Rows[i].Cells[2].Value.ToString().Contains("-"))
                        {
                            dgView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {
                            dgView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        dgView.Rows[i].DefaultCellStyle.BackColor = Color.White;

                    }

                }
                //menuUrunu
                 }
            UserLog.WConsole("Stok Listesi Kaydetme Basarili");


        }

        private void getStokFromFile()
        {//Dosyadan Stok Datasini alik gdView'a set eder
            int countOfRowsStok = dgView.RowCount;
            stokCount = countOfRowsStok;
            for (int i = countOfRowsStok-1; i >=0 ; i--)
            {
                try
                { 
                    dgView.Rows.RemoveAt(i);
                }
                catch
                {
                    UserLog.WConsole("KAYIT BULAMADI !!!");
                }
            }
            dgView.Refresh();
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
                    string menuUrunu = INI.Read("menuUrunu" + i.ToString(), "Stok");
                    menuAciklama[i] = aciklama;
                    menuID[i] = id;
                    menuPrice[i] = Convert.ToDecimal(birimFiyat, culture);

                    if (dinamikStokKontrolu == "true")
                    {
                        if (menuUrunu == "true")
                        {
                            dgView.Rows.Add(id, aciklama, adet, birim, (Convert.ToDecimal(birimFiyat,culture)).ToString(), paraBirimi, CheckState.Checked, CheckState.Checked);
                        }
                        else
                        {
                            dgView.Rows.Add(id, aciklama, adet, birim, (Convert.ToDecimal(birimFiyat, culture)).ToString(), paraBirimi, CheckState.Checked, CheckState.Unchecked);
                        }
                    }
                    else
                    {
                        if (menuUrunu == "true")
                        {
                            dgView.Rows.Add(id, aciklama, adet, birim, (Convert.ToDecimal(birimFiyat, culture)).ToString(), paraBirimi, CheckState.Unchecked, CheckState.Checked);
                        }
                        else
                        {
                            dgView.Rows.Add(id, aciklama, adet, birim, (Convert.ToDecimal(birimFiyat, culture)).ToString(), paraBirimi, CheckState.Unchecked, CheckState.Unchecked);
                        }
                    }
                    /////////CHECK AMOUNT OF STOK
                    if (Convert.ToBoolean(dgView.Rows[i].Cells[6].Value) == true)
                    {
                        if (Convert.ToInt32(dgView.Rows[i].Cells[2].Value) < 1)
                        {
                            dgView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {
                            dgView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
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
            countSearchIndexList = 0;
            if (searchModeOnStok == true)
            {
                getStokFromFile();
            }
            string searchKey = tbSearchKey.Text.ToString();
           
            for(int i = 0; i < dgView.RowCount; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (dgView.Rows[i].Cells[j].Value.ToString().ToUpper().Contains(searchKey.ToUpper()))
                    {
                        matchedStokIndexList[countSearchIndexList] = i;
                        countSearchIndexList++;
                    }
                }


            }
            /////
            bool rowFoundFlag = false;
            for (int i = dgView.RowCount-1; i >=0 ; i--)
            {
                for (int j = 0; j < countSearchIndexList; j++)
                {
                    if (i == matchedStokIndexList[j])
                    {
                        rowFoundFlag = true;
                    }
                }
                ///
                if (rowFoundFlag == false)
                {
                    dgView.Rows.RemoveAt(i);
                }
                rowFoundFlag = false;
            }
            searchModeOnStok = true;
            dgView.Refresh();
            ////

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
                    if (tableCounter >= maxCountOfTables)
                    {
                        maxCountOfTables = tableCounter;
                    }
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
                    bLeft.MouseDown += new MouseEventHandler(masa_click);
                    lMasaNo.Text = MasaNo.ToString();
                    string text = lMasaNo.Text;
                    switch (text.Length)
                    {
                        case 1:
                        case 2:
                        case 3:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 42f, lMasaNo.Font.Style);
                            break;
                        case 4:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 37f, lMasaNo.Font.Style);
                            break;
                        case 5:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 35f, lMasaNo.Font.Style);
                            break;
                        case 6:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 30f, lMasaNo.Font.Style);
                            break;
                        case 7:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 25f, lMasaNo.Font.Style);
                            break;
                        case 8:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 22f, lMasaNo.Font.Style);
                            break;
                        case 9:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 20f, lMasaNo.Font.Style);
                            break;
                        case 10:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 18f, lMasaNo.Font.Style);
                            break;
                        case 11:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 16f, lMasaNo.Font.Style);
                            break;
                        case 12:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 15f, lMasaNo.Font.Style);
                            break;
                        default:
                            lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 8f, lMasaNo.Font.Style);
                            break;

                    }
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
                        LastChoosenTable.reservation = true;
                    }
                    else
                    {
                        lMasaNo.ForeColor = Color.Red;
                        LastChoosenTable.reservation = false;
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
                    bPrint.Enabled = true;
                    bTableDetailChange.Enabled = true;
                    bTableClose.Enabled = true;
                    lToplamTutar.Text = "0 ₺";
                }
            }
            if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
            {
                bActiveEt.Visible = true;
                bActiveEt.Enabled = true;
                bPrint.Enabled = true;
                bSiparisEkle.Enabled = false;
                bTableDetailChange.Enabled = true;
                bTableClose.Text = "R. Iptal";
            }
            else
            {
                bActiveEt.Visible = false;
                bActiveEt.Enabled = false;
                bPrint.Enabled = true;
                bTableClose.Text = "Masa Kapat";
            }
            
        }

        private void masa_click(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            // Buton fonksiyonu
            UserLog.WConsole("Masanin ID si : " + b.Name);
            string masaNoLocal = b.Name;
            masaNoLocal = masaNoLocal.Replace("bLeft", "");
            lMasaNo.Text = masaNoLocal;
                            string text = lMasaNo.Text;
                            switch (text.Length)
                            {
                                case 1:
                                case 2:
                                case 3:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 42f, lMasaNo.Font.Style);
                                    break;
                                case 4:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 37f, lMasaNo.Font.Style);
                                    break;
                                case 5:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 35f, lMasaNo.Font.Style);
                                    break;
                                case 6:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 30f, lMasaNo.Font.Style);
                                    break;
                                case 7:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 25f, lMasaNo.Font.Style);
                                    break;
                                case 8:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 22f, lMasaNo.Font.Style);
                                    break;
                                case 9:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 20f, lMasaNo.Font.Style);
                                    break;
                                case 10:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 18f, lMasaNo.Font.Style);
                                    break;
                                case 11:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 16f, lMasaNo.Font.Style);
                                    break;
                                case 12:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 15f, lMasaNo.Font.Style);
                                    break;
                                default:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 8f, lMasaNo.Font.Style);
                                    break;

                            }
            //------------------------------------------------------------------
            //MaxTableIndex adinda bi degisken tut  i->MaxTableIndex e kadar tarasin. (EDIT)
            //------------------------------------------------------------------
            for (int i = 0; i <= maxCountOfTables; i++)
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
                        LastChoosenTable.reservation = true;
                    }
                    else
                    {
                        lMasaNo.ForeColor = Color.Red;
                        LastChoosenTable.reservation = false;
                    }
                    lPersonel.Text = "Personel : " + tableDetails[i * 3, 3];
                    LastChoosenTable.Waiter = tableDetails[i * 3, 3];
                    LastChoosenTable.iskonto = Convert.ToInt16(tableDetails[i * 3, 5]);
                    lIskonto.Text = "Iskonto Orani : " + tableDetails[i * 3, 5] + "%";
                    lMusteriAdi.Text = "Müşteri : " + tableDetails[i * 3, 6];
                    LastChoosenTable.musteriAdi = tableDetails[i * 3, 6];
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
                bTableDetailChange.Enabled = true;
                bTableClose.Text = "R. Iptal";
            }
            else
            {
                bActiveEt.Visible = false;
                bActiveEt.Enabled = false;
                bSiparisEkle.Enabled = true;
                bTableDetailChange.Enabled = true;
                bTableClose.Text = "Masa Kapat";
            }
            bTableClose.Enabled = true;
            clearSiparisTable();
            setSiparisListToTable();
            UserLog.WConsole("<<< masa_click Event>>>");
            if (e.Button == MouseButtons.Right)
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////Right Click
                UserLog.WConsole("Right Click");
                bSiparisEkle_Click(null, null);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////RightClick End
            }
            else if (e.Button == MouseButtons.Left)
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////Left Click 
                UserLog.WConsole("Left Click");
              
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////Left Click End
            }

        }
        #endregion

        #region PERSONEL PROCESSES
        private void bPersonelDuzenle_Click(object sender, EventArgs e)
        {

            /*
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
                dgViewWaiter.ForeColor = Color.Black;
                bPersonelDuzenle.ForeColor = Color.Green;
                bPersonelDuzenle.Image = Properties.Resources.switch_on;
                bDeletePersonel.Visible = false;
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
                dgViewWaiter.ForeColor = Color.Gray;
                bPersonelDuzenle.ForeColor = Color.Red;
                bPersonelDuzenle.Image = Properties.Resources.switch_off;
                bDeletePersonel.Visible = true;
                savePersonnelToFile();
            }
            */
            using (var personelAdd = new PersonelAdd())
            {
                PersonelAdd.PersonelColumn0 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[0].Value.ToString();
                PersonelAdd.PersonelColumn1 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[1].Value.ToString();
                PersonelAdd.PersonelColumn2 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[2].Value.ToString();
                PersonelAdd.PersonelColumn3 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[3].Value.ToString();
                PersonelAdd.PersonelColumn4 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[4].Value.ToString();
                PersonelAdd.PersonelColumn5 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[5].Value.ToString();
                PersonelAdd.PersonelColumn6 = dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[6].Value.ToString();
                PersonelAdd.personelEditMode = true;
                var result = personelAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[0].Value = PersonelAdd.PersonelColumn0;
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[1].Value = PersonelAdd.PersonelColumn1;
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[2].Value = PersonelAdd.PersonelColumn2;
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[3].Value = PersonelAdd.PersonelColumn3;
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[4].Value = PersonelAdd.PersonelColumn4;
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[5].Value = PersonelAdd.PersonelColumn5;
                    dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[6].Value = PersonelAdd.PersonelColumn6;
                }
            }
        }

        private void savePersonnelToFile()
        {//Personel Listesini duzenleme bitince dosyaya kaydeder.
            personelCount = dgViewWaiter.RowCount;
            try
            {
                INI.DeleteSection("Personel");
            }
            catch
            {
                UserLog.WConsole("(!) Personel Section'i Silinirken Hata Olustu !");
            }
                INI.Write("personelCount", personelCount.ToString(), "Personel");
            for (int i = 0; i < personelCount; i++)
            {
                INI.Write("id" + i.ToString(), dgViewWaiter.Rows[i].Cells[0].Value.ToString(), "Personel");
                INI.Write("name" + i.ToString(), dgViewWaiter.Rows[i].Cells[1].Value.ToString(), "Personel");
                INI.Write("address" + i.ToString(), dgViewWaiter.Rows[i].Cells[2].Value.ToString(), "Personel");
                INI.Write("tckn" + i.ToString(), dgViewWaiter.Rows[i].Cells[3].Value.ToString(), "Personel");
                INI.Write("tel" + i.ToString(), dgViewWaiter.Rows[i].Cells[4].Value.ToString(), "Personel");
                INI.Write("mail" + i.ToString(), dgViewWaiter.Rows[i].Cells[5].Value.ToString(), "Personel");
                INI.Write("work" + i.ToString(), dgViewWaiter.Rows[i].Cells[6].Value.ToString(), "Personel");
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
                    string address = INI.Read("address" + i.ToString(), "Personel");
                    string tckn = INI.Read("tckn" + i.ToString(), "Personel");
                    string tel = INI.Read("tel" + i.ToString(), "Personel");
                    string mail = INI.Read("mail" + i.ToString(), "Personel");
                    string work = INI.Read("work" + i.ToString(), "Personel");
                    personelNames[i] = name;
                    dgViewWaiter.Rows.Add(id, name,address,tckn,tel,mail, work);
                    dgViewWaiter.Refresh();
                }
                UserLog.WConsole("Dosyadan Personel Okuma Basarili !");

            }
            catch
            {
                UserLog.WConsole("(!) Dosyadan Personel Okuma Hatasi !");
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
                        UserLog.WConsole("<<F4 Pressed>>");
                        dailyTableCounter++; // dailyTableCounter -> Bugun acilan kacinci masa oldugunu gosteriyor
                        tableCounter++;  // countOfTables -> Su anda  mevcut kac tane acik masa oldugunu gosteriyor
                        if (tableCounter >= maxCountOfTables)
                        {
                            maxCountOfTables = tableCounter;
                        }
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
                        bLeft.MouseDown += new MouseEventHandler(masa_click);
                        bLeft.MouseDoubleClick += new MouseEventHandler(masa_MouseDoubleClick);
                        lMasaNo.Text = MasaNo.ToString();
                        string text = lMasaNo.Text;
                        switch (text.Length)
                        {
                            case 1:
                            case 2:
                            case 3:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 42f, lMasaNo.Font.Style);
                                break;
                            case 4:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 37f, lMasaNo.Font.Style);
                                break;
                            case 5:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 35f, lMasaNo.Font.Style);
                                break;
                            case 6:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 30f, lMasaNo.Font.Style);
                                break;
                            case 7:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 25f, lMasaNo.Font.Style);
                                break;
                            case 8:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 22f, lMasaNo.Font.Style);
                                break;
                            case 9:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 20f, lMasaNo.Font.Style);
                                break;
                            case 10:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 18f, lMasaNo.Font.Style);
                                break;
                            case 11:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 16f, lMasaNo.Font.Style);
                                break;
                            case 12:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 15f, lMasaNo.Font.Style);
                                break;
                            default:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 8f, lMasaNo.Font.Style);
                                break;
                        }
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
                            LastChoosenTable.reservation = true;
                        }
                        else
                        {
                            lMasaNo.ForeColor = Color.Red;
                            LastChoosenTable.reservation = false;
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
                        bPrint.Enabled = true;
                        bTableDetailChange.Enabled = true;
                        bTableClose.Enabled = true;
                        //                
                        lToplamTutar.Text = "0 ₺";  
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
                        if (tableCounter >= maxCountOfTables)
                        {
                            maxCountOfTables = tableCounter;
                        }
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
                        bLeft.MouseDown += new MouseEventHandler(masa_click);
                        lMasaNo.Text = MasaNo.ToString();
                        string text = lMasaNo.Text;
                        switch (text.Length)
                        {
                            case 1:
                            case 2:
                            case 3:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 42f, lMasaNo.Font.Style);
                                break;
                            case 4:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 37f, lMasaNo.Font.Style);
                                break;
                            case 5:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 35f, lMasaNo.Font.Style);
                                break;
                            case 6:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 30f, lMasaNo.Font.Style);
                                break;
                            case 7:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 25f, lMasaNo.Font.Style);
                                break;
                            case 8:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 22f, lMasaNo.Font.Style);
                                break;
                            case 9:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 20f, lMasaNo.Font.Style);
                                break;
                            case 10:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 18f, lMasaNo.Font.Style);
                                break;
                            case 11:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 16f, lMasaNo.Font.Style);
                                break;
                            case 12:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 15f, lMasaNo.Font.Style);
                                break;
                            default:
                                lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 8f, lMasaNo.Font.Style);
                                break;
                        }
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
                            LastChoosenTable.reservation = true;
                        }
                        else
                        {
                            lMasaNo.ForeColor = Color.Red;
                            LastChoosenTable.reservation = false;
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
                            bPrint.Enabled = true;
                            bTableDetailChange.Enabled = true;
                            bTableClose.Text = "R. Iptal";
                        }
                        else
                        {
                            bActiveEt.Visible = false;
                            bPrint.Enabled = true;
                            bActiveEt.Enabled = false;
                            bTableClose.Text = "Masa Kapat";
                        }
                        lToplamTutar.Text = "0 ₺";
                    }
                }
            }
            if (e.KeyCode == Keys.F1)
            {
                AboutBox about = new AboutBox();
                about.Show();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (tbSearchKey.Focused)
                {
                    bStokAra_Click(null, null);
                }
            }
            /*
            if (e.KeyCode == Keys.S) //Siparis Ekle
            {
                if (tabControl1.SelectedIndex == 0 && lMasaNo.Text != "-")
                {
                    bSiparisEkle_Click(null, null);
                }
            }
            */
        }

        private void ExchangeValuesChanged(object sender, EventArgs e)
        {
            if (tbEuro.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            if (tbDolar.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            if (tbGBP.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";

            if (tbDolar.Text == "" || tbDolar.Text == "0" || tbDolar.Text == "0.0" || tbDolar.Text == "0,0")
            {
                Settings.Dolar = false;
            }
            else
            {
                Settings.Dolar = true;
            }
            if (tbEuro.Text == "" || tbEuro.Text == "0" || tbEuro.Text == "0.0" || tbEuro.Text == "0,0")
            {
                Settings.Euro= false;
            }
            else
            {
                Settings.Euro = true;
            }
            if (tbGBP.Text == "" || tbGBP.Text == "0" || tbGBP.Text == "0.0" || tbGBP.Text == "0,0")
            {
                Settings.GBP= false;
            }
            else
            {
                Settings.GBP = true;
            }
            INI.Write("Dolar", tbDolar.Text.ToString(), "Exchange");
            INI.Write("Euro", tbEuro.Text.ToString(), "Exchange");
            INI.Write("GBP", tbGBP.Text.ToString(), "Exchange");
            try
            {
                LastChoosenTable.DefinedDolar = Convert.ToDecimal(tbDolar.Text, culture);
                UserLog.WConsole("Dolar Kuru basariyla kaydedildi !");
                lExchange.Text = "1 ₺ = " + Convert.ToDecimal(tbDolar.Text, culture).ToString() + " $ = ";
            }
            catch
            {
                UserLog.WConsole("(!) Dolar Kurunu LastChoosenTable'a Kaydetmede Hata !");
                lExchange.Text = "1 ₺ = " + ""+ " $ = ";
            }
            try
            {
                LastChoosenTable.DefinedEuro = Convert.ToDecimal(tbEuro.Text, culture);
                UserLog.WConsole("Euro Kuru basariyla kaydedildi !");
                lExchange.Text += Convert.ToDecimal(tbEuro.Text, culture).ToString() + " € = ";
            }
            catch
            {
                UserLog.WConsole("(!) Euro Kurunu LastChoosenTable'a Kaydetmede Hata !");
                lExchange.Text += "" + " € = ";
            }
            try
            {
                LastChoosenTable.DefinedGBP = Convert.ToDecimal(tbGBP.Text, culture);
                UserLog.WConsole("GBP Kuru basariyla kaydedildi !");
                lExchange.Text += Convert.ToDecimal(tbGBP.Text, culture).ToString() + " £";
            }
            catch
            {
                UserLog.WConsole("(!) GBP Kurunu LastChoosenTable'a Kaydetmede Hata !");
                lExchange.Text += ""+ " £";
            }
            

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
                    try
                    {
                        stokCount = Convert.ToInt32(INI.Read("stokCount", "Stok"));
                    }
                    catch
                    {
                        stokCount = 0;
                    }
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
                            if (StokAdd.menuUrunu == true)
                            {
                                if (StokAdd.dinamikStokKontrolu == true)
                                {
                                    dgView.Rows.Add(StokAdd.ID, StokAdd.Aciklama, StokAdd.Adet.ToString(), StokAdd.AdetTuru.ToString(), StokAdd.BirimFiyat.ToString(), StokAdd.ParaBirimi.ToString(), CheckState.Checked, CheckState.Checked);
                                }
                                else
                                {
                                    dgView.Rows.Add(StokAdd.ID, StokAdd.Aciklama, StokAdd.Adet.ToString(), StokAdd.AdetTuru.ToString(), StokAdd.BirimFiyat.ToString(), StokAdd.ParaBirimi.ToString(), CheckState.Unchecked, CheckState.Checked);

                                }
                            }
                            else
                            {
                                dgView.Rows.Add(StokAdd.ID, StokAdd.Aciklama, StokAdd.Adet.ToString(), StokAdd.AdetTuru.ToString(), StokAdd.BirimFiyat.ToString(), StokAdd.ParaBirimi.ToString(), CheckState.Unchecked, CheckState.Unchecked);
                            }
                            // dgView.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, CheckState.Checked);
                            saveStokToFile();
                            getStokFromFile();

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
            LastChoosenTable.lastClosedTableTutar = Convert.ToDecimal(masaToplam,culture);             //Tutar
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
                    UserLog.WConsole("LastChoosen Table Index : " + findTableOrder(LastChoosenTable.TableNumber));
                    UserLog.WConsole("LastChoosen Table Name : " +LastChoosenTable.TableNumber);
                    //stokAdd.ListName = "Personel";
                    var result = tableClose.ShowDialog();
                    if (result == DialogResult.OK)
                    {

                        dgView.Refresh();
                        ///Urunleri Stoktan Dus ----------------------------------------------------------------------
                        //tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 2] = toplamTutar.ToString(); // Tutar
                        //tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 4] = dgViewSiparis.RowCount.ToString(); // Toplam urun siparis cesidi
                        for (int k = 0; k < dgViewSiparis.RowCount; k++)
                        {
                            for (int m = 0; m < dgView.RowCount; m++)
                            {
                                if (dgView.Rows[m].Cells[0].Value.ToString() == tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 1, k])
                                {
                                    if (dgView.Rows[m].Cells[6].Value.ToString()=="Checked" || dgView.Rows[m].Cells[6].Value.ToString() == "True")
                                    {
                                        dgView.Rows[m].Cells[2].Value = Convert.ToInt32(dgView.Rows[m].Cells[2].Value) - Convert.ToInt32(tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 2, k]);
                                        UserLog.WConsole("Stoktan urun dusuldu !");
                                        saveStokToFile();
                                    }
                                }
                            }
                            //tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 1, k] = dgViewSiparis.Rows[k].Cells[0].Value.ToString(); // Urun id si
                            //tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3 + 2, k] = dgViewSiparis.Rows[k].Cells[2].Value.ToString(); // Urun adedi
                        }
                        decimal masa_nakit = 0, masa_kredi = 0;
                        ///Nakit Toplam  -----------------------------------------------------------------------------
                        string[] nakitDizi = lNakitToplam.Text.Split('+');
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                nakitDizi[0] = (Convert.ToDecimal(nakitDizi[0].Replace("₺", ""),culture) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "₺ +" + nakitDizi[1] + "+" + nakitDizi[2] + "+" + nakitDizi[3];
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""),culture) + Convert.ToDecimal(LastChoosenTable.nakit,culture)).ToString()+ "₺";
                                masa_nakit = Convert.ToDecimal(LastChoosenTable.nakit,culture);
                                break;
                            case " €":
                                nakitDizi[1] = (Convert.ToDecimal(nakitDizi[1].Replace("€", ""), culture) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "+ " + nakitDizi[1] + "€ +" + nakitDizi[2] + "+" + nakitDizi[3];
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.nakit, culture) /LastChoosenTable.DefinedEuro),2)).ToString() + "₺";
                                masa_nakit = Convert.ToDecimal(LastChoosenTable.nakit, culture) /LastChoosenTable.DefinedEuro;
                                break;
                            case " $":
                                nakitDizi[2] = (Convert.ToDecimal(nakitDizi[2].Replace("$", ""), culture) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "+" + nakitDizi[1] + "+ " + nakitDizi[2] + "$ +" + nakitDizi[3];
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.nakit, culture) / LastChoosenTable.DefinedDolar),2)).ToString() + "₺";
                                masa_nakit = Convert.ToDecimal(LastChoosenTable.nakit, culture) /LastChoosenTable.DefinedDolar;
                                break;
                            case " £":
                                nakitDizi[3] = (Convert.ToDecimal(nakitDizi[3].Replace("£", ""), culture) + LastChoosenTable.nakit).ToString();
                                lNakitToplam.Text = nakitDizi[0] + "+" + nakitDizi[1] + "+" + nakitDizi[2] + "+ " + nakitDizi[3] + "£";
                                lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.nakit, culture) / LastChoosenTable.DefinedGBP),2)).ToString() + "₺";
                                masa_nakit = Convert.ToDecimal(LastChoosenTable.nakit, culture) /LastChoosenTable.DefinedGBP;
                                break;

                        }
                        ///Kredi Toplam -----------------------------------------------------------------------------
                        string[] krediDizi = lKrediToplam.Text.Split('+');
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                krediDizi[0] = (Convert.ToDecimal(krediDizi[0].Replace("₺", ""), culture) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "₺ +" + krediDizi[1] + "+" + krediDizi[2] + "+" + krediDizi[3];
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""), culture) + Convert.ToDecimal(LastChoosenTable.krediKarti, culture)).ToString() + "₺";
                                masa_kredi = Convert.ToDecimal(LastChoosenTable.krediKarti, culture);
                                break;
                            case " €":
                                krediDizi[1] = (Convert.ToDecimal(krediDizi[1].Replace("€", ""), culture) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "+ " + krediDizi[1] + "€ +" + krediDizi[2] + "+" + krediDizi[3];
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.krediKarti, culture) / LastChoosenTable.DefinedEuro),2)).ToString() + "₺";
                                masa_kredi = Convert.ToDecimal(LastChoosenTable.krediKarti, culture) /LastChoosenTable.DefinedEuro;
                                break;
                            case " $":
                                krediDizi[2] = (Convert.ToDecimal(krediDizi[2].Replace("$", ""), culture) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "+" + krediDizi[1] + "+ " + krediDizi[2] + "$ +" + krediDizi[3];
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.krediKarti, culture) / LastChoosenTable.DefinedDolar),2)).ToString() + "₺";
                                masa_kredi = Convert.ToDecimal(LastChoosenTable.krediKarti, culture) /LastChoosenTable.DefinedDolar;
                                break;
                            case " £":
                                krediDizi[3] = (Convert.ToDecimal(krediDizi[3].Replace("£", ""), culture) + LastChoosenTable.krediKarti).ToString();
                                lKrediToplam.Text = krediDizi[0] + "+" + krediDizi[1] + "+" + krediDizi[2] + "+ " + krediDizi[3] + "£";
                                lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.krediKarti, culture) / LastChoosenTable.DefinedGBP),2)).ToString() + "₺";
                                masa_kredi = Convert.ToDecimal(LastChoosenTable.krediKarti, culture) /LastChoosenTable.DefinedGBP;
                                break;

                        }
                        ///Cari Toplam  -----------------------------------------------------------------------------
                        string[] cariDizi = lCariToplam.Text.Split('+');
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                cariDizi[0] = (Convert.ToDecimal(cariDizi[0].Replace("₺", ""), culture) + LastChoosenTable.cari).ToString();
                                lCariToplam.Text = cariDizi[0] + "₺ +" + cariDizi[1] + "+" + cariDizi[2] + "+" + cariDizi[3];
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", ""), culture) + Convert.ToDecimal(LastChoosenTable.cari, culture)).ToString() + "₺";
                                break;
                            case " €":
                                cariDizi[1] = (Convert.ToDecimal(cariDizi[1].Replace("€", ""), culture) + LastChoosenTable.cari).ToString();
                                lCariToplam.Text = cariDizi[0] + "+ " + cariDizi[1] + "€ +" + cariDizi[2] + "+" + cariDizi[3];
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.cari, culture) / LastChoosenTable.DefinedEuro),2)).ToString() + "₺";
                                break;
                            case " $":
                                cariDizi[2] = (Convert.ToDecimal(cariDizi[2].Replace("$", ""), culture) + LastChoosenTable.cari).ToString(); /// PATLAMA NOKTASI //
                                lCariToplam.Text = cariDizi[0] + "+" + cariDizi[1] + "+ " + cariDizi[2] + "$ +" + cariDizi[3];
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.cari, culture) / LastChoosenTable.DefinedDolar),2)).ToString() + "₺";
                                break;
                            case " £":
                                cariDizi[3] = (Convert.ToDecimal(cariDizi[3].Replace("£", ""), culture) + LastChoosenTable.cari).ToString();
                                lCariToplam.Text = cariDizi[0] + "+" + cariDizi[1] + "+" + cariDizi[2] + "+ " + cariDizi[3] + "£";
                                lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", ""), culture) + System.Math.Round((Convert.ToDecimal(LastChoosenTable.cari, culture) / LastChoosenTable.DefinedGBP),2)).ToString() + "₺";
                                break;

                        }
                        ////Kasa Islemleri  (Genel Toplam)   ---------------------------------------------------------
                        //lKasaToplam.Text = (OldKasaToplam+Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""))+ Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""))).ToString() + " ₺";
                        lKasaToplam.Text = (OldKasaToplam + masa_kredi + masa_nakit).ToString() + " ₺";
                        lBugunToplam.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""), culture) + Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", ""), culture)).ToString() + " ₺";
                        UserLog.WConsole("lKasaToplam.Text : " + lKasaToplam.Text.ToString());
                        //dgKasa ->> Zaman | Yapilan Islem | Masa Adi | Personel | Cari | Nakit | Kredi Karti | Tutar
                        // !!!!! ALTTAKI SATIR MASA KAPAMA TAMAMLANINCA ISLEME ACILACAK !!!!! 
                        //dgvKasa.Rows.Add(LastChoosenTable.lastClosedTableTime, "Masa Kapama", LastChoosenTable.lastClosedTable, LastChoosenTable.lastClosedTableWaiter, "0", "0", lToplamTutar.Text.ToString(), lToplamTutar.Text.ToString());
                        decimal sonMasaToplam = 0;
                        decimal tipToplam = 0;
                        switch (LastChoosenTable.paraBirimi)
                        {
                            case " ₺":
                                sonMasaToplam = Convert.ToDecimal(masaToplam, culture);
                                tipToplam = LastChoosenTable.tip;
                                break;
                            case " €":
                                sonMasaToplam = Convert.ToDecimal(masaToplam, culture) * LastChoosenTable.DefinedEuro;
                                tipToplam = LastChoosenTable.tip* LastChoosenTable.DefinedEuro;
                                break;
                            case " $":
                                sonMasaToplam = Convert.ToDecimal(masaToplam, culture) * LastChoosenTable.DefinedDolar;
                                tipToplam = LastChoosenTable.tip * LastChoosenTable.DefinedDolar;
                                break;
                            case " £":
                                sonMasaToplam = Convert.ToDecimal(masaToplam, culture) * LastChoosenTable.DefinedGBP;
                                tipToplam = LastChoosenTable.tip * LastChoosenTable.DefinedGBP;
                                break;

                        }
                        //Masa cost adding to Kasa
                        dgvKasa.Rows.Add(DateTime.UtcNow.ToLocalTime().ToLongDateString()+" "+ DateTime.UtcNow.ToLocalTime().ToLongTimeString(), "Masa Kapama", tableName.ToString(), lPersonel.Text.Replace("Personel :", ""), lMusteriAdi.Text.Replace("Müşteri : ",""), LastChoosenTable.nakit + LastChoosenTable.paraBirimi, LastChoosenTable.krediKarti + LastChoosenTable.paraBirimi, LastChoosenTable.cari + LastChoosenTable.paraBirimi , System.Math.Round(sonMasaToplam,2) + LastChoosenTable.paraBirimi,LastChoosenTable.iskonto+"%");
                        SaveDataToXml("Masa Kapama", "Masa_Kapama_Aciklama",(Convert.ToDecimal(LastChoosenTable.nakit, culture)).ToString(),(Convert.ToDecimal(LastChoosenTable.krediKarti,culture)).ToString(),(Convert.ToDecimal(LastChoosenTable.cari,culture)).ToString(),(Convert.ToDecimal(LastChoosenTable.lastClosedTableTutar,culture)).ToString(), lPersonel.Text.Replace("Personel :", ""), tableName.ToString(), LastChoosenTable.paraBirimi);
                        FirstLoadDataFromXml();
                        //Tip adding to Kasa
                        if (LastChoosenTable.tip != 0)
                        { 
                        dgvKasa.Rows.Add(DateTime.UtcNow.ToLocalTime().ToLongDateString() + " " + DateTime.UtcNow.ToLocalTime().ToLongTimeString(), "Tip", tableName.ToString(), lPersonel.Text.Replace("Personel :", ""), lMusteriAdi.Text.Replace("Müşteri : ", ""), "0" + LastChoosenTable.paraBirimi, "0"+ LastChoosenTable.paraBirimi, "0"+ LastChoosenTable.paraBirimi, System.Math.Round(LastChoosenTable.tip, 2) + LastChoosenTable.paraBirimi,"0%");
                        SaveDataToXml("Tip", "Tip_Aciklama",(Convert.ToDecimal(LastChoosenTable.tip,culture)).ToString(), "0", "0",(Convert.ToDecimal(LastChoosenTable.lastClosedTableTutar,culture)).ToString(), lPersonel.Text.Replace("Personel :", ""), tableName.ToString(), LastChoosenTable.paraBirimi);
                        LastChoosenTable.tip = 0;
                        FirstLoadDataFromXml();
                        }
                        UserLog.WConsole("LastChoosenTable.tip : " + LastChoosenTable.tip);
                        dgvKasa.Refresh();
                        lTipToplam.Text = (System.Math.Round(Convert.ToDecimal(lTipToplam.Text.Replace("₺", ""),culture),2) + System.Math.Round(tipToplam,2)).ToString() + " ₺";
                        //KasaSave();
                        ////Kasa Islemleri END ---------------------------------------------------------
                        ////////////////////////////////////////////////////////////////////////////////
                            clearSiparisTable();
                            int i = 0;
                            foreach (string tablenames in tableNumbers)
                            {
                            //UserLog.WConsole(tablenames);
                            //UserLog.WConsole(emptyTableList[i].ToString());
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
                                    bPrint.Enabled = false;
                                    bTableDetailChange.Enabled = false;
                                    bTableClose.Enabled = false;
                                    //SaveDataToXml("Masa Kapama", "Masa_Kapama_Aciklama", LastChoosenTable.nakit, LastChoosenTable.krediKarti, LastChoosenTable.cari, lPersonel.Text.Replace("Personel :", ""), tableName.ToString(), LastChoosenTable.paraBirimi);
                                    //FirstLoadDataFromXml();
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
                    reservedTableCount--;
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
                            dgvKasa.Rows.Add(DateTime.UtcNow.ToLocalTime().ToLongDateString() + " " + DateTime.UtcNow.ToLocalTime().ToLongTimeString(), "Rezervasyon Iptal", tableName.ToString(), lPersonel.Text.Replace("Personel :", ""), lMusteriAdi.Text.Replace("Müşteri : ", ""), "-", "-", "-", "-","-");
                            dgvKasa.Refresh();
                            //KasaSave();
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
                            bPrint.Enabled = false;
                            bTableDetailChange.Enabled = false;
                            bTableClose.Enabled = false;
                            SaveDataToXml("Rezervasyon Kapama", "Rezervasyon_Aciklama", "0", "0", "0", "0",lPersonel.Text.Replace("Personel :", ""), tableName.ToString(), "₺");
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

        private void KasaSave()
        {
            int countNo = Convert.ToInt32(INI.Read("NoOfRows", "KasaDaily"));
            UserLog.WConsole(" countNo 1 : " + countNo);
            
            /* try
            {
                
                for (int i = 0; i < countNo; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        INI.DeleteKey("Sat" + i + "Sut" + j , "KasaDaily");
                    }
                }
                
                INI.DeleteSection("KasaDaily");
                INI.DeleteKey("NoOfRows", "KasaDaily");
                UserLog.WConsole("Kasa kasyitlari basariyla silindi");
                
            }
            catch
            {
                UserLog.WConsole("(!) Kasa kaydi bulunamadi");
            }
            */
            countNo++;
            UserLog.WConsole(" countNo 2 : " + countNo);
            INI.Write("NoOfRows", (countNo).ToString(), "KasaDaily");
            int i = countNo - 1;
            UserLog.WConsole(" i : " + i);
            try
            {
               
                    for (int j = 0; j < 8; j++)//sutun
                    {
                        if (j > 3)
                        {
                            if (dgvKasa.Rows[dgvKasa.RowCount-1].Cells[j].Value.ToString().Contains("₺"))
                            {
                                INI.Write("Sat" +i + "Sut" + j, (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("₺", ""), culture)).ToString().Replace("₺", "TL"), "KasaDaily");
                                dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value = (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("₺", ""), culture)).ToString() + " ₺";
                            }
                            else if (dgvKasa.Rows[dgvKasa.RowCount-1].Cells[j].Value.ToString().Contains("€"))
                            {
                                INI.Write("Sat" + i + "Sut" + j, (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("€", ""), culture)).ToString().Replace("€", "EURO"), "KasaDaily");
                                dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value = (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("€", ""), culture)).ToString() + " €";
                            }
                            else if (dgvKasa.Rows[dgvKasa.RowCount-1].Cells[j].Value.ToString().Contains("$"))
                            {
                                INI.Write("Sat" + i + "Sut" + j, (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("$", ""), culture)).ToString().Replace("$", "DOLAR"), "KasaDaily");
                                dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value = (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("$", ""), culture)).ToString() + " $";
                            }
                            else if (dgvKasa.Rows[dgvKasa.RowCount-1].Cells[j].Value.ToString().Contains("£"))
                            {
                                INI.Write("Sat" + i + "Sut" + j, (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount-1].Cells[j].Value.ToString().Replace("£",""),culture)).ToString().Replace("£", "GBP"), "KasaDaily");
                                dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value = (Convert.ToDecimal(dgvKasa.Rows[dgvKasa.RowCount - 1].Cells[j].Value.ToString().Replace("£", ""), culture)).ToString() + " £";
                            }
                        }
                        else
                        {
                            INI.Write("Sat" +i + "Sut" + j, dgvKasa.Rows[dgvKasa.RowCount-1].Cells[j].Value.ToString(), "KasaDaily");
                        }
                    }
                UserLog.WConsole("Kasa kaydedildi ! : " + i);
            }
            catch
            {
                UserLog.WConsole("(!) Kasa kaydedilirken sorun olustu !");
            }
        }

        private void KasaLoad()
        {

            bool today = false;
            string tarih, yapilanIslem, masaAdi, personel, nakit, kredi, cari, toplam;
            dgvKasa.AllowUserToAddRows = true;
            dgvKasa.Refresh();
            DateTime date = Convert.ToDateTime(INI.Read("LoggedDate", "Login"));
            try
                {
                    int rowCount = Convert.ToInt16(INI.Read("NoOfRows", "KasaDaily"));
                    for (int i = 0; i < rowCount; i++)
                    {
                        tarih = INI.Read("Sat" + i + "Sut0", "KasaDaily");
                        yapilanIslem = INI.Read("Sat" + i + "Sut1", "KasaDaily");
                        masaAdi = INI.Read("Sat" + i + "Sut2", "KasaDaily");
                        personel = INI.Read("Sat" + i + "Sut3", "KasaDaily");
                        nakit = INI.Read("Sat" + i + "Sut4", "KasaDaily");
                        kredi= INI.Read("Sat" + i + "Sut5", "KasaDaily");
                        cari= INI.Read("Sat" + i + "Sut6", "KasaDaily");
                        toplam = INI.Read("Sat" + i + "Sut7", "KasaDaily");
                    if (Convert.ToDateTime(tarih).ToLongDateString() == System.DateTime.Now.ToLongDateString())
                        {
                        NoOfRecordToday++;
                        UserLog.WConsole("NoOfRecordToday : " + NoOfRecordToday);
                        
                        today =true;
                        }
                        if (toplam.Contains("TL"))
                            {
                            nakit = nakit.Replace("TL", "₺");
                            kredi= kredi.Replace("TL", "₺");
                            cari = cari.Replace("TL", "₺");
                            toplam= toplam.Replace("TL", "₺");

                            if(today)
                            { 
                                lNakitToplamTL.Text = System.Math.Round((Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("₺", ""))),2).ToString()+ "₺";
                                lKrediToplamTL.Text = System.Math.Round((Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(kredi.Replace("₺", ""))),2).ToString() + "₺";
                                lCariToplamTL.Text = System.Math.Round((Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(cari.Replace("₺", ""))),2).ToString() + "₺";
                                lBugunToplam.Text = System.Math.Round((Convert.ToDecimal(lBugunToplam.Text.Replace("₺", ""))  + Convert.ToDecimal(toplam.Replace("₺", ""))), 2).ToString() + "₺";
                        }
                            if (yapilanIslem.Contains("Tip") == true)
                            {
                            lTipToplam.Text = System.Math.Round(Convert.ToDecimal(toplam.Replace("₺","")) +(Convert.ToDecimal(lTipToplam.Text.Replace("₺", ""))), 2).ToString() + "₺";
                            }
                            lKasaToplam.Text = System.Math.Round((Convert.ToDecimal(lKasaToplam.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("₺", "")) + Convert.ToDecimal(kredi.Replace("₺", ""))),2).ToString() + "₺";
                        }
                        else if (toplam.Contains("EURO"))
                        {
                                nakit = nakit.Replace("EURO", "€");
                                kredi = kredi.Replace("EURO", "€");
                                cari = cari.Replace("EURO", "€");
                                toplam = toplam.Replace("EURO", "€");
                            if (today)
                            { 
                                lNakitToplamTL.Text = System.Math.Round((Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("€", "")) /LastChoosenTable.DefinedEuro),2).ToString() + "₺";
                                lKrediToplamTL.Text = System.Math.Round((Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(kredi.Replace("€", "")) /LastChoosenTable.DefinedEuro),2).ToString() + "₺";
                                lCariToplamTL.Text = System.Math.Round((Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(cari.Replace("€", "")) /LastChoosenTable.DefinedEuro),2).ToString() + "₺";
                                lBugunToplam.Text = System.Math.Round((Convert.ToDecimal(lBugunToplam.Text.Replace("₺", "")) + Convert.ToDecimal(toplam.Replace("€", "")) / LastChoosenTable.DefinedEuro), 2).ToString() + "₺";
                            }
                            if (yapilanIslem == "Tip")
                            {
                                 lTipToplam.Text = System.Math.Round(Convert.ToDecimal(toplam.Replace("€", "")) /LastChoosenTable.DefinedEuro + (Convert.ToDecimal(lTipToplam.Text.Replace("₺", ""))), 2).ToString() + "₺";
                            }
                            lKasaToplam.Text = System.Math.Round((Convert.ToDecimal(lKasaToplam.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("€", "")) /LastChoosenTable.DefinedEuro + Convert.ToDecimal(kredi.Replace("€", "")) /LastChoosenTable.DefinedEuro),2).ToString() + "₺";
                        }
                        else if (toplam.Contains("DOLAR"))
                        {
                                nakit = nakit.Replace("DOLAR", "$");
                                kredi = kredi.Replace("DOLAR", "$");
                                cari = cari.Replace("DOLAR", "$");
                                toplam = toplam.Replace("DOLAR", "$");
                            if (today)
                            {
                                lNakitToplamTL.Text = System.Math.Round((Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("$", "")) / LastChoosenTable.DefinedDolar),2).ToString() + "₺";
                                lKrediToplamTL.Text = System.Math.Round((Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(kredi.Replace("$", "")) / LastChoosenTable.DefinedDolar),2).ToString() + "₺";
                                lCariToplamTL.Text = System.Math.Round((Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(cari.Replace("$", "")) / LastChoosenTable.DefinedDolar),2).ToString() + "₺";
                                lBugunToplam.Text = System.Math.Round((Convert.ToDecimal(lBugunToplam.Text.Replace("₺", "")) + Convert.ToDecimal(toplam.Replace("$", "")) / LastChoosenTable.DefinedDolar), 2).ToString() + "₺";
                            }
                            if (yapilanIslem == "Tip")
                            {
                                lTipToplam.Text = System.Math.Round(Convert.ToDecimal(toplam.Replace("$", "")) / LastChoosenTable.DefinedDolar + (Convert.ToDecimal(lTipToplam.Text.Replace("₺", ""))), 2).ToString() + "₺";
                            }
                             lKasaToplam.Text = System.Math.Round((Convert.ToDecimal(lKasaToplam.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("$", "")) / LastChoosenTable.DefinedDolar+ Convert.ToDecimal(kredi.Replace("$", "")) / LastChoosenTable.DefinedDolar),2).ToString() + "₺";
                        }
                        else if (toplam.Contains("GBP"))
                        {
                            nakit = nakit.Replace("GBP", "£");
                            kredi = kredi.Replace("GBP", "£");
                            cari = cari.Replace("GBP", "£");
                            toplam = toplam.Replace("GBP", "£");
                        if (today)
                        {
                            lNakitToplamTL.Text = System.Math.Round((Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("£", "")) / LastChoosenTable.DefinedGBP),2).ToString() + "₺";
                            lKrediToplamTL.Text = System.Math.Round((Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(kredi.Replace("£", "")) / LastChoosenTable.DefinedGBP),2).ToString() + "₺";
                            lCariToplamTL.Text = System.Math.Round((Convert.ToDecimal(lCariToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(cari.Replace("£", "")) / LastChoosenTable.DefinedGBP),2).ToString() + "₺";
                            lBugunToplam.Text = System.Math.Round((Convert.ToDecimal(lBugunToplam.Text.Replace("₺", "")) +Convert.ToDecimal(toplam.Replace("£", "")) / LastChoosenTable.DefinedGBP), 2).ToString() + "₺";
                        }
                        if (yapilanIslem == "Tip")
                        {
                            lTipToplam.Text = System.Math.Round(Convert.ToDecimal(toplam.Replace("£", "")) / LastChoosenTable.DefinedGBP + (Convert.ToDecimal(lTipToplam.Text.Replace("₺", ""))), 2).ToString() + "₺";
                        }
                        lKasaToplam.Text = System.Math.Round((Convert.ToDecimal(lKasaToplam.Text.Replace("₺", "")) + Convert.ToDecimal(nakit.Replace("£", "")) / LastChoosenTable.DefinedGBP + Convert.ToDecimal(kredi.Replace("£", "")) / LastChoosenTable.DefinedGBP),2).ToString() + "₺";
                        }
                        if (today)
                        {
                            dgvKasa.Rows.Add(tarih, yapilanIslem, masaAdi, personel, nakit, kredi, cari, toplam);
                        //dgvKasa.Rows.Add("a","b","c","d","e","f","g","h");
                        dgvKasa.Refresh();
                    }
                       
                    }
                lBugunToplam.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace("₺", "")) + Convert.ToDecimal(lKrediToplamTL.Text.Replace("₺", ""))).ToString() + "₺";
                UserLog.WConsole("Kasa Okuma Tamamlandi");
                }
                catch
                {
                    UserLog.WConsole("(!)Kasa Okurken Hata Olustu !");

                }
                dgvKasa.AllowUserToAddRows = false;
                dgvKasa.Refresh();
            UserLog.WConsole("lKasaToplam.Text 1 : " + lKasaToplam.Text.ToString());
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
                        int countOfElement = Convert.ToInt32(dgViewSiparis.Rows[existedRow].Cells[2].Value);
                        UserLog.WConsole(countOfElement.ToString());
                        //Adet
                        dgViewSiparis.Rows[existedRow].Cells[2].Value = (countOfElement + 1).ToString();
                        //Tutar
                        string tutar = dgViewSiparis.Rows[existedRow].Cells[5].Value.ToString();  // 5 TL
                        tutar = tutar.Replace(" ₺", "");  // 5
                        tutar = (Convert.ToDecimal(tutar, culture) + Convert.ToDecimal(showListForm.Selected_Meal_Price, culture)).ToString();  // 10
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
                        dgViewSiparis.Rows.Add(mealID, meal, "1", null, null, (Convert.ToDecimal(mealPrice, culture)).ToString() + " ₺", (Convert.ToDecimal(mealPrice, culture)).ToString() + " ₺");
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
            if (e.ColumnIndex == 3 && e.RowIndex >= 0) // Siparis miktarini 1 artir
            {
                //UserLog.WConsole(e.RowIndex.ToString());

                int oldCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                dgViewSiparis.Rows[e.RowIndex].Cells[2].Value = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value) + 1).ToString();
                int newCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                //tutar hesabi
                string tutar =(dgViewSiparis.Rows[e.RowIndex].Cells[5].Value).ToString(); // 5 TL
                tutar = tutar.Replace(" ₺", "");
                tutar = ((Convert.ToDecimal(tutar,culture) / oldCount) * newCount).ToString();  // 10
                dgViewSiparis.Rows[e.RowIndex].Cells[5].Value = tutar + " ₺";
            }
            if (e.ColumnIndex == 4 && e.RowIndex>=0) // Siparis miktarini 1 azalt
            {
                UserLog.WConsole((e.RowIndex + 1).ToString() + ". siradaki urune tiklandi"); // Silinmeli
                int adet = Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value);
                if (adet != 1)
                {
                    int oldCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                    dgViewSiparis.Rows[e.RowIndex].Cells[2].Value = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value) - 1).ToString();
                    int newCount = (Convert.ToInt16(dgViewSiparis.Rows[e.RowIndex].Cells[2].Value));
                    //tutar hesabi
                    string tutar = (dgViewSiparis.Rows[e.RowIndex].Cells[5].Value).ToString(); // 5 TL
                    tutar = tutar.Replace(" ₺", "");
                    tutar = (((Convert.ToDecimal(tutar,culture)) / oldCount) * newCount).ToString();
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
            toplamTutar = toplamTutar-(toplamTutar * Convert.ToInt16(lIskonto.Text.Replace("Iskonto Orani : ", "").Replace("%", "")) / 100);
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
                        bTableDetailChange.Enabled = true;
                        bActiveEt.Enabled = false;
                    }
                    i++;
                }
                if (tableDetails[findTableOrder(LastChoosenTable.TableNumber) * 3, 7] == "R")
                {
                    bActiveEt.Visible = true;
                    bActiveEt.Enabled = true;
                    bSiparisEkle.Enabled = false;
                    bTableDetailChange.Enabled = true;
                    bTableClose.Text = "R. Iptal";
                }
                else
                {
                    bActiveEt.Visible = false;
                    bActiveEt.Enabled = false;
                    bSiparisEkle.Enabled = true;
                    bTableDetailChange.Enabled = true;
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
                    //GetReportDetails()
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

        private void SaveDataToXml(string islemTuru,string Aciklama,string Nakit,string KrediKarti,string Cari,string Toplam,string Personel,string masaAdi,string ParaBirimi)
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
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Resources/ReportDatabase.xml");
                //create node and add value
                XmlNode node = doc.CreateNode(XmlNodeType.Element, "Islem", null);
                //node.InnerText = "this is new node";

                //create title node
                XmlNode subNodeTarih = doc.CreateElement("Tarih");
                XmlNode subNodeZaman = doc.CreateElement("Zaman");
                XmlNode subNodeIslemTuru = doc.CreateElement("IslemTuru");
                XmlNode subNodeAciklama = doc.CreateElement("Aciklama");
                XmlNode subNodeNakit = doc.CreateElement("Nakit");
                XmlNode subNodeKrediKarti = doc.CreateElement("KrediKarti");
                XmlNode subNodeCari = doc.CreateElement("Cari");
                XmlNode subNodeToplam = doc.CreateElement("toplam");
                XmlNode subNodePersonel = doc.CreateElement("Personel");
                XmlNode subNodeMasaAdi = doc.CreateElement("MasaAdi");
                XmlNode subNodeParaBirimi = doc.CreateElement("ParaBirimi");
                XmlNode subNodeMusteri = doc.CreateElement("Musteri");
                XmlNode subNodeIskonto = doc.CreateElement("Iskonto");
                //add value for child nodes
                subNodeTarih.InnerText = System.DateTime.Now.ToLongDateString();
                subNodeZaman.InnerText = System.DateTime.Now.ToShortTimeString();
                //External Data (Outsource contents)
                subNodeIslemTuru.InnerText = islemTuru;
                subNodeAciklama.InnerText = Aciklama;
                subNodeNakit.InnerText = Nakit.ToString();
                subNodeKrediKarti.InnerText = KrediKarti.ToString();
                subNodeCari.InnerText = Cari.ToString();
                subNodeToplam.InnerText = Toplam;
                subNodePersonel.InnerText = Personel;
                subNodeMasaAdi.InnerText = masaAdi;
                subNodeParaBirimi.InnerText = ParaBirimi;
                subNodeMusteri.InnerText = lMusteriAdi.Text.Replace("Müşteri :", "");
                subNodeIskonto.InnerText = lIskonto.Text.Replace("Iskonto Orani : ","").Replace("%","");
                //add to parent node
                node.AppendChild(subNodeTarih);//1
                node.AppendChild(subNodeZaman);//2
                node.AppendChild(subNodeIslemTuru);//3
                node.AppendChild(subNodeAciklama);//4
                node.AppendChild(subNodeNakit);//5
                node.AppendChild(subNodeKrediKarti);//6
                node.AppendChild(subNodeCari);//7
                node.AppendChild(subNodeToplam);//8
                node.AppendChild(subNodePersonel);//9
                node.AppendChild(subNodeMasaAdi);//10
                node.AppendChild(subNodeParaBirimi);//11
                node.AppendChild(subNodeMusteri);//12
                node.AppendChild(subNodeIskonto);//13
                                                    //add to elements collection
                doc.DocumentElement.AppendChild(node);
                //save back
                doc.Save("Resources/ReportDatabase.xml");
                UserLog.WConsole("Saving Data To XML is Succesfull !");
            }
            catch
            {
                UserLog.WConsole("(!) XML Kaydedilirken Sorun Olustu !");
            }
        }

        private void LoadDataFromXml(DateTime startingDate,DateTime endingDate)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("Resources/ReportDatabase.xml");
            }
            catch
            {
                UserLog.WConsole("(!) XML Dosyasi Yuklenirken Hata Olustu !");
            }
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
            try
            {
                doc.Load("Resources/ReportDatabase.xml");
                XmlNodeList IslemList = doc.GetElementsByTagName("Islem");
                //////////////////////////////////////////////////////////

                ///////////////Gun Gecisleri Burada Ilk Verilen Tarihe Gun Ekleme Yontemiyle Yapiliyor
                //                int Day = startingDate.AddDays(i).Day;
                //                int Month = startingDate.AddDays(i).Month;


                ////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////Weekly Starts
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
                        decimal toplamKasa = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText,culture) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                        switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                        {
                            case " €":
                                toplamKasa = toplamKasa / LastChoosenTable.DefinedEuro;
                                break;
                            case " $":
                                toplamKasa = toplamKasa / LastChoosenTable.DefinedDolar;
                                break;
                            case " £":
                                toplamKasa = toplamKasa / LastChoosenTable.DefinedGBP;
                                break;
                        }
                        kasaToplamArray[i, 1] = toplamKasa.ToString();  // i,1 => Tutar
                        i++;
                        firstReadFlag = true;
                    }
                    else //Except for first read
                    {
                        if (Convert.ToDateTime(kasaToplamArray[i - 1, 0]) == Convert.ToDateTime(IslemElement.GetElementsByTagName("Tarih")[0].InnerText.ToString())) //Ayni tarih devam ise
                        {
                            decimal toplamKasa = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                            switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                            {
                                case " €":
                                    toplamKasa = toplamKasa / LastChoosenTable.DefinedEuro;
                                    break;
                                case " $":
                                    toplamKasa = toplamKasa / LastChoosenTable.DefinedDolar;
                                    break;
                                case " £":
                                    toplamKasa = toplamKasa / LastChoosenTable.DefinedGBP;
                                    break;
                            }
                            switch (IslemElement.GetElementsByTagName("IslemTuru")[0].InnerText)
                            {
                                case "Tip":
                                    //kasaToplamArray[i - 1, 1] = (Convert.ToDecimal(kasaToplamArray[i - 1, 1]) + toplamKasa).ToString();  // i,0 => Tutar
                                    UserLog.WConsole("== TipRecord");
                                    break;
                                case "Masa Kapama":
                                    kasaToplamArray[i - 1, 1] = (Convert.ToDecimal(kasaToplamArray[i - 1, 1], culture) + toplamKasa).ToString();  // i,0 => Tutar
                                    UserLog.WConsole("== Masa Kapama Record");
                                    break;
                                case "Kasadan Tip Dusme":
                                    //kasaToplamArray[i - 1, 1] = (Convert.ToDecimal(kasaToplamArray[i - 1, 1]) - toplamKasa).ToString();  // i,0 => Tutar
                                    UserLog.WConsole("== Kasadan Tip Dusme");
                                    break;
                            }
                            
                        }
                        else  //Sonraki Tarih
                        {
                            kasaToplamArray[i, 0] = IslemElement.GetElementsByTagName("Tarih")[0].InnerText; // i,0 => Tarih 
                            decimal toplamKasa = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                            switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                            {
                                case " €":
                                    toplamKasa = toplamKasa / LastChoosenTable.DefinedEuro;
                                    break;
                                case " $":
                                    toplamKasa = toplamKasa / LastChoosenTable.DefinedDolar;
                                    break;
                                case " £":
                                    toplamKasa = toplamKasa / LastChoosenTable.DefinedGBP;
                                    break;
                            }
                            switch (IslemElement.GetElementsByTagName("IslemTuru")[0].InnerText)
                            {
                                case "Tip":
                                    //kasaToplamArray[i, 1] = toplamKasa.ToString();  // i,1 => Tutar
                                    UserLog.WConsole("== TipRecord");
                                    break;
                                case "Masa Kapama":
                                    kasaToplamArray[i, 1] = toplamKasa.ToString();  // i,1 => Tutar
                                    UserLog.WConsole("== Masa Kapama Record");
                                    break;
                                case "Kasadan Tip Dusme":
                                    //kasaToplamArray[i, 1] = (Convert.ToDecimal(kasaToplamArray[i, 1])- toplamKasa).ToString();  // i,1 => Tutar
                                    UserLog.WConsole("kasadan tip dus :"+ (0 - toplamKasa).ToString());
                                    break;
                            }
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
                    for (int n = 0; n < databaseDayCounter; n++)
                    {

                        if (System.DateTime.Now.AddDays(-pointCounter).ToLongDateString() == kasaToplamArray[n, 0])
                        {
                            chartWeekly.Series["Gunluk Kazanc"].Points.AddXY(kasaToplamArray[n, 0], Convert.ToDecimal(kasaToplamArray[n, 1],culture));
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

                            decimal sum = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture) + Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                            switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                            {
                                case " €":
                                    sum = sum / LastChoosenTable.DefinedEuro;
                                    break;
                                case " $":
                                    sum = sum / LastChoosenTable.DefinedDolar;
                                    break;
                                case " £":
                                    sum = sum / LastChoosenTable.DefinedGBP;
                                    break;
                            }
                            //  UserLog.WConsole(sum);
                            string hour = (Convert.ToDateTime(IslemElement.GetElementsByTagName("Zaman")[0].InnerText).Hour).ToString();
                            if (dailyFlag == false)//first hour 
                            {
                                hourlySum[hourlyCounter, 1] = sum.ToString(); ;
                                //  UserLog.WConsole("hourly sum :" + hourlySum[hourlyCounter, 1]);
                                hourlySum[hourlyCounter, 0] = hour;
                                hourlyCounter++;
                                dailyFlag = true;
                            }
                            else//other hours
                            {
                                if (hourlySum[hourlyCounter - 1, 0] == hour) //Bi onceki kaitla ayni saat ise
                                {
                                    hourlySum[hourlyCounter - 1, 1] = (Convert.ToDecimal(hourlySum[hourlyCounter - 1, 1], culture) + Convert.ToDecimal(sum, culture)).ToString();
                                    //    UserLog.WConsole("hourly sum :" + hourlySum[hourlyCounter-1, 1]);
                                }
                                else // bi onceki kayit ile farkli saatler ise
                                {
                                    hourlySum[hourlyCounter, 1] = sum.ToString();
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
                UserLog.WConsole("Number of Hour Records for Today : " + hourlyCounter.ToString());
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
                            chartDaily.Series["Saatlik Kazanc"].Points.AddXY(hourlySum[n, 0], hourlySum[n, 1].Replace(",", "."));
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
            catch
            {
                UserLog.WConsole("(!) XML Dosyasini Bulunamadi !");
            }
        }

        private void bSendMailReport_Click(object sender, EventArgs e)
        {
            if(Settings.useMail==true)
            { 
                if (tbMail.Text != ""  && tbMail.Text.Contains("@"))
                {
                    bool result = sendMail();
                    if (!result)
                    {
                        MessageBox.Show("Mail gonderilirken sorun olustu !\nMail gonderimi basarisiz !");
                    }
                    else
                    {
                        MessageBox.Show("Mail Basariyla :\n" + tbMail.Text + "\nMail adresine gonderildi.");
                    }
                }
                else
                {
                    MessageBox.Show("Lutfen Ayarlar sekmesindeki mail adresi alanina gecerli bir mail adresi giriniz !");
                }
            }
            else
            {
                MessageBox.Show("Lutfen Ayarlar sekmesindeki mail adresi alanina gecerli bi mail adresi giriniz !!");
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
                bAyarlarDuzenle.Image = Properties.Resources.switch_on;
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
                ExchangeValuesChanged(null, null);
                bAyarlarDuzenle.ForeColor = Color.Red;
                bAyarlarDuzenle.Image = Properties.Resources.switch_off;
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
            if (tbMail.Text.Contains("@") == true && tbMail.Text.Contains(".") == true)
            {
                Settings.useMail = true;
            }
            else
            {
                Settings.useMail = false;
            }
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
            if (Settings.useMail = true)
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
                    msg.From = new MailAddress("bebckho@gmail.com", "Restorium Services");
                    //msg.To.Add("hozmen6024@gmail.com");
                    //msg.To.Add("bilalertkn@gmail.com");
                    //msg.To.Add("Mahone0619@gmail.com");
                    //msg.To.Add("burak.c.kocak@gmail.com");

                    msg.To.Add(tbMail.Text);
                    msg.Subject = "Restorium Gunluk Kasa Raporu : " + DateTime.Now.ToString();
                    msg.Body = "Gunluk Kasa Raporu :" + System.DateTime.Now.ToLongDateString() + "   " + System.DateTime.Now.ToLocalTime().ToLongTimeString();
                    msg.Body += "\n************************************************************";
                    msg.Body += "\nBugun Kasa Toplam : " + lBugunToplam.Text.Replace(" ₺"," TL")+"\n";
                    msg.Body += "\nGenel Kasa Toplam : " + lKasaToplam.Text.Replace(" ₺", " TL");
                    SmtpClient client = new SmtpClient();
                    //client.Host = "smtp.live.com";
                    msg.Attachments.Add(new Attachment("Daily_Chart.png"));
                    msg.Attachments.Add(new Attachment("Weekly_Chart.png"));
                    msg.Attachments.Add(new Attachment("Kasa.png"));
                    //client.Port = 25;
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("bebckho@gmail.com", "bilalburakhuseyin");
                    //client.Credentials = new NetworkCredential("bbhmbbhm@outlook.com", "bilalburakhuseyinmahmut1");
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
            else
            { 
            return false;
            }
        }

        private void bSendMailKasa_Click(object sender, EventArgs e)
        {
            if (Settings.useMail == true)
            {
                //KASAYI MAIL OLARAK GONDER
            }
            else
            {
                MessageBox.Show("Lutfen Ayarlar sekmesindeki mail adresi alanina gecerli bir mail adresi giriniz !");
            }
        }

        private void kasadanTipDusToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Kasa.islemAdi = "Kasadan Tip Al";
            Kasa.totalTip =Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture);
            using (var kasa= new Kasa())
            {
                var result =kasa.ShowDialog();
                if (result == DialogResult.OK)
                {
                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) - Kasa.tutar).ToString()+ " ₺";
                    dgvKasa.Rows.Add(DateTime.UtcNow.ToLocalTime().ToLongDateString() + " " + DateTime.UtcNow.ToLocalTime().ToLongTimeString(), "Kasadan Tip Dusme", "-", Kasa.Personel, "-", "0₺", "0₺", "0₺", Kasa.tutar+ "₺", "0 %");
                    dgvKasa.Refresh();
                    SaveDataToXml("Kasadan Tip Dusme",Kasa.aciklama,Kasa.tutar.ToString(),"0","0",Kasa.tutar.ToString(), Kasa.Personel,"-", "₺");
                }
            }
        }

        private void bKayitEkle_Click(object sender, EventArgs e)
        {
            using (var contactAdd = new ContactAdd())
            {
                var result = contactAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgvRehber.Rows.Add(ContactAdd.Column0, ContactAdd.Column1, ContactAdd.Column2, ContactAdd.Column3, ContactAdd.Column4, ContactAdd.Column5, ContactAdd.Column6, ContactAdd.Column7, ContactAdd.Column8);
                    dgvRehber.Refresh();
                    try
                    {
                        INI.Write("NoOfRecords", dgvRehber.Rows.Count.ToString(), "ContactList");
                        for (int i = 0; i < dgvRehber.Rows.Count; i++)
                        {
                            INI.Write("Sat" + i + "Sut0", dgvRehber.Rows[i].Cells[0].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut1", dgvRehber.Rows[i].Cells[1].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut2", dgvRehber.Rows[i].Cells[2].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut3", dgvRehber.Rows[i].Cells[3].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut4", dgvRehber.Rows[i].Cells[4].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut5", dgvRehber.Rows[i].Cells[5].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut6", dgvRehber.Rows[i].Cells[6].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut7", dgvRehber.Rows[i].Cells[7].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut8", dgvRehber.Rows[i].Cells[8].Value.ToString(), "ContactList");
                        }
                    }
                    catch
                    {
                        UserLog.WConsole("(!) Rehber Kaydedilirken Hata Olustu !");
                    }
                }
            }
        }

        private void bKayitSil_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvRehber.SelectedRows[0].Index;
            DialogResult dialogResult = MessageBox.Show(dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[0].Value + "\n" + dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[1].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[2].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[3].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[4].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[5].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[6].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[7].Value + "\n"+ dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[8].Value + "\n" ,"Kaydi Sil ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dgvRehber.Rows.RemoveAt(this.dgvRehber.SelectedRows[0].Index);
                dgvRehber.Refresh();
                try
                {
                    for (int i = 0; i < Convert.ToInt16(INI.Read("NoOfRecords", "ContactList")); i++)
                    {
                        INI.DeleteKey("Sat" + i + "Sut0", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut1", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut2", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut3", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut4", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut5", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut6", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut7", "ContactList");
                        INI.DeleteKey("Sat" + i + "Sut8", "ContactList");
                    }
                    UserLog.WConsole("Index : "+rowIndex.ToString()+"de Yer Alan Kayit Rehberden Silindi");
                }
                catch
                {
                    UserLog.WConsole("(!) Rehberden Kayit Silinirken Hata Olustu !");
                }
                ///////////
                // SAVE
                ///////////
                try
                {
                    INI.Write("NoOfRecords", dgvRehber.Rows.Count.ToString(), "ContactList");
                    for (int i = 0; i < dgvRehber.Rows.Count; i++)
                    {
                        INI.Write("Sat" + i + "Sut0", dgvRehber.Rows[i].Cells[0].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut1", dgvRehber.Rows[i].Cells[1].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut2", dgvRehber.Rows[i].Cells[2].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut3", dgvRehber.Rows[i].Cells[3].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut4", dgvRehber.Rows[i].Cells[4].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut5", dgvRehber.Rows[i].Cells[5].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut6", dgvRehber.Rows[i].Cells[6].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut7", dgvRehber.Rows[i].Cells[7].Value.ToString(), "ContactList");
                        INI.Write("Sat" + i + "Sut8", dgvRehber.Rows[i].Cells[8].Value.ToString(), "ContactList");
                    }
                    UserLog.WConsole("Rehber Basariyla Kaydedildi !");
                }
                catch
                {
                    UserLog.WConsole("(!) Rehber Kaydedilirken Hata Olustu !");
                }


            }
            }

        private void bDuzenleRehber_Click(object sender, EventArgs e)
        {
            /*
            if (rehberDuzenleFlag == false)
            {//duzenleme modu on
                rehberDuzenleFlag = true;//Flag
                dgvRehber.AllowUserToDeleteRows = true;
                dgvRehber.AllowUserToOrderColumns = true;
                dgvRehber.ReadOnly = false;
                dgvRehber.Refresh();
                bDuzenleRehber.BackColor = Color.YellowGreen;
                dgvRehber.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            else
            {//duzenleme modu off
                rehberDuzenleFlag = false;//Flag
                dgvRehber.AllowUserToDeleteRows = false;
                dgvRehber.AllowUserToOrderColumns = false;
                dgvRehber.AllowDrop = false;
                dgvRehber.ReadOnly = true;
                dgvRehber.Refresh();
                bDuzenleRehber.BackColor = Color.LightSlateGray;
                dgvRehber.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                */
            using (var contactAdd = new ContactAdd())
            {
                ContactAdd.Column0 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[0].Value.ToString();
                ContactAdd.Column1 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[1].Value.ToString();
                ContactAdd.Column2 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[2].Value.ToString();
                ContactAdd.Column3 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[3].Value.ToString();
                ContactAdd.Column4 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[4].Value.ToString();
                ContactAdd.Column5 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[5].Value.ToString();
                ContactAdd.Column6 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[6].Value.ToString();
                ContactAdd.Column7 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[7].Value.ToString();
                ContactAdd.Column8 = dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[8].Value.ToString();
                ContactAdd.editMode = true;
                var result = contactAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[0].Value = ContactAdd.Column0;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[1].Value = ContactAdd.Column1;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[2].Value = ContactAdd.Column2;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[3].Value = ContactAdd.Column3;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[4].Value = ContactAdd.Column4;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[5].Value = ContactAdd.Column5;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[6].Value = ContactAdd.Column6;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[7].Value = ContactAdd.Column7;
                    dgvRehber.Rows[dgvRehber.SelectedRows[0].Index].Cells[8].Value = ContactAdd.Column8;
                    try
                    {
                        for (int i = 0; i < Convert.ToInt16(INI.Read("NoOfRecords", "ContactList")); i++)
                        {
                            INI.DeleteKey("Sat" + i + "Sut0", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut1", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut2", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut3", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut4", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut5", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut6", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut7", "ContactList");
                            INI.DeleteKey("Sat" + i + "Sut8", "ContactList");
                        }
                    }
                    catch
                    {
                        UserLog.WConsole("(!) Rehberden Kayit Silinirken Hata Olustu !");
                    }
                    ///////////
                    // SAVE
                    ///////////
                    try
                    {
                        INI.Write("NoOfRecords", dgvRehber.Rows.Count.ToString(), "ContactList");
                        for (int i = 0; i < dgvRehber.Rows.Count; i++)
                        {
                            INI.Write("Sat" + i + "Sut0", dgvRehber.Rows[i].Cells[0].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut1", dgvRehber.Rows[i].Cells[1].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut2", dgvRehber.Rows[i].Cells[2].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut3", dgvRehber.Rows[i].Cells[3].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut4", dgvRehber.Rows[i].Cells[4].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut5", dgvRehber.Rows[i].Cells[5].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut6", dgvRehber.Rows[i].Cells[6].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut7", dgvRehber.Rows[i].Cells[7].Value.ToString(), "ContactList");
                            INI.Write("Sat" + i + "Sut8", dgvRehber.Rows[i].Cells[8].Value.ToString(), "ContactList");
                        }
                        UserLog.WConsole("Rehber Basariyla Kaydedildi !");
                    }
                    catch
                    {
                        UserLog.WConsole("(!) Rehber Kaydedilirken Hata Olustu !");
                    }
                }

            }
            
        }

        private void bStokSil_Click(object sender, EventArgs e)
        {
            if(this.dgView.SelectedRows.Count > 0 )
            { 
            int selectedRowIndex = dgView.SelectedRows[0].Index;
            DialogResult dialogResult = MessageBox.Show("ID : "+dgView.Rows[selectedRowIndex].Cells[0].Value.ToString() + "\n" +"ACIKLAMA : "+ dgView.Rows[selectedRowIndex].Cells[1].Value.ToString() + "\n" + "ADET : "+dgView.Rows[selectedRowIndex].Cells[2].Value.ToString() + " " + dgView.Rows[selectedRowIndex].Cells[3].Value.ToString() + "\n" +"BIRIM FIYAT : "+ dgView.Rows[selectedRowIndex].Cells[4].Value.ToString() + " " + dgView.Rows[selectedRowIndex].Cells[5].Value.ToString(), "Ilgili Urunu Stoktan Silmek Istediginize Emin Misiniz ?", MessageBoxButtons.YesNo);
            UserLog.WConsole("selectedRowIndex : "+selectedRowIndex.ToString());
            if (dialogResult == DialogResult.Yes)
            {
                try {
                    dgView.AllowUserToDeleteRows = true;
                    dgView.ReadOnly = false;
                    dgView.Refresh();
                    dgView.Rows.RemoveAt(dgView.Rows[selectedRowIndex].Index);
                    dgView.Refresh();
                    saveStokToFile();
                    getStokFromFile();

                    //getStokFromFile();
                    MessageBox.Show("Ilgili stok kaydi basariyla silindi !");
                }
                catch
                {
                    MessageBox.Show("(!) Kayit silinirken hata olustu !");
                }
                dgView.AllowUserToDeleteRows = true;
                dgView.ReadOnly = true;
                dgView.Refresh();
            }
            }
        }

        private void StokSearchBoxTextChanged(object sender, EventArgs e)
        {
            if (tbSearchKey.Text == "")
            {
                getStokFromFile();
                searchModeOnStok = false;
                countSearchIndexList = 0;
                //Thread thread = new Thread(new ThreadStart(getStokFromFile));
                //thread.Start();
            }
        }

        private void bDeletePersonel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[0].Value + "\n" + dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[1].Value + "\n" + dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[2].Value + "\n" + dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[3].Value + "\n" + dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[4].Value + "\n" + dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[5].Value + "\n" + dgViewWaiter.Rows[dgViewWaiter.SelectedRows[0].Index].Cells[6].Value  + "\n", "Ilgili personel kaydini silmek istediginizden emin misiniz ? ", MessageBoxButtons.YesNo);
            //
            if (dialogResult == DialogResult.Yes)
            {
                dgViewWaiter.Rows.RemoveAt(dgViewWaiter.SelectedRows[0].Index);
                savePersonnelToFile();
            }
        }

        private void bPersonelEkle_Click(object sender, EventArgs e)
        {
            using (var personelAdd = new PersonelAdd())
            {
                var result = personelAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgViewWaiter.Rows.Add(PersonelAdd.PersonelColumn0, PersonelAdd.PersonelColumn1, PersonelAdd.PersonelColumn2, PersonelAdd.PersonelColumn3, PersonelAdd.PersonelColumn4, PersonelAdd.PersonelColumn5, PersonelAdd.PersonelColumn6);
                    dgViewWaiter.Refresh();
                    try
                    {
                        savePersonnelToFile();
                    }
                    catch
                    {
                        UserLog.WConsole("(!) Rehber Kaydedilirken Hata Olustu !");
                    }
                }
            }
        }

        private void pbWifi_Click(object sender, EventArgs e)
        {
            MainDisplay md = new MainDisplay();
            md.Show();
        }

        private void bPersonelSettings_Click(object sender, EventArgs e)
        {
            using (var U_Settings = new UserSettings())
            {
                UserSettings.userType = "User";
                var result = U_Settings.ShowDialog();
                if (result == DialogResult.OK)
                {
                    UserLog.WConsole("Admin Ayarlari Basariyla Degistirildi!");
                }
            }
        }

        private void bAdminSettings_Click(object sender, EventArgs e)
        {
            using (var U_Settings = new UserSettings())
            {
                UserSettings.userType = "Admin";
                var result = U_Settings.ShowDialog();
                if (result == DialogResult.OK)
                {
                    UserLog.WConsole("Admin Ayarlari Basariyla Degistirildi!");
                }
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {

        }

        private void GetKasaFromXML()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                //UserLog.WConsole("Kasa Bugun Toplam : " + lBugunToplam.Text + " Ile Acildi !");
                doc.Load("Resources/ReportDatabase.xml");
                XmlNodeList IslemList = doc.GetElementsByTagName("Islem");
                decimal Nakit, Kredi, cari, toplam;
                //////////////////////////////////////////////////////////
                foreach (XmlNode node in IslemList)
                {
                    XmlElement IslemElement = (XmlElement)node;
                    ////////////////////// MASA KAPAMA //////////////////////////////////////////
                    if (IslemElement.GetElementsByTagName("IslemTuru")[0].InnerText == "Masa Kapama")
                    {
                        Nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                        Kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                        cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                  //      toplam = Convert.ToDecimal(IslemElement.GetElementsByTagName("Toplam")[0].InnerText.Replace(",", "."));
                        switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                        {
                            case " €":
                                Nakit = Nakit / LastChoosenTable.DefinedEuro;
                                Kredi = Kredi / LastChoosenTable.DefinedEuro;
                                cari = cari / LastChoosenTable.DefinedEuro;
                //                toplam = toplam / LastChoosenTable.DefinedEuro;
                                break;
                            case " $":
                                Nakit = Nakit / LastChoosenTable.DefinedDolar;
                                Kredi = Kredi / LastChoosenTable.DefinedDolar;
                                cari = cari / LastChoosenTable.DefinedDolar;
               //                 toplam = toplam / LastChoosenTable.DefinedDolar;
                                break;
                            case " £":
                                Nakit = Nakit / LastChoosenTable.DefinedGBP;
                                Kredi = Kredi / LastChoosenTable.DefinedGBP;
                                cari = cari / LastChoosenTable.DefinedGBP;
             //                   toplam = toplam / LastChoosenTable.DefinedGBP;
                                break;
                        }
                        decimal toplamKasa = Nakit + Kredi;
                        toplamKasa = System.Math.Round(Convert.ToDecimal(toplamKasa, culture), 2);
                        //UserLog.WConsole("toplamKasa: " + toplamKasa + "TL Ile Acildi !");
                        if (System.DateTime.Now.ToLongDateString() == IslemElement.GetElementsByTagName("Tarih")[0].InnerText) /////////////TODAY
                        {
                            ////KASAYA YAZDIR
                            string tarih = IslemElement.GetElementsByTagName("Tarih")[0].InnerText;
                            tarih = tarih + " " + IslemElement.GetElementsByTagName("Zaman")[0].InnerText;
                            switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                            {
                                case " ₺":
                                    decimal tl_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText,culture);
                                    decimal tl_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                    decimal tl_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                            //        decimal tl_toplam_xml = Convert.ToDecimal(IslemElement.GetElementsByTagName("Toplam")[0].InnerText.Replace(",", "."));
                                    decimal tl_toplam = tl_nakit + tl_kredi + tl_cari;
                                    dgvKasa.Rows.Add(tarih, "Masa Kapama", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, tl_nakit + " ₺", tl_kredi + " ₺", tl_cari + " ₺", tl_toplam + " ₺", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                    break;
                                case " €":
                                    decimal euro_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                    decimal euro_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                    decimal euro_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                          //          decimal euro_toplam_xml = Convert.ToDecimal(IslemElement.GetElementsByTagName("Toplam")[0].InnerText.Replace(",", "."));
                                    decimal euro_toplam = euro_nakit + euro_kredi + euro_cari;
                                    dgvKasa.Rows.Add(tarih, "Masa Kapama", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, euro_nakit + " €", euro_kredi + " €", euro_cari + " €", euro_toplam + " €", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                    break;
                                case " $":
                                    decimal dolar_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                    decimal dolar_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                    decimal dolar_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                          //          decimal dolar_toplam_xml = Convert.ToDecimal(IslemElement.GetElementsByTagName("Toplam")[0].InnerText.Replace(",", "."));
                                    decimal dolar_toplam = dolar_nakit + dolar_kredi + dolar_cari;
                                    dgvKasa.Rows.Add(tarih, "Masa Kapama", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, dolar_nakit + " $", dolar_kredi + " $", dolar_cari + " $", dolar_toplam + " $", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                    break;
                                case " £":
                                    decimal gbp_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                    decimal gbp_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                    decimal gbp_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                       //             decimal gbp_toplam_xml = Convert.ToDecimal(IslemElement.GetElementsByTagName("Toplam")[0].InnerText.Replace(",", "."));
                                    decimal gbp_toplam = gbp_nakit + gbp_kredi + gbp_cari;
                                    dgvKasa.Rows.Add(tarih, "Masa Kapama", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, gbp_nakit + " £", gbp_kredi + " £", gbp_cari + " £", gbp_toplam + " £", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                    break;
                            }
                            ///KASAYA YAZDIR - END
                            lNakitToplamTL.Text = (Convert.ToDecimal(lNakitToplamTL.Text.Replace(" ₺", ""), culture) + Nakit).ToString() + " ₺";
                            lKrediToplamTL.Text = (Convert.ToDecimal(lKrediToplamTL.Text.Replace(" ₺", ""), culture) + Kredi).ToString() + " ₺";
                            lCariToplamTL.Text = (Convert.ToDecimal(lCariToplamTL.Text.Replace(" ₺", ""), culture) + cari).ToString() + " ₺";
                            lBugunToplam.Text = (Convert.ToDecimal(lBugunToplam.Text.Replace(" ₺", ""), culture) + toplamKasa).ToString() + " ₺";

                        }
                        OldKasaToplam = System.Math.Round(Convert.ToDecimal(OldKasaToplam, culture), 2);
                        //UserLog.WConsole("Kasa Genel Toplam : " + OldKasaToplam + "TL Ile Acildi !");
                        OldKasaToplam += toplamKasa;  // i,1 => Tutar
                    }
                    ////////////////////// TIP //////////////////////////////////////////
                    else if (IslemElement.GetElementsByTagName("IslemTuru")[0].InnerText == "Tip")
                    {

                        string tarih = IslemElement.GetElementsByTagName("Tarih")[0].InnerText;
                        tarih = tarih + " " + IslemElement.GetElementsByTagName("Zaman")[0].InnerText;
                        switch (IslemElement.GetElementsByTagName("ParaBirimi")[0].InnerText.ToString())
                        {
                            case " ₺":
                                decimal tl_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                decimal tl_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                decimal tl_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                                decimal tl_toplam = tl_nakit + tl_kredi + tl_cari;
                                if (System.DateTime.Now.ToLongDateString() == IslemElement.GetElementsByTagName("Tarih")[0].InnerText)
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + tl_nakit).ToString() + " ₺";
                                    dgvKasa.Rows.Add(tarih, "Tip", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, "0" + " ₺", "0" + " ₺", "0" + " ₺", tl_toplam + " ₺", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                }
                                else
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + tl_nakit).ToString() + " ₺";
                                }
                                break;
                            case " €":
                                decimal euro_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                decimal euro_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                decimal euro_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                                decimal euro_toplam = euro_nakit + euro_kredi + euro_cari;
                                if (System.DateTime.Now.ToLongDateString() == IslemElement.GetElementsByTagName("Tarih")[0].InnerText)
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + (euro_nakit / LastChoosenTable.DefinedEuro)).ToString() + " ₺";
                                    dgvKasa.Rows.Add(tarih, "Tip", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, "0" + " €", "0" + " €", "0" + " €", euro_toplam + " €", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                }
                                else
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + (euro_nakit / LastChoosenTable.DefinedEuro)).ToString() + " ₺";
                                }
                                break;
                            case " $":
                                decimal dolar_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                decimal dolar_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                decimal dolar_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                                decimal dolar_toplam = dolar_nakit + dolar_kredi + dolar_cari;
                                if (System.DateTime.Now.ToLongDateString() == IslemElement.GetElementsByTagName("Tarih")[0].InnerText)
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + (dolar_nakit / LastChoosenTable.DefinedDolar)).ToString() + " ₺";
                                    dgvKasa.Rows.Add(tarih, "Tip", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, "0" + " $", "0" + " $", "0" + " $", dolar_toplam + " $", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                }
                                else
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + (dolar_nakit / LastChoosenTable.DefinedDolar)).ToString() + " ₺";
                                }
                                break;
                            case " £":
                                decimal gbp_nakit = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                                decimal gbp_kredi = Convert.ToDecimal(IslemElement.GetElementsByTagName("KrediKarti")[0].InnerText, culture);
                                decimal gbp_cari = Convert.ToDecimal(IslemElement.GetElementsByTagName("Cari")[0].InnerText, culture);
                                decimal gbp_toplam = gbp_nakit + gbp_kredi + gbp_cari;
                                if (System.DateTime.Now.ToLongDateString() == IslemElement.GetElementsByTagName("Tarih")[0].InnerText)
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + (gbp_nakit / LastChoosenTable.DefinedGBP)).ToString() + " ₺";
                                    dgvKasa.Rows.Add(tarih, "Tip", IslemElement.GetElementsByTagName("MasaAdi")[0].InnerText, IslemElement.GetElementsByTagName("Personel")[0].InnerText, IslemElement.GetElementsByTagName("Musteri")[0].InnerText, "0" + " £", "0" + " £", "0" + " £", gbp_toplam + " £", IslemElement.GetElementsByTagName("Iskonto")[0].InnerText + "%");
                                }
                                else
                                {
                                    lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) + (gbp_nakit / LastChoosenTable.DefinedGBP)).ToString() + " ₺";
                                }
                                break;
                        }

                    }
                    ////////////////////// KASADAN TIP DUSME//////////////////////////////////////////
                    else if (IslemElement.GetElementsByTagName("IslemTuru")[0].InnerText == "Kasadan Tip Dusme")
                    {
                        string tarih = IslemElement.GetElementsByTagName("Tarih")[0].InnerText;
                        tarih = tarih + " " + IslemElement.GetElementsByTagName("Zaman")[0].InnerText;
                        decimal KasadanTipDusTutar = Convert.ToDecimal(IslemElement.GetElementsByTagName("Nakit")[0].InnerText, culture);
                        if (System.DateTime.Now.ToLongDateString() == IslemElement.GetElementsByTagName("Tarih")[0].InnerText)
                        {
                            dgvKasa.Rows.Add(tarih, "Kasadan Tip Dusme", "-", IslemElement.GetElementsByTagName("Personel")[0].InnerText, "-", "0₺", "0₺", "0₺", -KasadanTipDusTutar+ "₺", "0 %");
                            dgvKasa.Refresh();
                            OldKasaToplam = OldKasaToplam - KasadanTipDusTutar;
                            lBugunToplam.Text = (Convert.ToDecimal(lBugunToplam.Text.Replace(" ₺", ""), culture) - KasadanTipDusTutar).ToString() + " ₺";
                            lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) - KasadanTipDusTutar).ToString()+ " ₺";
                        }
                        else
                        {
                            OldKasaToplam = OldKasaToplam - KasadanTipDusTutar;
                            lTipToplam.Text = (Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture) - KasadanTipDusTutar).ToString() + " ₺";
                        }
                    }
                    ////////////////////// REZERVASYON IPTAL//////////////////////////////////////////
                    else if (IslemElement.GetElementsByTagName("IslemTuru")[0].InnerText == "Rezervasyon Iptal")
                    {

                    }
                 }
                ////// END OF FOREACH (NODE)
                OldKasaToplam += System.Math.Round(Convert.ToDecimal(lTipToplam.Text.Replace(" ₺", ""), culture),2);

            }
            catch
            {
                UserLog.WConsole("(!) XML den Kasa Okunurken Sorun Olustu !");
            }
            finally
            {
                UserLog.WConsole("::::::: Kasa Okuma Tamamlandi :::::::");
                UserLog.WConsole("Kasa Genel Toplam : " + OldKasaToplam + "TL Ile Acildi !");
                UserLog.WConsole("Kasa Bugun Toplam : " + lBugunToplam.Text + " Ile Acildi !");
                lKasaToplam.Text = OldKasaToplam + " ₺";
            }
           }

        private void bTableDetailChange_Click(object sender, EventArgs e)
        {
            TableOpenForm.tableSettingsChange = true;
            string tableName = lMasaNo.Text;
            string tableCount, todaysCount;
            int i = 0;
            foreach (string tablenames in tableNumbers)
            {
                //UserLog.WConsole(tablenames);
                //UserLog.WConsole(emptyTableList[i].ToString());
                if (tableNumbers[i] == tableName)
                {
                    using (var tableOpenForm = new TableOpenForm())
                    {
                        //LastChoosenTable.reservation = false;
                        var result = tableOpenForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            tableNumbers[i] = LastChoosenTable.TableNumber;
                            tableDetails[i * 3, 0] = LastChoosenTable.TableNumber;
                            tableDetails[i * 3, 3] = LastChoosenTable.Waiter;
                            tableDetails[i * 3, 5] = LastChoosenTable.iskonto.ToString();
                            tableDetails[i * 3, 6] = LastChoosenTable.musteriAdi;
                            tableName = "bLeft" + tableName;
                            tableLayoutPanel1.Controls.RemoveByKey(tableName);
                            ///////////////
                            tableName = "bLeft" + LastChoosenTable.TableNumber;
                            Button bLeft = new Button();
                            bLeft.Name = "bLeft" + LastChoosenTable.TableNumber;
                            bLeft.Text = LastChoosenTable.TableNumber.ToString() + "\n(" + tableDetails[i*3,1] + ")";
                            bLeft.AutoSize = true;
                            if (tableDetails[i * 3, 7] == "N")
                            {
                                bLeft.ForeColor = Color.Black;
                                bLeft.BackColor = Color.Red;
                                lMasaNo.ForeColor = Color.Red;
                            }
                            else
                            {
                                bLeft.ForeColor = Color.Black;
                                bLeft.BackColor = Color.Turquoise;
                                lMasaNo.ForeColor = Color.Turquoise;
                            }

                            bLeft.Size = new Size(80, 80);
                            bLeft.MouseDown += new MouseEventHandler(masa_click);
                            bLeft.MouseDoubleClick += new MouseEventHandler(masa_MouseDoubleClick);
                            lMasaNo.Text = tableDetails[i * 3, 0].ToString();
                            string text = lMasaNo.Text;
                            switch (text.Length)
                            {
                                case 1:
                                case 2:
                                case 3:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 42f, lMasaNo.Font.Style);
                                    break;
                                case 4:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 37f, lMasaNo.Font.Style);
                                    break;
                                case 5:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 35f, lMasaNo.Font.Style);
                                    break;
                                case 6:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 30f, lMasaNo.Font.Style);
                                    break;
                                case 7:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 25f, lMasaNo.Font.Style);
                                    break;
                                case 8:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 22f, lMasaNo.Font.Style);
                                    break;
                                case 9:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 20f, lMasaNo.Font.Style);
                                    break;
                                case 10:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 18f, lMasaNo.Font.Style);
                                    break;
                                case 11:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 16f, lMasaNo.Font.Style);
                                    break;
                                case 12:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 15f, lMasaNo.Font.Style);
                                    break;
                                default:
                                    lMasaNo.Font = new Font(lMasaNo.Font.FontFamily, 8f, lMasaNo.Font.Style);
                                    break;

                            }
                            lPersonel.Text = "Personel : " + tableDetails[i * 3, 3];
                            lIskonto.Text = "Iskonto Orani : " + tableDetails[i* 3, 5] + "%";
                            lMusteriAdi.Text = "Müşteri : " + tableDetails[i* 3, 6];
                            ////
                            tableLayoutPanel1.Controls.Add(bLeft, (findTableOrder(LastChoosenTable.TableNumber) % 5), (findTableOrder(LastChoosenTable.TableNumber) / 5));
                            ////
                            UserLog.WConsole("Masanin Adi : \"" + LastChoosenTable.TableNumber.ToString() + "\"");
                            lTableCounter.Text = "Bugun acilan " + tableDetails[i * 3, 1] + ". masa";
                            /////////////
                            saveAdisyonToTable();
                        }
                    }
                           
                }
                i++;
            }
            TableOpenForm.tableSettingsChange = false;
        }
        //AYARLAR +
        //Dukkan kapanma saati ve gonderilecek maili ekle+
        //Iskonto uygulanmiyor
        //Bugunun kasa toplami ve bugune kadar ki kasa toplam ayri olarak gosterilmeli
        //Kasadan para alindiginda kasaya (-) miktar girilebilmeli
        //Masaya siparis eklemek istendiginde acilan sayfadaki "iptal" tusu calismiyor
        //Program acildiginda gun bitimi gerceklesmediyse kaydedilen satislari kasaya yazdir
        //Masa tasima
        //Masa icerigini degistirme
        /////
        //Android unity : find the person in a crowded place (idea)
        //Android unity : daga tirmanma yarisi . en hizli olan kazanir
        //How to videos 
        //Android tiklama yarisi oyunu
        //*********************
        //Rehber - Kayit duzenle (edit)
    }
}
