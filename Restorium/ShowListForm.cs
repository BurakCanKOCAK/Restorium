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
    public partial class ShowListForm : Form
    {
        public string ListName{ get; set; }
        public string Selected_Meal_ID{ get; set; }
        public decimal Selected_Meal_Price {get;  set; }
        public string SelectedWaiter{ get; set; }
        public string Selected_Meal{ get; set; }
        public static string[] PersonelNames =new string[100];

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
                this.Selected_Meal_Price = Convert.ToDecimal(dgViewStok.Rows[index].Cells[4].Value);
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
           
        }

        private void ShowListForm_Load(object sender, EventArgs e)
        {
            IniFile INI = new IniFile();
            ////STOK SECIMI ICIN
            if (ListName == "Stok")
            {
                dgViewWaiter.Visible = false;
                dgViewStok.Visible = true;
                int listCount = Convert.ToInt16(INI.Read("stokCount", "Stok"));
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

                        dgViewStok.Rows.Add(id, aciklama, adet, birim, birimFiyat, paraBirimi, dinamikStokKontrolu);
                        dgViewStok.Refresh();
                    }
                    UserLog.WConsole("Dosyadan Stok Okuma Basarili ! <<ShowListForm.cs>>");

                }
                catch
                {
                    UserLog.WConsole(" (HATA) Dosyadan Stok Okuma Hatasi !  <<ShowListForm.cs>>");
                }
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
        }
    }
}
