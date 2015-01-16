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

namespace DataRetriever
{
    public partial class FocusApp : Form
    {
        private const double MOUSE_OFF_OPACITY = 0.5                // Opacity of the app when mouse is off
            , MOUSE_ON_OPACITY = 0.9;                               // Opacity of the app when mouse is on

        private OLVObject obj;                                      // OLVObject to display the information
        private CustomLabel name_label;                             // Custom label for name
        private CustomLabel ip_label;                               // Custom label for ip
        private CustomLabel status_label;                           // Custom label for status
        private int index;                                          // Index location of this app
        private DataRetriever data;                                         // Data from main screen

        public FocusApp(DataRetriever f)
        {
            InitializeComponent();

            // Following are the custom UI Initialization
            customInit();

            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.data = f;

            this.name_label.Parent = this.ip_label.Parent = this.status_label.Parent
                = this.close_label.Parent = this.bg_box;
            this.name_label.BackColor = this.ip_label.BackColor = this.status_label.BackColor
                = this.close_label.BackColor = Color.Transparent;

            this.close_label.MouseEnter += new EventHandler(EnterColorChange);
            this.close_label.MouseLeave += new EventHandler(LeaveColorChange);

            this.Opacity = MOUSE_OFF_OPACITY;

            this.bg_box.MouseEnter += new EventHandler(OnMouseEnter);
            this.bg_box.MouseLeave += new EventHandler(OnMouseLeave);

            this.bg_box.MouseDown += new MouseEventHandler(PicMouseDown);
        }

        #region Internal methods
        // Custom initialization of UI
        private void customInit()
        {
            this.name_label = new CustomLabel();
            this.ip_label = new CustomLabel();
            this.status_label = new CustomLabel();

            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Enabled = false;
            this.name_label.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_label.Location = new System.Drawing.Point(15, 40);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(38, 13);
            this.name_label.TabIndex = 0;
            this.name_label.Text = "Name:";

            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Enabled = false;
            this.ip_label.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ip_label.Location = new System.Drawing.Point(15, 13);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(61, 13);
            this.ip_label.TabIndex = 2;
            this.ip_label.Text = "IP Address:";

            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Enabled = false;
            this.status_label.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_label.Location = new System.Drawing.Point(219, 40);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(40, 13);
            this.status_label.TabIndex = 3;
            this.status_label.Text = "Status:";
            this.status_label.color = Color.Black;

            this.Controls.Add(this.status_label);
            this.Controls.Add(this.ip_label);
            this.Controls.Add(this.name_label);
        }

        // Set background of the app
        private void setBG(Image img)
        {
            bg_box.Image = img;
        }

        // Set all text of this apps and change background accordingly
        public void setText(OLVObject obj)
        {

            if (obj != null)
            {
                this.obj = obj;
            }

            // Change label
            if (obj.name.Equals(String.Empty))
            {
                this.name_label.Text = "Server: N/A";
            }
            else
            {
                this.name_label.Text = "Server: " + obj.name;
            }
            this.ip_label.Text = "IP Address: " + obj.ip;
            this.status_label.Text = "Status: " + obj.status;

            // Change BG
            if (obj.status.Equals("Wait_For_Test"))
            {
                setBG(global::DataRetriever.Properties.Resources.BG_Wait);
            }
            else if (obj.status.Equals("No_Response"))
            {
                setBG(global::DataRetriever.Properties.Resources.BG_No_Response);
            }
            else if (obj.status.Equals("Online"))
            {
                setBG(global::DataRetriever.Properties.Resources.BG_Online);
            }
            else
            {
                setBG(global::DataRetriever.Properties.Resources.BG_Offline);
            }

            this.name_label.Refresh();
            this.ip_label.Refresh();
            this.status_label.Refresh();

            GC.Collect();
        }

        // Set location index for the app and bring them to front
        public void setLocation(int index, OLVObject obj)
        {
            if (index != -1)
            {
                this.index = index;
            }

            if (obj != null)
            {
                this.obj = obj;
            }

            if (InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)(() => this.setText(this.obj)));
                this.BeginInvoke((MethodInvoker)(() => this.WindowState = FormWindowState.Normal));
                this.BeginInvoke((MethodInvoker)(() => this.StartPosition = FormStartPosition.Manual));
                this.BeginInvoke((MethodInvoker)(() => this.BringToFront()));
                this.BeginInvoke((MethodInvoker)(() => this.Visible = true));
                this.BeginInvoke((MethodInvoker)(() => this.Refresh()));
            }
            else
            {
                this.setText(this.obj);
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.Manual;
                this.BringToFront();

                if (index != -1)
                {
                    this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 5 - this.Width
                       , System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 40 - (this.index * (this.Height + 5)));
                }

                this.Visible = true;
                this.Refresh();
            }
        }

        // This restore the main screen
        private void close_label_Click(object sender, EventArgs e)
        {
            data.restore();
        }
        #endregion

        #region Custom Event handler
        // Mouse enter, increase opacity
        private void OnMouseEnter(Object sender, EventArgs e)
        {
            while (this.Opacity < MOUSE_ON_OPACITY)
            {
                this.Opacity += 0.1;
                Thread.Sleep(10);
            }
        }

        // Mouse leave, decrease opacity
        private void OnMouseLeave(Object sender, EventArgs e)
        {
            while (this.Opacity > MOUSE_OFF_OPACITY)
            {
                this.Opacity -= 0.1;
                Thread.Sleep(10);
            }
        }

        // Mouse enter, color change for custom label
        private void EnterColorChange(Object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }

        // Mouse leave, color change for custom label
        private void LeaveColorChange(Object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.White;
        }
        
        #endregion

        #region Custom override methods
        // Allow user to move borderless form around
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
        
        // Allow user to move app around
        private void PicMouseDown(Object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
        #endregion

    }

    public class CustomLabel : Label
    {
        public Color color = Color.White;

        public CustomLabel()
        {
            this.SetStyle(ControlStyles.UserPaint, true); //Call in constructor, Use UserPaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Enabled)
            {
                SolidBrush drawBrush = new SolidBrush(color); //Choose colour

                e.Graphics.DrawString(Text, Font, drawBrush, 0f, 0f); //Dra whatever text was on the label
            }
            else
            {
                base.OnPaint(e); //Default Forecolours
            }
        }
    }
}
