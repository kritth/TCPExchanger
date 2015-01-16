using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using TelnetExpect;

namespace TCP_Exchanger
{
    public class ConnectionQueue
    {
        private Boolean suspend;                                    // Check if the progress is asked to be suspended
        private Boolean terminate;                                  // Check if the process is asked to be terminated

        private System.Windows.Forms.ToolStripStatusLabel status;   // Status bar shown on main screen
        private BrightIdeasSoftware.ObjectListView olv_list;        // Object list view from main screen to update data shown
        private Logger logger;                                      // Logger

        private ConnectionData current_data;                        // Current data that is being executed

        private List<ConnectionData> waiting_list;                  // Connection data that is waiting to be checked
        private List<ConnectionData> finish_list;                   // Connection data that has finished checking

        private Thread running_thread;                              // Thread controlling the termination of this class
        private Thread connection_thread;                           // Thread controlling the connection of this class

        // Constructor
        public ConnectionQueue(System.Windows.Forms.ToolStripStatusLabel ts
            , BrightIdeasSoftware.ObjectListView olv_list
            , Logger logger)
        {
            status = ts;
            this.olv_list = olv_list;
            this.logger = logger;

            suspend = false;
            terminate = false;

            waiting_list = new List<ConnectionData>();
            finish_list = new List<ConnectionData>();
        }

        // Run this class as thread
        public void run()
        {
            running_thread = new Thread(queuing_thread);
            running_thread.Start();
            while (!terminate)
            {
                // Do nothing
                Thread.Sleep(1000);
            }
        }

        // Add connectiondata to list
        public void Add(ConnectionData cd)
        {
            if (!finish_list.Contains(cd) && !waiting_list.Contains(cd))
            {
                waiting_list.Add(cd);
            }
        }

        // Remove connectiondata from the list
        public Boolean Remove(ConnectionData cd)
        {
            Boolean result = false;

            if (waiting_list.Contains(cd))
            {
                result = waiting_list.Remove(cd);
            }
            if (finish_list.Contains(cd))
            {
                result = finish_list.Remove(cd);
            }

            return result;
        }

        // Return the total number of connection data in all queues
        public int Size()
        {
            return waiting_list.Count + finish_list.Count;
        }

        // Give the priority to put connections in the list in the next
        // to be executed
        public void prioritize_queue(List<ConnectionData> list)
        {
            List<ConnectionData> toRemove = new List<ConnectionData>();

            foreach (ConnectionData cd in waiting_list)
            {
                if (!list.Contains(cd))
                {
                    finish_list.Add(cd);
                    toRemove.Add(cd);
                }
            }

            foreach (ConnectionData cd in toRemove)
            {
                waiting_list.Remove(cd);
            }

            toRemove.Clear();

            foreach (ConnectionData cd in finish_list)
            {
                if (list.Contains(cd))
                {
                    toRemove.Add(cd);
                    waiting_list.Add(cd);
                }
            }

            foreach (ConnectionData cd in toRemove)
            {
                finish_list.Remove(cd);
            }

            toRemove.Clear();

            GC.Collect();
        }

        // thread function for queuing and executing telnetconnection
        private void queuing_thread()
        {
            // If terminate is not requested
            while (!terminate)
            {
                // If suspend is not requested
                while (!suspend)
                {
                    // Thread is not running and free to create new thread
                    if (connection_thread == null || !connection_thread.IsAlive)
                    {
                        // Add to finish list
                        if (current_data != null && waiting_list.Contains(current_data))
                        {
                            finish_list.Add(current_data);
                            waiting_list.Remove(current_data);
                            current_data = null;
                        }

                        // Get and run it
                        current_data = waiting_list.FirstOrDefault();
                        if (current_data != null)
                        {
                            status.Text = "Processing: " + current_data.name + " at " + current_data.remote_ip + ":" + current_data.remote_port; 
                            TelnetConnection tc = new TelnetConnection(current_data, olv_list, logger);
                            connection_thread = new Thread(tc.run);
                            connection_thread.Start();
                        }
                    }

                    // Can still loop through available connections
                    if (waiting_list.Count == 0 && finish_list.Count > 0)
                    {
                        foreach (ConnectionData cd in finish_list)
                        {
                            waiting_list.Add(cd);
                        }
                        finish_list.Clear();
                    }

                    // No more connection available
                    if (waiting_list.Count == 0 && finish_list.Count == 0)
                    {
                        if (status != null && status.GetCurrentParent() != null)
                        {
                            status.GetCurrentParent().BeginInvoke(
                                (System.Windows.Forms.MethodInvoker)(() => status.Text = "There is no connection available to test."));
                        }
                    }

                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }
            // Terminate the thread
            if (connection_thread != null && connection_thread.IsAlive)
            {
                while (connection_thread.IsAlive)
                {
                    connection_thread.Abort();
                    Thread.Sleep(1000);
                }
            }
        }

        // Resume the suspended thread
        public void Resume()
        {
            if (suspend)
            {
                suspend = false;
                status.Text = "Resume connection";
            }
        }

        // Suspend the active thread
        public void Suspend()
        {
            if (!suspend)
            {
                suspend = true;
                status.Text = "Suspend connection checker";
            }
        }

        // Dispose the thread
        public void Dispose()
        {
            terminate = true;
            Suspend();
        }
    }

    public class TelnetConnection
    {
        private const int TIMEOUT = 15000;                      // Maximum timeout of the connection
        private TcpClient client;                               // Main client to connect to the client

        private ConnectionData data;                            // Connection data that is being processed
        private BrightIdeasSoftware.ObjectListView olv_list;    // Object list view to be updated
        private Logger logger;                                  // Logger

        // Constructor
        public TelnetConnection(ConnectionData cd, BrightIdeasSoftware.ObjectListView olv_list
            , Logger logger)
        {
            data = cd;
            this.olv_list = olv_list;
            this.logger = logger;
        }

        // Run this class
        public void run()
        {
            data.availability = NetworkAvailability.Wait_For_Test;
            olv_list.RefreshObject(data);
            Thread.Sleep(3000);
            trySend(data.remote_ip, Convert.ToInt32(data.remote_port));
        }

        // This will run as an individual thread to check time out of each operation
        private void interupter()
        {
            try
            {
                Thread.Sleep(TIMEOUT);
                if (client != null)
                {
                    data.availability = NetworkAvailability.No_Response;

                    // Log
                    if (!logger.IsDisposed)
                    {
                        logger.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => logger.Log(data.Log(), data)));
                    }

                    olv_list.RefreshObject(data);
                    client.Close();
                }
            }
            catch (Exception)
            {
            }

            GC.Collect();
        }

        // Try and send command to destination ip and port
        private void trySend(String ip, int port)
        {
            Thread interupt = null;

            try
            {
                interupt = new Thread(interupter);
                interupt.Start();

                client = new TcpClient(ip, port);
                TelnetStream telnet = new TelnetStream(client.GetStream());
                telnet.SetRemoteMode(TelnetOption.Echo, false);

                Expector ex = new Expector(telnet);
                ex.SendLine(data.command);
                byte[] buffer = new byte[1024];

                telnet.Read(buffer, 0, 1024);

                ex.Dispose();
                telnet.Close();
                client.Close();

                data.availability = NetworkAvailability.Online;
                
                // Log
                if (!logger.IsDisposed)
                {
                    logger.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => logger.Log(data.Log(), data)));
                }

                olv_list.RefreshObject(data);
            }
            catch (SocketException)
            {
                if (client != null && client.Connected)
                {
                    client.Close();
                }
                
                data.availability = NetworkAvailability.Offline;

                // Log
                if (!logger.IsDisposed)
                {
                    logger.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => logger.Log(data.Log(), data)));
                }

                olv_list.RefreshObject(data);
            }
            catch (IOException)
            {
                if (client != null && client.Connected)
                {
                    client.Close();
                }
                if (data.availability != NetworkAvailability.No_Response)
                {
                    data.availability = NetworkAvailability.Online;

                    // Log
                    if (!logger.IsDisposed)
                    {
                        logger.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => logger.Log(data.Log(), data)));
                    }

                    olv_list.RefreshObject(data);
                }
            }
            catch (Exception)
            {
                data.availability = NetworkAvailability.Offline;

                // Log
                if (!logger.IsDisposed)
                {
                    logger.BeginInvoke((System.Windows.Forms.MethodInvoker)(() => logger.Log(data.Log(), data)));
                }

                olv_list.RefreshObject(data);
            }

            if (interupt != null)
            {
                while (interupt.IsAlive)
                {
                    Thread.Sleep(1000);
                    interupt.Abort();
                }
            }
        }
    }
}
