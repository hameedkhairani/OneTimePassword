namespace OneTimePassword.App.Contracts
{
    public interface IPasswordVerifier
    {
        bool Verify(string userName, string password);
    }
}