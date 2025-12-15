namespace EFarming.Common.Encription
{
    /// <summary>
    /// Encriptor Interface
    /// </summary>
    public interface IEncriptor
    {
        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <returns></returns>
        string GenerateSalt();

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        string HashPassword(string password, string salt);
    }
}
