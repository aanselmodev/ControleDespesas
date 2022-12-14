using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesControl.Libraries
{
    public class Cryptography
    {
        public static string Encrypt(string password, string salt)
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes($"{password}{salt}");
                byte[] hash = HashAlgorithm.Create("SHA1").ComputeHash(bytes);

                return Convert.ToBase64String(hash).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(Messages.MSG_EX1, ex.Message));
            }
        }
    }
}
