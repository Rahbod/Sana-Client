using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Net;

namespace Sana_Clinet
{
    public partial class MainFrm : Form
    {
        private bool closing = false;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void InitConnections()
        {
            Uri localAddress = new Uri("http://"+this.ReadConfig("LocalServer", "ip"));
            String sohaAddress = this.ReadConfig("Soha", "host")+":"+this.ReadConfig("Soha", "port")+this.ReadConfig("Soha", "path");
                      
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(localAddress);
            request.Timeout = 15000;
            request.Method = "HEAD"; // As per Lasse's comment
            String address = sohaAddress;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    address = "http://" + this.ReadConfig("LocalServer", "ip") + ":" + this.ReadConfig("LocalServer", "port") + this.ReadConfig("LocalServer", "path");
                }
            }
            catch (WebException){}
            MessageBox.Show(address);
            webBrowser1.Navigate(address);
            webBrowser1.Visible = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Alt | Keys.C))
            {
                this.closing = true;
                System.Windows.Forms.Application.Exit();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Alt | Keys.M))
            {
                this.WindowState = FormWindowState.Minimized;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.closing)
                e.Cancel = true;
        }


        public string ReadConfig(string section, string key)
        {
            string retVal = string.Empty;
            string bankname = string.Empty;
            string basePath = System.Environment.CurrentDirectory + "\\" + "settings";
            IniFile ini = new IniFile(basePath + "\\" + "config.ini");
            if (!Directory.Exists(basePath))
            {
                SettingFrm settingFrm = new SettingFrm();
                settingFrm.ShowDialog();
            }

            retVal = ini.IniReadValue(section, key);
            return retVal;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            InitConnections();
        }

    }
}