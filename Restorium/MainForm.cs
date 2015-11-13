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
        IniFile INI = new IniFile();
        private bool duzenlemeModu = false;
        int countOfTables = 1;
        public MainForm()
        {
            InitializeComponent();
            StokDataSet();
        }

        private void bYeniMasa_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tp_Stok);
            UserLog.WConsole("-Deneme-");
            string Ad = INI.Read("NameTag","Users");
            UserLog.WConsole(Ad);
        }

        private void bMasaTasi_Click(object sender, EventArgs e)
        {
            UserLog.UserName = "BBH";
            INI.Write("NameTag", "BBH", "Users");
            INI.Write("NameTag", "Burak", "Users");
            INI.Write("NameTag2", "BBH", "Users");
            INI.Write("NameTag3", "BBH-Restorium", "BBH-Adminsss");
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

     
        private void MainForm_Minimize(object sender, FormClosingEventArgs e)
        {
            this.MinimizeBox = true;
        }

        #region STOK PROCESSES
        private void StokDataSet()
        {
            //
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));        //  COLUMN 1
            dataTable.Columns.Add("Aciklama", typeof(string));  //  COLUMN 2
            dataTable.Columns.Add("Adet", typeof(int));         //  COLUMN 3
            dgView.DataSource = dataTable;

            DataGridViewComboBoxColumn dgvComboBoxColumn = new DataGridViewComboBoxColumn();    //  COLUMN 4
            dgvComboBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvComboBoxColumn.HeaderText = "Birim";
            dgvComboBoxColumn.Items.IndexOf("Birim");
            dgvComboBoxColumn.Items.Add("Birim");
            dgvComboBoxColumn.Items.Add("Porsiyon");
            dgvComboBoxColumn.Items.Add("Kutu");
            dgvComboBoxColumn.Items.Add("Kg");
            dgvComboBoxColumn.Items.Add("Kasa");
            dgvComboBoxColumn.Items.Add("Kucuk");
            dgvComboBoxColumn.Items.Add("Buyuk");
            dgView.Columns.Add(dgvComboBoxColumn);      

            dataTable.Columns.Add("Birim Fiyat", typeof(string));   //  COLUMN 5
            DataGridViewComboBoxColumn dgvComboBoxColumnFiyat = new DataGridViewComboBoxColumn();   //  COLUMN 6
            dgvComboBoxColumnFiyat.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvComboBoxColumnFiyat.HeaderText = "Para Birimi";
            dgvComboBoxColumnFiyat.Items.IndexOf("TL");
            dgvComboBoxColumnFiyat.Items.Add("TL");
            dgvComboBoxColumnFiyat.Items.Add("Dolar");
            dgvComboBoxColumnFiyat.Items.Add("Euro");
            dgvComboBoxColumnFiyat.Items.Add("GBP");
            dgView.Columns.Add(dgvComboBoxColumnFiyat);

            DataGridViewCheckBoxColumn dgvCheckBoxColumn = new DataGridViewCheckBoxColumn(); //  COLUMN 7
            dgvCheckBoxColumn.HeaderText = "Dinamik Stok Kontrolu";
            dgView.Columns.Add(dgvCheckBoxColumn);
            //
            this.dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void bDuzenle_Click(object sender, EventArgs e)
        {
            duzenlemeModu = !duzenlemeModu;
            if (duzenlemeModu == true)
            {
                dgView.AllowUserToAddRows = true;
                dgView.AllowUserToDeleteRows = true;
                dgView.AllowUserToOrderColumns = true;
                dgView.AllowDrop = true;
                dgView.ReadOnly = false;
                dgView.Refresh();
                bDuzenle.BackColor = Color.Green;
                bDuzenle.Text = "Duzenle(Acik)";
            }
            else
            {
                dgView.AllowUserToAddRows = false;
                dgView.AllowUserToDeleteRows = false;
                dgView.AllowUserToOrderColumns = false;
                dgView.AllowDrop = false;
                dgView.ReadOnly = true;
                dgView.Refresh();
                bDuzenle.BackColor = Color.Silver;
                bDuzenle.Text = "Duzenle(Kapali)";
            }
        }

        private void bStokAra_Click(object sender, EventArgs e)
        {
            //tbSearchKey'de yazani al ona gore DGView'de arama yap.
        }

        private void SaveToDatabase()
        {
            //Save DataGridView to DataBase
        }

        #endregion

        private void bRezervasyon_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel1.Controls.Clear();
            /*
            Button b = new Button();
            b.Name = countOfTables.ToString();
            b.Text = countOfTables.ToString();
            b.AutoSize = true;
            b.ForeColor = Color.Red;
            //flowLayoutPanel2.Controls.Add(b);
            //
            Label label = new Label();
            label.Text = "Masa No : " + countOfTables.ToString() + " | " + " Tutar = " + (countOfTables * 20).ToString() + "TL";
            //label.Text = "Masa No : ";
            label.Name = "LABEL" + countOfTables.ToString();
            label.AutoSize = true;
            label.BackColor = Color.LightGray;
            label.ForeColor = Color.DarkBlue;
            //flowLayoutPanel2.Controls.Add(label);
            //
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Name = "flowLayoutPanel" + countOfTables.ToString();
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.Controls.Add(b);
            flowLayoutPanel.Controls.Add(label);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel);
            //
            Label label2 = new Label();
            label2.Text = "__________________________________________________";
            label2.Name = "Split" + countOfTables.ToString();
            label2.AutoSize = true;
            label2.BackColor = Color.Gray;
            label2.ForeColor = Color.Gold;
            flowLayoutPanel2.Controls.Add(label2);
            */
            countOfTables++;

        }

        
    }
}
