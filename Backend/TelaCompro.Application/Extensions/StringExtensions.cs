using System.Security.Cryptography;
using System.Text;

namespace TelaCompro.Application.Extensions
{
    public static class StringExtensions
    {
        public static string Hash(this string text)
        {
            using var sha256Hash = SHA256.Create();

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")).ToString();
            }

            return builder.ToString();
        }
    }
}
