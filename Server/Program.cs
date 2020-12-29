using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 442);
            server.Start();

            while(true)
            {
                Console.WriteLine("waiting...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("conntected");
                NetworkStream stream = client.GetStream();

                while(true)
                {
                    byte[] read = new byte[512];
                    stream.Read(read);

                    System.Console.WriteLine(Encoding.UTF8.GetString(read));
                }



            }

        }
    }
}
