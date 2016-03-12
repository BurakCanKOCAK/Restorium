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
    public partial class ContactAdd : Form
    {
        public static string Column0 = "";
        public static string Column1 = "";
        public static string Column2 = "";
        public static string Column3 = "";
        public static string Column4 = "";
        public static string Column5 = "";
        public static string Column6 = "";
        public static string Column7 = "";
        public static string Column8 = "";

        public ContactAdd()
        {
            InitializeComponent();
        }

        private void bKaydetRehber_Click(object sender, EventArgs e)
        {
            if (tbColumn0.Text != "" )
            {
                try
                {
                    if (Convert.ToInt16(tbColumn5.Text) > 100 || Convert.ToInt16(tbColumn5.Text) < 0)
                    {
                        MessageBox.Show("Lutfen Iskonto Alanina Gecerli Bir Deger Giriniz !");
                        label6.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        Column0 = tbColumn0.Text;
                        Column1 = tbColumn1.Text;
                        Column2 = tbColumn2.Text;
                        Column3 = tbColumn3.Text;
                        Column4 = tbColumn4.Text;
                        if (tbColumn5.Text == "")
                        {
                            Column5 = "0";
                        }
                        else if (Convert.ToInt16(tbColumn5.Text) <= 100 && Convert.ToInt16(tbColumn5.Text) >= 0)
                        {
                            Column5 = tbColumn5.Text;
                        }
                        Column6 = tbColumn6.Text;
                        Column7 = tbColumn7.Text;
                        Column8 = tbColumn8.Text;
                        this.Close();
                    }

                }
                catch
                {
                    MessageBox.Show("Lutfen Iskonto Alanina Gecerli Bir Deger Giriniz !");
                    label6.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("Lutfen Girilmesi Zorunlu Alanlarin Eksiksiz Girildiginden Emin Olunuz !");
                label1.ForeColor = Color.Red;
            }
        }

        private void bIptalRehber_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
