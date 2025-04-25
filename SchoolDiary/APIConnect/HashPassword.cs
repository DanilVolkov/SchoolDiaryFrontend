using System;
using System.Security.Cryptography;
using System.Text;

namespace SchoolDiary.APIConnect
{
    class HashPassword
    {
        public static string GetHash(string password)
        {
            var v = Environment.GetEnvironmentVariable("salt", EnvironmentVariableTarget.User);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(v), 1000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
