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

namespace TCP_Exchanger
{
    public partial class AddNew : Form
    {
        private ConnectionData response;        // Data that is observed by main screen

        public AddNew(ConnectionData cd)
        {
            InitializeComponent();
            response = cd;
            this.Visible = true;

            // The following lines are used to handle information passed down from
            // add existing and doesn't allow editing of the two fields
            if (!cd.remote_ip.Equals(String.Empty))
            {
                ip_text.Text = cd.remote_ip;
                ip_text.Enabled = false;
            }
            if (!cd.remote_port.Equals(String.Empty))
            {
                port_text.Text = cd.remote_port;
                port_text.Enabled = false;
            }
        }

        // Main function of this window form
        private void add_btn_Click(object sender, EventArgs e)
        {
            // Match IP and port with regular expressiong
            Boolean is_ip = Regex.IsMatch(ip_text.Text
                , @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            Boolean is_port = Regex.IsMatch(port_text.Text
                , @"\b\d{1,5}\b");

            if (is_ip && is_port)
            {
                // If given input is ip and port, then pass it back to main screen
                response.name = name_box.Text;
                response.remote_ip = ip_text.Text;
                response.remote_port = port_text.Text;
                response.command = command_text.Text;
                this.Dispose();
            }
            else
            {
                // Throw error
                if (!is_ip)
                {
                    errorProvider.SetError(ip_text, "Please enter valid IP");
                }
                if (!is_port)
                {
                    errorProvider.SetError(port_text, "Please enter valid port");
                }
            }
        }

        #region Custom event handler
        // Cancel all the actions plus reverting all the information
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            response.remote_ip = String.Empty;
            response.remote_port = String.Empty;
            response.command = String.Empty;
            this.Dispose();
        }

        // Minimize the form
        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Make the command box clear off everytime it is clicked
        private void command_text_Click(object sender, MouseEventArgs e)
        {
            if (!command_text.Text.Equals(String.Empty))
            {
                command_text.Text = String.Empty;
            }
        }
        #endregion

        #region Custom override method

        // Allow user to move borderless window form
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
