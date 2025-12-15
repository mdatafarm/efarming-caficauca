using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Common.Encription
{
    /// <summary>
    /// Encriptor classes
    /// </summary>
    public class SHA256Encriptor : IEncriptor
    {
        /// <summary>
        /// The _cryto service provider
        /// </summary>
        private RNGCryptoServiceProvider _crytoServiceProvider;
        /// <summary>
        /// The sal t_ size
        /// </summary>
        private const int SALT_SIZE = 32;

        /// <summary>
        /// Initializes a new instance of the <see cref="SHA256Encriptor"/> class.
        /// </summary>
        public SHA256Encriptor()
        {
            _crytoServiceProvider = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <returns></returns>
        public string GenerateSalt()
        {
            var saltBytes = new byte[SALT_SIZE];
            _crytoServiceProvider.GetNonZeroBytes(saltBytes);
            var salt = Utility.GetString(saltBytes);
            return salt;
        }

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public string HashPassword(string password, string salt)
        {
            var computedPassword = password + salt;
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            var dataBytes = Utility.GetBytes(computedPassword);
            var resultBytes = sha256.ComputeHash(dataBytes);
            return Utility.GetString(resultBytes);
        }
    }
}
