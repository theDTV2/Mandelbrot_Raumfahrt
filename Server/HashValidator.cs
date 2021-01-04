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
        private static ConcurrentBag<string> hashList = new ConcurrentBag<string>();
        
        //Check, if given hash is in list
        public static bool ValidateHash(string hashtoValidate)
        {
            return hashList.Contains(hashtoValidate);
        }

        //Add Hash To List
        public static void ValidateAndAdd(string hashtoValidate)
        {
            if (!ValidateHash(hashtoValidate))
                AddHash(hashtoValidate);
        }

        private static void AddHash(string hashToAdd)
        {
            hashList.Add(hashToAdd);
        }

    }
}
