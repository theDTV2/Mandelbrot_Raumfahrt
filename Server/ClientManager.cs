using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class ClientManager
    {
        public static void Manage(TcpClient client, int WorkerID)
        {
            try { 
            NetworkStream stream = client.GetStream();
            DateTime startTime = new DateTime();
            double elapsedTime = 0;
            int addedHashes = 0;

                while (true)
                {
                    byte[] read = new byte[384];
                    stream.Read(read, 0, 384);
                    string readstring = Encoding.UTF8.GetString(read);
                    readstring = readstring.Replace("\0", String.Empty);
                    
                    switch (readstring)
                    {
                        case "START":
                            startTime = DateTime.Now;
                            break;
                        case "STOP":
                            DateTime endTime = DateTime.Now;
                            elapsedTime = (endTime - startTime).TotalMilliseconds;

                            break;
                        default:
                            if (readstring.StartsWith("HASH"))
                            {
                                //Each client can add 4 hashes to the server internal list,
                                //after that each new hash is considered faulty
                                if (addedHashes < 4)
                                {
                                    HashValidator.ValidateAndAdd(WorkerID, readstring);
                                    addedHashes++;
                                    break;
                                }
                                if (HashValidator.ValidateHash(readstring))
                                    DataWriter.WriteData(WorkerID, readstring, elapsedTime);
                                else
                                    System.Console.WriteLine("Worker " + WorkerID + ": Invalid Data read");

                            }

                            break;
                    }
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
