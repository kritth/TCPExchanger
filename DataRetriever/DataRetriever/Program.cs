using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataRetriever
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
                    Application.Run(new DataRetriever());
                }
            }
        }

        static String _mutexID = "aroiew1312-12h4oh1i2o-12o3ih12hashdsal";
    }
}
