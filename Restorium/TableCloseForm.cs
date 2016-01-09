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

        }

        private void TableCloseForm_Load(object sender, EventArgs e)
        {
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
            pictureBox1.BackgroundImage = Properties.Resources.cart_add_icon;
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
            if ( )
            { }
            else if ( )
            { }



            if (lKalan.Text == "0.00 ₺")
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
    }
}
