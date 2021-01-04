using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class ProcessVariables
    {
        /*
         * Used to not make the main function too disorganized
         * */

        public static  ProcessStartInfo GetClientVariable()
        {
            var clientpath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "client" + Path.DirectorySeparatorChar + "Client.exe";

            return new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                CreateNoWindow = false,
                FileName = clientpath,
                UseShellExecute = true

            };
        }


        public static ProcessStartInfo GetServerVariable()
        {
            var serverpath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "server" + Path.DirectorySeparatorChar + "Server.exe";

            return new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                CreateNoWindow = false,
                FileName = serverpath,
                UseShellExecute = true

            };

        }
    }
}
