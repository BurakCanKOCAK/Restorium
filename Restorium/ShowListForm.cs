using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restorium
{
    public partial class ShowListForm : Form
    {
        public string ListName{ get; set; }
        public string Selected_Meal_ID{ get; set; }
        public decimal Selected_Meal_Price {get;  set; }
        public string SelectedWaiter{ get; set; }
        public string Selected_Meal{ get; set; }
        public static string[] PersonelNames =new string[100];
        IniFile INI = new IniFile();
        CultureInfo culture = new CultureInfo("tr-TR", true);

        public ShowListForm()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (ListName == "Stok")
            {

                this.DialogResult = DialogResult.OK;
                int index = dgViewStok.CurrentRow.Index;
                this.Selected_Meal = dgViewStok.Rows[index].Cells[1].Value.ToString();
                this.Selected_Meal_ID = dgViewStok.Rows[index].Cells[0].Value.ToString();
                this.Selected_Meal_Price = Convert.ToDecimal(dgViewStok.Rows[index].Cells[4].Value, culture);
                this.Close();
            }
            else if(ListName=="Personel")
            {
                int index = dgViewWaiter.CurrentRow.Index;
                this.SelectedWaiter = dgViewWaiter.Rows[index].Cells[1].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tSearchTextChanged(object sender, EventArgs e)
        {
            int rowCount = dgViewStok.RowCount;
            UserLog.WConsole("aaaaaa"+ dgViewStok.RowCount.ToString());
            if (ListName == "Stok")
            {
                if (rowCount > 0)
                {
                    UserLog.WConsole("bbbbb");
                    for (int i = rowCount - 1; i >= 0; i--)
                    {
                            UserLog.WConsole("ccccc");
                            //UserLog.WConsole(dgViewStok.Rows.Count.ToString());
                            try
                            {
                                UserLog.WConsole("ddddd");
                                dgViewStok.Rows.RemoveAt(i);
                                UserLog.WConsole("eeee");
                            }
                        catch
                        {
                            UserLog.WConsole("(!)Stok Silmede Hata(ShowListForm.cs)");
                        }
                    }
                }
                ///////
                int listCount = 0;
                try
                {
                    listCount = Convert.ToInt32(INI.Read("stokCount", "Stok"));
                }
                catch
                {
                    listCount = 0;
                    UserLog.WConsole("(!) Kayitli Stok Urunu Bulunamadi !");
                }
                UserLog.WConsole("sssdasdad");
                UserLog.WConsole("listCount :" + listCount);
                for (int i = 0; i < listCount; i++)
                {
                    
                    UserLog.WConsole("5555555555555"+ i);
                    string id = INI.Read("id" + i.ToString(), "Stok");
                    string aciklama = INI.Read("aciklama" + i.ToString(), "Stok");
                    string adet = INI.Read("adet" + i.ToString(), "Stok");
                    string birim = INI.Read("birim" + i.ToString(), "Stok");
                    string birimFiyat = INI.Read("birimFiyat" + i.ToString(), "Stok");
                    string paraBirimi = INI.Read("paraBirimi" + i.ToString(), "Stok");
                    string dinamikStokKontrolu = INI.Read("dinamikStokKontrolu" + i.ToString(), "Stok");
                    string menuUrunu = INI.Read("menuUrunu" + i, "Stok");
                    if (menuUrunu == "true")
                    {
                        if (id.ToUpper().Contains(tbSearch.Text.ToUpper()) || aciklama.ToUpper().Contains(tbSearch.Text.ToUpper()))
                        {
                            if (dinamikStokKontrolu == "true")
                            {
                                dgViewStok.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, CheckState.Checked);
                            } else
                            {
                                dgViewStok.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, CheckState.Unchecked);
                            }
                            UserLog.WConsole("aAAAAAAAAAAAAAAAAAAAA");
                            dgViewStok.Refresh();
                        }
                        if (dgViewStok.Rows[i].Cells[2].Value.ToString()=="0" || dgViewStok.Rows[i].Cells[2].Value.ToString().Contains("-"))
                        {
                            dgViewStok.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            UserLog.WConsole("RRRRRRRRRRRRRRRRR");
                        }
                        else
                        {
                            dgViewStok.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            UserLog.WConsole("WWWWWWWWWWWWWWWWWWWW");
                        }
                        dgViewStok.Refresh();
                        UserLog.WConsole("FFFFFFFFFF");
                    }
                }
                //////////////

            }
            else
            {
                ///////////////////////////////////
                for (int i = dgViewWaiter.Rows.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        dgViewWaiter.Rows.RemoveAt(i);
                    }
                    catch
                    {
                        UserLog.WConsole("(!)Stok Silmede Hata(ShowListForm.cs)");
                    }
                }
                /////////////
                int listCount = Convert.ToInt16(INI.Read("personelCount", "Personel"));
                try
                {
                    for (int i = 0; i < listCount; i++)
                    {
                        string id = INI.Read("id" + i.ToString(), "Personel");
                        string name = INI.Read("name" + i.ToString(), "Personel");
                        string work = INI.Read("work" + i.ToString(), "Personel");
                        PersonelNames[i] = name;
                        UserLog.WConsole(PersonelNames[i]);
                        if (id.ToUpper().Contains(tbSearch.Text.ToUpper()) || name.ToUpper().Contains(tbSearch.Text.ToUpper()) || work.ToUpper().Contains(tbSearch.Text.ToUpper()))
                        { 
                        dgViewWaiter.Rows.Add(id, name, work);
                        dgViewWaiter.Refresh();
                        }
                    }
                    UserLog.WConsole("Dosyadan Personel Okuma Basarili ! <<ShowListForm.cs>>");

                }
                catch
                {
                    UserLog.WConsole(" (HATA) Dosyadan Personel Okuma Hatasi ! <<ShowListForm.cs>>");
                }
            }
        }

        private void ShowListForm_Load(object sender, EventArgs e)
        {
            
            ////STOK SECIMI ICIN
            if (ListName == "Stok")
            {
                dgViewWaiter.Visible = false;
                dgViewStok.Visible = true;
                int listCount = 0;
                try
                {
                    listCount = Convert.ToInt16(INI.Read("stokCount", "Stok"));
                }
                catch
                {
                    listCount = 0;
                    UserLog.WConsole("(!) Kayitli Stok Urunu Bulunamadi !");
                }
                    try
                {

                    for (int i = 0; i < listCount; i++)
                    {
                        string id = INI.Read("id" + i.ToString(), "Stok");
                        string aciklama = INI.Read("aciklama" + i.ToString(), "Stok");
                        string adet = INI.Read("adet" + i.ToString(), "Stok");
                        string birim = INI.Read("birim" + i.ToString(), "Stok");
                        string birimFiyat = INI.Read("birimFiyat" + i.ToString(), "Stok");
                        string paraBirimi = INI.Read("paraBirimi" + i.ToString(), "Stok");
                        string dinamikStokKontrolu = INI.Read("dinamikStokKontrolu" + i.ToString(), "Stok");
                        string menuUrunu = INI.Read("menuUrunu" + i.ToString(), "Stok");
                        if (menuUrunu == "true")
                        { 
                        dgViewStok.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, dinamikStokKontrolu);
                        dgViewStok.Refresh();
                        }

                    }
                    UserLog.WConsole("Dosyadan Stok Okuma Basarili ! <<ShowListForm.cs>>");

                }
                catch
                {
                    UserLog.WConsole(" (HATA) Dosyadan Stok Okuma Hatasi !  <<ShowListForm.cs>>");
                }
                for (int i = 0; i < dgViewStok.Rows.Count; i++)
                {
                    if (dgViewStok.Rows[i].Cells[2].Value.ToString()=="0" || dgViewStok.Rows[i].Cells[2].Value.ToString().Contains("-"))
                    {    
                        dgViewStok.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        dgViewStok.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                dgViewStok.Refresh();
            }
            else
            {
                ////PERSONEL SECIMI ICIN
                dgViewWaiter.Visible = true;
                dgViewStok.Visible = false;
                int listCount = Convert.ToInt16(INI.Read("personelCount", "Personel"));
                try
                {
                    for (int i = 0; i < listCount; i++)
                    {
                        string id = INI.Read("id" + i.ToString(), "Personel");
                        string name = INI.Read("name" + i.ToString(), "Personel");
                        string work = INI.Read("work" + i.ToString(), "Personel");
                        PersonelNames[i] = name;
                        UserLog.WConsole(PersonelNames[i]);
                        dgViewWaiter.Rows.Add(id, name, work);
                        dgViewWaiter.Refresh();
                    }
                    UserLog.WConsole("Dosyadan Personel Okuma Basarili ! <<ShowListForm.cs>>");
                    
                }
                catch
                {
                    UserLog.WConsole(" (HATA) Dosyadan Personel Okuma Hatasi ! <<ShowListForm.cs>>");
                }
            }

        }

        private void key_press(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bOK.Focus();
            }
        }
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
