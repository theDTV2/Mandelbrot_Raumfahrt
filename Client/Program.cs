using System;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            System.Console.WriteLine("Client, Version " + version);
            try
            {
                if (args.Length == 0)
                {
                    System.Console.WriteLine("No Port provided, using default value");
                    NetworkConnector.SetUpConnection(442);
                }
                else
                {
                    System.Console.WriteLine("Using provided port: " + args[0]);
                    NetworkConnector.SetUpConnection(Convert.ToInt32(args[0]));
                }

                System.Console.WriteLine("Connected.");


                Mandelbrot.GenerateMandelBrotParams();
                while (true)
                {
                    Mandelbrot.DoMandelBrot();
                }
            }

            catch (Exception e)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine(e.Message);
                return;
            }

        }
    }
}
