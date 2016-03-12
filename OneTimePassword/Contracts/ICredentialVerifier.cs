namespace OneTimePassword.Contracts
{
    public interface ICredentialVerifier
    {
        bool Verify(string userName, string password);
    }
}