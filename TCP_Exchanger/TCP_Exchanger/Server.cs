using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TCP_Exchanger
{
    // This class is responsible for monitering the connection coming into the server
    public partial class Server : Form
    {
        private Thread listening_thread;                // Thread to run ASyncServer in listening condition
        private ASyncServer aserver;                    // Store the object to listen for connection
        public List<ConnectionData> olv_lib;            // List of connection data passed down from main screen
                                                        // to send to clients

        public Server(List<ConnectionData> olv_lib)
        {
            InitializeComponent();

            this.olv_lib = olv_lib;
        }

        #region Internal Methods
        // Update the list to a new one in case the list from main screen has been cleared
        public void update(List<ConnectionData> list)
        {
            olv_lib = list;
            aserver.update();

            GC.Collect();
        }

        // Get current time in string format to log
        private String getTime()
        {
            return "[" + DateTime.Now.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH:mm:ss") + "] ";
        }

        // Log the information
        public void Log(String s)
        {
            if (log_box.InvokeRequired)
            {
                log_box.BeginInvoke((MethodInvoker)(() => log_box.AppendText(getTime() + s + "\n")));
            }
            else
            {
                log_box.AppendText(getTime() + s + "\n");
            }
        }
        #endregion

        #region Custom Event handler
        private void start_btn_Click(object sender, EventArgs e)
        {
            if (listening_thread == null || !listening_thread.IsAlive)
            {
                start_btn.Enabled = false;
                stop_btn.Enabled = true;

                // Start the thread
                aserver = new ASyncServer(this);
                listening_thread = new Thread(aserver.StartListening);
                listening_thread.Start();
            }
        }

        private void done_btn_Click(object sender, EventArgs e)
        {
            if (aserver != null)
            {
                aserver.Interrupt();
            }
            if (listening_thread != null)
            {
                while (listening_thread.IsAlive)
                {
                    Thread.Sleep(1000);
                }
            }
            this.Dispose();
        }

        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            if (listening_thread.IsAlive)
            {
                start_btn.Enabled = true;
                stop_btn.Enabled = false;

                aserver.Interrupt();
            }
        }
        #endregion

        #region Custom Override Methods
        // Allow user to move borderless window form around
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

    // This class is responsible for connecting with the connection
    // and send information and update log
    public class ASyncServer
    {
        private const int MAX_CONNECTION = 10;          // Maximum concurrent connections that could happen

        private ManualResetEvent allDone = new ManualResetEvent(false); // Signaller if the ready to take a new connection
        private Server data;                            // data from Server class
        private String toSend;                          // String that will be sent to the clients
        private IPAddress myIP;                         // My IP
        private Boolean interrupt;                      // Indicate that the process will be interrupted
        private Socket listener;                        // Socket responsible to listen

        public ASyncServer(Server toGetData)
        {
            data = toGetData;
            interrupt = false;

            update();
        }

        #region Internal Methods
        // Update the string to send to clients
        public void update()
        {
            toSend = String.Empty;

            foreach (ConnectionData cd in data.olv_lib)
            {
                toSend += cd.ToString() + "\n";
            }
        }

        // Interrupt the server
        public void Interrupt()
        {
            interrupt = true;

            if (listener.Connected)
            {
                listener.Shutdown(SocketShutdown.Both);
            }

            allDone.Set();
        }

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
        #endregion

        #region Connection Methods
        public void StartListening()
        {
            // Get local ip
            myIP = LocalIP();

            // data buffer
            byte[] bytes = new byte[StateObject.BufferSize];

            // Establish local end point
            IPEndPoint localEndPoint = new IPEndPoint(myIP, 10);

            // Create TCP/IP socket
            listener = new Socket(AddressFamily.InterNetwork
                , SocketType.Stream, ProtocolType.Tcp);

            // Bind socket to local end point to listen to all incoming connection
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(MAX_CONNECTION);
                data.Log("Server started");

                while (!interrupt)
                {
                    // Reset to non-signal state
                    allDone.Reset();

                    // Update string to send
                    this.update();

                    // Start async
                    listener.BeginAccept(new AsyncCallback(AcceptCallBack), listener);

                    // Wait til a connection has been made
                    allDone.WaitOne();
                }

                if (listener.Connected)
                {
                    listener.Shutdown(SocketShutdown.Both);
                }
                listener.Close();

                data.Log("Server stopped.");
            }
            catch (Exception e)
            {
                // Add to log file
                data.Log("Server crashed with message: " + e.ToString());
                
                if (listener.Connected)
                {
                    listener.Shutdown(SocketShutdown.Both);
                }
                listener.Close();
            }
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            // Signal main thread to continue
            allDone.Set();

            // Get socket to handle request
            Socket listener = (Socket)ar.AsyncState;
            if (interrupt && !listener.Connected)
            {
                return;
            }
            Socket handler = listener.EndAccept(ar);

            try
            {
                // Create state object
                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0
                    , new AsyncCallback(ReadCallBack), state);
            }
            catch
            {
                // Add to log
                data.Log("Server lost connection with [" + handler.RemoteEndPoint.ToString() + "] at AcceptCallBack");
            }
        }

        private void ReadCallBack(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve state object and handler socket
            // from async state object
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data
            int bytesRead = handler.EndReceive(ar);

            String[] ip = handler.RemoteEndPoint.ToString().Split(':');

            try
            {
                if (bytesRead > 0)
                {
                    // There might be more data
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for eof tag
                    content = state.sb.ToString();

                    if (content.IndexOf("<EOF>") > -1)
                    {
                        // All data has been read
                        if (content.Contains("RQ_DATA"))
                        {
                            data.Log("Server accepted connection from [" + handler.RemoteEndPoint.ToString() + "]");
                            // Send acknowledgement
                            Acknowledge(handler);
                        }
                        else
                        {
                            data.Log("Server rejected connection from [" + handler.RemoteEndPoint.ToString() + "]");

                            try
                            {
                                handler.Shutdown(SocketShutdown.Both);
                                handler.Close();
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            // Not all has been read
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0
                                , new AsyncCallback(ReadCallBack), state);
                        }
                        catch
                        {
                            // Add to log file
                            data.Log("Server lost connection with [" + handler.RemoteEndPoint.ToString() + "] at ReadCallBack");
                        }
                    }
                }
            }
            catch
            {
                data.Log("Connection aborted");
            }
        }

        private void Acknowledge(Socket handler)
        {
            // Convert to byte
            byte[] byteData = Encoding.ASCII.GetBytes(toSend);

            try
            {
                // Begin send data
                handler.BeginSend(byteData, 0, byteData.Length, 0
                    , new AsyncCallback(SendCallBack), handler);
            }
            catch
            {
                // Add to log file
                data.Log("Server lost connection with [" + handler.RemoteEndPoint.ToString() + "] at Acknowledge");
            }
        }

        private void SendCallBack(IAsyncResult ar)
        {
            // Retrieve socket from ar
            Socket handler = (Socket)ar.AsyncState;
            String address = String.Empty + handler.RemoteEndPoint;

            try
            {
                // Complete sending data
                int bytesSent = handler.EndSend(ar);
                data.Log("Server sent data to [" + address + "]");

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                data.Log("Server disconnected with [" + address + "]");
            }
            catch
            {
                data.Log("Server disconnected with [" + address + "]");
            }
        }
        #endregion
    }

    #region Custom Objects
    public class ExternalConnection
    {
        public String client_ip { get; set; }
        public String client_port { get; set; }
        public String endpoint_ip { get; set; }
        public String endpoint_port { get; set; }
        public NetworkAvailability availability { get; set; }

        public ExternalConnection(String cip, String cp
            , String eip, String ep, NetworkAvailability avail)
        {
            client_ip = cip;
            client_port = cp;
            endpoint_ip = eip;
            endpoint_port = ep;
            availability = avail;
        }

        public Boolean Equals(ExternalConnection ex)
        {
            return ex.client_ip.Equals(client_ip) && ex.client_port.Equals(client_port)
                && ex.endpoint_ip.Equals(endpoint_ip) && ex.endpoint_port.Equals(endpoint_port)
                && ex.availability.Equals(availability);
        }

        public override string ToString()
        {
            return client_ip + ":" + client_port + " to " + endpoint_ip + ":" + endpoint_port;
        }
    }

    public class StateObject
    {
        // Client socket
        public Socket workSocket = null;
        // Size of receive buffer
        public const int BufferSize = 1024;
        // Receive buffer
        public byte[] buffer = new byte[BufferSize];
        // Received data string
        public StringBuilder sb = new StringBuilder();
    }
    #endregion
}
