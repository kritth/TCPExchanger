using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace TCP_Exchanger
{
    public partial class IPGetter : Form
    {
        private const String cannot_find = "Cannot find IP for given URL";
        private ConnectionData toAdd;

        public IPGetter(ConnectionData cd)
        {
            InitializeComponent();

            this.Visible = true;

            toAdd = cd;
        }

        // Main function for this class
        private String getIpOf(String address)
        {
            try
            {
                IPAddress[] addrList = Dns.GetHostAddresses(address);
                return addrList[0].ToString();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        #region Custom event handler
        private void search_btn_Click(object sender, EventArgs e)
        {
            if (!url_box.Text.Equals(String.Empty))
            {
                String myIp = getIpOf(url_box.Text);
                if (!myIp.Equals(String.Empty))
                {
                    ip_box.Text = myIp;
                    add_btn.Enabled = true;
                }
                else
                {
                    ip_box.Text = cannot_find;
                }
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            toAdd.remote_ip = ip_box.Text;
            this.Dispose();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Custom override method
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        #endregion
    }
}
