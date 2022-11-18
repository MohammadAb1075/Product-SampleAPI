using System;
using System.Linq;
using System.Text;

namespace SampleAPI.Utils
{
    public class Crypto
    {
        public static string ToSHA512(string plain)
        {
            var SHA512 = System.Security.Cryptography.SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(plain);
            var hash = SHA512.ComputeHash(bytes);
            //byte to hex string
            return String.Concat(hash.Select(b => b.ToString("X2")).ToArray());
        }

    }
}
