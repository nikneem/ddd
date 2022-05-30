using HexMaster.DomainDrivenDesign.Helpers;

namespace HexMaster.DomainDrivenDesign.ValueObjects
{
    public sealed class Password
    {

        public string EncryptedPassword { get; }
        public string Secret { get; }

        public bool Verify(string plainTextPassword)
        {
            return PasswordHasherHelper.Verify(plainTextPassword, EncryptedPassword, Secret);
        }

        public Password(string plainTextPassword)
        {
            Secret = PasswordHasherHelper.GenerateSalt();
            EncryptedPassword = plainTextPassword.Hash(Secret);
        }

        public Password(string encryptedPassword, string secret)
        {
            EncryptedPassword = encryptedPassword;
            Secret = secret;
        }
    }
}