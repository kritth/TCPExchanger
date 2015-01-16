using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DataRetriever
{
    public partial class DataRetriever : Form
    {
        public const String Disconnected_String = "Disconnected";

        public List<String> IP_lib;                             // Store host's IP
        public List<OLVObject> focus_list;                      // List of focus connection
        public Dictionary<String, OLVObject> lookup;            // Map for all connection
        public Dictionary<String, List<OLVObject>> ip_map;      // Map: key = IP, values = list of connections associated with this 
        public Dictionary<FocusApp, OLVObject> focus_app;       // Map for app and focus connection
        private Thread connection;                              // Thread for the connection
        private Thread option_thread;                           // Thread for customizing option
        private Thread about_thread;                            // Thread for about page
        private Boolean interrupt;                              // Indicate whether this application is interrupted
        private Option_Object option;                           // Option

        public DataRetriever()
        {
            InitializeComponent();

            menuStrip1.MouseDown += new MouseEventHandler(this.menuStrip1_MouseDown);
            toolStripStatusLabel1.Text = "Status: Application started";
            this.Text = "Data Retriever";

            init();
        }

        private void Debug()
        {
            IP_lib.Add(LocalIp().ToString());
        }

        #region Internal methods

        // Restore the application from system bar
        public void restore()
        {
            this.notifyIcon1_MouseDoubleClick(null, null);
        }

        // Get olv list
        public BrightIdeasSoftware.ObjectListView getOlvList()
        {
            return olv_list;
        }

        // Get focus list
        public BrightIdeasSoftware.ObjectListView getOlvFocus()
        {
            return olv_focus;
        }

        // Get status bar
        public ToolStripStatusLabel getStatus()
        {
            return toolStripStatusLabel1;
        }

        // Initialize of this class
        private void init()
        {
            IP_lib = new List<String>();
            focus_list = new List<OLVObject>();
            lookup = new Dictionary<string, OLVObject>();
            focus_app = new Dictionary<FocusApp, OLVObject>();
            ip_map = new Dictionary<String, List<OLVObject>>();
            interrupt = false;

            // Debug();

            // Load option
            option = new Option_Object();
            option.show_system_tray = false;
            option.close_to_system_tray = false;
            option.min_to_system_tray = true;
            option.recon_interval = 3000;
            option.max_focus = 5;

            for (int i = 1; i <= option.max_focus; i++)
            {
                focus_app.Add(new FocusApp(this), null);
            }

            connection = new Thread(() => connect(IP_lib));
            connection.Start();

            // Check if ip exist
            if (File.Exists("ip.txt"))
            {
                String[] lines = File.ReadAllLines("ip.txt");
                foreach (String s in lines)
                {
                    Boolean is_ip = Regex.IsMatch(s, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                    if (is_ip)
                    {
                        IP_lib.Add(s);
                    }
                }
            }

            GC.Collect();
        }

        // Get local IP
        private IPAddress LocalIp()
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

        // Interrupt the application
        private void Interrupt()
        {
            interrupt = true;
        }

        // Continuously connect to the IP on list
        private void connect(List<String> IP_lib)
        {
            while (!interrupt)
            {
                // Try to connect to this ip
                List<String> toConnect = new List<String>();
                lock (IP_lib)
                {
                    foreach (String ip in IP_lib)
                    {
                        toConnect.Add(ip);
                    }
                }

                foreach (String ip in toConnect)
                {
                    TCPAsync async = new TCPAsync(IPAddress.Parse(ip), this);
                    async.StartClient();
                }
                Thread.Sleep(option.recon_interval);
            }
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

        #endregion

        #region Custom Event Handler
        // Allow move from menu strip
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        // Exit application
        private void close_btn_Click(object sender, EventArgs e)
        {
            if (option.close_to_system_tray)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else
            {
                Environment.Exit(0);
                Application.Exit();
            }
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Application.Exit();
        }

        // Minimize the application
        private void minimize_btn_Click(object sender, EventArgs e)
        {
            if (option.min_to_system_tray)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Visible = false;
                this.ShowInTaskbar = false;

                if (option_thread != null && option_thread.IsAlive)
                {
                    option_thread.Abort();
                }

                int i = 1;
                foreach (FocusApp fa in focus_app.Keys)
                {
                    if (focus_app[fa] != null)
                    {
                        OLVObject obj = focus_app[fa];
                        fa.setText(obj);
                        fa.setLocation(i, obj);
                        fa.Visible = true;
                        i++;
                    }
                }
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        // Restore the application by double click on system bar
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;

            foreach (FocusApp fa in focus_app.Keys)
            {
                if (fa.Visible)
                {
                    fa.BeginInvoke((MethodInvoker)(() => fa.Visible = false));
                }
            }

            this.Activate();
        }

        // Add checked connection to focus list
        private void add_to_focus_Click(object sender, EventArgs e)
        {
            if (olv_list.CheckedObjects.Count + focus_list.Count <= option.max_focus)
            {
                // If there are enough spots in focus list
                List<OLVObject> toRemove = new List<OLVObject>();
                
                foreach (OLVObject obj in olv_list.CheckedObjects)
                {
                    focus_list.Add(obj);
                    olv_focus.AddObject(obj);
                    olv_focus.RefreshObject(obj);
                    toRemove.Add(obj);

                    // Add to focus list app
                    foreach (FocusApp fa in focus_app.Keys)
                    {
                        if (focus_app[fa] == null)
                        {
                            focus_app[fa] = obj;
                            break;
                        }
                    }
                }

                foreach (OLVObject obj in toRemove)
                {
                    olv_list.RemoveObject(obj);
                }
                olv_list.Refresh();

                toolStripStatusLabel1.Text = "Status: " + toRemove.Count + " connections were moved to focus list";

                GC.Collect();
            }
            else
            {
                // There is not enough spot in focus list
                toolStripStatusLabel1.Text = "Error: Too many connections on focus list";
            }
        }

        // Remove checked connection from focus list
        private void remove_from_focus_Click(object sender, EventArgs e)
        {
            if (focus_list.Count > 0)
            {
                List<OLVObject> toRemove = new List<OLVObject>();

                if (olv_focus.CheckedObjects.Count > 0)
                {
                    foreach (OLVObject obj in olv_focus.CheckedObjects)
                    {
                        focus_list.Remove(obj);
                        olv_list.AddObject(obj);
                        olv_list.RefreshObject(obj);
                        toRemove.Add(obj);

                        // Remove from focus app list
                        foreach (FocusApp fa in focus_app.Keys)
                        {
                            if (focus_app[fa] == obj)
                            {
                                focus_app[fa] = null;
                                break;
                            }
                        }
                    }

                    foreach (OLVObject obj in toRemove)
                    {
                        olv_focus.RemoveObject(obj); ;
                    }
                    olv_focus.Refresh();
                }

                toolStripStatusLabel1.Text = "Status: " + toRemove.Count + " connections were moved out of focus list";
            }
            else
            {
                toolStripStatusLabel1.Text = "Error: No connection on focus list";
            }
        }

        // Remove all connection and force the application to check again
        private void flush_btn_Click(object sender, EventArgs e)
        {
            this.Interrupt();
            while (connection.IsAlive)
            {
                Thread.Sleep(100);
            }

            toolStripStatusLabel1.Text = "Status: All connections flushed";

            olv_list.Items.Clear();
            olv_focus.Items.Clear();

            focus_app.Clear();

            GC.Collect();

            init();
        }

        #endregion

        #region Option thread

        private void run_option()
        {
            Application.Run(new Option(IP_lib, option));
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            option_thread = new Thread(run_option);
            option_thread.SetApartmentState(ApartmentState.STA);
            option_thread.Start();
        }

        #endregion

        #region About thread
        private void about()
        {
            Application.Run(new About());
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about_thread = new Thread(about);
            about_thread.SetApartmentState(ApartmentState.STA);
            about_thread.Start();
        }
        #endregion

    }

    public class TCPAsync
    {
        // port number
        private const int port = 10;

        // ManualResetEvent instances signal completeion
        private ManualResetEvent connectDone;
        private ManualResetEvent sendDone;
        private ManualResetEvent receiveDone;

        // Response from server
        private String response = String.Empty;

        private IPAddress myIP;                                 // My IP Address

        private BrightIdeasSoftware.ObjectListView olv_list;    // Object list view from main screen
        private BrightIdeasSoftware.ObjectListView olv_focus;   // Object list view of focus list from main screen
        private Dictionary<String, OLVObject> lookup;           // Map to look up all OLVObject
        private Dictionary<FocusApp, OLVObject> focus_app;      // List of focus app along with its corresponding OLVObject
        private Dictionary<String, List<OLVObject>> ip_map;     // Map to look up server's ip with its associated connection
        private List<OLVObject> focus_list;                     // List of OLVObject in focus list
        private ToolStripLabel status;                          // Status bar

        public TCPAsync(IPAddress address, DataRetriever dr)
        {
            connectDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
            receiveDone = new ManualResetEvent(false);

            myIP = address;
            olv_list = dr.getOlvList();
            olv_focus = dr.getOlvFocus();
            lookup = dr.lookup;
            focus_list = dr.focus_list;
            status = dr.getStatus();
            focus_app = dr.focus_app;
            ip_map = dr.ip_map;
        }

        #region TCP Section
        public void StartClient()
        {
            // Connect
            IPAddress ipAddress = myIP;
            try
            {
                // Establish connection
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create TCP/IP Socket
                Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to endpoint
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallBack), client);
                connectDone.WaitOne();

                status.Text = "Status: Connection accepted from " + client.RemoteEndPoint;

                // Add to ip map
                if (!ip_map.Keys.Contains(ipAddress.ToString()))
                {
                    ip_map.Add(ipAddress.ToString(), new List<OLVObject>());
                }

                // Send number of lines we want to send
                String toSend = "RQ_DATA\n<EOF>";

                Send(client, toSend);
                sendDone.WaitOne();

                // Receive Response from server
                Receive(client);
                receiveDone.WaitOne();

                // Write response to object list view
                String[] data = response.Split('\n');

                foreach (String info in data)
                {
                    String[] toWrite = info.Split('\t');
                    if (toWrite.Length == 3)
                    {
                        // if doesn't contain then just add
                        if (!lookup.Keys.Contains(toWrite[0] + toWrite[1]))
                        {
                            OLVObject obj = new OLVObject(toWrite[0], toWrite[1], toWrite[2]);
                            lookup.Add(toWrite[0] + toWrite[1], obj);
                            ip_map[ipAddress.ToString()].Add(obj);
                            olv_list.AddObject(obj);
                            olv_list.RefreshObject(obj);
                        }
                        else
                        {
                            OLVObject obj = lookup[toWrite[0] + toWrite[1]];
                            obj.status = toWrite[2];
                            
                            if (focus_list.Contains(obj))
                            {
                                olv_focus.RefreshObject(obj);
                            }
                            else
                            {
                                olv_list.RefreshObject(obj);
                            }

                            foreach (FocusApp fa in focus_app.Keys)
                            {
                                if (fa.Visible && focus_app[fa].Equals(obj))
                                {
                                    fa.setLocation(-1, obj);
                                    break;
                                }
                            }
                        }
                    }
                }

                // Clear old disconnected connection
                foreach (OLVObject obj in ip_map[ipAddress.ToString()])
                {
                    if (obj.status.Equals(DataRetriever.Disconnected_String))
                    {
                        // Remove all references except in focus lis
                        if (!focus_list.Contains(obj))
                        {
                            olv_list.RemoveObject(obj);
                            lookup.Remove(obj.name + obj.ip);
                        }
                    }
                }

                olv_list.BeginInvoke((MethodInvoker)(() => olv_list.Refresh()));

                GC.Collect();

                status.Text = "Status: Close connection with " + client.RemoteEndPoint;

                // Release the socket
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch
            {
                status.GetCurrentParent().BeginInvoke((MethodInvoker) (() => status.Text = "Error: Either connection is rejected or server is offline"));

                // change status to disconnect
                if (ip_map.Keys.Contains(ipAddress.ToString()))
                {
                    foreach (OLVObject obj in ip_map[ipAddress.ToString()])
                    {
                        obj.status = DataRetriever.Disconnected_String;

                        if (focus_list.Contains(obj))
                        {
                            olv_focus.RefreshObject(obj);
                        }
                        else
                        {
                            olv_list.RefreshObject(obj);
                        }

                        foreach (FocusApp fa in focus_app.Keys)
                        {
                            if (fa.Visible && focus_app[fa].Equals(obj))
                            {
                                fa.setLocation(-1, obj);
                                break;
                            }
                        }
                    }
                }

                GC.Collect();
            }

            GC.Collect();
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                // Retrieve data from state object
                Socket client = (Socket)ar.AsyncState;

                // Complete connection
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that connection has been made
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                if (connectDone != null)
                {
                    connectDone.Set();
                }
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create state object
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from server
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallBack), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                // Retrieve state object and the client socket
                // from asynchronous state object
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from  the server
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallBack), state);
                }
                else
                {
                    // All the data has been received
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }

                    // Signal that all the data has been received
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                receiveDone.Set();
            }
        }

        private void Send(Socket client, String data)
        {
            // Convert string data to byte using ASCII encoding
            byte[] bytesData = Encoding.ASCII.GetBytes(data);

            // Begin sending data to server
            client.BeginSend(bytesData, 0, bytesData.Length, 0,
                new AsyncCallback(SendCallBack), client);
        }

        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                // Retrieve the data from the state object
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to server
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Send {0} bytes to server", bytesSent);

                // signal that all bytes have been sent
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sendDone.Set();
            }
        }
        #endregion
    }

    #region Custom object

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class OLVObject
    {
        public String name { get; set; }
        public String ip { get; set; }
        public String status { get; set; }

        public OLVObject(String n, String i, String s)
        {
            name = n;
            ip = i;
            status = s;
        }

        public Boolean Equals(OLVObject obj)
        {
            return name.Equals(obj.name) && ip.Equals(obj.ip);
        }
    }

    public class Option_Object
    {
        public Boolean show_system_tray { get; set; }
        public Boolean close_to_system_tray { get; set; }
        public Boolean min_to_system_tray { get; set; }
        public int recon_interval { get; set; }
        public int max_focus { get; set; }
    }

    #endregion
}
