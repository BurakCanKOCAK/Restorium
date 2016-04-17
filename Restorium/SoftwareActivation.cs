using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Restorium
{
    public partial class SoftwareActivation : Form
    {
            Thread t;
            IPAddress IPAddress;
            TcpClient baglantikur;
            TcpListener dinle;
            Socket soket;
            NetworkStream ag;
            StreamReader streamReader, oku;
            StreamWriter yaz;
            WebResponse webResponse;
            public bool reConnectFlag = false;
        public bool connected2Server = false;
            public static string OpenIP;
            public static StreamWriter streamWriter;
            private static bool connectionFlag = false;
            public delegate void ricdegis(string text);


            public SoftwareActivation()
            {
                InitializeComponent();
            }

            private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
            {

            }

            private void SoftwareActivation_Load(object sender, EventArgs e)
            {
            if (isConnectedToInternet())
            {
                IPAddress = IPAddress.Parse(GetLocalIP().ToString());
                lInternalIpAddress.Text = "Internal IP   : " + IPAddress.ToString();
                pbStatusBar.Value = 15;
                Thread threadPublicIp = new Thread(new ThreadStart(GetPublicIP));
                threadPublicIp.Start();
            }
            }
        #region SetControlValueCallback
        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
            private void SetControlPropertyValue(Control oControl, string propName, object propValue)
            {
                if (oControl.InvokeRequired)
                {
                    SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                    oControl.Invoke(d, new object[] { oControl, propName, propValue });
                }
                else
                {
                    Type t = oControl.GetType();
                    PropertyInfo[] props = t.GetProperties();
                    foreach (PropertyInfo p in props)
                    {
                        if (p.Name.ToUpper() == propName.ToUpper())
                        {
                            p.SetValue(oControl, propValue, null);
                            pbStatusBar.Value = 30;
                        }
                    }
                }
            }
        #endregion
        public void GetPublicIP()
            {
                String publicIp = "";
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                webResponse = request.GetResponse();
                Thread.Sleep(3000);
                streamReader = new StreamReader(webResponse.GetResponseStream());
                publicIp = streamReader.ReadToEnd();

                //Search for the ip in the html
                int first = publicIp.IndexOf("Address: ") + 9;
                int last = publicIp.LastIndexOf("</body>");
                publicIp = publicIp.Substring(first, last - first);
                SetControlPropertyValue(lExternalIpAddress, "Text", "External IP : " + publicIp);
                OpenIP = publicIp;
                //lExternalIpAddress.Text = "External IP : " + publicIp;
            }

        private object GetLocalIP()
            {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                    lInternalIpAddress.Text = "Internal IP   : " + IPAddress.ToString();
                }
            }

            throw new Exception("Local IP Address Not Found!");
            }

        private void timerTick(object sender, EventArgs e)
            {
                //WiFi Check
                if (isConnectedToInternet())
                {
                    pbWifiStatus.BackgroundImage= Properties.Resources.WIFI_GREEN;
                    pbWifiStatus.Refresh();
                    pbNoWifi.Visible = false;
                    IPAddress = IPAddress.Parse(GetLocalIP().ToString());
                    lInternalIpAddress.Text = "Internal IP   : " + IPAddress.ToString();
                if (reConnectFlag == true && lInternalIpAddress.Text != "Internal IP   : -")
                    {
                    Thread threadPublicIp= new Thread(new ThreadStart(GetPublicIP));
                    threadPublicIp.Start();
                    reConnectFlag = false;
                    }
                }
                else
                {
                    pbWifiStatus.BackgroundImage = Properties.Resources.WIFI_BLACK;
                    pbWifiStatus.Refresh();
                    lInternalIpAddress.Text = "Internal IP   : -";
                    lExternalIpAddress.Text = "External IP : -";
                    pbStatusBar.Value = 0;
                    pbNoWifi.Visible = true;
                    if (pbWifiStatus.BackgroundImage == Properties.Resources.WIFI_BLACK && lInternalIpAddress.Text == "Internal IP   : -")
                    {
                        reConnectFlag = true;
                        connected2Server = false;
                    }
                }
            if (lInternalIpAddress.Text != "Internal IP   : -" && lExternalIpAddress.Text != "External IP : -")
            {
                pbStage1.Visible = true;
                if(connected2Server==false)
                { 
                    VerifyMyKeyCode();
                }
            }
            else
            {
                pbStage1.Visible = false;
            }
            }

        private void VerifyMyKeyCode()
        {
            //////////////////////////////////////////
            baglanti_kur();
            connected2Server = true;
            //////////////////////////////////////////
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void okumayabasla()
        {
            ag = baglantikur.GetStream();
            oku = new StreamReader(ag);
            yaz = new StreamWriter(ag);
            while (true)
            {
                try
                {
                    string yazi = oku.ReadLine();
                    ekranabas(yazi);
                }
                catch
                {
                    return;
                }
            }
        }
        public void ekranabas(string s)
        {
            if (this.InvokeRequired)
            {
                ricdegis degis = new ricdegis(ekranabas);
                this.Invoke(degis, s);
            }
            else
            {
                s = "Admin : " + s;
                //richTextBox1.AppendText(s + "\n");
                MessageBox.Show(s);
            }
        }
        public void baglanti_kur()
        {
            try
            {
                //Ben Lochalhos üzerinde deneme yapacagim icin 127.0.0.1 verdim
                baglantikur = new TcpClient("95.51.21.114", 8012);
                //baglantikur = new TcpClient(OpenIP, 8080);
                t = new Thread(new ThreadStart(okumayabasla));
                t.Start();
                //richTextBox1.AppendText(DateTime.Now.ToString() + " Baglanti kuruldu...\n");
                connectionFlag = true;
                //MessageBox.Show("Server ile baglanti kuruldu !");
                pbStatusBar.Value = 50;
                sendData("Hello World . This Is Restorium...!");
            }
            catch (Exception)
            {

                MessageBox.Show("Server ile baglanti kurulurken hata olustu !");
            }
        }
        public void DisconnectFromServer()
        {
            if (connectionFlag)
            {
                baglantikur.Client.Close();
                connectionFlag = false;
                MessageBox.Show("Succesfullly Disconnected !");
            }
            else {
                MessageBox.Show("Already disconnected...");
            }
        }
        private void sendData(string dataString)
        {
                yaz.WriteLine(dataString);
                yaz.Flush();
                //richTextBox1.AppendText("Me : " + textBox2.Text + "\n");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool isConnectedToInternet()
            {
                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timerTickPublic(object sender, EventArgs e)
        {
            if (isConnectedToInternet())
            {
                Thread threadPublicIp = new Thread(new ThreadStart(GetPublicIP));
                threadPublicIp.Start();
            }
        }
    }
}
