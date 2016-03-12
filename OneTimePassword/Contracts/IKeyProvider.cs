namespace OneTimePassword.Contracts
{
    public interface IKeyProvider
    {
        string GetHashKey();
    }
}