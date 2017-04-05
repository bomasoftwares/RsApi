using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.Crosscut.Services
{
    public static class SecurityService
    {
        public static string Encrypt(string password)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] hash = md5.Hash; 
                StringBuilder resultado = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    resultado.Append(hash[i].ToString("x"));
                }

                return resultado.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}
