using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class HashValidator
    {

        //Threadsafe List
        private static readonly ConcurrentBag<string> hashList = new ConcurrentBag<string>();
        
        //Check, if given hash is in list
        public static bool ValidateHash(string hashtoValidate)
        {
            return hashList.Contains(hashtoValidate);
        }

        //Add Hash To List
        public static void ValidateAndAdd(int WorkerID, string hashtoValidate)
        {
            if (!ValidateHash(hashtoValidate))
                AddHash(hashtoValidate);


            int complexity = Convert.ToInt32(hashtoValidate.Substring(5, 3));
            int iterations = Convert.ToInt32(hashtoValidate.Substring(9, 3));

            Console.WriteLine("Worker " + WorkerID + ": added Hash for:" + complexity + " " + iterations);




        }

        private static void AddHash(string hashToAdd)
        {
            hashList.Add(hashToAdd);
        }

    }
}
