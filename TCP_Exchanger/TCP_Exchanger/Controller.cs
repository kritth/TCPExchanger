using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace TCP_Exchanger
{
    public partial class Controller : Form
    {
        // Change this into the window form
        private Thread connectionThread;
        private Thread searchThread;
        private Thread addExistingThread;
        private Thread addThread;
        private Thread editThread;
        private Thread ipFinderThread;
        private Thread portScannerThread;
        private Thread controllerThread;
        private Thread hostServerThread;
        private Thread aboutThread;
        private Thread removeThread;

        private readonly List<ConnectionData> data = new List<ConnectionData>();

        private readonly SearchSystem so = new SearchSystem();
        private readonly Dictionary<String, ConnectionData> lookup = new Dictionary<String, ConnectionData>();

        // For passing to AddNew and extract data after finish filling up info
        private ConnectionData forAdding;
        private AddNew addNew;

        private AddExisting addExisting;

        private AdvancedSearch advancedSearch;

        private Confirm_Remove remove;

        private Edit edit;

        private ConnectionQueue connection;

        private IPGetter ipGetter;

        private Server server;

        private Thread logThread;
        private Logger logger;

        public Controller()
        {
            InitializeComponent();
            init();
            this.Visible = true;
        }

        private void init()
        {
            // custom mouse drag
            menuStrip1.MouseDown += new MouseEventHandler(this.menuStrip_MouseDown);
            toolStrip1.MouseDown += new MouseEventHandler(this.menuStrip_MouseDown);

            minimize_btn.BackColor = exit_btn.BackColor = Color.Transparent;

            this.Text = "Connection Checker";
        }

        #region Custom internal methods
        
        private void internal_init()
        {
            start_log();

            // Wait until logger is accessible
            while (logger == null)
            {
                // Do nothing
            }

            while (!logger.Visible)
            {
            }
            
            logger.BeginInvoke((MethodInvoker)(() => logger.Visible = false));
            logger.Log("Application started.", null);

            connection = new ConnectionQueue(statusBarText, olv_list, logger);
            connectionThread = new Thread(connection.run);
            connectionThread.Start();

            toolStrip1.Text = "Test";

            // Debug();
        }

        private void Debug()
        {
            add_to_olv(new ConnectionData("Line Cookie Server", "125.209.222.88", "10010", "rm"), false);
            add_to_olv(new ConnectionData("Google", "203.144.132.78", "80"), false);
            add_to_olv(new ConnectionData("ROV Server", "103.4.157.135", "8000"), false);
            add_to_olv(new ConnectionData("Random", "1.1.3.1", "1311", TCPStatus.Listen, "dir"), false);
        }

        private void update_olv()
        {
            // set all object if is searching all
            if (!so.multipleSearch() && olv_list.GetItemCount() != data.Count)
            {
                olv_list.SetObjects(data);
            }
            else if (so.multipleSearch())
            {
                List<ConnectionData> addingList = new List<ConnectionData>();
                Boolean search_switch = false;                      // True when a search condition is met
                Boolean found_candidate = true;                     // False if does not meet criteria
                foreach (ConnectionData cd in data)
                {
                    if (so.Search_Quick)
                    {
                        // Quick search
                        search_switch = cd.remote_ip.Contains(so.quick)
                            || cd.remote_port.Contains(so.quick)
                            || cd.status.ToString().Contains(so.quick)
                            || cd.availability.ToString().Contains(so.quick)
                            || cd.command.Contains(so.quick)
                            || cd.name.Contains(so.quick);
                    }
                    else
                    {
                        // Search IP
                        if (so.Search_IP)
                        {
                            if (!search_switch)
                            {
                                search_switch = cd.remote_ip.Contains(so.ip);
                            }
                            else
                            {
                                if (found_candidate)
                                {
                                    found_candidate = cd.remote_ip.Contains(so.ip);
                                }
                            }
                        }
                        // Search port
                        if (so.Search_Port)
                        {
                            if (!search_switch)
                            {
                                search_switch = cd.remote_port.Contains(so.port);
                            }
                            else
                            {
                                if (found_candidate)
                                {
                                    found_candidate = cd.remote_port.Contains(so.port);
                                }
                            }
                        }
                        // Search TCP Status
                        if (so.Search_Status)
                        {
                            if (!search_switch)
                            {
                                search_switch = cd.status.Equals(so.status);
                            }
                            else
                            {
                                if (found_candidate)
                                {
                                    found_candidate = cd.status.Equals(so.status); ;
                                }
                            }
                        }
                        // Search Availability
                        if (so.Search_Availability)
                        {
                            if (!search_switch)
                            {
                                search_switch = cd.availability.Equals(so.availability);
                            }
                            else
                            {
                                if (found_candidate)
                                {
                                    found_candidate = cd.availability.Equals(so.availability);
                                }
                            }
                        }
                        // Search command
                        if (so.Search_Command)
                        {
                            if (!search_switch)
                            {
                                search_switch = cd.command.Contains(so.command);
                            }
                            else
                            {
                                if (found_candidate)
                                {
                                    found_candidate = cd.command.Contains(so.command);
                                }
                            }
                        }
                    }
                    // If found in search and meet all criteria
                    if (search_switch && found_candidate)
                    {
                        addingList.Add(cd);
                    }
                }

                // If has more than one, set objects to addingList, else just collect
                if (addingList.Count > 0 && addingList.Count != olv_list.GetItemCount())
                {
                    olv_list.SetObjects(addingList);
                }
            }

            GC.Collect();
        }

        private void bring(Form f)
        {
            if (f != null && !f.IsDisposed)
            {
                f.BeginInvoke((MethodInvoker)(() => f.WindowState = FormWindowState.Normal));
                f.BeginInvoke((MethodInvoker)(() => f.Activate()));
            }
        }

        private void front()
        {
            bring(logger);
            bring(addNew);
            bring(addExisting);
            bring(advancedSearch);
            bring(edit);
            bring(ipGetter);
            bring(portScanner);
            bring(server);
        }
        #endregion

        #region Custom Override
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            internal_init();
        }

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

        protected override void OnClientSizeChanged(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                front();
            }

            base.OnClientSizeChanged(e);
        }
        #endregion

        #region Custom event handler
        private void exit_Click(object sender, EventArgs e)
        {
            connection.Dispose();
            logThread.Interrupt();
            if (controllerThread != null && controllerThread.IsAlive)
            {
                controllerThread.Abort();
            }

            Environment.Exit(0);
            Application.Exit();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void menuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        // Clear all the items from list
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            data.Clear();
            lookup.Clear();
            olv_list.Items.Clear();
        }

        #endregion

        #region Add to list code section

        #region Internal adding methods
        // Add one to olv
        private void add_to_olv(ConnectionData cmp, Boolean requireUpdate)
        {
            foreach (ConnectionData original in data)
            {
                if (cmp.Equals(original))
                {
                    return;
                }
            }

            data.Add(cmp);
            olv_list.AddObject(cmp);
            connection.Add(cmp);
            lookup.Add(cmp.remote_ip + ":" + cmp.remote_port, cmp);


            if (requireUpdate)
            {
                update_olv();
            }
        }

        // Add list to olv
        private void add_to_olv(List<ConnectionData> cmp)
        {
            // Iterately compare if new data exist in the original lib
            foreach (ConnectionData toAdd in cmp)
            {
                add_to_olv(toAdd, false);
            }

            update_olv();
        }
        #endregion

        #region External adding methods: AddNew thread
        
        private void add_new_thread()
        {
            if (forAdding != null)
            {
                Application.Run(addNew = new AddNew(forAdding));
            }
        }

        private void add_new_moniter()
        {
            forAdding = new ConnectionData();
            // Create new thread to add new item
            addThread = new Thread(add_new_thread);
            addThread.SetApartmentState(ApartmentState.STA);
            addThread.Start();

            while (addThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            // Add new data to lib
            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                add_to_olv(forAdding, true);
            }
            forAdding = null;

            GC.Collect();
        }

        private void add_new_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(add_new_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }

        // Call AddNew from menu strip
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_new_Click(sender, e);
        }
        #endregion

        #region External adding methods: AddExisting thread

        private void add_existing_thread()
        {
            if (forAdding != null)
            {
                Application.Run(addExisting = new AddExisting(forAdding));
            }
        }

        private void add_existing_moniter()
        {
            forAdding = new ConnectionData();
            // Create new thread to add new item
            addExistingThread = new Thread(add_existing_thread);
            addExistingThread.SetApartmentState(ApartmentState.STA);
            addExistingThread.Start();

            while (addExistingThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                addThread = new Thread(add_new_thread);
                addThread.SetApartmentState(ApartmentState.STA);
                addThread.Start();

                while (addThread.IsAlive)
                {
                    Thread.Sleep(1000);
                }
            }

            // Add new data to lib
            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                add_to_olv(forAdding, true);
            }
            forAdding = null;

            GC.Collect();
        }

        private void add_existing_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(add_existing_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }

        #endregion

        #endregion

        #region Remove from list code section
        public void remove_from_olv(ConnectionData cmp, Boolean requireUpdate)
        {
            String key = cmp.remote_ip + ":" + cmp.remote_port;

            if (connection.Remove(lookup[key]))
            {
                data.Remove(lookup[key]);
                if (InvokeRequired)
                {
                    olv_list.BeginInvoke((MethodInvoker)(() => olv_list.RemoveObject(lookup[key])));
                }
                else
                {
                    olv_list.RemoveObject(lookup[key]);
                }
                lookup.Remove(key);
            }

            if (requireUpdate)
            {
                if (InvokeRequired)
                {
                    olv_list.BeginInvoke((MethodInvoker)(() => update_olv()));
                }
                else
                {
                    update_olv();
                }
            }
        }

        private void remove_thread()
        {
            Application.Run(remove = new Confirm_Remove("Do you really want to remove connections?"));
        }

        private void remove_selected()
        {
            foreach (ConnectionData cd in olv_list.CheckedObjects)
            {
                remove_from_olv(cd, false);
            }

            update_olv();

            GC.Collect();
        }

        private void remove_selected_moniter()
        {
            // Create new thread to add new item
            removeThread = new Thread(remove_thread);
            removeThread.SetApartmentState(ApartmentState.STA);
            removeThread.Start();

            while (removeThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            if (remove.Confirm)
            {
                olv_list.BeginInvoke((MethodInvoker)(() => remove_selected()));
            }

            GC.Collect();
        }

        // Remove selected from lt
        private void remove_selected_Click(object sender, EventArgs e)
        {
            if (olv_list.CheckedObjects.Count > 0)
            {
                if (controllerThread == null || !controllerThread.IsAlive)
                {
                    try
                    {
                        controllerThread = new Thread(remove_selected_moniter);
                        controllerThread.Start();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    statusBarText.Text = "Error: Only one window can be opened at a time.";
                    this.front();
                }
            }
        }

        private void remove_all()
        {
            List<ConnectionData> toDelete = data.ToList<ConnectionData>();
            foreach (ConnectionData cd in toDelete)
            {
                remove_from_olv(cd, false);
            }

            update_olv();

            GC.Collect();
        }

        private void remove_all_moniter()
        {
            // Create new thread to add new item
            removeThread = new Thread(remove_thread);
            removeThread.SetApartmentState(ApartmentState.STA);
            removeThread.Start();

            while (removeThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            if (remove.Confirm)
            {
                olv_list.BeginInvoke((MethodInvoker)(() => remove_all()));
            }

            GC.Collect();
        }

        // Remove all from list
        private void remove_all_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(remove_all_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }
        #endregion

        #region Search code section

        #region Internal Search methods

        private void quick_search_btn_Click(object sender, EventArgs e)
        {
            if (!this.quick_search_box.Text.Equals(String.Empty))
            {
                this.so.Clear();

                this.so.Search_Quick = true;
                this.so.quick = quick_search_box.Text;
            }
            else
            {
                this.so.Clear();
            }

            update_olv();
        }

        #endregion

        #region External Search methods: AdvancedSearch Thread

        private void advanced_search_thread()
        {
            Application.Run(advancedSearch = new AdvancedSearch(so));
        }

        private void search_moniter()
        {
            // Create new thread to add new item
            searchThread = new Thread(advanced_search_thread);
            searchThread.SetApartmentState(ApartmentState.STA);
            searchThread.Start();

            while (searchThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            // Search
            update_olv();

            GC.Collect();
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(search_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            search_Click(sender, e);
        }

        #endregion

        #endregion

        #region Edit code section

        private void edit_thread()
        {
            Application.Run(edit = new Edit(lookup));
        }

        private void edit_moniter()
        {
            // Create new thread to edit new item
            editThread = new Thread(edit_thread);
            editThread.SetApartmentState(ApartmentState.STA);
            editThread.Start();

            while (editThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            // Edit
            update_olv();

            GC.Collect();
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(edit_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }

        #endregion

        #region Connection code section

        private void connection_btn_Click(object sender, EventArgs e)
        {
            // This function does not stop the application
            // Create new thread to add new item
            connection.Resume();

            GC.Collect();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            connection.Resume();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            connection.Suspend();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (olv_list.CheckedObjects.Count > 0)
            {
                List<ConnectionData> priority = new List<ConnectionData>();
                foreach (ConnectionData cd in olv_list.CheckedObjects)
                {
                    logger.Log("[" + cd.remote_ip + ":" + cd.remote_port + "] " + cd.name + " has been prioritized.", cd);
                    priority.Add(cd);
                }
                connection.prioritize_queue(priority);
            }
        }

        #endregion

        #region IP Finder code section

        private void ip_finder_thread()
        {
            if (forAdding != null)
            {
                Application.Run(ipGetter = new IPGetter(forAdding));
            }
        }

        private void ip_finder_moniter()
        {
            forAdding = new ConnectionData();
            // Create new thread to find ip
            ipFinderThread = new Thread(ip_finder_thread);
            ipFinderThread.SetApartmentState(ApartmentState.STA);
            ipFinderThread.Start();

            while (ipFinderThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                addThread = new Thread(add_new_thread);
                addThread.SetApartmentState(ApartmentState.STA);
                addThread.Start();

                while (addThread.IsAlive)
                {
                    // Wait for new thread to finish
                    Thread.Sleep(1000);
                }
            }

            // Add new data to lib
            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                add_to_olv(forAdding, true);
            }
            forAdding = null;

            GC.Collect();
        }

        private void ipFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(ip_finder_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }
        #endregion

        #region Port scanner section

        private PortScanner portScanner;

        private void port_scanner_thread()
        {
            if (forAdding != null)
            {
                Application.Run(portScanner = new PortScanner(forAdding));
            }
        }

        private void port_scanner_moniter()
        {
            forAdding = new ConnectionData();
            // Create new thread to find ip
            portScannerThread = new Thread(port_scanner_thread);
            portScannerThread.SetApartmentState(ApartmentState.STA);
            portScannerThread.Start();

            while (portScannerThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                addThread = new Thread(add_new_thread);
                addThread.SetApartmentState(ApartmentState.STA);
                addThread.Start();

                while (addThread.IsAlive)
                {
                    // Wait for new thread to finish
                    Thread.Sleep(1000);
                }
            }

            // Add new data to lib
            if (!forAdding.remote_ip.Equals(String.Empty))
            {
                add_to_olv(forAdding, true);
            }
            forAdding = null;

            GC.Collect();
        }

        private void portScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(port_scanner_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }
        #endregion

        #region About section
        private void about_thread()
        {
            Application.Run(new About());
        }

        private void about_moniter()
        {
            // Create new thread
            aboutThread = new Thread(about_thread);
            aboutThread.SetApartmentState(ApartmentState.STA);
            aboutThread.Start();

            while (aboutThread.IsAlive)
            {
                // Wait for new thread to finish
                Thread.Sleep(1000);
            }

            GC.Collect();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (controllerThread == null || !controllerThread.IsAlive)
            {
                try
                {
                    controllerThread = new Thread(about_moniter);
                    controllerThread.Start();
                }
                catch
                {
                }
            }
            else
            {
                statusBarText.Text = "Error: Only one window can be opened at a time.";
                this.front();
            }
        }
        #endregion

        #region TCP Async section

        private void host_server_thread()
        {
            List<ConnectionData> toHost = new List<ConnectionData>();
            if (olv_list.Objects != null)
            {
                foreach (ConnectionData cd in olv_list.Objects)
                {
                    toHost.Add(cd);
                }
            }
            Application.Run(server = new Server(toHost));
        }

        private void host_server_moniter()
        {
            hostServerThread = new Thread(host_server_thread);
            hostServerThread.SetApartmentState(ApartmentState.STA);
            hostServerThread.Start();

            while (hostServerThread.IsAlive)
            {
                Thread.Sleep(1000);
            }

            GC.Collect();
        }

        private void hostServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (olv_list.Objects != null && connection.Size() > 0)
            {
                if (controllerThread == null || !controllerThread.IsAlive)
                {
                    try
                    {
                        controllerThread = new Thread(host_server_moniter);
                        controllerThread.Start();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    statusBarText.Text = "Error: Only one window can be opened at a time.";
                    this.front();
                }
            }
        }

        #endregion

        #region Importer and Exporter

        #region Importer
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            String file = openFileDialog.FileName;
            try
            {
                String text = File.ReadAllText(file);
                String[] split = text.Split('\n');
                foreach (String s in split)
                {
                    ConnectionData cd = JsonConvert.DeserializeObject<ConnectionData>(s);
                    add_to_olv(cd, false);
                }
            }
            catch
            {
            }

            update_olv();
        }
        #endregion

        #region Exporter
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            String name = saveFileDialog.FileName;
            String[] output = new String[data.Count];

            int i = 0;

            foreach (ConnectionData cd in data)
            {
                output[i] = JsonConvert.SerializeObject(cd);
                i++;
            }

            System.IO.File.WriteAllLines(name, output);
        }
        #endregion

        #endregion

        #region Log thread
        
        private void log_thread()
        {
            try
            {
                Application.Run(logger = new Logger(this));
            }
            catch
            {
                // Do nothing
            }
        }

        private void start_log()
        {
            logThread = new Thread(log_thread);
            logThread.SetApartmentState(ApartmentState.STA);
            logThread.Start();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (logThread.IsAlive)
            {
                logger.BeginInvoke((MethodInvoker)(() => logger.Visible = true));
            }
        }

        #endregion

    }

    #region Custom enum and helper classes
    
    public enum NetworkAvailability
    {
        All, Online, Wait_For_Test, Offline, No_Response
        // All should be used in search option only
    }

    public enum TCPStatus
    {
        // All should be used in search option only
        All, Unknown, Closed, Listen, SynSent, SynReceived, Established
        , FinWait1, FinWait2, CloseWait, Closing, LastAck, TimeWait, DeleteTcb, ExternalIP
    }

    public class Helper
    {
        public static NetworkAvailability toNetworkAvailability(String s)
        {
            foreach (NetworkAvailability value in Enum.GetValues(typeof(NetworkAvailability)))
            {
                if (s.Equals(value.ToString()))
                {
                    return value;
                }
            }

            return NetworkAvailability.All;
        }

        public static TCPStatus toStatus(String s)
        {
            foreach (TCPStatus value in Enum.GetValues(typeof(TCPStatus)))
            {
                if (s.Equals(value.ToString()))
                {
                    return value;
                }
            }

            return TCPStatus.All;
        }
    }
    #endregion

    #region Custom Object
    public class ConnectionData
    {
        private const String std_command = "ls";

        // Remote IP
        public String remote_ip { get; set; }
        // Remote Port
        public String remote_port { get; set; }
        // TCP Status
        public TCPStatus status { get; set; }
        // Connection availability
        public NetworkAvailability availability { get; set; }
        // Command to put through telnet
        public String command { get; set; }
        // Name of the connection
        public String name { get; set; }

        public ConnectionData()
        {
            status = TCPStatus.ExternalIP;
            availability = NetworkAvailability.Wait_For_Test;
            remote_ip = String.Empty;
            remote_port = String.Empty;
            command = std_command;
            name = "No name";
        }

        #region Constructor
        public ConnectionData(String n)
            : this()
        {
            name = n;
        }

        public ConnectionData(String n, String ip, String port)
            : this(n)
        {
            remote_ip = ip;
            remote_port = port;
        }

        public ConnectionData(String n, String ip, String port, String cmd)
            : this (n, ip, port)
        {
            command = cmd;
        }

        public ConnectionData(String n, String ip, String port, TCPStatus stat, String cmd)
            : this(n, ip, port, stat)
        {
            command = cmd;
        }

        public ConnectionData(String n, String ip, String port, TCPStatus stat)
            : this(n, ip, port)
        {
            status = stat;
        }
        #endregion

        public Boolean Equals(ConnectionData cmp)
        {
            return remote_ip.Equals(cmp.remote_ip) && remote_port.Equals(cmp.remote_port);
        }

        public override string ToString()
        {
            return name + "\t" + remote_ip + ":" + remote_port + "\t" + availability.ToString();
        }

        public String Log()
        {
            String available = "[Status:";
            if (availability == NetworkAvailability.Online
                || availability == NetworkAvailability.Offline)
            {
                available += availability + "] \t";
            }
            else
            {
                available += availability + "]\t";
            }

            String ip_part = "[" + remote_ip + ":" + remote_port + "]";

            if (ip_part.Length < 21)
            {
                while (ip_part.Length < 21)
                {
                    ip_part += " ";
                }
                ip_part += "\t";
            }
            else
            {
                ip_part += "\t";
            }

            return available + ip_part + name;
        }
    }

    public class SearchSystem
    {
        public Boolean Search_IP { get; set; }
        public Boolean Search_Port { get; set; }
        public Boolean Search_Status { get; set; }
        public Boolean Search_Availability { get; set; }
        public Boolean Search_Command { get; set; }
        public Boolean Search_Name { get; set; }
        public Boolean Search_Quick { get; set; }

        public String ip { get; set; }
        public String port { get; set; }
        public TCPStatus status { get; set; }
        public NetworkAvailability availability { get; set; }
        public String command { get; set; }
        public String name { get; set; }
        public String quick { get; set; }

        public SearchSystem()
        {
            Clear();
        }

        // Reset all the search option
        public void Clear()
        {
            Search_IP = false;
            Search_Port = false;
            Search_Status = false;
            Search_Availability = false;
            Search_Command = false;
            Search_Name = false;
            Search_Quick = false;

            reset();
        }

        private void reset()
        {
            ip = "";
            port = "";
            status = TCPStatus.All;
            availability = NetworkAvailability.All;
            command = "";
            name = "";
        }

        public Boolean multipleSearch()
        {
            return Search_IP || Search_Port || Search_Status
                || Search_Availability || Search_Command || Search_Name || Search_Quick;
        }
    }
    #endregion
}