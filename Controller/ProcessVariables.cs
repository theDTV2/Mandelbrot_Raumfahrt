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
            var clientPathWorking = AppContext.BaseDirectory + "client" + Path.DirectorySeparatorChar;

            string clientpath;
            if (OperatingSystem.IsWindows())
            {
                var clientPath = AppContext.BaseDirectory + "client" + Path.DirectorySeparatorChar + "Client.exe";
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
                clientpath = AppContext.BaseDirectory + "client" + Path.DirectorySeparatorChar + "Client.dll";
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
            var serverPathWorking = AppContext.BaseDirectory + "server" + Path.DirectorySeparatorChar;

            if (OperatingSystem.IsWindows())
            {
                serverpath = AppContext.BaseDirectory + "server" + Path.DirectorySeparatorChar + "Server.exe";
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
                serverpath = AppContext.BaseDirectory + "server" + Path.DirectorySeparatorChar + "Server.dll";

                System.Console.WriteLine("Using Path: " + serverpath);
                System.Console.WriteLine("Working Directory: " + serverPathWorking);

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
