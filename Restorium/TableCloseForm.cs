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
    public partial class TableCloseForm : Form
    {
        public static int iskonto;
        public TableCloseForm()
        {
            InitializeComponent();
            DateTime result = DateTime.Today.Subtract(TimeSpan.FromDays(0));
        }

        

        private void CalculateKalanTutar(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("tr-TR", true);
            if (tbNakit.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            if (tbKredi.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            if (tbCari.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ","; 
            #region TL_Calculation
            if (cbTL.CheckState == CheckState.Checked)
            {
                labelKredi.Text = "Kredi Karti(₺)";
                labelCari.Text = "Cari(₺)";
                labelNakit.Text = "Nakit (₺)";
                decimal nakit = 0;
                decimal kredi = 0;
                decimal cari = 0;
                //kredi
                try
                {
                    kredi = Convert.ToDecimal(tbKredi.Text,culture);
                }
                catch
                {
                    kredi = 0;
                    //Kredi Entry Error//
                }
                //Nakit
                try
                {
                    nakit = Convert.ToDecimal(tbNakit.Text, culture);
                }
                catch
                {
                    nakit = 0;
                    //Nakit Entry Error//
                }
                //Cari
                try
                {
                    cari = Convert.ToDecimal(tbCari.Text, culture);
                }
                catch
                {
                    cari = 0;
                    //Cari Entry Error//
                }
                decimal toplam = nakit + cari + kredi;
               
                    if (cbTip.CheckState == CheckState.Checked)
                    {
                        lTip.Visible = true;
                        if ((nakit + kredi) >= Convert.ToDecimal(lTL.Text))
                        {
                            bMasaKapat.Enabled = true;
                            bMasaKapat.BackColor = Color.Green;
                            labelKalan.ForeColor = Color.GreenYellow;
                            lKalan.ForeColor = Color.GreenYellow;
                            lKalan.Text = "0 ₺";
                            LastChoosenTable.tip = Convert.ToDecimal((nakit + kredi) - (Convert.ToDecimal(lTL.Text)));
                            lTip.Text = "Tip : "+LastChoosenTable.tip+ " ₺"; ;
                        }
                        else
                        {
                            lTip.Text = "Tip : 0 ₺"; ;
                            bMasaKapat.Enabled = false;
                            bMasaKapat.BackColor = Color.Red;
                            labelKalan.ForeColor = Color.DarkRed;
                            lKalan.ForeColor = Color.DarkRed;
                            lKalan.Text = (Convert.ToDecimal(lTL.Text) - toplam).ToString() + " ₺";
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(toplam) == Convert.ToDecimal(lTL.Text))
                        {
                            bMasaKapat.Enabled = true;
                            bMasaKapat.BackColor = Color.Green;
                            labelKalan.ForeColor = Color.GreenYellow;
                            lKalan.ForeColor = Color.GreenYellow;
                            lKalan.Text = (Convert.ToDecimal(lTL.Text) - toplam).ToString() + " ₺";
                        }
                        else
                        { 
                            lTip.Visible = false;
                            bMasaKapat.Enabled = false;
                            bMasaKapat.BackColor = Color.Red;
                            labelKalan.ForeColor = Color.DarkRed;
                            lKalan.ForeColor = Color.DarkRed;
                            lKalan.Text = (Convert.ToDecimal(lTL.Text) - toplam).ToString() + " ₺";
                        }
                     }
                
                checkLogicSituations();
            }
            #endregion
            #region Euro_Calculation
            else if (cbEuro.CheckState == CheckState.Checked)
            {
                labelKredi.Text = "Kredi Karti(€)";
                labelCari.Text = "Cari(€)";
                labelNakit.Text = "Nakit (€)";
                decimal nakit = 0;
                decimal kredi = 0;
                decimal cari = 0;
                //kredi
                try
                {
                    kredi = Convert.ToDecimal(tbKredi.Text, culture);
                }
                catch
                {
                    kredi = 0;
                    //Kredi Entry Error//
                }
                //Nakit
                try
                {
                    nakit = Convert.ToDecimal(tbNakit.Text, culture);
                }
                catch
                {
                    nakit = 0;
                    //Nakit Entry Error//
                }
                //Cari
                try
                {
                    cari = Convert.ToDecimal(tbCari.Text, culture);
                }
                catch
                {
                    cari = 0;
                    //Cari Entry Error//
                }
                decimal toplam = nakit + cari + kredi;
                
                if (cbTip.CheckState == CheckState.Checked)
                {
                    lTip.Visible = true;
                    if ((nakit + kredi) >= Convert.ToDecimal(lEuro.Text))
                    {
                        bMasaKapat.Enabled = true;
                        bMasaKapat.BackColor = Color.Green;
                        labelKalan.ForeColor = Color.GreenYellow;
                        lKalan.ForeColor = Color.GreenYellow;
                        lKalan.Text = "0 €";
                        LastChoosenTable.tip = Convert.ToDecimal((nakit + kredi) - (Convert.ToDecimal(lEuro.Text)));
                        lTip.Text = "Tip : " + LastChoosenTable.tip + " €";
                    }
                    else
                    {
                        lTip.Text = "Tip : 0 €";
                        bMasaKapat.Enabled = false;
                        bMasaKapat.BackColor = Color.Red;
                        labelKalan.ForeColor = Color.DarkRed;
                        lKalan.ForeColor = Color.DarkRed;
                        lKalan.Text = (Convert.ToDecimal(lEuro.Text) - toplam).ToString() + " €";
                    }
                }
                else
                {
                    if (Convert.ToDecimal(toplam) == Convert.ToDecimal(lEuro.Text))
                    {
                        lTip.Visible = false;
                        bMasaKapat.Enabled = true;
                        bMasaKapat.BackColor = Color.Green;
                        labelKalan.ForeColor = Color.GreenYellow;
                        lKalan.ForeColor = Color.GreenYellow;
                        lKalan.Text = (Convert.ToDecimal(lEuro.Text) - toplam).ToString() + " €";
                    }
                    else
                    {
                        lTip.Visible = false;
                        bMasaKapat.Enabled = false;
                        bMasaKapat.BackColor = Color.Red;
                        labelKalan.ForeColor = Color.DarkRed;
                        lKalan.ForeColor = Color.DarkRed;
                        lKalan.Text = (Convert.ToDecimal(lEuro.Text) - toplam).ToString() + " €";
                    }
                }


                checkLogicSituations();
            }
            #endregion
            #region Dolar_Calculation
            else if (cbDolar.CheckState == CheckState.Checked)
            {
                labelKredi.Text = "Kredi Karti($)";
                labelCari.Text = "Cari($)";
                labelNakit.Text = "Nakit ($)";
                decimal nakit = 0;
                decimal kredi = 0;
                decimal cari = 0;
                //kredi
                try
                {
                    kredi = Convert.ToDecimal(tbKredi.Text, culture);
                }
                catch
                {
                    kredi = 0;
                    //Kredi Entry Error//
                }
                //Nakit
                try
                {
                    nakit = Convert.ToDecimal(tbNakit.Text, culture);
                }
                catch
                {
                    nakit = 0;
                    //Nakit Entry Error//
                }
                //Cari
                try
                {
                    cari = Convert.ToDecimal(tbCari.Text, culture);
                }
                catch
                {
                    cari = 0;
                    //Cari Entry Error//
                }
                decimal toplam = nakit + cari + kredi;
                //
                if (cbTip.CheckState == CheckState.Checked)
                {
                    lTip.Visible = true;
                    if ((nakit + kredi) >= Convert.ToDecimal(lDolar.Text))
                    {
                        bMasaKapat.Enabled = true;
                        bMasaKapat.BackColor = Color.Green;
                        labelKalan.ForeColor = Color.GreenYellow;
                        lKalan.ForeColor = Color.GreenYellow;
                        lKalan.Text = "0 $";
                        LastChoosenTable.tip = Convert.ToDecimal((nakit + kredi) - (Convert.ToDecimal(lDolar.Text)));
                        lTip.Text = "Tip : " + LastChoosenTable.tip + " $";
                    }
                    else
                    {
                        lTip.Text = "Tip : 0 $";
                        bMasaKapat.Enabled = false;
                        bMasaKapat.BackColor = Color.Red;
                        labelKalan.ForeColor = Color.DarkRed;
                        lKalan.ForeColor = Color.DarkRed;
                        lKalan.Text = (Convert.ToDecimal(lDolar.Text) - toplam).ToString() + " $";
                    }
                }
                else
                {
                    if (Convert.ToDecimal(toplam) == Convert.ToDecimal(lDolar.Text))
                    {
                        lTip.Visible = false;
                        bMasaKapat.Enabled = true;
                        bMasaKapat.BackColor = Color.Green;
                        labelKalan.ForeColor = Color.GreenYellow;
                        lKalan.ForeColor = Color.GreenYellow;
                        lKalan.Text = (Convert.ToDecimal(lDolar.Text) - toplam).ToString() + " $";
                    }
                    else
                    {
                        lTip.Visible = false;
                        bMasaKapat.Enabled = false;
                        bMasaKapat.BackColor = Color.Red;
                        labelKalan.ForeColor = Color.DarkRed;
                        lKalan.ForeColor = Color.DarkRed;
                        lKalan.Text = (Convert.ToDecimal(lDolar.Text) - toplam).ToString() + " $";
                    }
                }
                checkLogicSituations();
            }
            #endregion
            #region GBP_Calculation
            else if (cbGBP.CheckState == CheckState.Checked)
            {
                labelKredi.Text = "Kredi Karti(£)";
                labelCari.Text = "Cari(£)";
                labelNakit.Text = "Nakit (£)";
                decimal nakit = 0;
                decimal kredi = 0;
                decimal cari = 0;
                //kredi
                try
                {
                    kredi = Convert.ToDecimal(tbKredi.Text, culture);
                }
                catch
                {
                    kredi = 0;
                    //Kredi Entry Error//
                }
                //Nakit
                try
                {
                    nakit = Convert.ToDecimal(tbNakit.Text, culture);
                }
                catch
                {
                    nakit = 0;
                    //Nakit Entry Error//
                }
                //Cari
                try
                {
                    cari = Convert.ToDecimal(tbCari.Text, culture);
                }
                catch
                {
                    cari = 0;
                    //Cari Entry Error//
                }
                decimal toplam = nakit + cari + kredi;
                //
                if (cbTip.CheckState == CheckState.Checked)
                {
                    lTip.Visible = true;
                    if ((nakit + kredi) >= Convert.ToDecimal(lGBP.Text))
                    {
                        bMasaKapat.Enabled = true;
                        bMasaKapat.BackColor = Color.Green;
                        labelKalan.ForeColor = Color.GreenYellow;
                        lKalan.ForeColor = Color.GreenYellow;
                        lKalan.Text = "0 £";
                        LastChoosenTable.tip = Convert.ToDecimal((nakit + kredi) - (Convert.ToDecimal(lGBP.Text)));
                        lTip.Text = "Tip : " + LastChoosenTable.tip + " £";
                    }
                    else
                    {
                        lTip.Text = "Tip : 0 £";
                        bMasaKapat.Enabled = false;
                        bMasaKapat.BackColor = Color.Red;
                        labelKalan.ForeColor = Color.DarkRed;
                        lKalan.ForeColor = Color.DarkRed;
                        lKalan.Text = (Convert.ToDecimal(lGBP.Text) - toplam).ToString() + " £";
                    }
                }
                else
                {
                    if (Convert.ToDecimal(toplam) == Convert.ToDecimal(lGBP.Text))
                    {
                        lTip.Visible = false;
                        bMasaKapat.Enabled = true;
                        bMasaKapat.BackColor = Color.Green;
                        labelKalan.ForeColor = Color.GreenYellow;
                        lKalan.ForeColor = Color.GreenYellow;
                        lKalan.Text = (Convert.ToDecimal(lGBP.Text) - toplam).ToString() + " £";
                    }
                    else
                    {
                        lTip.Visible = false;
                        bMasaKapat.Enabled = false;
                        bMasaKapat.BackColor = Color.Red;
                        labelKalan.ForeColor = Color.DarkRed;
                        lKalan.ForeColor = Color.DarkRed;
                        lKalan.Text = (Convert.ToDecimal(lGBP.Text) - toplam).ToString() + " £";
                    }
                }
                //

              
                checkLogicSituations();
            }
            #endregion
        }

        private void checkLogicSituations()
        {
            if (tbNakit.Text.Contains("-"))
            {
                tbNakit.Text = "";
            }
            else if (tbCari.Text.Contains("-"))
            {
                tbCari.Text = "";
            }
            else if (tbKredi.Text.Contains("-"))
            {
                tbKredi.Text = "";
            }
        }

        private void TableCloseForm_Load(object sender, EventArgs e)
        {
            iskonto = LastChoosenTable.iskonto;
            lIskontoOrani.Text = iskonto.ToString() + "%";
            bMasaKapat.Enabled = false;
            bMasaKapat.BackColor = Color.Red;
            //Masa Adi Set
             lTableName.Text = LastChoosenTable.lastClosedTableName.ToString();
            string text = lTableName.Text;
            switch (text.Length)
            {
                case 1:
                case 2:
                case 3:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 42f, lTableName.Font.Style);
                    break;
                case 4:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 37f, lTableName.Font.Style);
                    break;
                case 5:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 35f, lTableName.Font.Style);
                    break;
                case 6:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 30f, lTableName.Font.Style);
                    break;
                case 7:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 25f, lTableName.Font.Style);
                    break;
                case 8:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 22f, lTableName.Font.Style);
                    break;
                case 9:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 20f, lTableName.Font.Style);
                    break;
                case 10:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 18f, lTableName.Font.Style);
                    break;
                case 11:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 16f, lTableName.Font.Style);
                    break;
                case 12:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 15f, lTableName.Font.Style);
                    break;
                default:
                    lTableName.Font = new Font(lTableName.Font.FontFamily, 8f, lTableName.Font.Style);
                    break;

            }
            //Tutar Set
            decimal tutarConvert = LastChoosenTable.lastClosedTableTutar;
            UserLog.WConsole("Tutar : " + LastChoosenTable.lastClosedTableTutar.ToString());
            lTL.Text    = System.Math.Round(tutarConvert,2).ToString();
            if (Settings.Dolar)
            {
                lDolar.Text = System.Math.Round((tutarConvert * LastChoosenTable.DefinedDolar), 2).ToString();
            }
            else
            {
                cbDolar.Enabled = false;
                lDolar.Text = "-";
            }
            if(Settings.Euro)
            { 
            lEuro.Text  = System.Math.Round((tutarConvert * LastChoosenTable.DefinedEuro),2).ToString();
            }
            else
            {
                cbEuro.Enabled = false;
                lEuro.Text = "-";
            }
            if (Settings.GBP)
            { 
            lGBP.Text   = System.Math.Round((tutarConvert * LastChoosenTable.DefinedGBP),2).ToString();
            }
            else
            {
                cbGBP.Enabled = false;
                lGBP.Text = "-";
            }
            //Kalan Set
            lKalan.Text = lTL.Text + " ₺";
        }
        
        private void MouseClicked(object sender, MouseEventArgs e)
        {
           // pbCalculator.BackgroundImage = Properties.Resources.cart_add_icon;
        }

        private void ExchangeCalculateTL(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde TL///
            if (cbTL.CheckState == CheckState.Checked)
            {
                cbDolar.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
            }
            LastChoosenTable.lastClosedTableTutar = Convert.ToDecimal(lTL.Text);
            labelKredi.Text = "Kredi Karti (₺)";
            labelCari.Text = "Cari (₺)";
            labelNakit.Text = "Nakit (₺)";
            tbCari.Text = "";
            tbKredi.Text = "";
            tbNakit.Text = "";
            recalculateKalanTutar("TL");
            labelKalan.ForeColor = Color.Red;
            lKalan.ForeColor = Color.Red;
         }

        private void ExchangeCalculateEURO(object sender, EventArgs e)
        {
            if (cbEuro.CheckState == CheckState.Checked)
            {
                CalculateKalanTutar(null, null);
                cbTL.CheckState = CheckState.Unchecked;
                cbDolar.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
            }
            LastChoosenTable.lastClosedTableTutar = Convert.ToDecimal(lEuro.Text);
            labelKredi.Text = "Kredi Karti (€)";
            labelCari.Text = "Cari (€)";
            labelNakit.Text = "Nakit (€)";
            tbCari.Text = "";
            tbKredi.Text = "";
            tbNakit.Text = "";
            recalculateKalanTutar("Euro");
            labelKalan.ForeColor = Color.Red;
            lKalan.ForeColor = Color.Red;
        }

        private void ExchangeCalculateDOLAR(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde DOLAR///
            if (cbDolar.CheckState == CheckState.Checked)
            {
                cbTL.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbGBP.CheckState = CheckState.Unchecked;
            }
            LastChoosenTable.lastClosedTableTutar = Convert.ToDecimal(lDolar.Text);
            labelKredi.Text = "Kredi Karti ($)";
            labelCari.Text = "Cari ($)";
            labelNakit.Text = "Nakit ($)";
            tbCari.Text = "";
            tbKredi.Text = "";
            tbNakit.Text = "";
            recalculateKalanTutar("Dolar");
            labelKalan.ForeColor = Color.Red;
            lKalan.ForeColor = Color.Red;
        }

        private void ExchangeCalculateGBP(object sender, EventArgs e)
        {//Checkbox stateleri degistiginde GBP///
            if (cbGBP.CheckState == CheckState.Checked)
            {
                cbTL.CheckState = CheckState.Unchecked;
                cbEuro.CheckState = CheckState.Unchecked;
                cbDolar.CheckState = CheckState.Unchecked;
            }
            LastChoosenTable.lastClosedTableTutar = Convert.ToDecimal(lGBP.Text);
            labelKredi.Text = "Kredi Karti (£)";
            labelCari.Text = "Cari (£)";
            labelNakit.Text = "Nakit (£)";
            tbCari.Text = "";
            tbKredi.Text = "";
            tbNakit.Text = "";
            recalculateKalanTutar("GBP");
            labelKalan.ForeColor = Color.Red;
            lKalan.ForeColor = Color.Red;

        }

        private void bMasaKapat_Click(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US", true);
            if (tbNakit.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            if (tbKredi.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            if (tbCari.Text.LastIndexOf(',') != -1)
                culture.NumberFormat.NumberDecimalSeparator = ",";
            string birim="";
            if(cbTL.CheckState == CheckState.Checked)
                {
                birim = " ₺";
                }
            else if(cbDolar.CheckState == CheckState.Checked)
                {
                birim = " $";
                }
            else if(cbEuro.CheckState == CheckState.Checked)
                {
                birim = " €";
                }
            else
                {
                birim = " £";
                }
            LastChoosenTable.paraBirimi = birim;
            //------------------------------------------------------------------------------------------------------//
            UserLog.WConsole("A1");
            if (Convert.ToDecimal(lKalan.Text.Replace(birim, ""))==0)
            //if (lKalan.Text == "0.0 ₺" || lKalan.Text == "0.0 €" || lKalan.Text == "0.0 £" || lKalan.Text == "0.0 $")
            {
                try
                {
                    LastChoosenTable.krediKarti = Convert.ToDecimal(tbKredi.Text, culture);
                }
                catch {
                    LastChoosenTable.krediKarti = 0;
                }
                //////
                try
                {
                    LastChoosenTable.nakit = Convert.ToDecimal(tbNakit.Text, culture);
                }
                catch {
                    LastChoosenTable.nakit = 0;
                }
                //////
                try
                {
                    LastChoosenTable.cari = Convert.ToDecimal(tbCari.Text, culture);
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
                UserLog.WConsole("Tip : "+LastChoosenTable.tip+" "+LastChoosenTable.paraBirimi);
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

        private void cbTipCheckStateChanged(object sender, EventArgs e)
        {
            if (cbDolar.CheckState == CheckState.Unchecked)
            {
                LastChoosenTable.tip = 0;
                tbCari.Text = "0";
                tbKredi.Text = "0";
                tbNakit.Text = "0";
                lTip.Visible = false;
                bMasaKapat.Enabled = false;
                bMasaKapat.BackColor = Color.Red;
                labelKalan.ForeColor = Color.DarkRed;
                lKalan.ForeColor = Color.DarkRed;
                CalculateKalanTutar(null,null);
            }
        }
    }
}
