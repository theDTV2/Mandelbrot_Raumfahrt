using System;
using System.Numerics;
using System.Text;
using System.Threading;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkConnector.SetUpConnection(442);

            while (true)
            {
               var toSend =  Encoding.UTF8.GetString(Mandelbrot.GenerateMandelbrotSet(400, 500));

                NetworkConnector.SendData(toSend);
                                
            }


        }
    }
}
