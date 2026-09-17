using System.Security.Cryptography;
using System.Text;

namespace SistemaMercadoMVC
{
    public static class HashService
    {
        public static byte[] Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }
    }
}