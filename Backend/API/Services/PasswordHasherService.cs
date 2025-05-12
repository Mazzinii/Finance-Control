using System.Security.Cryptography;

namespace Person.Services 
{
    public sealed class PasswordHasherService : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int Hashsize = 32;
        private const int Iterations = 100000;

        private readonly HashAlgorithmName Algoritm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt,Iterations, Algoritm,Hashsize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password,string passwordHash)
        {
            string[] parts = passwordHash.Split("-");
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algoritm, Hashsize);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
