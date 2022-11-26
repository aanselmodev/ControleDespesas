using AccessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Libraries
{
    public class PasswordManagement
    {
        public static string GenerateCodePasswordReset()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[8];
            int size = 8;

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                crypto.GetBytes(data);
            
            StringBuilder result = new StringBuilder(size);
            
            foreach (byte b in data)
                result.Append(chars[b % (chars.Length)]);
    
            return result.ToString();
        }

        public static string Encrypt(string password)
        {
            byte[] encryptedPassword = ASCIIEncoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(encryptedPassword);
        }

        public static string Decrypt(string password)
        {
            byte[] decryptedPassword = Convert.FromBase64String(password);
            return ASCIIEncoding.ASCII.GetString(decryptedPassword);
        }
    }
}
