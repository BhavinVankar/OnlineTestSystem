using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models.Common
{
    public static class AppEncrypt
    {
        public static string CreateHash(string password)
        {
            var provider = MD5.Create();
            string salt = "R3a$lyS@lt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
