using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.IO.Compression;

namespace Server
{
    class DataWriter
    {
        
        private static readonly ConcurrentBag<string> toWrite = new ConcurrentBag<string>();

        private static StreamWriter dataFileStream;

        private static DateTime LastZipTime;
        //Add Data to be written in a threadsafe way
        public static void WriteData(int WorkerID, string hash, double duration)
        {
            int complexity = Convert.ToInt32(hash.Substring(5, 3));
            int iterations = Convert.ToInt32(hash.Substring(9, 3));


            Console.WriteLine("Worker " + WorkerID + ": finished calculation, with parameters:" + complexity + " " + iterations + " in " + duration + " ms");

            toWrite.Add(hash.Substring(5, 6) +" " + Math.Round(duration,1));
        }

        //write all data, that is to be written
        public static void FileWriteWorker()
        {
            while (true)
            {
                if (!toWrite.IsEmpty)
                {
                    Console.Write("Writing " + toWrite.Count + " entries to file...");
                    while (toWrite.TryTake(out string lineToWrite))
                    {
                        dataFileStream.WriteLine(lineToWrite);

                    }
                    dataFileStream.Flush();
                    Console.WriteLine("done");
                }


#if DEBUG
                //In debug we do it once per minute
                if ((DateTime.Now - LastZipTime).TotalSeconds > 60)
                    ZipOutputFile();
#else

                //Once per hour we zip our file for the satellite 
                //TODO: Change this to .Minutes before release
                if ((DateTime.Now - LastZipTime).TotalSeconds > 60)
                    ZipOutputFile();
#endif
                //No need to check for new messages all the time
                System.Threading.Thread.Sleep(4000);
            }
        }

        public static void SetUpFile()
        {
            var path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "outputfile";

            if (File.Exists(path))
                File.Delete(path);

            dataFileStream = new StreamWriter(path);

            LastZipTime = DateTime.Now;
        }



        private static void ZipOutputFile()
        {
            System.Console.Write("Zipping Data...");

            var pathSource = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "outputfile";
            var pathSourceToZip = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "outputfiletozip";
            var pathTarget = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "output.zip";

            if (File.Exists(pathSourceToZip))
                File.Delete(pathSourceToZip);

            if (File.Exists(pathTarget))
                File.Delete(pathTarget);


            File.Copy(pathSource, pathSourceToZip);
            using (ZipArchive archive = ZipFile.Open(pathTarget, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(pathSourceToZip,"output.txt",CompressionLevel.Optimal);
            }
        

        LastZipTime = DateTime.Now;
            System.Console.WriteLine("done");


            //This file acts as an Semaphore, the satellite has to wait until it disappears before it can copy the zip file
            if (File.Exists(pathSourceToZip))
                File.Delete(pathSourceToZip);

        }


    }
}
