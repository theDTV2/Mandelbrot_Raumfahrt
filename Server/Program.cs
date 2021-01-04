using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Server;
using System.Reflection;



namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            System.Console.WriteLine("Server, Version " + version);


            TcpListener server;
            if (args.Length == 0)
                server = new TcpListener(IPAddress.Any, 442);
            else
                server = new TcpListener(IPAddress.Any, Convert.ToInt32(args[0]));

            List<Thread> networkWorkers = new List<Thread>();
            int threadcount = 0;
            server.Start();

            DataWriter.SetUpFile();
            Thread FileWorker = new Thread(() => DataWriter.FileWriteWorker());
            FileWorker.Start();

            while (true)
            {
                
                Console.WriteLine("Waiting for connection");
                TcpClient client = server.AcceptTcpClient();
                Console.Write("Connected new client, passing to new thread...");
                threadcount++;
                var newThread = new Thread(() => ClientManager.Manage(client, threadcount));
                networkWorkers.Add(newThread);
                newThread.Start();
                Console.WriteLine("done");
                Console.WriteLine("New Threadcount: " + threadcount);

            }

        }
    }
}
