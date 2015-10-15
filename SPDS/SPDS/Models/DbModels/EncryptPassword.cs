using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace encryptPassword
{
    class Program
    {

        public static string EncryptPassword(string pass)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            var encrypter = new SHA256Managed();
            string hashedString = String.Empty;
            byte[] hash = encrypter.ComputeHash(bytes);
            foreach (var x in hash)
            {
                hashedString += $"{x:x2}";
            }
            return hashedString;
        }
    }
}
