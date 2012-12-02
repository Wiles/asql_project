///
///
///

namespace Prestige.Services
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Class for encryting passwords.
    /// </summary>
    public class Encryptor : IEncryptor
    {
        /// <summary>
        /// Gets the hash of a string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The hash.</returns>
        public string GetHash(string str)
        {
            var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(str);
            var hash = md5.ComputeHash(bytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
