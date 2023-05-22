using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

namespace CuratorsHelper
{
    static class ArgonClass
    {
        public static byte[] CreateSalt()
        {
            var salt = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);


            return salt;
        }
        public static string HashPassword(string password, byte[] salt)
        {

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 8;
            argon2.MemorySize = 120*120; // 1 GB

            StringBuilder sb = new StringBuilder();
            foreach (var item in argon2.GetBytes(16))
                sb.Append(item.ToString("X2"));

            return sb.ToString();
        }
    }
}
