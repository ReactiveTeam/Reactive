using System;
using System.IO;
using System.Security.Cryptography;

namespace Reactive.Framework.Security
{
    public static class SecurityUtils
    {
        public static string CalculateChecksum(string fileName)
        {
            string result;
            using (MD5 mD = MD5.Create())
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    result = BitConverter.ToString(mD.ComputeHash(fileStream)).Replace("-", "").ToLower();
                }
            }
            return result;
        }

        public static string CalculateChecksum(byte[] data)
        {
            string result;
            using (MD5 mD = MD5.Create())
            {
                result = BitConverter.ToString(mD.ComputeHash(data)).Replace("-", "").ToLower();
            }
            return result;
        }
    }
}
