using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Controller
{
    class Program
    {
        static List<Process> processList = new List<Process>();
            
        static void Main(string[] args)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            System.Console.WriteLine("Controller, Version " + version);

            int amountOfClients;

            
            if (args.Length == 0)    
            {
                System.Console.Write("No Amount of Clients specified, using default value (3)");
                amountOfClients = 3;
            }
            else
            {
                System.Console.Write("Creating specified amount of clients: " + args[0]);
                amountOfClients = Convert.ToInt32(args[0]);
            }


            while (true)
            {
                System.Console.Write("Starting Server...");
                processList.Add(Process.Start(ProcessVariables.GetServerVariable()));
                System.Console.WriteLine("done");


                //Wait for the server to start up (shouldnt take more than a couple of ms, but we are not in a hurry
                System.Threading.Thread.Sleep(2500);

                for (int i = 0; i < amountOfClients; i++)
                {
                    System.Console.Write("Starting Client " + i +"...") ;
                    processList.Add(Process.Start(ProcessVariables.GetClientVariable()));
                    System.Console.WriteLine("done");

                }


                bool killEverything = false;
                while(true)
                {
                    foreach (var item in processList)
                    {
                        if (item.HasExited)
                        {
                            System.Console.WriteLine("One of the Applications crashed! Resetting and restarting everything");
                            killEverything = true;
                            break;
                        }
                    }

                    if (killEverything)
                    {
                        foreach (var item in processList)
                        {
                            item.Kill();
                        }
                        processList.Clear();
                        break;
                    }
                    System.Threading.Thread.Sleep(10000);
                }



            }



        }
    }
}
