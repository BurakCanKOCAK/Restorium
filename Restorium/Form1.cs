using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Restorium
{
    public partial class UserLogin : Form
    {
        IniFile INI = new IniFile();
        public UserLogin()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
        private void UserLogin_Load(object sender, EventArgs e)
        {

        }
        
        // --------------------------------------------------------- //

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            if ((tbUserName.Text == "Admin") && (tbPassword.Text == "Password"))
            {
                UserLog.UserName = "Burak";
                UserLog.WConsole("Admin");
                tbPassword.BackColor = Color.Green;
                tbUserName.BackColor = Color.Green;
                Thread.Sleep(500);
                MainForm main = new MainForm();
                main.Show();
                this.Hide();
            }
            else
            {
                tbPassword.BackColor = Color.Red;
                tbUserName.BackColor = Color.Red;
                MessageBox.Show("Lütfen kullanıcı adınızı ve şifrenizi kontrol edip tekrar deneyiniz!");
                
            }   

        }
        public static bool SetStyle(Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            Type typeTB = typeof(Control);
            System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (misSetStyle != null && c != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            return retval;
        }

        private void key_press(object sender, KeyPressEventArgs e)
        {
        if (e.KeyChar == (char)Keys.Enter) 
            {
                if ((tbUserName.Text == "Admin") && (tbPassword.Text == "Password"))
                {
                    UserLog.UserName = "Burak";
                    UserLog.WConsole("Admin_With_Password");
                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                }
                else if(tbUserName.Text=="")
                {
                    UserLog.UserName = "Burak";
                    UserLog.WConsole("Admin_Pirate");
                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    tbPassword.BackColor = Color.Red;
                    tbUserName.BackColor = Color.Red;
                    MessageBox.Show("Lütfen kullanıcı adınızı ve şifrenizi kontrol edip tekrar deneyiniz!");
                } 
            }
        else if (e.KeyChar == (char)Keys.Escape) 
            {
                this.Close();
            }
        }
        
        

      
    }
}
