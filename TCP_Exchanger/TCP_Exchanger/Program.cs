using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Exchanger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boolean _isNotRunning;

            using (Mutex _mutex = new Mutex(true, _mutexID, out _isNotRunning))
            {
                if (_isNotRunning)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Controller());
                }
            }
        }

        static String _mutexID = "werhawo-12312-1faew-r12123";
    }
}
