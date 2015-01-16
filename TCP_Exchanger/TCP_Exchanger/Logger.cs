using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace TCP_Exchanger
{
    public partial class Logger : Form
    {
        private Dictionary<ConnectionData, NetworkAvailability> shouldWrite;        // Look up map to check if the data should be logged or not
        private int index;                                                          // index written file

        public Logger(Controller c)
        {
            InitializeComponent();

            shouldWrite = new Dictionary<ConnectionData, NetworkAvailability>();
            index = 1;

            this.Visible = false;
        }

        // Main function for this class
        public void Log(String toLog, ConnectionData cd)
        {
            if (cd != null)
            {
                if (!shouldWrite.Keys.Contains(cd))
                {
                    shouldWrite.Add(cd, cd.availability);
                }
                else
                {
                    // Different status
                    if (shouldWrite[cd] != cd.availability)
                    {
                        shouldWrite[cd] = cd.availability;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // Update box and write to file
            // Just add then write to file
            String write = getTime() + toLog + "\n";

            if (log_box.InvokeRequired)
            {
                log_box.BeginInvoke((MethodInvoker)(() => log_box.AppendText(write)));
            }
            else
            {
                log_box.AppendText(write);
            }
            
            // Write to txt file
            if (!Directory.Exists("log"))
            {
                Directory.CreateDirectory("log");
            }

            if (!Directory.Exists(@"log\" + getDate()))
            {
                Directory.CreateDirectory(@"log\" + getDate());
            }

            String file_name = @"log\" + getDate() + @"\" + getDate() + "-" + getIndex();

            if (File.Exists(file_name + ".txt"))
            {
                FileInfo f = new FileInfo(file_name + ".txt");
                long len = f.Length;

                if (len > 1024000)
                {
                    index++;
                    file_name = @"log\" + getDate() + @"\" + getDate() + "-" + getIndex();
                    File.Create(file_name + ".txt").Dispose();
                    //File.Create(file_name + ".lrd").Dispose();
                }
            }
            else
            {
                File.Create(file_name + ".txt").Dispose();
                //File.Create(file_name + ".lrd").Dispose();
            }

            using (StreamWriter w = File.AppendText(file_name + ".txt"))
            {
                w.WriteLine(write);
            }

            // Write in JSon format for log reader
            // Convert log into object
            /*Log toWrite = new Log();
            toWrite.Date = getDate();
            toWrite.Time = DateTime.Now.ToString("HH.mm.ss");
            if (cd != null)
            {
                toWrite.Name = cd.name;
                toWrite.IP = cd.remote_ip;
                toWrite.Port = cd.remote_port;
                toWrite.Availability = cd.availability.ToString();
            }
            else
            {
                toWrite.Name = toLog;
                toWrite.IP = toWrite.Port = toWrite.Availability = String.Empty;
            }

            using (StreamWriter w = File.AppendText(file_name + ".lrd"))
            {
                w.WriteLine(JsonConvert.SerializeObject(toWrite));
            }*/
        }

        #region Internal Methods
        private String getTime()
        {
            return "[Date:" + getDate() + "]\t[Time:" + DateTime.Now.ToString("HH.mm.ss") + "]\t";
        }

        private String getIndex()
        {
            if (index < 10)
            {
                return "00" + index;
            }
            if (index < 100)
            {
                return "0" + index;
            }
            return index.ToString();
        }

        private String getDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

        #region Custom Event Handler
        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

    public class Log
    {
        public String Date;
        public String Time;
        public String Name;
        public String IP;
        public String Port;
        public String Availability;
    }
}