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
        public static string admin, user;
        public static string adminPass, userPass;
        public UserLogin()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
        private void UserLogin_Load(object sender, EventArgs e)
        {
            try
            {
                admin = INI.Read("Admin", "Login");
                adminPass = INI.Read("AdminPass", "Login");
            }
            catch
            {
                UserLog.WConsole("(!)Admin Hesabi Olusturulmamis !");
                admin = "Admin";
                adminPass = "password";
            }
            try
            {
                user = INI.Read("User", "Login");
                userPass = INI.Read("UserPass", "Login");
            }
            catch
            {
                UserLog.WConsole("(!)Normal Kullanici Hesabi Olusturulmamis !");
                admin = "User";
                adminPass = "password";
            }
        }
        
        // --------------------------------------------------------- //

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ///////////ADMIN
                admin = INI.Read("Admin", "Login");
                adminPass = INI.Read("AdminPass", "Login");
                if (admin == "" || adminPass == "")
                {
                    UserLog.WConsole("(!)Admin Hesabi Olusturulmamis !");
                    admin = "Admin";
                    adminPass = "password";
                }else
                { 
                UserLog.WConsole("Admin Credentials Are Loaded :"+admin+"-"+adminPass);
                }

                user = INI.Read("User", "Login");
                userPass = INI.Read("UserPass", "Login");
            ///////////USER
            if (user == "" || userPass == "")
            {
                UserLog.WConsole("(!)Normal Kullanici Hesabi Olusturulmamis !");
                admin = "User";
                adminPass = "password";
            }
            else
            {
                UserLog.WConsole("User Credentials Loaded :" + user + "-" + userPass);
            }

        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            if ((tbUserName.Text ==admin) && (tbPassword.Text == adminPass))
            {
                // Key : LoggedUser - Value : Admin - Section : Login
                INI.Write("LoggedUser", "Admin", "Login");
                // Key : LoggedDate - Value : Login Date - Section : Login
                INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                UserLog.UserName = "ADMIN";
                UserLog.WConsole("Succesfully Logged In");
                tbPassword.BackColor = Color.Green;
                tbUserName.BackColor = Color.Green;
                Thread.Sleep(2500);
                this.BackgroundImage = Properties.Resources.BACK_4_Success;
                this.Refresh();
                Thread.Sleep(1000);
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
                this.BackgroundImage = Properties.Resources.BACK_4_Fail;

            }

        }

        private void key_press(object sender, KeyPressEventArgs e)
        {
        if (e.KeyChar == (char)Keys.Enter) 
            {
                if ((tbUserName.Text == admin) && (tbPassword.Text == adminPass))
                {
                    // Key : LoggedUser - Value : Admin - Section : Login
                    INI.Write("LoggedUser", "Admin", "Login");
                    // Key : LoggedDate - Value : Login Date - Section : Login
                    INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                    UserLog.UserName = "ADMIN";
                    UserLog.WConsole("Succesfully Logged In (KeyPressed)");
                    this.BackgroundImage = Properties.Resources.BACK_4_Success;
                    this.Refresh();
                    Thread.Sleep(1000);
                    MainForm main = new MainForm();
                    main.Show();
                    //  DebugMonitor debug = new DebugMonitor();
                    //  debug.Show();

                    this.Hide();
                }
                else if (tbUserName.Text == "")
                {
                    //MailActivation md = new MailActivation();
                    //md.Show();
                    // Key : LoggedUser - Value : Admin - Section : Login
                    INI.Write("LoggedUser", "Admin", "Login");
                    // Key : LoggedDate - Value : Login Date - Section : Login
                    INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                    UserLog.UserName = "ADMIN";
                    UserLog.WConsole("Succesfully Logged In (Admin_Pirate)");
                    this.BackgroundImage = Properties.Resources.BACK_4_Success;
                    this.Refresh();
                    Thread.Sleep(1000);
                    MainForm main = new MainForm();
                    main.Show();
                    //  DebugMonitor debug = new DebugMonitor();
                    //  debug.Show();

                    this.Hide();
                }
                else if ((tbUserName.Text == user) && (tbPassword.Text == userPass))
                {
                    // Key : LoggedUser - Value : Admin - Section : Login
                    INI.Write("LoggedUser", "Guest", "Login");
                    // Key : LoggedDate - Value : Login Date - Section : Login
                    INI.Write("LoggedDate", DateTime.UtcNow.ToLocalTime().ToString(), "Login");
                    UserLog.UserName = "USER";
                    UserLog.WConsole("Succesfully Logged In (User_Pirate)");
                    this.BackgroundImage = Properties.Resources.BACK_4_Success;
                    this.Refresh();
                    Thread.Sleep(1000);
                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    tbPassword.BackColor = Color.Red;
                    tbUserName.BackColor = Color.Red;
                    MessageBox.Show("Lütfen kullanıcı adınızı ve şifrenizi kontrol edip tekrar deneyiniz!");
                    this.BackgroundImage = Properties.Resources.BACK_4_Fail;
                } 
            }
        else if (e.KeyChar == (char)Keys.Escape) 
            {
                this.Close();
            }
        }
    }

    public class Form1
    {
    }
}
