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
    public partial class UserSettings : Form
    {
        public static string userType,oldUser,oldPass,newPass,NewUser;
        IniFile INI = new IniFile();

        public UserSettings()
        {
            InitializeComponent();
        }

        private void PassMatch(object sender, EventArgs e)
        {
            if (tbYeniSifre.Text == tbYeniSifreTekrar.Text)
            {
                pbCheckPass.Visible = true;
                pbCheckPass.Image = Properties.Resources.check__2_1;
            }
            else
            {
                pbCheckPass.Visible = true;
                pbCheckPass.Image = Properties.Resources.circle__7_;
            }
            
        }

        private void tbKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            if (userType == "Admin")
            {
                lKullaniciTuru.Text = "Admin Ayarlari";
                oldUser = INI.Read("Admin", "Login");
                if (oldUser == "")
                {
                    oldUser = "Admin";
                }
                oldPass = INI.Read("AdminPass", "Login");
                if (oldPass == "")
                {
                    oldPass = "password";
                }

            }
            else
            {
                lKullaniciTuru.Text = "Personel Ayarlari";
                oldUser = INI.Read("User", "Login");
                if (oldUser == "")
                {
                    oldUser = "User";
                }
                oldPass = INI.Read("UserPass", "Login");
                if (oldPass == "")
                {
                    oldPass = "password";
                }

            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bKasaCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bKasaOK_Click(object sender, EventArgs e)
        {
            if (userType == "Admin")////////////////ADMIN
            {
                if (tbKullaniciAdi.Text != "" && tbEskiSifre.Text != "" && tbYeniSifre.Text != "" && tbYeniSifreTekrar.Text != "" && tbYeniKullaniciAdi.Text != "")
                {
                    if (tbKullaniciAdi.Text != oldUser || tbEskiSifre.Text != oldPass) ///Hatali Kullanici Adi Veya Sifre
                    {
                        MessageBox.Show("Lutfen Kullanici Bilgilerininiz Dogru Oldugundan Emin Olup Tekrar Deneyiniz");
                    }
                    else
                    {
                        if (tbYeniSifre.Text == tbYeniSifreTekrar.Text)
                        {
                            MessageBox.Show("Kullanici Adi ve Sifre Basariyla Degistirildi !");
                            INI.Write("Admin", tbYeniKullaniciAdi.Text, "Login");
                            INI.Write("AdminPass", tbYeniSifre.Text, "Login");
                        }
                    }
                }
                else ///Herhangi bir texbox bos ise
                {
                    MessageBox.Show("Lutfen Butun Alanlari Eksiksiz Doldurunuz !");
                }
            }
            else/////////////////////////////USER
            {
                if (tbKullaniciAdi.Text != "" && tbEskiSifre.Text != "" && tbYeniSifre.Text != "" && tbYeniSifreTekrar.Text != "" && tbYeniKullaniciAdi.Text != "")
                {
                    if (tbKullaniciAdi.Text != oldUser || tbEskiSifre.Text != oldPass) ///Hatali Kullanici Adi Veya Sifre
                    {
                        MessageBox.Show("Lutfen Kullanici Bilgilerininiz Dogru Oldugundan Emin Olup Tekrar Deneyiniz");
                    }
                    else
                    {
                        if (tbYeniSifre.Text == tbYeniSifreTekrar.Text)
                        {
                            MessageBox.Show("Kullanici Adi ve Sifre Basariyla Degistirildi !");
                            INI.Write("User", tbYeniKullaniciAdi.Text, "Login");
                            INI.Write("UserPass", tbYeniSifre.Text, "Login");
                        }
                        else
                        {
                            MessageBox.Show("Girdiginiz Sifreler Birbirinden Farkli , Lutfen Kontrol Edip Tekrar Deneyiniz !");
                        }
                    }
                }
                else ///Herhangi bir texbox bos ise
                {
                    MessageBox.Show("Lutfen Butun Alanlari Eksiksiz Doldurunuz !");
                }

            }
        }
    }
}
