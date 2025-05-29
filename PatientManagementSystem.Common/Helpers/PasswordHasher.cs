namespace PatientManagementSystem.Common.Helpers
{   /// <summary>
/// Utility class for hashing and verifying passwords using BCrypt.
/// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Hashes a password using BCrypt.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        /// <summary>
        /// Verifies a hashed password against a plain text password,Verify specifically checks the hash code salt version and encrypts the password and then checks if they both are equal.
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
