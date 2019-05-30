using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sana_Clinet
{
    public partial class SettingFrm : Form
    {
        private bool closing = false;

        public SettingFrm()
        {
            InitializeComponent();
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

        private void SettingFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.closing)
                e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocalIp.Text))
                MessageBox.Show("Plaese Fill Local IP.");
            else if (string.IsNullOrWhiteSpace(txtLocalPort.Text))
                MessageBox.Show("Plaese Fill Local Port.");
            else if (string.IsNullOrWhiteSpace(txtLocalPath.Text))
                MessageBox.Show("Plaese Fill Local Path.");
            else
            {

                string basePath = System.Environment.CurrentDirectory + "\\" + "Settings";
                IniFile ini = new IniFile(basePath + "\\" + "config.ini");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                ini.IniWriteValue("Soha", "host", "http://sohalibrary.com");
                ini.IniWriteValue("Soha", "port", "80");
                ini.IniWriteValue("Soha", "path", "/");

                ini.IniWriteValue("LocalServer", "ip", txtLocalIp.Text);
                ini.IniWriteValue("LocalServer", "port", txtLocalPort.Text);
                ini.IniWriteValue("LocalServer", "path", txtLocalPath.Text);

                this.closing = true;
                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtLocalPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}