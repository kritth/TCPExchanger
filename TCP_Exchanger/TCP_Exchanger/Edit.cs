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
    public partial class Edit : Form
    {
        private Dictionary<String, ConnectionData> lookup;

        public Edit(Dictionary<String, ConnectionData> dict)
        {
            InitializeComponent();

            this.lookup = dict;
            
            // Get data from given dictionary
            entry_box.Items.AddRange(dict.Keys.ToArray());

            // Set enabled off
            disable_box();

            this.Visible = true;
        }

        // Main function for this class
        private void save_btn_Click(object sender, EventArgs e)
        {
            Boolean is_ip = Regex.IsMatch(ip_box.Text, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            Boolean is_port = Regex.IsMatch(port_box.Text, @"\b\d{1,5}\b");
            if (is_ip && is_port)
            {
                ConnectionData current = lookup[entry_box.Text];
                current.remote_ip = ip_box.Text;
                current.remote_port = port_box.Text;
                current.command = command_box.Text;
                current.name = name_box.Text;

                disable_box();
            }
            else
            {
                if (!is_ip)
                {
                    errorProvider.SetError(ip_box, "Please enter valid IP in x.x.x.x "
                        + " where x is 1 to 3 digits integer");
                }
                if (!is_port)
                {
                    errorProvider.SetError(port_box, "Please enter 1 to 5 digits integer");
                }
            }
        }

        #region Internal method
        private void disable_box()
        {
            // Disable all the controls
            ip_box.Enabled = false;
            port_box.Enabled = false;
            command_box.Enabled = false;
            name_box.Enabled = false;
            save_btn.Enabled = false;
        }
        #endregion

        #region Custom event handler
        private void entry_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectionData toParse = lookup[entry_box.Text];

            // Fill the data
            name_box.Text = toParse.name;
            ip_box.Text = toParse.remote_ip;
            port_box.Text = toParse.remote_port;
            command_box.Text = toParse.command;
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            // Enable all the box
            ip_box.Enabled = true;
            port_box.Enabled = true;
            name_box.Enabled = true;
            command_box.Enabled = true;
            save_btn.Enabled = true;
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
