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
            #region TL_Calculation
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
            #endregion
            #region Euro_Calculation
            else if (cbEuro.CheckState == CheckState.Checked)
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
                if (Convert.ToString(toplam) == lEuro.Text.ToString())
                {
                    bMasaKapat.Enabled = true;
                    bMasaKapat.BackColor = Color.Green;
                    lKalan.Text = (Convert.ToDecimal(lEuro.Text) - toplam).ToString() + " €";
                }
                else
                {
                    bMasaKapat.Enabled = false;
                    bMasaKapat.BackColor = Color.Red;
                    lKalan.Text = (Convert.ToDecimal(lEuro.Text) - toplam).ToString() + " €";
                }
            }
            #endregion
            #region Dolar_Calculation
            else if (cbDolar.CheckState == CheckState.Checked)
            {
            }
            #endregion
            #region GBP_Calculation
            else if (cbGBP.CheckState == CheckState.Checked)
            {
            }
            #endregion
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
            if (cbTL.CheckState == CheckState.Checked)
            {
                cbDolar.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
            }
            recalculateKalanTutar("TL");
         }

        private void ExchangeCalculateEURO(object sender, EventArgs e)
        {
            if (cbEuro.CheckState == CheckState.Checked)
            {
                cbTL.CheckState = CheckState.Unchecked;
                cbDolar.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
            }
            recalculateKalanTutar("Euro");
        }

        private void ExchangeCalculateDOLAR(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde DOLAR///
            if (cbDolar.CheckState == CheckState.Checked)
            {
                cbTL.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
            }
            recalculateKalanTutar("Dolar");
        }

        private void ExchangeCalculateGBP(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde GBP///
            if (cbGBP.CheckState == CheckState.Checked)
            {
                cbTL.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbDolar.CheckState = CheckState.Unchecked;
            }
            recalculateKalanTutar("GBP");

        }

        private void bMasaKapat_Click(object sender, EventArgs e)
        {
            string birim="";
            if(cbTL.CheckState == CheckState.Checked)
                {
                birim = "TL";
                }
            else if(cbDolar.CheckState == CheckState.Checked)
                {
                birim = "Dolar";
                }
            else if(cbEuro.CheckState == CheckState.Checked)
                {
                birim = "Euro";
                }
            else
                {
                birim = "GBP";
                }
            LastChoosenTable.paraBirimi = birim;
            //------------------------------------------------------------------------------------------------------//
            if (lKalan.Text == "0.0 ₺" || lKalan.Text == "0.0 €" || lKalan.Text == "0.0 £" || lKalan.Text == "0.0 $")
            {
                try
                {
                    LastChoosenTable.krediKarti = Convert.ToDecimal(tbKredi.Text);
                }
                catch {
                    LastChoosenTable.krediKarti = 0;
                }
                //////
                try
                {
                    LastChoosenTable.nakit = Convert.ToDecimal(tbNakit.Text);
                }
                catch {
                    LastChoosenTable.nakit = 0;
                }
                //////
                try
                {
                    LastChoosenTable.cari = Convert.ToDecimal(tbCari.Text);
                }
                catch
                {
                    LastChoosenTable.cari = 0;
                }
                UserLog.WConsole("************************************************");
                UserLog.WConsole("<< Masa Odeme Alinarak Kapatildi >>");
                UserLog.WConsole("Masa Adi : " + lTableName.Text);
                UserLog.WConsole("Kredi : " + LastChoosenTable.krediKarti+ " | " + "Nakit : "+ LastChoosenTable.nakit +" | " + "Cari : "+ LastChoosenTable.cari );
                UserLog.WConsole("Toplam Tutar : "+lTL.Text+ "TL");
                UserLog.WConsole("************************************************");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void bIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Recalculation of Kalan Tutar (lKalan)
        /// </summary>
        /// <param name="currency"></param>
        private void recalculateKalanTutar(string currency)
        {
            switch(currency)
            {
                case "TL":
                    lKalan.Text = lTL.Text + " ₺";
                    break;
                case "Euro":
                    lKalan.Text = lEuro.Text + " €";
                    break;
                case "Dolar":
                    lKalan.Text = lDolar.Text + " $";
                    break;
                case "GBP":
                    lKalan.Text = lGBP.Text + " £";
                    break;
            }
        }
        private void Calculator(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
        }
    }
}
