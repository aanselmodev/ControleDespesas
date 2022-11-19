using ControleDespesas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries.Senha
{
    public class Senha
    {
        public static string GerarCodigoRedefinicaoSenha()
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
    }
}
