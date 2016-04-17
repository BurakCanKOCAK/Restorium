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
    public partial class Kasa : Form
    {
        public static string Personel;
        public static string aciklama;
        public static string islemAdi;
        public static decimal tutar;
        public static decimal totalTip;
        CultureInfo culture = new CultureInfo("tr-TR", true);

        public Kasa()
        {
            InitializeComponent();
        }

        private void Kasa_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < MainForm.personelCount; i++)
            {
                cbPersonel.Items.Add(MainForm.personelNames[i]);
            }
            /////////////
            tbIslemAdi.Enabled = false;
            tbIslemAdi.Text = islemAdi;

        }

        private void bKasaOK_Click(object sender, EventArgs e)
        {
            switch (islemAdi)
            {

                case "Kasadan Tip Al":
                    if (tbAciklama.Text != "" && tbTutar.Text != "")
                    {
                        try
                        {
                            tutar = Convert.ToDecimal(tbTutar.Text, culture);
                            UserLog.WConsole("Kasadaki Tip Toplami : " + totalTip);
                            UserLog.WConsole("Kasadan Dusulecek Tip Tutari : " + tutar);
                            if (tutar <= totalTip)
                            {
                                aciklama = tbAciklama.Text;
                                Personel = cbPersonel.SelectedItem.ToString();
                                islemAdi = tbIslemAdi.Text;
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Kasadan Alinabilecek Maksimum Tutardan Daha Buyuk Bir Tutar Giremezsiniz ! \n( Maksimum Tutar: " + totalTip+" )");
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Lutfen Tum Alanlarin Eksiksiz ve Dogru Olarak Dolduruldugundan Emin Olunuz!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Lutfen Aciklama Alanina Aciklama Yaziniz !");
                    }
                    break;
            }
            
        }

        private void bKasaCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
