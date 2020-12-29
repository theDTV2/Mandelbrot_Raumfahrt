using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Client
{
    public static class HashCalculator
    {

        public static byte[] GetSHA384Hash(this string ToHash)
        {
            SHA384 sha384Hash = SHA384.Create();
            return sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(ToHash));
        }

        public static string GetSHA384HashAsString(this string toHash)
        {
            return Encoding.UTF8.GetString(toHash.GetSHA384Hash());

        }


    }
}
