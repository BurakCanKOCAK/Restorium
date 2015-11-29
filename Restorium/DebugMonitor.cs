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
    public partial class DebugMonitor : Form
    {
        public DebugMonitor()
        {
            InitializeComponent();
        }

        private void DebugMonitor_Load(object sender, EventArgs e)
        {

        }
        public void User_Monitor(string monitorText)
        {
            lbLogs.Text += "\n"+monitorText;

        }
    }
}
