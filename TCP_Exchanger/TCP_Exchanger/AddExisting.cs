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
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TCP_Exchanger
{
    public partial class AddExisting : Form
    {
        public AddExisting(ConnectionData cd)
        {
            InitializeComponent();

            toPass = cd;
            this.Visible = true;

            init();
        }

        private void init()
        {
            List<String> list = new List<String>();

            IPAddress myIP = LocalIP();
            olv_lib = new List<IP>();

            this.getAllConnections();
            
            // Put connections into string and store it
            foreach (TcpConnectionInformation info in TCPactive)
            {
                if (myIP.Equals(info.LocalEndPoint.Address)
                    && !list.Contains(info.RemoteEndPoint.Address.ToString()
                        + ":" + info.RemoteEndPoint.Port.ToString()))
                {
                    IP tmp = new IP(info.RemoteEndPoint.Address.ToString()
                        , info.RemoteEndPoint.Port.ToString(), info.State.ToString());
                    list.Add(tmp.Address + ":" + tmp.Port);
                    olv_lib.Add(tmp);
                }
            }

            // If there is no available connection then display no connection
            if (olv_lib.Count <= 0)
            {
                olv_lib.Add(new IP("No connection", "", ""));
            }
            olv_list.SetObjects(olv_lib);

            next_btn.Enabled = false;

            GC.Collect();
        }

        #region Main Function
        private ConnectionData toPass;                  // Store connectiondata to pass to addnew
        private List<IP> olv_lib;                       // Store the list of IP available
        private TcpConnectionInformation[] TCPactive;   // Store the list of active TCP connection
        #pragma warning disable 0169                    // Ignore warning
        // May be used later
        private IPEndPoint[] TCPlisten;                 // Store the list of listening TCP connection
        private IPEndPoint[] UDPlisten;                 // Store the list of listening UDP connection
        #pragma warning restore 0169

        // Get local IP of this pc
        private IPAddress LocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return null;
        }

        // Initialize the active TCP list
        private void getAllConnections()
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TCPactive = ipProperties.GetActiveTcpConnections();
        }
        #endregion

        #region Custom event handler

        // When clicked, it will pass down connectiondata selected and give it to addNew
        private void next_btn_Click(object sender, EventArgs e)
        {
            toPass.remote_ip = ((IP)olv_list.SelectedObject).Address;
            toPass.remote_port = ((IP)olv_list.SelectedObject).Port;
            toPass.status = Helper.toStatus(((IP)olv_list.SelectedObject).State);

            this.Dispose();
        }

        // If there is anything available and clicked, next button will be enabled
        private void olv_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            next_btn.Enabled = true;
        }

        // Cancel all action done in this window
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // Minimize the window
        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Custom override method

        // Enable drag on borderless form
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

    #region Custom Object
    public class IP
    {
        public String Address { get; set; }
        public String Port { get; set; }
        public String State { get; set; }

        public IP(String Address, String Port, String State)
        {
            this.Address = Address;
            this.Port = Port;
            this.State = State;
        }
    }
    #endregion
}
