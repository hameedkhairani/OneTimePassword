namespace OneTimePassword.Contracts
{
    public interface IPasswordGenerator
    {
        string Generate(string userName);
    }
}