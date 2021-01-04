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
            var clientPathWorking = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "client" + Path.DirectorySeparatorChar;

            string clientpath;
            if (OperatingSystem.IsWindows())
            {
                var clientPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "client" + Path.DirectorySeparatorChar + "Client.exe";
                return new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    CreateNoWindow = false,
                    WorkingDirectory = clientPathWorking,
                    FileName = clientPath,
                    UseShellExecute = true

                };
            }
            else
            {
                clientpath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "client" + Path.DirectorySeparatorChar + "Client.dll";
                return new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    CreateNoWindow = false,
                    FileName = "dotnet",
                    WorkingDirectory = clientPathWorking,
                    Arguments = clientpath,
                    UseShellExecute = true

                };

            }

          
        }


        public static ProcessStartInfo GetServerVariable()
        {
            string serverpath;
            var serverPathWorking = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "server" + Path.DirectorySeparatorChar;

            if (OperatingSystem.IsWindows())
            {
                serverpath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "server" + Path.DirectorySeparatorChar + "Server.exe";
                return new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    CreateNoWindow = false,
                    WorkingDirectory = serverPathWorking,
                    FileName = serverpath,
                    UseShellExecute = true

                };

            }

            else
            {
                serverpath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "server" + Path.DirectorySeparatorChar + "Server.dll";
                
                return new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    CreateNoWindow = false,
                    FileName = "dotnet",
                    Arguments = serverpath,
                    WorkingDirectory = serverPathWorking,
                    UseShellExecute = true

                };


            }
           

        }
    }
}
