using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DataRetriever
{
    public partial class Option : Form
    {
        private Option_Object option;           // Option from main screen
        private List<String> ipList;            // All server ip list

        public Option(List<String> ip_list, Option_Object option)
        {
            InitializeComponent();

            ipList = ip_list;
            this.option = option;

            foreach (String s in ipList)
            {
                host_list_box.Items.Add(s);
            }

            close_to_tray_btn.Checked = option.close_to_system_tray;
            min_to_tray_btn.Checked = option.min_to_system_tray;
            interval_box.Text = (option.recon_interval / 1000).ToString();
        }

        #region Custom event handler
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void add_host_btn_Click(object sender, EventArgs e)
        {
            Boolean is_ip = Regex.IsMatch(host_box.Text, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

            if (is_ip)
            {
                host_list_box.Items.Add(host_box.Text);
                host_box.Text = String.Empty;
            }
            else
            {
                errorProvider1.SetError(host_box, "Please enter correct ip");
            }
        }

        private void host_remove_btn_Click(object sender, EventArgs e)
        {
            if (host_list_box.CheckedItems.Count > 0)
            {
                List<String> toRemove = new List<String>();

                foreach (String o in host_list_box.CheckedItems)
                {
                    toRemove.Add(o);
                }

                foreach (String s in toRemove)
                {
                    host_list_box.Items.Remove(s);
                }

                toRemove.Clear();

                GC.Collect();
            }
            else
            {
                errorProvider1.SetError(host_list_box, "Please select connection before attempt to remove");
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            Boolean is_int = Regex.IsMatch(interval_box.Text, @"^[0-9]+$");

            if (is_int)
            {
                if (Convert.ToInt32(interval_box.Text) >= 1)
                {
                    try
                    {
                        lock (ipList)
                        {
                            ipList.Clear();

                            if (File.Exists("ip.txt"))
                            {
                                File.Delete("ip.txt");
                            }

                            String toWrite = String.Empty;
                            // Add ip
                            foreach (String s in host_list_box.Items)
                            {
                                ipList.Add(s);
                                toWrite += s + "\n";
                            }

                            if (!toWrite.Equals(String.Empty))
                            {
                                File.WriteAllText("ip.txt", toWrite);
                            }
                        }
                    }
                    catch
                    {
                        errorProvider1.SetError(host_box, "File is already opened, new ip added cannot be saved");
                    }

                    // Set each option
                    option.close_to_system_tray = close_to_tray_btn.Checked;
                    option.min_to_system_tray = min_to_tray_btn.Checked;
                    option.recon_interval = Convert.ToInt32(interval_box.Text) * 1000;
                }
                else
                {
                    errorProvider1.SetError(interval_box, "Please enter integer larger than 1");
                }
            }
            else
            {
                if (!is_int)
                {
                    errorProvider1.SetError(interval_box, "Please enter between 1 to 9999");
                }
            }
        }
        #endregion

        #region Custom override methods
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0Xa1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        #endregion
    }
}