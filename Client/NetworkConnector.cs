using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public static class NetworkConnector
    {

        private static TcpClient client;

        public static void SetUpConnection(int Port)
        {
            client = new TcpClient("127.0.0.1", Port);
        }

        public static void SendData (string ToSend)
        {
            var stream = client.GetStream();

            var ToSendBytes = Encoding.UTF8.GetBytes(ToSend);
            stream.Write(ToSendBytes);

        }


    }
}
