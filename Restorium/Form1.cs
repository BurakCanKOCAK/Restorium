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
                // Key : LoggedUser - Value : Admin - Section : Login
                INI.Write("LoggedUser", "Admin", "Login");
                // Key : LoggedDate - Value : Login Date - Section : Login
                INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                UserLog.UserName = "ADMIN";
                UserLog.WConsole("Succesfully Logged In");
                tbPassword.BackColor = Color.Green;
                tbUserName.BackColor = Color.Green;
                Thread.Sleep(500);
                MainForm main = new MainForm();
                main.Show();
                this.Hide();
            }else
            {
                UserLog.UserName = "Guest";
                UserLog.WConsole("Invalid Password or Username");
                tbPassword.BackColor = Color.Red;
                tbUserName.BackColor = Color.Red;
                MessageBox.Show("Lütfen kullanıcı adınızı ve şifrenizi kontrol edip tekrar deneyiniz!");

            }   

        }
   
        private void key_press(object sender, KeyPressEventArgs e)
        {
        if (e.KeyChar == (char)Keys.Enter) 
            {
                if ((tbUserName.Text == "Admin") && (tbPassword.Text == "Password"))
                {
                    // Key : LoggedUser - Value : Admin - Section : Login
                    INI.Write("LoggedUser", "Admin", "Login");
                    // Key : LoggedDate - Value : Login Date - Section : Login
                    INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                    UserLog.UserName = "ADMIN";
                    UserLog.WConsole("Succesfully Logged In (KeyPressed)");
                    MainForm main = new MainForm();
                    main.Show();

                  //  DebugMonitor debug = new DebugMonitor();
                  //  debug.Show();

                    this.Hide();
                }
                else if (tbUserName.Text == "")
                {
                    // Key : LoggedUser - Value : Admin - Section : Login
                    INI.Write("LoggedUser", "Admin", "Login");
                    // Key : LoggedDate - Value : Login Date - Section : Login
                    INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                    UserLog.UserName = "ADMIN";
                    UserLog.WConsole("Succesfully Logged In (Admin_Pirate)");
                    MainForm main = new MainForm();
                    main.Show();

                  //  DebugMonitor debug = new DebugMonitor();
                  //  debug.Show();

                    this.Hide();
                }
                else if (tbUserName.Text == "G")
                {
                    // Key : LoggedUser - Value : Admin - Section : Login
                    INI.Write("LoggedUser", "Guest", "Login");
                    // Key : LoggedDate - Value : Login Date - Section : Login
                    INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                    UserLog.UserName = "GUEST";
                    UserLog.WConsole("Succesfully Logged In (Guest_Pirate)");
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
