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
        
        private static ConcurrentBag<string> toWrite = new ConcurrentBag<string>();

        private static StreamWriter dataFileStream;

        private static DateTime LastZipTime;
        //Add Data to be written in a threadsafe way
        public static void WriteData(int WorkerID, string hash, double duration)
        {
            var Message = "ID: " + WorkerID + " " + hash.Substring(0, 12) + " " + duration;
            toWrite.Add(hash.Substring(5, 6) +" " + Math.Round(duration,1));
        }

        //write all data, that is to be written
        public static void FileWriteWorker()
        {
            while (true)
            {
                while (toWrite.TryTake(out string lineToWrite))
                {
                    dataFileStream.WriteLine(lineToWrite);
                    
                }
                dataFileStream.Flush();
             
                


#if DEBUG
                //In debug we do it once per minute
                if ((DateTime.Now - LastZipTime).Minutes > 1)
                    ZipOutputFile();
#else

                //Once per hour we zip our file for the satellite 
                //TODO: Change this to .Minutes before release
                if ((DateTime.Now - LastZipTime).Seconds > 60)
                    ZipOutputFile();
#endif
                //No need to check for new messages all the time
                System.Threading.Thread.Sleep(2000);
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
            System.Console.WriteLine("Zipping Data...");

            var pathSource = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "outputfile";
            var pathSourceToZip = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "outputfiletozip";
            var pathTarget = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "outputfilezipped.zip";

           

            if (File.Exists(pathSourceToZip))
                File.Delete(pathSourceToZip);

            if (File.Exists(pathTarget))
                File.Delete(pathTarget);


            File.Copy(pathSource, pathSourceToZip);
            using (ZipArchive archive = ZipFile.Open(pathTarget, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(pathSourceToZip, pathTarget,CompressionLevel.Optimal);
            }
        

        LastZipTime = DateTime.Now;
            System.Console.WriteLine("Data zipped successfully");


            //This file acts as an Semaphore, the satellite has to wait until it disappears before it can copy the zip file
            if (File.Exists(pathSourceToZip))
                File.Delete(pathSourceToZip);

        }


    }
}
