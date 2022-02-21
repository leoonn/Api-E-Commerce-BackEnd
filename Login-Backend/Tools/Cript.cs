using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Login_Backend.Tools
{
    public class Cript
    {
        public string encript(string text)
        {
            MD5 md5Hash = MD5.Create();
            byte[] Criptography = md5Hash.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder ecriptText = new StringBuilder();
            for (int i = 0; i < Criptography.Length; i++)
            {
                ecriptText.Append(Criptography[i].ToString("x2"));
            }
            return ecriptText.ToString();
        }
        public string HashSha1(string text)
        {
            
            var sha = new SHA1CryptoServiceProvider();
            byte[] Criptography = sha.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder ecriptText = new StringBuilder();
            for (int i = 0; i < Criptography.Length; i++)
            {
                ecriptText.Append(Criptography[i].ToString("x2"));
            }
            return ecriptText.ToString();
            
        }
    }
}
