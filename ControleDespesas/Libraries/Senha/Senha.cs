using ControleDespesas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries
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

        public static string Criptografar(string senha)
        {
            byte[] senhaCripto = ASCIIEncoding.ASCII.GetBytes(senha);
            return Convert.ToBase64String(senhaCripto);
        }

        public static string Descriptografar(string senha)
        {
            byte[] senhaDescripto = Convert.FromBase64String(senha);
            return ASCIIEncoding.ASCII.GetString(senhaDescripto);
        }
    }
}
