using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public static class NetworkConnector
    {

        private static TcpClient client;
        private static NetworkStream stream;

        public static void SetUpConnection(int Port)
        {
            client = new TcpClient("127.0.0.1", Port);
            stream = client.GetStream();
        }

        public static void SendData(string ToSend)
        {

            byte[] ToSendBytes = new byte[384];
            ToSendBytes = Encoding.UTF8.GetBytes(ToSend);

            //Server/Client agree to always use 384 byte size messages, so we fill up the missing characters with 0
            ToSendBytes = ToSendBytes.Concat(Enumerable.Repeat((byte)0x0, 384 - ToSendBytes.Length).ToArray()).ToArray();
            stream.Write(ToSendBytes,0, 384);

        }

        public static void SendData(int complexity, int hash, string toSend)
        {
            string data = "HASH " + complexity + " " + hash + " " + toSend;

            SendData(data);
        }


    }
}
