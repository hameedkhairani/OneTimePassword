namespace OneTimePassword.App.Contracts
{
    public interface IHashGenerator
    {
        string Generate(string key, string token);
    }
}