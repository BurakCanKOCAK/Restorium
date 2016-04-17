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
    public partial class PersonelAdd : Form
    {
        public static string PersonelColumn0 = "";
        public static string PersonelColumn1 = "";
        public static string PersonelColumn2 = "";
        public static string PersonelColumn3 = "";
        public static string PersonelColumn4 = "";
        public static string PersonelColumn5 = "";
        public static string PersonelColumn6 = "";

        public static bool personelEditMode;
        public PersonelAdd()
        {
            InitializeComponent();
        }

        private void bKaydetPersonel_Click(object sender, EventArgs e)
        {
            if (tbPersonelColumn0.Text != "" && tbPersonelColumn1.Text != "")
            {
                this.DialogResult = DialogResult.OK;
                PersonelColumn0 = tbPersonelColumn0.Text;
                PersonelColumn1 = tbPersonelColumn1.Text;
                PersonelColumn2 = tbPersonelColumn2.Text;
                PersonelColumn3 = tbPersonelColumn3.Text;
                PersonelColumn4 = tbPersonelColumn4.Text;
                PersonelColumn5 = tbPersonelColumn5.Text;
                PersonelColumn6 = tbPersonelColumn6.Text;
                this.Close();
            }
            else
            {
                if(tbPersonelColumn0.Text == "")
                {
                    lPersonelID.ForeColor = Color.Red;
                }
                if(tbPersonelColumn1.Text =="")
                {
                    lPersonelAdi.ForeColor = Color.Red;
                }
                MessageBox.Show("Lutfen zorunlu alanlari eksiksiz doldurunuz !");
            }

        }

        private void bIptalPersonel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PersonelAdd_Load(object sender, EventArgs e)
        {
            if (personelEditMode == true)
            {
                tbPersonelColumn0.Text = PersonelColumn0;
                tbPersonelColumn1.Text = PersonelColumn1;
                tbPersonelColumn2.Text = PersonelColumn2;
                tbPersonelColumn3.Text = PersonelColumn3;
                tbPersonelColumn4.Text = PersonelColumn4;
                tbPersonelColumn5.Text = PersonelColumn5;
                tbPersonelColumn6.Text = PersonelColumn6;
                
            }
        }
    }
}
