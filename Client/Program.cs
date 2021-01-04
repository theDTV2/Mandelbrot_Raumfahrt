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
            if (args.Length == 0)
                NetworkConnector.SetUpConnection(442);
            else
                NetworkConnector.SetUpConnection(Convert.ToInt32(args[0]));
            //Register with Server

            /* //Start Calculation
             while (true)
             {
                 //Send what we will calculate now
                 //Calculate
                 //Send, that we finished, and send result
             } */

            Mandelbrot.GenerateMandelBrotParams();
            while (true)
            {
                Mandelbrot.DoMandelBrot();


                Console.WriteLine("Loop done");
            }

        }
    }
}
