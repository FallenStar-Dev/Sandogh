using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sandogh.Utility.Cryptography
{
    public static class Aes
    {
        private static byte[] Encrypt(byte[] clearData, byte[] key, byte[] ivBytes)
        {
            using var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = ivBytes;
            var cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            var encryptedData = ms.ToArray();
            alg.Dispose();
            cs.Dispose();
            ms.Dispose();
            return encryptedData;
        }


        public static string Encrypt(string data, string password, int bits)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(data);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,

                new byte[] { 0x00, 0x01, 0x02, 0x1C, 0x1D, 0x1E, 0x03, 0x04, 0x05, 0x0F, 0x20, 0x21, 0xAD, 0xAF, 0xA4 });
            byte[] encryptedData;
            switch (bits)
            {
                case (128):
                    encryptedData = Encrypt(clearBytes, pdb.GetBytes(16), pdb.GetBytes(16));
                    break;
                case (192):
                    encryptedData = Encrypt(clearBytes, pdb.GetBytes(24), pdb.GetBytes(16));
                    break;
                case (256):
                    encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                    break;
                default:
                    return string.Concat(bits);
            }
            return Convert.ToBase64String(encryptedData);
        }


        private static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            using var ms = new MemoryStream();
            using var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            using var cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            cs.Dispose();
            var decryptedData = ms.ToArray();
            ms.Dispose();
            alg.Dispose();
            return decryptedData;
        }


        public static string Decrypt(string data, string password, int bits)
        {
            byte[] cipherBytes = Convert.FromBase64String(data);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,
            new byte[] { 0x00, 0x01, 0x02, 0x1C, 0x1D, 0x1E, 0x03, 0x04, 0x05, 0x0F, 0x20, 0x21, 0xAD, 0xAF, 0xA4 });

            byte[] decryptedData;
            switch (bits)
            {
                case (128):
                    decryptedData = Decrypt(cipherBytes, pdb.GetBytes(16), pdb.GetBytes(16));
                    break;
                case (192):
                    decryptedData = Decrypt(cipherBytes, pdb.GetBytes(24), pdb.GetBytes(16));
                    break;
                case (256):
                    decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                    break;
                default:
                    return string.Concat(bits);
            }

            return Encoding.Unicode.GetString(decryptedData);
        }
    }
}
