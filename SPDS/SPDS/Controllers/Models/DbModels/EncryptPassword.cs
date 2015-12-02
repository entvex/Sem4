using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLModel
{
    class Encryption
    {
        public static string EncryptPassword(string password)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                if (password != null)
                {
                    var result = hash.ComputeHash(enc.GetBytes(password));
          
                    foreach (var b in result)
                        Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
    }
}
