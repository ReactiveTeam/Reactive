using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Reactive.scripts.Framework.Security
{
    class SimpleAES
    {
        public SimpleAES()
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            this.encryptor = rijndaelManaged.CreateEncryptor(SimpleAES.key, SimpleAES.vector);
            this.decryptor = rijndaelManaged.CreateDecryptor(SimpleAES.key, SimpleAES.vector);
            this.encoder = new UTF8Encoding();
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(this.Encrypt(this.encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return this.encoder.GetString(this.Decrypt(Convert.FromBase64String(encrypted)));
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.encryptor);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(buffer, 0, buffer.Length);
            }
            return memoryStream.ToArray();
        }

        private static byte[] key = new byte[]
        {
            13,
            27,
            119,
            151,
            224,
            216,
            8,
            95,
            14,
            184,
            217,
            168,
            137,
            34,
            32,
            122,
            21,
            241,
            158,
            194,
            137,
            153,
            96,
            9,
            24,
            26,
            47,
            118,
            231,
            236,
            59,
            209
        };

        private static byte[] vector = new byte[]
        {
            81,
            101,
            222,
            3,
            25,
            8,
            213,
            21,
            34,
            55,
            89,
            144,
            233,
            32,
            114,
            56
        };

        private ICryptoTransform encryptor;

        private ICryptoTransform decryptor;

        private UTF8Encoding encoder;
    }
}
