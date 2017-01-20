using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Reactive.Framework.Security
{
    internal static class Encryption
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        public static string GenerateKey()
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = (DESCryptoServiceProvider)DES.Create();
            return Convert.ToBase64String(dESCryptoServiceProvider.Key);
        }

        public static void EncryptFile(string inputFilename, string outputFilename, string key)
        {
            using (FileStream fileStream = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileStream2 = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                {
                    ICryptoTransform transform = new DESCryptoServiceProvider
                    {
                        Key = Convert.FromBase64String(key),
                        IV = Convert.FromBase64String(key)
                    }.CreateEncryptor();
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream2, transform, CryptoStreamMode.Write))
                    {
                        byte[] array = new byte[fileStream.Length];
                        fileStream.Read(array, 0, array.Length);
                        cryptoStream.Write(array, 0, array.Length);
                    }
                }
            }
        }

        public static void DecryptFile(string inputFilename, string outputFilename, string key)
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            dESCryptoServiceProvider.Key = Convert.FromBase64String(key);
            dESCryptoServiceProvider.IV = Convert.FromBase64String(key);
            using (FileStream fileStream = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                ICryptoTransform transform = dESCryptoServiceProvider.CreateDecryptor();
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, transform, CryptoStreamMode.Read))
                {
                    using (StreamWriter streamWriter = new StreamWriter(outputFilename))
                    {
                        streamWriter.Write(new StreamReader(cryptoStream).ReadToEnd());
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] array = new byte[16384];
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int count;
                while ((count = input.Read(array, 0, array.Length)) > 0)
                {
                    memoryStream.Write(array, 0, count);
                }
                result = memoryStream.ToArray();
            }
            return result;
        }

        public static byte[] DecryptFile(string inputFilename, string key)
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            dESCryptoServiceProvider.Key = Convert.FromBase64String(key);
            dESCryptoServiceProvider.IV = Convert.FromBase64String(key);
            byte[] result;
            using (FileStream fileStream = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                ICryptoTransform transform = dESCryptoServiceProvider.CreateDecryptor();
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, transform, CryptoStreamMode.Read))
                {
                    result = Encryption.ReadFully(cryptoStream);
                }
            }
            return result;
        }

        public static byte[] DecryptFile(byte[] file, string key)
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            dESCryptoServiceProvider.Key = Convert.FromBase64String(key);
            dESCryptoServiceProvider.IV = Convert.FromBase64String(key);
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream(file))
            {
                ICryptoTransform transform = dESCryptoServiceProvider.CreateDecryptor();
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
                {
                    result = Encryption.ReadFully(cryptoStream);
                }
            }
            return result;
        }
    }
}
