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
    public partial class TableCloseForm : Form
    {
        public TableCloseForm()
        {
            InitializeComponent();
            DateTime result = DateTime.Today.Subtract(TimeSpan.FromDays(0));
        }

        

        private void CalculateKalanTutar(object sender, EventArgs e)
        {
            if (cbTL.CheckState == CheckState.Checked)
            {
                decimal nakit = 0;
                decimal kredi = 0;
                decimal cari = 0;
                //kredi
                try
                {
                    kredi = Convert.ToDecimal(tbKredi.Text);
                }
                catch
                {
                    kredi = 0;
                    //Kredi Entry Error//
                }
                //Nakit
                try
                {
                    nakit = Convert.ToDecimal(tbNakit.Text);
                }
                catch
                {
                    nakit = 0;
                    //Nakit Entry Error//
                }
                //Cari
                try
                {
                    cari = Convert.ToDecimal(tbCari.Text);
                }
                catch
                {
                    cari = 0;
                    //Cari Entry Error//
                }
                decimal toplam = nakit + cari + kredi;
                if (Convert.ToString(toplam) == lTL.Text.ToString())
                {
                    bMasaKapat.Enabled = true;
                    bMasaKapat.BackColor = Color.Green;
                    lKalan.Text = (Convert.ToDecimal(lTL.Text) - toplam).ToString()+ " ₺";
                }
                else
                {
                    bMasaKapat.Enabled = false;
                    bMasaKapat.BackColor = Color.Red;
                    lKalan.Text = (Convert.ToDecimal(lTL.Text) - toplam).ToString() + " ₺";
                }
            }
            else if (cbEuro.CheckState == CheckState.Checked)
            {
            }
            else if (cbDolar.CheckState == CheckState.Checked)
            {
            }
            else if (cbGBP.CheckState == CheckState.Checked)
            {
            }

        }

        private void TableCloseForm_Load(object sender, EventArgs e)
        {
            bMasaKapat.Enabled = false;
            bMasaKapat.BackColor = Color.Red;
            //Masa Adi Set
             lTableName.Text = LastChoosenTable.lastClosedTableName.ToString();
            //Tutar Set
             decimal tutarConvert = LastChoosenTable.lastClosedTableTutar;
            UserLog.WConsole("Tutar : " + LastChoosenTable.lastClosedTableTutar.ToString());
             lTL.Text    =  tutarConvert.ToString();
             lDolar.Text = (tutarConvert * LastChoosenTable.DefinedDolar).ToString();
             lEuro.Text  = (tutarConvert * LastChoosenTable.DefinedEuro).ToString();
             lGBP.Text   = (tutarConvert * LastChoosenTable.DefinedGBP).ToString();
            //Kalan Set
            lKalan.Text = lTL.Text + " ₺";
        }
        
        private void MouseClicked(object sender, MouseEventArgs e)
        {
            pbCalculator.BackgroundImage = Properties.Resources.cart_add_icon;
        }

        private void ExchangeCalculateTL(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde TL///
            if (cbTL.CheckState == CheckState.Unchecked)
            {
                cbTL.CheckState = CheckState.Checked;
            }
                cbDolar.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;   
        }

        private void ExchangeCalculateEURO(object sender, EventArgs e)
        {
            if (cbEuro.CheckState == CheckState.Unchecked)
            {
                cbEuro.CheckState = CheckState.Checked;
            }
                cbTL.CheckState = CheckState.Unchecked;
                cbDolar.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
        }

        private void ExchangeCalculateDOLAR(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde DOLAR///
            if (cbDolar.CheckState == CheckState.Unchecked)
            {
                cbDolar.CheckState = CheckState.Checked;
            }
                cbTL.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
        }

        private void ExchangeCalculateGBP(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde GBP///
            if (cbGBP.CheckState == CheckState.Unchecked)
            {
                cbGBP.CheckState = CheckState.Checked;
            }
                cbTL.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbDolar.CheckState = CheckState.Unchecked;
        }

        private void bMasaKapat_Click(object sender, EventArgs e)
        {
           // if ( )
           // { }
           // else if ( )
           // { }



            if (lKalan.Text == "0.0 ₺")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void bIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Calculator(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
        }
    }
}
