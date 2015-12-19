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
            dateTimePicker1.Value = result;
        }

        private void asd(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Value.ToString();
        }
    }
}
