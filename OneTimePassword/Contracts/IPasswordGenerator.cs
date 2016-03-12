namespace OneTimePassword.App.Contracts
{
    public interface IPasswordGenerator
    {
        string Generate(string userName);
    }
}