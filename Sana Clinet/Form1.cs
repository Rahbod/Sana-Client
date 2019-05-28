using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sana_Clinet
{
    public partial class Form1 : Form
    {
        private bool closing = false;

        public Form1()
        {
            InitializeComponent();
            InitConnections();
        }

        private void InitConnections()
        {
            /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 15000;
            request.Method = "HEAD"; // As per Lasse's comment
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                return false;
            }*/
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

    }
}