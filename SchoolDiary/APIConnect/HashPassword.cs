using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.APIConnect
{
    class HashPassword
    {
        public static string GetHash(string password)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(Properties.Resources.salt), 1000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
