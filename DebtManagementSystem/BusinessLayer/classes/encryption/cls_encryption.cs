using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BusinessLayer.classes.encryption
{
    public class cls_encryption
    {

        private static readonly string _salt = "salt";

        // Completed Testing.
        public static string HashEncryption(string plainText)
        {

            plainText += _salt;

            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
            catch
            {
                return "";
            }

        }

    }
}
