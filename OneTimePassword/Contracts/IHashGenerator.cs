namespace OneTimePassword.Contracts
{
    public interface IHashGenerator
    {
        string Generate(string key, string token);
    }
}