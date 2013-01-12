using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Review_Site.Core
{
    public static class PasswordHashing
    {
        private static String salt = "HackTh1sN0obz";

        public static byte[] GetHash(String password)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] stringToHash = Encoding.UTF8.GetBytes(password);
            stringToHash.Concat(Encoding.UTF8.GetBytes(salt));
            return algorithm.ComputeHash(stringToHash);
        }

        public static bool HashesMatch(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length) return false;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}