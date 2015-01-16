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
    public partial class AdvancedSearch : Form
    {
        private SearchSystem mySearch;                      // Search options of this program

        public AdvancedSearch(SearchSystem so)
        {
            InitializeComponent();

            this.Visible = true;

            // Get data from given enum into combo box

            Array status = Enum.GetValues(typeof(TCPStatus));
            String[] status_array = new String[status.Length];
            int i = 0;
            foreach (var stat in status)
            {
                status_array[i] = stat.ToString();
                i++;
            }
            status_box.Items.AddRange((object[])status_array);
            status_box.SelectedIndex = 0;

            Array availability = Enum.GetValues(typeof(NetworkAvailability));
            String[] availability_array = new String[availability.Length];
            i = 0;
            foreach (var avail in availability)
            {
                availability_array[i] = avail.ToString();
                i++;
            }
            availability_box.Items.AddRange((object[])availability_array);
            availability_box.SelectedIndex = 0;

            mySearch = so;

            GC.Collect();
        }

        // Main function of this window form
        private void search_btn_Click(object sender, EventArgs e)
        {
            // Match the input with regular expression
            Boolean is_ip = ip_box.Text.Equals(String.Empty) || Regex.IsMatch(ip_box.Text
                , @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b") || Regex.IsMatch(ip_box.Text
                , @"\b\d{1,3}\b");
            Boolean is_port = port_box.Text.Equals(String.Empty) || Regex.IsMatch(port_box.Text
                , @"\b\d{1,5}\b");

            if (is_ip && is_port)
            {
                mySearch.Clear();

                // Set ip search
                if (!ip_box.Text.Equals(String.Empty))
                {
                    mySearch.Search_IP = true;
                    mySearch.ip = ip_box.Text;
                }
                else
                {
                    mySearch.Search_IP = false;
                }
                // Set port search
                if (!port_box.Text.Equals(String.Empty))
                {
                    mySearch.Search_Port = true;
                    mySearch.port = port_box.Text;
                }
                else
                {
                    mySearch.Search_Port = false;
                }
                // Set status search
                if (status_box.SelectedIndex != 0)
                {
                    mySearch.Search_Status = true;
                    mySearch.status = Helper.toStatus(status_box.SelectedText);
                }
                else
                {
                    mySearch.Search_Status = false;
                }
                // Set availability search
                if (availability_box.SelectedIndex != 0)
                {
                    mySearch.Search_Availability = true;
                    mySearch.availability = Helper.toNetworkAvailability(availability_box.SelectedText);
                }
                else
                {
                    mySearch.Search_Availability = false;
                }
                // Set command search
                if (!command_box.Text.Equals(String.Empty))
                {
                    mySearch.Search_Command = true;
                    mySearch.command = command_box.Text;
                }
                else
                {
                    mySearch.Search_Command = false;
                }
                // Set name search
                if (!name_box.Text.Equals(String.Empty))
                {
                    mySearch.Search_Name = true;
                    mySearch.name = name_box.Text;
                }
                else
                {
                    mySearch.Search_Name = false;
                }
                this.Dispose();
            }
            else
            {
                if (!is_ip)
                {
                    errorProvider.SetError(ip_box, "Please enter valid IP in x.x.x.x "
                        + "or x or empty where x is 1 to 3 digits integer");
                }
                if (!is_port)
                {
                    errorProvider.SetError(port_box, "Please enter 1 to 5 digits integer");
                }
            }
        }

        #region Custom event handler
        // Cancel all the actions done in this window
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

        #region Custom override methods
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
