using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCP_Exchanger
{
    public partial class PortScanner : Form
    {
        private ConnectionData data;                    // Use to store data to pass
        private Dictionary<int, Port_Check> lookup;     // Lookup map to put in the object list view
        private Port_Check current;                     // To store the current pointer
        private Thread scannerThread;                   // Separate thread for scanning port
        private Boolean interrupt;

        public PortScanner(ConnectionData cd)
        {
            InitializeComponent();

            data = cd;

            interrupt = false;
        }

        #region Main Function
        public void Interrupt()
        {
            this.interrupt = true;
        }

        private void ScanPorts(int start, int end, BrightIdeasSoftware.ObjectListView olv_list)
        {
            try
            {
                for (int i = start; i <= end; i++)
                {
                    current = lookup[i];
                    ScanPort(i);
                    olv_list.RefreshObject(lookup[i]);

                    if (interrupt)
                    {
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        private void ScanPort(int port)
        {
            var tcp = new TcpClient();
            var result = tcp.BeginConnect(ip_box.Text, port, null, null);
            result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
            if (!tcp.Connected)
            {
                // Refuse connection
                current.status = "Refused";
            }
            else
            {
                // Connection successful
                current.status = "Open";
                tcp.EndConnect(result);
            }
            tcp.Close();
        }
        #endregion

        #region Custom Event Handler
        private void start_btn_Click(object sender, EventArgs e)
        {
            Boolean is_ip = !ip_box.Text.Equals(String.Empty) && Regex.IsMatch(ip_box.Text
                , @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            Boolean is_start_port = !start_box.Text.Equals(String.Empty) && Regex.IsMatch(start_box.Text
                , @"\b\d{1,5}\b");
            Boolean is_end_port = !end_box.Text.Equals(String.Empty) && Regex.IsMatch(end_box.Text
                , @"\b\d{1,5}\b");
            if (is_ip && is_start_port && is_end_port)
            {
                int start_port = Convert.ToInt32(start_box.Text);
                int end_port = Convert.ToInt32(end_box.Text);
                if (end_port >= start_port)
                {
                    lookup = new Dictionary<int,Port_Check>();

                    ip_box.Enabled = false;
                    start_btn.Enabled = false;
                    start_box.Enabled = false;
                    end_box.Enabled = false;
                    for (int i = Convert.ToInt32(start_box.Text); i <= Convert.ToInt32(end_box.Text); i++)
                    {
                        lookup.Add(i, new Port_Check(i.ToString(), "Waiting"));
                        olv_list.AddObject(lookup[i]);
                    }

                    scannerThread = new Thread(() => 
                        ScanPorts(Convert.ToInt32(start_box.Text), Convert.ToInt32(end_box.Text), olv_list));
                    scannerThread.Start();
                }
                else
                {
                    errorProvider.SetError(start_box, "Please enter start port higher than end port");
                }
            }
            else
            {
                if (!is_ip)
                {
                    errorProvider.SetError(ip_box, "Please enter valid IP");
                }
                if (!is_start_port)
                {
                    errorProvider.SetError(start_box, "Please enter valid port");
                }
                if (!is_end_port)
                {
                    errorProvider.SetError(end_box, "Please enter valid port");
                }
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            data.remote_ip = ip_box.Text;
            data.remote_port = ((Port_Check)olv_list.SelectedObject).remote_port;
            this.Interrupt();

            while (scannerThread != null && scannerThread.IsAlive)
            {
                // Spin
            }

            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Interrupt();

            while (scannerThread != null && scannerThread.IsAlive)
            {
                // Spin
            }

            this.Dispose();
        }

        private void olv_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            add_btn.Enabled = true;
        }
        #endregion

        #region Custom Override Methods
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

    public class Port_Check
    {
        public String remote_port;
        public String status;

        public Port_Check(String p, String s)
        {
            remote_port = p;
            status = s;
        }
    }
}
