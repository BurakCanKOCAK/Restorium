using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NetComm;

namespace Restorium
{
    public partial class MainDisplay : Form
    {
        public MainDisplay()
        {
            InitializeComponent();
        }

        private void MainDisplay_Load(object sender, EventArgs e)
        {
           
        }

        private void Second(object sender, EventArgs e)
        {
            for (int i = dgvData.RowCount; i >=0 ; i--)
            {
                try
                {
                    dgvData.Rows.RemoveAt(i);
                }
                catch
                {
                    UserLog.WConsole("There is no data to delete");
                }
            }
            dgvData.Refresh();

            for (int i = 0; i < 200; i++)
            {
                dgvData.Rows.Add(MainForm.tableDetails[i, 0], MainForm.tableDetails[i, 1], MainForm.tableDetails[i, 2], MainForm.tableDetails[i, 3], MainForm.tableDetails[i, 4], MainForm.tableDetails[i, 5], MainForm.tableDetails[i, 6], MainForm.tableDetails[i, 7], MainForm.tableDetails[i, 8], MainForm.tableDetails[i, 9], MainForm.tableDetails[i, 10]);
            }
            dgvData.Refresh();
        }
    }
}
