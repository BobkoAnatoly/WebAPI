using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers.Criptography
{
    public static class PasswordHasher
    {
        public static void HashPassword(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            var hmac = new HMACSHA1();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public static bool VerifyPassword(byte[] passwordSalt, byte[] passwordHash,string password)
        {
            var hmac = new HMACSHA512(passwordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return hash.SequenceEqual(passwordHash);
        }
    }
}
